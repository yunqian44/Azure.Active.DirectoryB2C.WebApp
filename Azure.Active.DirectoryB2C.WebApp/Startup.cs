using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Azure.Active.DirectoryB2C.WebApp.Common;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.AzureADB2C.UI;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Azure.Active.DirectoryB2C.WebApp
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IWebHostEnvironment environment)
        {
            Configuration = configuration;
            Environment = environment;
        }

        public IWebHostEnvironment Environment { get; }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton(new Appsettings(Environment.ContentRootPath));


            services.AddAuthentication(AzureADB2CDefaults.AuthenticationScheme)
          .AddAzureADB2C(options =>
          {
              options.Instance = Appsettings.app("Azure_AD_B2C", "Instance");
              options.ClientId = Appsettings.app("Azure_AD_B2C", "ClientId");
              options.CallbackPath = Appsettings.app("Azure_AD_B2C", "CallbackPath");
              options.Domain = Appsettings.app("Azure_AD_B2C", "Domain");
              options.SignUpSignInPolicyId = Appsettings.app("Azure_AD_B2C", "SignUpSignInPolicyId");
              options.ResetPasswordPolicyId = Appsettings.app("Azure_AD_B2C", "ResetPasswordPolicyId");
              options.EditProfilePolicyId = Appsettings.app("Azure_AD_B2C", "EditProfilePolicyId");
          });

            services.AddControllersWithViews();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseStaticFiles();

            app.UseRouting();

            // open authentication middleware
            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
