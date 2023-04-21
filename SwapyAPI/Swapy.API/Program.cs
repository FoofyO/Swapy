using Swapy.DAL;
using System.Text;
using Swapy.DAL.Entities;
using Swapy.DAL.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace Swapy.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            /// <summary>
            /// Services setup
            /// </summary>

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddDbContext<SwapyDbContext>(option =>
            {
                option.UseLazyLoadingProxies();
                option.UseSqlServer(builder.Configuration.GetConnectionString("DockerSQL"));
            });


            /// <summary>
            /// Services registration
            /// </summary>
            //builder.Services.AddScoped<IFolderRepository, FolderRepository>();

            builder.Services.AddCors(option =>
            {
                option.AddPolicy("Default", policy =>
                {
                    policy.AllowAnyOrigin();
                    policy.AllowAnyMethod();
                    policy.AllowAnyHeader();
                });
            });

            /// <summary>
            /// Register Identity Service
            /// </summary>
            builder.Services.AddIdentity<User, IdentityRole>(option => option.SignIn.RequireConfirmedAccount = false)
                .AddEntityFrameworkStores<SwapyDbContext>()
                .AddDefaultTokenProviders();



            /// <summary>
            /// Configurations for JWToken
            /// </summary>
            var guid = builder.Configuration["JWT-Key"];
            var key = Encoding.ASCII.GetBytes(guid);

            builder.Services.AddAuthentication(options =>
            {
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = false;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = false,
                    ValidateLifetime = true,
                    ValidateAudience = false,
                    ValidateIssuerSigningKey = true,
                    ClockSkew = TimeSpan.FromDays(5),
                    IssuerSigningKey = new SymmetricSecurityKey(key)
                };
            });



            /// <summary>
            /// Application setup
            /// </summary>

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseCors("Default");

            app.UseRouting();

            app.UseAuthorization();
            app.UseAuthentication();
            
            app.UseHttpsRedirection();

            app.MapControllers();

            app.Run();
        }
    }
}