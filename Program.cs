using ETHShop.Interfaces;
using ETHShop.Servieces;
using ETHShop.PassWordLogic;
using Microsoft.EntityFrameworkCore;
using ETHShop.Repositories;
using ETHShop.Controllers;
using ETHShop.Jwt;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.Extensions.Configuration;
using System.Security.Cryptography.X509Certificates;

namespace ETHShop
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            var services = builder.Services;

            var configuration = builder.Configuration;

            services.AddCors(options =>
            {
                options.AddDefaultPolicy(policy =>
                {
                    policy.WithOrigins("http://localhost:5173");
                    policy.AllowAnyHeader();
                    policy.AllowAnyMethod();
                });
            });
            builder.Services.AddSwaggerGen();

            services.AddDbContext<ShopDbContext>(
                options =>
                {
                    options.UseNpgsql(builder.Configuration.GetConnectionString(nameof(ShopDbContext)));
                });
            services.AddControllers();

            

            services.Configure<JwtOptions>(configuration.GetSection(nameof(JwtOptions)));
            
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IUsersRepository, UserRepository>();

            services.AddScoped<ISellersService, SellersService>();
            services.AddScoped<IProductsService, ProductsService>();

                        

            services.AddScoped<IMyPasswordHasher, PasswordHasher>();

            services.AddScoped<IJwtProvider, JwtProvider>();
            



            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            //app.UseHttpsRedirection();
            app.UseCors();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
