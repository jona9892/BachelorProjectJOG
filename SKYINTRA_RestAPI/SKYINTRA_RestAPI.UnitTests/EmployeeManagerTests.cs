using DomainModel;
using NSubstitute;
using NUnit.Framework;
using SKYINTRA_RestAPI.BLL.Implementation;
using SKYINTRA_RestAPI.DAL.Repository.Abstraction;
using SKYINTRA_RestAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SKYINTRA_RestAPI.UnitTests
{
    public class EmployeeManagerTests
    {
        [Test]
        public void GetBirthdaysWeek_3EmployeesUpcomingBirtdays_Return3Aniversarries()
        {
            EmployeeManager manager = CreateEmployeeManager();

            IEnumerable<Anniversary> aniversarries = manager.GetBirthdaysWeek();
            Assert.AreEqual(3, aniversarries.Count());
        }

        [Test]
        public void GetEmploymentsWeek_3EmployeesUpcomingAniversarries_Return3Aniversarries()
        {
            EmployeeManager manager = CreateEmployeeManager();

            IEnumerable<Anniversary> aniversarries = manager.GetEmploymentsWeek();
            Assert.AreEqual(3, aniversarries.Count());
        }

        private EmployeeManager CreateEmployeeManager()
        {
            var employees = new Employee[]
            {                
                new Employee { objectid = 2, name = "Jette Hansen", birthday = DateTime.Today.AddYears(-50), employment = DateTime.Today },
                new Employee { objectid = 3, name = "Jan Jørgensen", birthday = DateTime.Today.AddYears(-50).AddDays(1), employment = DateTime.Today.AddDays(1) },
                new Employee { objectid = 4, name = "Jørgen Bjerre", birthday = DateTime.Today.AddYears(-33).AddDays(7), employment = DateTime.Today.AddDays(7) },                
            };

            IEmployeeRepository employeeRepository = Substitute.For<IEmployeeRepository>();
            employeeRepository.ReadBirthdayWeek().Returns(employees);
            employeeRepository.ReadEmployementWeek().Returns(employees);

            return new EmployeeManager(employeeRepository);
        }
    }
}
