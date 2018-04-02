using DomainModel;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using SKYINTRA_RestAPI.BLL.Abstraction;
using SKYINTRA_RestAPI.BLL.Implementation;
using SKYINTRA_RestAPI.DAL.Context;
using SKYINTRA_RestAPI.DAL.Repository.Abstraction;
using SKYINTRA_RestAPI.DAL.Repository.Implementation;

namespace SKYINTRA_RestAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //DbContext
            services.AddDbContext<SKYINTRA_DBContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            services.Add(new ServiceDescriptor(typeof(IPWMetazoContext), new IPWMetazoContext(Configuration.GetConnectionString("IPWConnection"))));

            //Repositories
            services.AddScoped<IGuestRepository, GuestRepository>();
            services.AddScoped<IInformationRepository, InformationRepository>();
            services.AddScoped<IRepository<FileImage>, FileImageRepository>();
            services.AddScoped<IInfoscreenRepository, InfoscreenRepository>();
            services.AddScoped<IManyToMany<Infoscreen, Information>, InfoscreenInformationRepository>();
            services.AddScoped<IManyToMany<Infoscreen, FileImage>, InfoscreenFileImageRepository>();
            services.AddScoped<IEmployeeRepository, EmployeeRepository>();
            services.AddScoped<IFrontPageRepository, FrontPageRepository>();

            //Managers
            services.AddScoped<IGuestManager, GuestManager>();
            services.AddScoped<IEmployeeManager, EmployeeManager>();

            services.AddMvc()
                .AddJsonOptions(options =>
                {
                    options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                });

            services.AddCors(options =>
            {
                options.AddPolicy("AllowSpecificOrigin",
                    builder => builder.WithOrigins("http://example.com"));
            });
            services.AddCors();
            services.Configure<IISOptions>(options =>
            {
                options.ForwardClientCertificate = false;
            });

            services
        .AddMvcCore(options =>
        {
            options.RequireHttpsPermanent = true; // does not affect api requests
            options.RespectBrowserAcceptHeader = true; // false by default

            options.RespectBrowserAcceptHeader = true;
        })
        .AddFormatterMappings()
        .AddJsonFormatters(); 


        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            
            app.UseMvc();

            
        }
    }
}
