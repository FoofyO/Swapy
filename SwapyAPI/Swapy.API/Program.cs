using Swapy.DAL;
using Swapy.BLL.Services;
using Swapy.DAL.Interfaces;
using Swapy.BLL.Interfaces;
using Swapy.Common.Entities;
using Swapy.DAL.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MediatR;
using System.Reflection;
using Swapy.BLL.Domain.Auth.CommandHandlers;
using Swapy.BLL.Domain.Auth.Commands;
using Swapy.BLL.Domain.Chats.Commands;
using Swapy.BLL.Domain.Chats.Queries;
using Swapy.BLL.Domain.Products.Commands;
using Swapy.BLL.Domain.Products.Queries;
using Swapy.BLL.Domain.Shops.Commands;
using Swapy.BLL.Domain.Shops.Queries;
using Swapy.BLL.Domain.Users.Commands;
using Swapy.BLL.Domain.Users.Queries;
using Swapy.BLL.Domain.Products.CommandHandlers;
using Swapy.BLL.Domain.Users.CommandHandlers;
using Swapy.BLL.Domain.Users.QueryHandlers;
using Swapy.BLL.Domain.Chats.CommandHandlers;
using Swapy.BLL.Domain.Products.QueryHandlers;
using Swapy.BLL.Domain.Shops.QueryHandlers;
using Swapy.BLL.Domain.Chats.QueryHandlers;
using Swapy.BLL.Domain.Shops.CommandHandlers;
using System.Security.Claims;
using Microsoft.OpenApi.Models;
using Swapy.API.Middleware;
using Swapy.Common.DTO.Products.Responses;
using Swapy.Common.DTO.Auth.Responses;
using System.Text.Json.Serialization;
using Swapy.Common.DTO.Shops.Responses;
using Swapy.Common.DTO.Users.Responses;

