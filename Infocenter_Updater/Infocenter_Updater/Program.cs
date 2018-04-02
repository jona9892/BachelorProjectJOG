
using Infocenter_Updater.BLL.Abstraction;
using Infocenter_Updater.BLL.Implementation;
using Infocenter_Updater.DAL.Context;
using Infocenter_Updater.DAL.Repository.Abstraction;
using Infocenter_Updater.DAL.Repository.Implementation;
using DTOModel.DomainModel;
using Infocenter_Updater.MessagingGateway.Abstraction;
using Infocenter_Updater.MessagingGateway.Implementation;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Infocenter_Updater
{
    public class Program
    {
        public static void Main(string[] args)
        {

            // configure services
            var services = new ServiceCollection()
                .AddTransient<IAppService, AppService>();

            services.AddLogging(); 
            services.AddOptions(); 

            //DbContext
            services.AddDbContext<SKYINTRA_DBContext>(options => options.UseSqlServer(@"Data Source=localhost\SQLEXPRESS;Initial Catalog=SKY-INTRA_DB;Integrated Security=True;"));
            
            
            //Repositories
            services.AddScoped<IGuestRepository, GuestRepository>();
            services.AddScoped<IInfoscreenRepository, InfoscreenRepository>();

            //Managers
            services.AddScoped<IInfoscreenManager, InfoscreenManager>();
            services.AddSingleton<IGuestManager, GuestManager>();

            //MessageGateway
            services.AddScoped<IMessageGateway<Infoscreen>, InfoscreenGateway>();
            services.AddScoped<IGuestMessageGateway, GuestMessageGateway>();

            // create a service provider from the service collection
            var serviceProvider = services.BuildServiceProvider();

            // resolve the dependency graph
            var appService = serviceProvider.GetService<IAppService>();

            // run the application
            appService.Run();
        }
    }
}
