using DTOModel.DomainModel;
using Infocenter_Updater.BLL.Implementation;
using Infocenter_Updater.DAL.Repository.Abstraction;
using NSubstitute;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infocenter_Updater.UnitTests
{
    public class InfoscreenManagerTests
    {

        private string RSSFeedURL = "http://www.dr.dk/nyheder/service/feeds/allenyheder";

        [Test]
        public void GetRssFeedFromUrl_Getfeedfromurl_Returnstrue()
        {
            InfoscreenManager manager = CreateInfoscreenManager();
            string Rssfeed = manager.GetRssFeedFromUrl(RSSFeedURL);
            Assert.IsNotNull(Rssfeed);
        }

        [Test]
        public void GetInfoscreenInformations_InformationssEqualsCache_ReturnsNull()
        {
            InfoscreenManager manager = CreateInfoscreenManager();
            List<Information> informations1 = manager.GetInfoscreenInformations("Kundeservice");
            List<Information> informations2 = manager.GetInfoscreenInformations("Kundeservice");

            Assert.IsNull(informations2);
        }
        
        [Test]
        public void GetInfoscreenInformations_InformationsIsNotEqualsCache_Returns4InformationMessages()
        {
            InfoscreenManager manager = CreateInfoscreenManager();
            List<Information> informations1 = manager.GetInfoscreenInformations("Kundeservice");
            Assert.AreEqual(4, informations1.Count);
        }

        [Test]
        public void GetInfoscreenInformations_InformationsEqualsCache_ReturnsNull()
        {
            InfoscreenManager manager = CreateInfoscreenManager();
            List<Information> informations1 = manager.GetInfoscreenInformations("Kundeservice");
            List<Information> informations2 = manager.GetInfoscreenInformations("Kundeservice");

            Assert.IsNull(informations2);
        }

        private InfoscreenManager CreateInfoscreenManager()
        {
            var informations = new Information[]
                {
                    new Information {Id = 1001, Header = "Header1", Body = "Body1"},
                    new Information {Id = 1002, Header = "Header2", Body = "Body2"},
                    new Information {Id = 1003, Header = "Header3", Body = "Body3" },
                    new Information { Id = 1004,Header = "Header4", Body = "Body4"}
                };
            Infoscreen infoscreen = new Infoscreen { Id = 1001, Name = "Kundeservice" };
            var ii = new InfoscreenInformation[] 
            {
                new InfoscreenInformation { InformationId = 1001, InfoscreenId = 1001 },
                new InfoscreenInformation { InformationId = 1002, InfoscreenId = 1001 },
                new InfoscreenInformation { InformationId = 1003, InfoscreenId = 1001 },
                new InfoscreenInformation { InformationId = 1004, InfoscreenId = 1001 },
            };
            
            IInfoscreenRepository infoscreenRepository = Substitute.For<IInfoscreenRepository>();
            infoscreenRepository.Read("Kundeservice").Returns(infoscreen);

            return new InfoscreenManager(infoscreenRepository);
        }
    }
}