namespace Swapy.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            /// <summary>
            /// Services Setup
            /// </summary>
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();

            //Swagger Registration
            builder.Services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo { Title = "Swapy Api", Description = "'Swapy' REST Api", Version = "v1" });
                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Please insert JWT with Bearer into field",
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey
                });
                options.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        new string[] { }
                    }
                });
            });


            /// <summary>
            /// Database Registration
            /// </summary>
            builder.Services.AddDbContext<SwapyDbContext>(option =>
            {
                option.UseSqlServer(builder.Configuration.GetConnectionString("SamedSQL"));
            });


            /// <summary>
            /// JSON Registration
            /// </summary>
            builder.Services.AddControllers().AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve;
            });

            /// <summary>
            /// Repository Registration
            /// </summary>
            builder.Services.AddScoped<IAnimalAttributeRepository, AnimalAttributeRepository>();
            builder.Services.AddScoped<IAnimalBreedRepository, AnimalBreedRepository>();
            builder.Services.AddScoped<IAutoAttributeRepository, AutoAttributeRepository>();
            builder.Services.AddScoped<IAutoBrandRepository, AutoBrandRepository>();
            builder.Services.AddScoped<IAutoModelRepository, AutoModelRepository>();
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
            builder.Services.AddScoped<IUserTokenRepository, UserTokenRepository>();


            /// <summary>
            /// Service Registration
            /// </summary>
            builder.Services.AddScoped<IEmailService, EmailService>();
            builder.Services.AddScoped<ISubcategoryService, SubcategoryService>();
            builder.Services.AddScoped<IUserTokenService, UserTokenService>();


            /// <summary>
            /// CQRS registration
            /// </summary>
            builder.Services.AddTransient<IRequestHandler<AddAnimalAttributeCommand, AnimalAttribute>, AddAnimalAttributeCommandHandler>();
            builder.Services.AddTransient<IRequestHandler<AddAutoAttributeCommand, AutoAttribute>, AddAutoAttributeCommandHandler>();
            builder.Services.AddTransient<IRequestHandler<AddClothesAttributeCommand, ClothesAttribute>, AddClothesAttributeCommandHandler>();
            builder.Services.AddTransient<IRequestHandler<AddElectronicAttributeCommand, ElectronicAttribute>, AddElectronicAttributeCommandHandler>();
            builder.Services.AddTransient<IRequestHandler<AddFavoriteProductCommand, FavoriteProduct>, AddFavoriteProductCommandHandler>();
            builder.Services.AddTransient<IRequestHandler<AddItemAttributeCommand, ItemAttribute>, AddItemAttributeCommandHandler>();
            builder.Services.AddTransient<IRequestHandler<AddLikeCommand, Like>, AddLikeCommandHandler>();
            builder.Services.AddTransient<IRequestHandler<AddRealEstateAttributeCommand, RealEstateAttribute>, AddRealEstateAttributeCommandHandler>();
            builder.Services.AddTransient<IRequestHandler<AddSubscriptionCommand, Subscription>, AddSubscriptionCommandHandler>();
            builder.Services.AddTransient<IRequestHandler<AddTVAttributeCommand, TVAttribute>, AddTVAttributeCommandHandler>();
            builder.Services.AddTransient<IRequestHandler<CheckLikeQuery, bool>, CheckLikeQueryHandler>();
            builder.Services.AddTransient<IRequestHandler<CheckSubscriptionQuery, bool>, CheckSubscriptionQueryHandler>();
            builder.Services.AddTransient<IRequestHandler<CreateChatCommand, Chat>, CreateChatCommandHandler>();
            builder.Services.AddTransient<IRequestHandler<EmailCommand, bool>, CheckEmailCommandHandler>();
            builder.Services.AddTransient<IRequestHandler<GetAllAnimalAttributesQuery, ProductsResponseDTO<AnimalAttribute>>, GetAllAnimalAttributesQueryHandler>();
            builder.Services.AddTransient<IRequestHandler<GetAllAnimalAttributesQuery, ProductsResponseDTO<AnimalAttribute>>, GetAllAnimalAttributesQueryHandler>();
            builder.Services.AddTransient<IRequestHandler<GetAllAnimalBreedsQuery, IEnumerable<AnimalBreed>>, GetAllAnimalBreedsQueryHandler>();
            builder.Services.AddTransient<IRequestHandler<GetAllAutoAttributesQuery, ProductsResponseDTO<AutoAttribute>>, GetAllAutoAttributesQueryHandler>();
            builder.Services.AddTransient<IRequestHandler<GetAllAutoBrandsQuery, IEnumerable<AutoBrand>>, GetAllAutoBrandsQueryHandler>();
            builder.Services.AddTransient<IRequestHandler<GetAllBuyerChatsQuery, IEnumerable<Chat>>, GetAllBuyerChatsQueryHandler>();
            builder.Services.AddTransient<IRequestHandler<GetAllCitiesQuery, IEnumerable<City>>, GetAllCitiesQueryHandler>();
            builder.Services.AddTransient<IRequestHandler<GetAllClothesAttributesQuery, ProductsResponseDTO<ClothesAttribute>>, GetAllClothesAttributesQueryHandler>();
            builder.Services.AddTransient<IRequestHandler<GetAllClothesBrandsQuery, IEnumerable<ClothesBrand>>, GetAllClothesBrandsQueryHandler>();
            builder.Services.AddTransient<IRequestHandler<GetAllClothesSeasonsQuery, IEnumerable<ClothesSeason>>, GetAllClothesSeasonsQueryHandler>();
            builder.Services.AddTransient<IRequestHandler<GetAllClothesSizesQuery, IEnumerable<ClothesSize>>, GetAllClothesSizesQueryHandler>();
            builder.Services.AddTransient<IRequestHandler<GetAllClothesViewsQuery, IEnumerable<ClothesView>>, GetAllClothesViewsQueryHandler>();
            builder.Services.AddTransient<IRequestHandler<GetAllColorsQuery, IEnumerable<Color>>, GetAllColorsQueryHandler>();
            builder.Services.AddTransient<IRequestHandler<GetAllCurrenciesQuery, IEnumerable<Currency>>, GetAllCurrenciesQueryHandler>();
            builder.Services.AddTransient<IRequestHandler<GetAllElectronicAttributesQuery, ProductsResponseDTO<ElectronicAttribute>>, GetAllElectronicAttributesQueryHandler>();
            builder.Services.AddTransient<IRequestHandler<GetAllElectronicBrandsQuery, IEnumerable<ElectronicBrand>>, GetAllElectronicBrandsQueryHandler>();
            builder.Services.AddTransient<IRequestHandler<GetAllFavoriteProductsQuery, ProductsResponseDTO<FavoriteProduct>>, GetAllFavoriteProductsQueryHandler>();
            builder.Services.AddTransient<IRequestHandler<GetAllFuelTypesQuery, IEnumerable<FuelType>>, GetAllFuelTypesQueryHandler>();
            builder.Services.AddTransient<IRequestHandler<GetAllGendersQuery, IEnumerable<Gender>>, GetAllGendersQueryHandler>();
            builder.Services.AddTransient<IRequestHandler<GetAllItemAttributesQuery, ProductsResponseDTO<ItemAttribute>>, GetAllItemAttributesQueryHandler>();
            builder.Services.AddTransient<IRequestHandler<GetAllMemoriesQuery, IEnumerable<Memory>>, GetAllMemoriesQueryHandler>();
            builder.Services.AddTransient<IRequestHandler<GetAllModelsQuery, IEnumerable<Model>>, GetAllModelsQueryHandler>();
            builder.Services.AddTransient<IRequestHandler<GetAllProductsQuery, ProductsResponseDTO<Product>>, GetAllProductsQueryHandler>();
            builder.Services.AddTransient<IRequestHandler<GetAllRealEstateAttributesQuery, ProductsResponseDTO<RealEstateAttribute>>, GetAllRealEstateAttributesQueryHandler>();
            builder.Services.AddTransient<IRequestHandler<GetAllScreenDiagonalsQuery, IEnumerable<ScreenDiagonal>>, GetAllScreenDiagonalsQueryHandler>();
            builder.Services.AddTransient<IRequestHandler<GetAllScreenResolutionsQuery, IEnumerable<ScreenResolution>>, GetAllScreenResolutionsQueryHandler>();
            builder.Services.AddTransient<IRequestHandler<GetAllSellerChatsQuery, IEnumerable<Chat>>, GetAllSellerChatsQueryHandler>();
            builder.Services.AddTransient<IRequestHandler<GetAllShopsQuery, ShopsResponseDTO>, GetAllShopsQueryHandler>();
            builder.Services.AddTransient<IRequestHandler<GetAllTVAttributesQuery, ProductsResponseDTO<TVAttribute>>, GetAllTVAttributesQueryHandler>();
            builder.Services.AddTransient<IRequestHandler<GetAllTVBrandsQuery, IEnumerable<TVBrand>>, GetAllTVBrandsQueryHandler>();
            builder.Services.AddTransient<IRequestHandler<GetAllTVTypesQuery, IEnumerable<TVType>>, GetAllTVTypesQueryHandler>();
            builder.Services.AddTransient<IRequestHandler<GetAllTransmissionTypesQuery, IEnumerable<TransmissionType>>, GetAllTransmissionTypesQueryHandler>();
            builder.Services.AddTransient<IRequestHandler<GetByIdAnimalAttributeQuery, AnimalAttribute>, GetByIdAnimalAttributesQueryHandler>();
            builder.Services.AddTransient<IRequestHandler<GetByIdAutoAttributeQuery, AutoAttribute>, GetByIdAutoAttributeQueryHandler>();
            builder.Services.AddTransient<IRequestHandler<GetByIdClothesAttributeQuery, ClothesAttribute>, GetByIdClothesAttributeQueryHandler>();
            builder.Services.AddTransient<IRequestHandler<GetByIdElectronicAttributeQuery, ElectronicAttribute>, GetByIdElectronicAttributeQueryHandler>();
            builder.Services.AddTransient<IRequestHandler<GetByIdFavoriteProductQuery, FavoriteProduct>, GetByIdFavoriteProductQueryHandler>();
            builder.Services.AddTransient<IRequestHandler<GetByIdItemAttributeQuery, ItemAttribute>, GetByIdItemAttributeQueryHandler>();
            builder.Services.AddTransient<IRequestHandler<GetByIdRealEstateAttributeQuery, RealEstateAttribute>, GetByIdRealEstateAttributeQueryHandler>();
            builder.Services.AddTransient<IRequestHandler<GetByIdShopQuery, ShopDetailResponseDTO>, GetByIdShopQueryHandler>();
            builder.Services.AddTransient<IRequestHandler<GetByIdTVAttributeQuery, TVAttribute>, GetByIdTVAttributeQueryHandler>();
            builder.Services.AddTransient<IRequestHandler<GetByIdUserQuery, UserResponseDTO>, GetByIdUserQueryHandler>();
            builder.Services.AddTransient<IRequestHandler<GetDetailChatQuery, Chat>, GetDetailChatQueryHandler>();
            builder.Services.AddTransient<IRequestHandler<GetUserSubscriptionsQuery, IEnumerable<Subscription>>, GetUserSubscriptionsQueryHandler>();
            builder.Services.AddTransient<IRequestHandler<IncrementProductViewsCommand, Unit>, IncrementProductViewsCommandHandler>();
            builder.Services.AddTransient<IRequestHandler<LoginCommand, AuthResponseDTO>, LoginCommandHandler>();
            builder.Services.AddTransient<IRequestHandler<LogoutCommand, Unit>, LogoutCommandHandler>();
            builder.Services.AddTransient<IRequestHandler<PhoneNumberCommand, bool>, CheckPhoneCommandHandler>();
            builder.Services.AddTransient<IRequestHandler<UpdateUserTokenCommand, AuthResponseDTO>, UpdateUserTokenCommandHandler>();
            builder.Services.AddTransient<IRequestHandler<RemoveAnimalAttributeCommand, Unit>, RemoveAnimalAttributeCommandHandler>();
            builder.Services.AddTransient<IRequestHandler<RemoveAutoAttributeCommand, Unit>, RemoveAutoAttributeCommandHandler>();
            builder.Services.AddTransient<IRequestHandler<RemoveClothesAttributeCommand, Unit>, RemoveClothesAttributeCommandHandler>();
            builder.Services.AddTransient<IRequestHandler<RemoveElectronicAttributeCommand, Unit>, RemoveElectronicAttributeCommandHandler>();
            builder.Services.AddTransient<IRequestHandler<RemoveFavoriteProductCommand, Unit>, RemoveFavoriteProductCommandHandler>();
            builder.Services.AddTransient<IRequestHandler<RemoveItemAttributeCommand, Unit>, RemoveItemAttributeCommandHandler>();
            builder.Services.AddTransient<IRequestHandler<RemoveLikeCommand, Unit>, RemoveLikeCommandHandler>();
            builder.Services.AddTransient<IRequestHandler<RemoveRealEstateAttributeCommand, Unit>, RemoveRealEstateAttributeCommandHandler>();
            builder.Services.AddTransient<IRequestHandler<RemoveSubscriptionCommand, Unit>, RemoveSubscriptionCommandHandler>();
            builder.Services.AddTransient<IRequestHandler<RemoveTVAttributeCommand, Unit>, RemoveTVAttributeCommandHandler>();
            builder.Services.AddTransient<IRequestHandler<RemoveUserCommand, Unit>, RemoveUserCommandHandler>();
            builder.Services.AddTransient<IRequestHandler<SendMessageCommand, Message>, SendMessageCommandHandler>();
            builder.Services.AddTransient<IRequestHandler<ShopNameCommand, bool>, CheckShopNameCommandHandler>();
            builder.Services.AddTransient<IRequestHandler<ShopRegistrationCommand, AuthResponseDTO>, ShopRegistrationCommandHandler>();
            builder.Services.AddTransient<IRequestHandler<UpdateAnimalAttributeCommand, Unit>, UpdateAnimalAttributeCommandHandler>();
            builder.Services.AddTransient<IRequestHandler<UpdateAutoAttributeCommand, Unit>, UpdateAutoAttributeCommandHandler>();
            builder.Services.AddTransient<IRequestHandler<UpdateClothesAttributeCommand, Unit>, UpdateClothesAttributeCommandHandler>();
            builder.Services.AddTransient<IRequestHandler<UpdateElectronicAttributeCommand, Unit>, UpdateElectronicAttributeCommandHandler>();
            builder.Services.AddTransient<IRequestHandler<UpdateItemAttributeCommand, Unit>, UpdateItemAttributeCommandHandler>();
            builder.Services.AddTransient<IRequestHandler<UpdateRealEstateAttributeCommand, Unit>, UpdateRealEstateAttributeCommandHandler>();
            builder.Services.AddTransient<IRequestHandler<UpdateShopCommand, Unit>, UpdateShopCommandHandler>();
            builder.Services.AddTransient<IRequestHandler<UpdateTVAttributeCommand, Unit>, UpdateTVAttributeCommandHandler>();
            builder.Services.AddTransient<IRequestHandler<UpdateUserCommand, Unit>, UpdateUserCommandHandler>();
            builder.Services.AddTransient<IRequestHandler<UserRegistrationCommand, AuthResponseDTO>, UserRegistrationCommandHandler>();


            /// <summary>
            /// Claims Principal Registration
            /// </summary>
            builder.Services.AddHttpContextAccessor();

            builder.Services.AddScoped<ClaimsPrincipal>(provider =>
            {
                var httpContextAccessor = provider.GetRequiredService<IHttpContextAccessor>();
                return httpContextAccessor.HttpContext?.User;
            });


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
            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = "Basic";
                options.DefaultChallengeScheme = "Basic";
            }).AddScheme<BasicAuthenticationOptions, SwapyAuthenticationHandler>("Basic", null);


            /// <summary>
            /// Configurations for MediatR
            /// </summary>
            builder.Services.AddMediatR(Assembly.GetExecutingAssembly());


            /// <summary>
            /// Application Setup
            /// </summary>
            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseCors();

            app.UseRouting();
            
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseHttpsRedirection();

            app.MapControllers();

            app.Run();
        }
    }
}