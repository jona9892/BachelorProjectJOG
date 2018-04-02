using DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SKYINTRA_RestAPI.DAL.Repository.Abstraction
{
    public interface IEmployeeRepository
    {
        Employee GetEmployee(string name);

        IEnumerable<Employee> ReadBirthdayWeek();
        IEnumerable<Employee> ReadEmployementWeek();
    }
}
