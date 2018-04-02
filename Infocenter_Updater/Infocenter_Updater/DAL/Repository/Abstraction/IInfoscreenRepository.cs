using DTOModel.DomainModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infocenter_Updater.DAL.Repository.Abstraction
{
    public interface IInfoscreenRepository
    {
        IEnumerable<Infoscreen> ReadAll();
        Infoscreen Read(int id);
        Infoscreen Read(string name);
    }
}
