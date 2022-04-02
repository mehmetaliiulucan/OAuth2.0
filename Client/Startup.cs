using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OAuth;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Client
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
            services.AddAuthentication(config => {
                // We check the cookie to confirm that we are authenticated
                config.DefaultAuthenticateScheme = "ClientCookie";
                // When we sign in we will deal out a cookie
                config.DefaultSignInScheme = "ClientCookie";
                // use this to check if we are allowed to do something.
                config.DefaultChallengeScheme = "OurServer";
            })
               .AddCookie("ClientCookie")
            .AddOAuth("OurServer", config =>
             {
                 config.ClientId = "client_id";
                 config.ClientSecret = "client_secret";
                 config.CallbackPath = "/oauth/callback";
                 config.AuthorizationEndpoint = "https://localhost:44358/oauth/authorize";
                 config.TokenEndpoint = "https://localhost:44358/token/token";
                 config.SaveTokens = true;
                 config.Scope.Add("profil"); 
                 config.Events = new OAuthEvents()
                 {
                     OnCreatingTicket = context =>
                     {
                         var accessToken = context.AccessToken;
                         var base64payload = GetBase64String(accessToken.Split('.')[1]);
                         var bytes = Convert.FromBase64String(base64payload);
                         var jsonPayload = Encoding.UTF8.GetString(bytes);
                         var claims = JsonConvert.DeserializeObject<Dictionary<string, string>>(jsonPayload);

                         foreach (var claim in claims)
                         {
                             context.Identity.AddClaim(new Claim(claim.Key, claim.Value));
                         }

                         return Task.CompletedTask;
                     }
                 };
             }

            );
    //        services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    //.AddCookie(CookieAuthenticationDefaults.AuthenticationScheme,
    //    options =>
    //    {
    //        options.LoginPath = new PathString("/home/index");
    //        options.AccessDeniedPath = new PathString("/auth/denied");
    //    });

            services.AddHttpClient()
                .AddHttpContextAccessor();
            services.AddControllersWithViews();
        }

        private string GetBase64String(string base64payload)
        {
            base64payload = base64payload.Replace('_', '/').Replace('-', '+');
            switch (base64payload.Length % 4)
            {
                case 2: base64payload += "=="; break;
                case 3: base64payload += "="; break;
            }

            return base64payload;
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
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();


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
