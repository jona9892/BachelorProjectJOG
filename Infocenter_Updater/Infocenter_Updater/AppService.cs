using DTOModel.DomainModel;
using Infocenter_Updater.BLL.Abstraction;
using Infocenter_Updater.DAL.Repository.Abstraction;
using Infocenter_Updater.MessagingGateway.Abstraction;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Infocenter_Updater
{
    public interface IAppService
    {
        void Run();
    }

    public class AppService : IAppService
    {
        private readonly IGuestRepository _guestRepository;
        private readonly IInfoscreenRepository _infoscreenRepository;
        private readonly IMessageGateway<Infoscreen> _messageGateway;
        private readonly IGuestMessageGateway _GuestMessageGateway;

        private string RSSFeedURL = "http://www.dr.dk/nyheder/service/feeds/allenyheder";

        //BLL
        private readonly IInfoscreenManager _infoscreenManager;

        public AppService(IGuestRepository guestRepository, IInfoscreenRepository infoscreenRepository,
            IInfoscreenManager infoscreenManager, IMessageGateway<Infoscreen> messageGateway, IGuestMessageGateway GuestMessageGateway)
        {
            _guestRepository = guestRepository;
            _infoscreenRepository = infoscreenRepository;


            _infoscreenManager = infoscreenManager;

            _messageGateway = messageGateway;
            _GuestMessageGateway = GuestMessageGateway;
        }

        public void Run()
        {

            var startTimeSpan = TimeSpan.Zero;
            var periodTimeSpan = TimeSpan.FromMinutes(5);

            var timer = new System.Threading.Timer((e) =>
            {
                _GuestMessageGateway.PublishGuest();

                _infoscreenManager.GetInfoscreenInformations("Kantine");
                _infoscreenManager.GetInfoscreenInformations("Ekstrudering");
                _infoscreenManager.GetInfoscreenInformations("Termoform");

                _infoscreenManager.GetRssFeedFromUrl(RSSFeedURL);

            },
            null,
            startTimeSpan, periodTimeSpan);

            Console.ReadLine();
        }
    }
}
