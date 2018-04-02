using DTOModel.DomainModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infocenter_Updater.BLL.Abstraction
{
    public interface IInfoscreenManager
    {
        string GetRssFeedFromUrl(string url);

        List<Information> GetInfoscreenInformations(string infoscreen);
        
    }
}
