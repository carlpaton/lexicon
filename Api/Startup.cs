using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Repository.Interface;
using Repository.MsSQL;

namespace Api
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
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            // DI
            services.AddSingleton<ICategoryRepository>(new CategoryRepository(AppState.ConnectionString));
            services.AddSingleton<IEntryRepository>(new EntryRepository(AppState.ConnectionString));
            services.AddSingleton<IEntryPlatformRepository>(new EntryPlatformRepository(AppState.ConnectionString));
            services.AddSingleton<IPlatformRepository>(new PlatformRepository(AppState.ConnectionString));
            services.AddSingleton<ISubCategoryRepository>(new SubCategoryRepository(AppState.ConnectionString));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
                app.UseDeveloperExceptionPage();
            else
                app.UseHsts();

            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
