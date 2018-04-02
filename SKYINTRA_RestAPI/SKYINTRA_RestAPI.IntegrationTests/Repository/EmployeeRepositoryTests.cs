using DomainModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MySql.Data.MySqlClient;
using NUnit.Framework;
using SKYINTRA_RestAPI.DAL.Context;
using SKYINTRA_RestAPI.DAL.Repository.Abstraction;
using SKYINTRA_RestAPI.DAL.Repository.Implementation;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace SKYINTRA_RestAPI.IntegrationTests.Repository
{
    public class EmployeeRepositoryTests
    {
        [SetUp]
        public void Setup()
        {
            InitializeDatabaseWithDataTest();
        }

        public void InitializeDatabaseWithDataTest()
        {
            var ctx =  new IPWMetazoContext("Server=localhost;Database=ipwtestdb;Uid=jonathan;Pwd=testdb1;");

            var employees = new Employee[]
            {
                new Employee { objectid = 1, name = "Mikael Rasmussen", birthday = DateTime.Today.AddYears(-22).AddDays(-1), employment = DateTime.Today.AddDays(-1) },
                new Employee { objectid = 2, name = "Jette Hansen", birthday = DateTime.Today.AddYears(-50), employment = DateTime.Today },
                new Employee { objectid = 3, name = "Jan Jørgensen", birthday = DateTime.Today.AddYears(-50).AddDays(1), employment = DateTime.Today.AddDays(1) },
                new Employee { objectid = 4, name = "Jørgen Bjerre", birthday = DateTime.Today.AddYears(-33).AddDays(7), employment = DateTime.Today.AddDays(7) },
                new Employee { objectid = 5, name = "Lars Calender", birthday = DateTime.Today.AddYears(-44).AddDays(8), employment = DateTime.Today.AddDays(8) },
            };

            MySqlConnection conn = ctx.GetConnection();           
            
            foreach (var emp in employees)
            {
                conn.Open();
                MySqlCommand cmd2 = new MySqlCommand("INSERT INTO ipwtestdb.employee(objectid,name,birthday,employment)" +
                                                    " VALUES(" + emp.objectid + ", '" + emp.name + "', STR_TO_DATE('" + emp.birthday.ToShortDateString() + "','%d-%m-%Y') " + 
                                                    ", STR_TO_DATE('" + emp.birthday.ToShortDateString() + "','%d-%m-%Y')" + ");", conn);
                cmd2.ExecuteReader();
                conn.Close();
            }         
        }

        [Test]
        public void ReadBirthdayWeek_3EmployeesUpcomingBirtdays_Return3Employees()
        {
            IEmployeeRepository repo = CreateRepository();

            IEnumerable<Employee> employees = repo.ReadBirthdayWeek();
            Assert.AreEqual(3, employees.Count());
        }

        [Test]
        public void ReadEmployementWeek_3EmployeesUpcomingAnniversaries_Return3Employees()
        {
            IEmployeeRepository repo = CreateRepository();

            IEnumerable<Employee> employees = repo.ReadEmployementWeek();
            Assert.AreEqual(3, employees.Count());
        }


        public IEmployeeRepository CreateRepository()
        {
            var ctx = new IPWMetazoContext("Server=localhost;Database=ipwtestdb;Uid=jonathan;Pwd=bek95gbr;");
            IEmployeeRepository employeeRep = new EmployeeRepository(ctx);
            return employeeRep;
        }
    }
}
