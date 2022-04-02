using AuthorizationServer.DataAccess;
using AuthorizationServer.Flows.Concrete;
using AuthorizationServer.Flows.Abstract;
using AuthorizationServer.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authentication.OAuth;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using AuthorizationServer.Flows.TokenFlow.Concrete;
using AuthorizationServer.Flows.Concrete.AuthorizationGrantFlow;

namespace AuthorizationServer
{
    public class Startup
    {
        public static string SECRET_KEY { get; } = "secret_key_for_token_validation_sdlsdksldksldks_dsldksld";
        public const string ISSUER = "https://localhost:44358/";
        public const string AUDIANCE = ISSUER;
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddAuthentication("OAuth")
                .AddJwtBearer("OAuth", config =>
                {
                    var secretBytes = Encoding.UTF8.GetBytes(SECRET_KEY);
                    var key = new SymmetricSecurityKey(secretBytes);
                    var x = key.ToString();

                    config.TokenValidationParameters = new TokenValidationParameters()
                    {
                        ValidIssuer = ISSUER,
                        ValidAudience = AUDIANCE,
                        IssuerSigningKey = key,
                        ValidateIssuerSigningKey = true,
                        ValidateIssuer = true,
                        ValidateAudience = true
                    };
                });

            services.AddSingleton<IClientRepository, ClientRepository>();
            services.AddSingleton<ITokenGenerator, JwtTokenGenerator>();
            services.AddSingleton<IClientManager, ClientManager>();
            services.AddSingleton<IGrantFlow, AuthorizationGrantFlow>();
            services.AddSingleton<IGrantFlow, ImplicitGrantFlow>();
            services.AddSingleton<ITokenFlow, AuthorizationTokenFlow>();
            services.AddSingleton<ITokenFlow, RefreshTokenFlow>();
            services.AddSingleton<ITokenFlow, PasswordCredentialsTokenFlow>();
            services.AddSingleton<ITokenFlow, ClientCredentialsTokenFlow>();
            services.AddSingleton<ITokenService, JwtTokenService>();

            services.AddControllersWithViews();

            services.AddHttpClient().AddHttpContextAccessor();
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
        private static string GetSigningKey()
        {
            using var cryptoServiceProvider = new RNGCryptoServiceProvider();
            var randomBytes = new byte[256];
            cryptoServiceProvider.GetBytes(randomBytes);

            return BitConverter.ToString(randomBytes).Replace("-", "");
        }
    }
}
