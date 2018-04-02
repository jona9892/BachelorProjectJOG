using DomainModel;
using SKYINTRA_RestAPI.BLL.Abstraction;
using SKYINTRA_RestAPI.DAL.Repository.Abstraction;
using SKYINTRA_RestAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SKYINTRA_RestAPI.BLL.Implementation
{
    public class EmployeeManager : IEmployeeManager
    {
        private readonly IEmployeeRepository employeeRepository;

        public EmployeeManager(IEmployeeRepository _employeeRepository)
        {
            employeeRepository = _employeeRepository;
        }

        public IEnumerable<Anniversary> GetBirthdaysWeek()
        {
            List<Anniversary> list = new List<Anniversary>();

            List<Employee> employees = employeeRepository.ReadBirthdayWeek().ToList();

            foreach (Employee emp in employees)
            {
                Anniversary anniversary = new Anniversary();
                DateTime now = DateTime.Today;
                int age = now.Year - emp.birthday.Year;

                anniversary.date = emp.birthday;
                anniversary.employee = emp.name;
                anniversary.type = "fødselsdag";
                anniversary.years = age;
                list.Add(anniversary);
            }
            return list;

        }

        public IEnumerable<Anniversary> GetEmploymentsWeek()
        {
            List<Anniversary> list = new List<Anniversary>();

            List<Employee> employees = employeeRepository.ReadEmployementWeek().ToList();

            foreach (Employee emp in employees)
            {
                Anniversary anniversary = new Anniversary();
                DateTime now = DateTime.Today;
                int age = now.Year - emp.employment.Year;

                anniversary.date = emp.employment;
                anniversary.employee = emp.name;
                anniversary.type = "jubilæum";
                anniversary.years = age;
                list.Add(anniversary);
            }
            return list;
        }
    }



}

