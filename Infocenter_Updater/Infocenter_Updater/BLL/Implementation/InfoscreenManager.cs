using Infocenter_Updater.BLL.Abstraction;
using Infocenter_Updater.DAL.Repository.Abstraction;
using DTOModel.DomainModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Net;
using System.Xml.Linq;

namespace Infocenter_Updater.BLL.Implementation
{
    public class InfoscreenManager : IInfoscreenManager
    {
        private readonly IInfoscreenRepository infoscreenRep;

        private List<Information> cachedKantineInformation;
        private List<Information> cachedEkstruderingInformation;
        private List<Information> cachedTermoformInformation;
        private Dictionary<Information, string> cachedInformations;

        public InfoscreenManager(IInfoscreenRepository isr)
        {
            infoscreenRep = isr;
            cachedKantineInformation = new List<Information>();
            cachedEkstruderingInformation = new List<Information>();
            cachedTermoformInformation = new List<Information>();
            cachedInformations = new Dictionary<Information, string>();
        }

        public List<Information> GetInfoscreenInformations(string infoscreen)
        {
            List<Information> informations = infoscreenRep.Read(infoscreen).InfoscreenInformations.Select(x => x.Information).ToList();

            if (!informations.OrderBy(x => x.Id).Select(x => x.Id).SequenceEqual(cachedInformations.Where(x => x.Value == infoscreen).OrderBy(x => x.Key.Id).Select(x => x.Key.Id)))
            {
                cachedInformations.Clear();

                foreach (var info in informations)
                {
                    cachedInformations.Add(info, infoscreen);
                }

                return cachedInformations.Where(x => x.Value == infoscreen).Select(y => y.Key).ToList();
            }
            return null;
        }

        public string GetRssFeedFromUrl(string url)
        {
            string rssString = "";
            if (url != null)
            {
                WebClient wclient = new WebClient();
                wclient.Encoding = Encoding.UTF8;
                string RSSData = wclient.DownloadString(url);

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
