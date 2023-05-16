using Swapy.DAL;
using System.Text;
using Swapy.BLL.Services;
using Swapy.DAL.Interfaces;
using Swapy.BLL.Interfaces;
using Swapy.Common.Entities;
using Swapy.DAL.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using MediatR;
using System.Reflection;

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
                option.UseSqlServer(builder.Configuration.GetConnectionString("SamedSQL"));
            });


            /// <summary>
            /// Repository registration
            /// </summary>
            builder.Services.AddScoped<IAnimalAttributeRepository, AnimalAttributeRepository>();
            builder.Services.AddScoped<IAnimalBreedRepository, AnimalBreedRepository>();
            builder.Services.AddScoped<IAutoAttributeRepository, AutoAttributeRepository>();
            builder.Services.AddScoped<IAutoBrandRepository, AutoBrandRepository>();
            builder.Services.AddScoped<IAutoBrandTypeRepository, AutoBrandTypeRepository>();
            builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
            builder.Services.AddScoped<IChatRepository, ChatRepository>();
            builder.Services.AddScoped<ICityRepository, CityRepository>();
            builder.Services.AddScoped<IClothesAttributeRepository, ClothesAttributeRepository>();
            builder.Services.AddScoped<IClothesBrandRepository, ClothesBrandRepository>();
            builder.Services.AddScoped<IClothesBrandViewRepository, ClothesBrandViewRepository>();
            builder.Services.AddScoped<IClothesSeasonRepository, ClothesSeasonRepository>();
            builder.Services.AddScoped<IClothesSizeRepository, ClothesSizeRepository>();
            builder.Services.AddScoped<IClothesViewRepository, ClothesViewRepository>();
            builder.Services.AddScoped<IColorRepository, ColorRepository>();
            builder.Services.AddScoped<ICurrencyRepository, CurrencyRepository>();
            builder.Services.AddScoped<IElectronicAttributeRepository, ElectronicAttributeRepository>();
            builder.Services.AddScoped<IElectronicBrandRepository, ElectronicBrandRepository>();
            builder.Services.AddScoped<IElectronicBrandTypeRepository, ElectronicBrandTypeRepository>();
            builder.Services.AddScoped<IFavoriteProductRepository, FavoriteProductRepository>();
            builder.Services.AddScoped<IFuelTypeRepository, FuelTypeRepository>();
            builder.Services.AddScoped<IGenderRepository, GenderRepository>();
            builder.Services.AddScoped<IItemAttributeRepository, ItemAttributeRepository>();
            builder.Services.AddScoped<ILikeRepository, LikeRepository>();
            builder.Services.AddScoped<IMemoryModelRepository, MemoryModelRepository>();
            builder.Services.AddScoped<IMemoryRepository, MemoryRepository>();
            builder.Services.AddScoped<IMessageRepository, MessageRepository>();
            builder.Services.AddScoped<IModelColorRepository, ModelColorRepository>();
            builder.Services.AddScoped<IModelRepository, ModelRepository>();
            builder.Services.AddScoped<IProductImageRepository, ProductImageRepository>();
            builder.Services.AddScoped<IProductRepository, ProductRepository>();
            builder.Services.AddScoped<IRefreshTokenRepository, RefreshTokenRepository>();
            builder.Services.AddScoped<IRealEstateAttributeRepository, RealEstateAttributeRepository>();
            builder.Services.AddScoped<IScreenDiagonalRepository, ScreenDiagonalRepository>();
            builder.Services.AddScoped<IScreenResolutionRepository, ScreenResolutionRepository>();
            builder.Services.AddScoped<IShopAttributeRepository, ShopAttributeRepository>();
            builder.Services.AddScoped<ISubcategoryRepository, SubcategoryRepository>();
            builder.Services.AddScoped<ISubscriptionRepository, SubscriptionRepository>();
            builder.Services.AddScoped<ITransmissionTypeRepository, TransmissionTypeRepository>();
            builder.Services.AddScoped<ITVAttributeRepository, TVAttributeRepository>();
            builder.Services.AddScoped<ITVBrandRepository, TVBrandRepository>();
            builder.Services.AddScoped<ITVTypeRepository, TVTypeRepository>();
            builder.Services.AddScoped<IUserLikeRepository, UserLikeRepository>();
            builder.Services.AddScoped<IUserSubscriptionRepository, UserSubscriptionRepository>();


            /// <summary>
            /// Service registration
            /// </summary>
            builder.Services.AddScoped<IEmailService, EmailService>();
            builder.Services.AddScoped<ISubcategoryService, SubcategoryService>();
            builder.Services.AddScoped<ITokenService, TokenService>();


            /// <summary>
            /// CORS(Cross - Origin Resource Sharing)
            /// </summary>
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
            builder.Services.AddIdentity<User, IdentityRole>(options => options.SignIn.RequireConfirmedAccount = false)
                .AddEntityFrameworkStores<SwapyDbContext>()
                .AddDefaultTokenProviders();


            /// <summary>
            /// Configurations for JWToken
            /// </summary>
            var guid = builder.Configuration["JWTKey"];
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
            /// Configurations for MediatR
            /// </summary>
            builder.Services.AddMediatR(Assembly.GetExecutingAssembly());


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