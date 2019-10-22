using JwtAuth.DataAccess;
using JwtAuth.Services;
using JwtAuth.Services.Jwt;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Text;
using System.Threading.Tasks;

namespace JwtAuth
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<ITokenService, JwtTokenManager>();

            

            services.AddDbContext<AppDbContext>(_ => _.UseSqlServer(Configuration.GetSection("ConnectionStrings:Default").Value));
            services.AddIdentity<IdentityUser, IdentityRole>().AddEntityFrameworkStores<AppDbContext>();

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }
            )
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateAudience = true,
                    ValidAudience = "JwtAuth",
                    ValidateIssuer = true,
                    ValidIssuer = "JwtAuth",
                    ValidateLifetime = true,
                    //ClockSkew=TimeSpan.Zero,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes("lorem ipsum dolar lorem ipsum dolar lorem ipsum dolar lorem ipsum dolar lorem ipsum dolar "))
                };

                options.Events = new JwtBearerEvents
                {
                    OnTokenValidated = ctx =>
                    {
                        //Gerekirse burada gelen token içerisindeki çeşitli bilgilere göre doğrulam yapılabilir.
                        return Task.CompletedTask;
                    },
                    OnAuthenticationFailed = ctx =>
                    {
                        Console.WriteLine("Exception:{0}", ctx.Exception.Message);
                        return Task.CompletedTask;
                    }
                };
            });

            services.AddSwaggerGen(opt =>
            {
                opt.SwaggerDoc("v1", new Swashbuckle.AspNetCore.Swagger.Info
                {
                    Title = "Jwt Sample ",
                });
            });


            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);



        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env is null)
            {
                throw new System.ArgumentNullException(nameof(env));
            }

            app.UseDeveloperExceptionPage();
            app.UseExceptionHandler("/swagger");
            app.UseSwagger();
            app.UseSwaggerUI(opt =>
            {
                opt.SwaggerEndpoint("/swagger/v1/swagger.json", "Jwt Sample");
            });

            app.UseMiddleware<ExceptionMiddleware>();
            app.UseAuthentication();
            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
