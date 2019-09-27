using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Web.Models;
using Repository.Interface;
using Repository.MsSQL;
using Business;
using AutoMapper;
using Repository.Schema;
using Business.Models;
using System;
using Web.Services;

namespace Web
{
    public class Startup
    {
        public IConfiguration _configuration { get; }

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            services.AddDbContext<DummyContext>(options =>
                    options.UseSqlServer(_configuration.GetConnectionString("DummyContext")));

            // Services
            var localIPv4 = new LocalIPv4();
            services.AddSingleton<ILocalIPv4>(localIPv4);

            var publicIP = new PublicExternalIP();
            services.AddSingleton<IPublicIP>(publicIP);

            var connectionString = (GetEnvConnectionWithLocalMachineIpSubsitution(localIPv4, publicIP) ?? _configuration.GetConnectionString("ConnMsSQL"));
            Environment.SetEnvironmentVariable("ActualConnectionString", connectionString);

            var lexiconEntryBusiness = new EntryBusiness(
                    new CategoryRepository(connectionString),                      
                    new SubCategoryRepository(connectionString),
                    new PlatformRepository(connectionString));

            // Repository
            services.AddSingleton<ICategoryRepository>(new CategoryRepository(connectionString));
            services.AddSingleton<ISubCategoryRepository>(new SubCategoryRepository(connectionString));
            services.AddSingleton<IPlatformRepository>(new PlatformRepository(connectionString));
            services.AddSingleton<IEntryRepository>(new EntryRepository(connectionString));
            services.AddSingleton<IEntryPlatformRepository>(new EntryPlatformRepository(connectionString));
            
            // Automagic \ :D /
            services.AddAutoMapper(x => x.AddProfile(new MappingEntity()));

            // Business
            services.AddSingleton<IEntryBusiness>(lexiconEntryBusiness);
            services.AddSingleton<IViewDataSelectList>(new ViewDataSelectList(lexiconEntryBusiness));
        }

        /// <summary>
        /// TOOD ~ kill this code smell with FIRE! Meh.
        /// </summary>
        /// <param name="localIPv4"></param>
        /// <returns></returns>
        private string GetEnvConnectionWithLocalMachineIpSubsitution(ILocalIPv4 localIPv4, IPublicIP publicIP)
        {
            if (Environment.GetEnvironmentVariable("LEXICON_SQL_CONNECTION") == null)
                return null;

            var conn = Environment.GetEnvironmentVariable("LEXICON_SQL_CONNECTION");
            conn = "Server=@@MACHINE_NAME@@,1433;Database=lexicon;User Id=sa;Password=Password123;";

            if (Environment.GetEnvironmentVariable("SUBSTITUTE_LOCAL_IP") != null) // dumbass this means if you set this env key value to `sweet blue balls` it will work
                conn = conn.Replace("@@MACHINE_NAME@@", localIPv4.GetLocalIPv4(System.Net.NetworkInformation.NetworkInterfaceType.Ethernet));

            if (Environment.GetEnvironmentVariable("SUBSTITUTE_PUBLIC_IP") != null)
                conn = conn.Replace("@@MACHINE_NAME@@", publicIP.GetPublicIP());

            return conn;
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
                app.UseDeveloperExceptionPage();
            else
                app.UseExceptionHandler("/Home/Error");

            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }

    }

    /// <summary>
    /// Automagic with Automapper
    /// www.c-sharpcorner.com/article/mapping-similar-objects-in-asp-net-core-2-0/
    /// </summary>
    public class MappingEntity : Profile  
    {  
        public MappingEntity()  
        {  
            // `Repository` to `Business` models
            CreateMap<CategoryModel, CategoryBusinessModel>();
            CreateMap<PlatformModel, PlatformBusinessModel>();
            CreateMap<SubCategoryModel, SubCategoryBusinessModel>();
            CreateMap<EntryModel, EntryBusinessModel>();

            // `Repository` to `View` models 
            CreateMap<EntryModel, EntryViewModel>();
           
            // `View` to `Repository` models
            // TODO ~ I think this works both ways AND is not actually needed with primitive types
            CreateMap<EntryViewModel, EntryModel>();
        }  
    } 
}
