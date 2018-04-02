using DTOModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Infocenter_Screen.BLL.Abstraction
{
    public interface IInfoscreenManager
    {
        List<Information> GetInformationFromInfoscreen(int id);
        string GetRSSFeedPath(int id);
        string GetRSSFeed(string RSSURL);
    }
}