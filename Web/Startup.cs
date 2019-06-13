﻿using Microsoft.AspNetCore.Builder;
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

namespace Web
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
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            services.AddDbContext<DummyContext>(options =>
                    options.UseSqlServer(Configuration.GetConnectionString("DummyContext")));

            var connectionString = (Environment.GetEnvironmentVariable("LEXICON_SQL_CONNECTION") ?? Configuration.GetConnectionString("ConnMsSQL"));
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
