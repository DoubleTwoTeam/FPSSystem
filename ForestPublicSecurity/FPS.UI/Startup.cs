using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using FPS.Services;
using FPS.IServices;
using FPS.UI.Common;
using Microsoft.Extensions.Caching.Redis;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace FPS.UI
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
            services.AddTransient<IPageHelper, PageHelp>();
            services.AddTransient<IStudent, StudentServices>();
            services.AddTransient<IJurisdiction, JurisdictionService>();
            services.AddTransient<IPoliceCase, PoliceCaseServices>();
            services.AddTransient<IApprove, ApproveServices>();
            services.AddTransient<IAlarm, AlarmServices>();
            services.AddSingleton<IConfiguration>(Configuration);

            services.AddDistributedMemoryCache();
            services.AddSession();

            var dbConnectionString = "Data Source=169.254.159.216/orcl;User ID=scott;Password=tiger;";
            SugerBase.DBConnectionString = dbConnectionString;
            //services.Configure<CookiePolicyOptions>(options =>
            //{
            //    // This lambda determines whether user consent for non-essential cookies is needed for a given request.
            //    options.CheckConsentNeeded = context => true;
            //    options.MinimumSameSitePolicy = SameSiteMode.None;
            //});


            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            //注册Redis服务
            services.AddSingleton(typeof(ICacheService), new RedisCacheService(new RedisCacheOptions()
            {
                Configuration = Configuration.GetSection("Cache:ConnectionString").Value,
                InstanceName = Configuration.GetSection("Cache:InstanceName").Value
            }));
            //注册Cookie身份验证服务
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                    .AddCookie(options => options.LoginPath = new PathString("/home/index"));

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            //开启验证
            app.UseAuthentication();

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();
            app.UseSession();


            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Center}/{action=Index}/{id?}");
            });
        }
    }
}
