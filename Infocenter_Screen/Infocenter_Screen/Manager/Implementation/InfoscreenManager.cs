using DTOModel;
using Infocenter_Screen.BLL.Abstraction;
using Infocenter_Screen.Models;
using ServiceGateway.APIGateway.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Xml.Linq;

namespace Infocenter_Screen.BLL.Implementation
{
    public class InfoscreenManager : IInfoscreenManager
    {
        private readonly IInfoscreenAPIGateway infoscreenGateway;

        public InfoscreenManager(IInfoscreenAPIGateway _infoscreenGateway)
        {
            infoscreenGateway = _infoscreenGateway;
        }

        public List<Information> GetInformationFromInfoscreen(int id)
        {
            List<InfoscreenInformation> infoscreenInformations = infoscreenGateway.Read(id).InfoscreenInformations.ToList();
            List<Information> informations = new List<Information>();
            if (infoscreenInformations.Count() != 0)
            {
                informations = infoscreenInformations.Select(x => x.Information).ToList();
                return informations;
            }

            return informations;
        }

        public string GetRSSFeedPath(int id)
        {
            string RssFeedPath = infoscreenGateway.Read(id).RSSFeed;

            return RssFeedPath;
        }

        public string GetRSSFeed(string RSSURL)
        {
            string rssString = "";
            if (RSSURL != null)
            {
                WebClient wclient = new WebClient();
                wclient.Encoding = Encoding.UTF8;
                string RSSData = wclient.DownloadString(RSSURL);

                XDocument xml = XDocument.Parse(RSSData);
                xml.Declaration = new XDeclaration("1.0", "utf-8", null);
                var RSSFeedData = (from x in xml.Descendants("item")
                                   select new RSSFeed
                                   {
                                       Title = ((string)x.Element("title"))
                                   }).ToList();
                rssString = string.Join(" | ", RSSFeedData.Select(x => x.Title).ToArray());
            }
            
            return rssString;
        }
    }
}