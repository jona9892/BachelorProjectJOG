using DomainModel;
using SKYINTRA_RestAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SKYINTRA_RestAPI.BLL.Abstraction
{
    public interface IEmployeeManager
    {
        IEnumerable<Anniversary> GetBirthdaysWeek();
        IEnumerable<Anniversary> GetEmploymentsWeek();
    }
}
