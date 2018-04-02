
using DTOModel.DomainModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infocenter_Updater.DAL.Repository.Abstraction
{
    public interface IGuestRepository
    {
        IEnumerable<Guest> ReadAllTodaysGuests();
    }
}
