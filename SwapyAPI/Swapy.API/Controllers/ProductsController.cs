using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swapy.BLL.Domain.Products.Commands;
using Swapy.BLL.Domain.Products.Queries;
using Swapy.Common.DTO.Products.Requests.Commands;
using Swapy.Common.DTO.Products.Requests.Queries;
using Swapy.Common.Exceptions;
using System.Security.Claims;

namespace Swapy.API.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    [Produces("application/json")]
    public class ProductsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ProductsController(IMediator mediator) => _mediator = mediator;

        [HttpGet("ping")]
        [Authorize]
        public async Task<IActionResult> Ping()
        {
            return Ok("ping");
        }


        /// <summary>
        /// Animals Attribute
        /// </summary>
        [HttpPost("animals")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> AddAnimalAsync(AddAnimalAttributeCommandDTO dto)
        {
            try
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                var command = new AddAnimalAttributeCommand()
                {
                    UserId = userId,
                    Title = dto.title,
                    Description = dto.description,
                    Price = dto.price,
                    CurrencyId = dto.currencyId,
                    CategoryId = dto.categoryId,
                    SubcategoryId = dto.subcategoryId,
                    CityId = dto.cityId,
                    AnimalBreedId = dto.animalBreedId
                };

                var result = await _mediator.Send(command);
                var locationUri = new Uri($"{Request.Scheme}://{Request.Host.ToUriComponent()}/animals/{result.Id}");
                return Created(locationUri, result);
            }
            catch (ArgumentException ex)
            {
                return BadRequest("Invalid parameters: " + ex.Message);
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while processing the request: " + ex.Message);
            }
        }

        [HttpDelete("animals")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> RemoveAnimalAsync([FromRoute] RemoveAnimalAttributeCommandDTO dto)
        {
            try
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                var command = new RemoveAnimalAttributeCommand()
                {
                    UserId = userId,
                    AnimalAttributeId = dto.animalAttributeId
                };

                var result = await _mediator.Send(command);
                return NoContent();
            }
            catch (NoAccessException ex)
            {
                return Forbid(ex.Message);
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while processing the request: " + ex.Message);
            }
        }

        [HttpPut("animals")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateAnimalAsync([FromQuery] UpdateAnimalAttributeCommandDTO dto)
        {
            try
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                var command = new UpdateAnimalAttributeCommand()
                {
                    UserId = userId,
                    Title = dto.title,
                    Description = dto.description,
                    Price = dto.price,
                    CurrencyId = dto.currencyId,
                    CategoryId = dto.categoryId,
                    SubcategoryId = dto.subcategoryId,
                    CityId = dto.cityId,
                    ProductId = dto.productId,
                    AnimalBreedId = dto.animalBreedId
                };

                var result = await _mediator.Send(command);
                return NoContent();
            }
            catch (NoAccessException ex)
            {
                return Forbid(ex.Message);
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while processing the request: " + ex.Message);
            }
        }

        [HttpGet("animals")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllAnimalsAsync([FromQuery] GetAllAnimalAttributesQueryDTO dto)
        {
            try
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                var query = new GetAllAnimalAttributesQuery()
                {
                    UserId = userId,
                    Page = dto.page,
                    PageSize = dto.pageSize,
                    Title = dto.title,
                    CurrencyId = dto.currencyId,
                    PriceMin = dto.priceMin,
                    PriceMax = dto.priceMax,
                    CategoryId = dto.categoryId,
                    SubcategoryId = dto.subcategoryId,
                    CityId = dto.cityId,
                    OtherUserId = dto.otherUserId,
                    SortByPrice = dto.sortByPrice,
                    ReverseSort = dto.reverseSort,
                    AnimalBreedsId = dto.animalBreedsId,
                    AnimalTypesId = dto.animalTypesId
                };

                var result = await _mediator.Send(query);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while processing the request: " + ex.Message);
            }
        }

        [HttpGet("animals/{productId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetByIdAnimalAsync([FromRoute] GetByIdProductQueryDTO dto)
        {
            try
            {
                var query = new GetByIdAnimalAttributeQuery()
                {
                    ProductId = dto.productId
                };

                var result = await _mediator.Send(query);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while processing the request: " + ex.Message);
            }
        }


        /// <summary>
        /// Auto Attribute
        /// </summary>
        [HttpPost("autos")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> AddAutoAsync(AddAutoAttributeCommandDTO dto)
        {
            try
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                var command = new AddAutoAttributeCommand()
                {
                    UserId = userId,
                    Title = dto.title,
                    Description = dto.description,
                    Price = dto.price,
                    CurrencyId = dto.currencyId,
                    CategoryId = dto.categoryId,
                    SubcategoryId = dto.subcategoryId,
                    CityId = dto.cityId,
                    Miliage = dto.miliage,
                    EngineCapacity = dto.engineCapacity,
                    ReleaseYear = dto.releaseYear,
                    IsNew = dto.isNew,
                    FuelTypeId = dto.fuelTypeId,
                    AutoColorId = dto.autoColorId,
                    TransmissionTypeId = dto.transmissionTypeId,
                    AutoModelId = dto.autoModelId
                };

                var result = await _mediator.Send(command);
                var locationUri = new Uri($"{Request.Scheme}://{Request.Host.ToUriComponent()}/autos/{result.Id}");
                return Created(locationUri, result);
            }
            catch (ArgumentException ex)
            {
                return BadRequest("Invalid parameters: " + ex.Message);
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while processing the request: " + ex.Message);
            }
        }

        [HttpDelete("autos")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> RemoveAutoAsync([FromRoute] RemoveAutoAttributeCommandDTO dto)
        {
            try
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                var command = new RemoveAutoAttributeCommand()
                {
                    UserId = userId,
                    AutoAttributeId = dto.autoAttributeId
                };

                var result = await _mediator.Send(command);
                return NoContent();
            }
            catch (NoAccessException ex)
            {
                return Forbid(ex.Message);
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while processing the request: " + ex.Message);
            }
        }

        [HttpPut("autos")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateAutoAsync([FromQuery] UpdateAutoAttributeCommandDTO dto)
        {
            try
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                var command = new UpdateAutoAttributeCommand()
                {
                    UserId = userId,
                    Title = dto.title,
                    Description = dto.description,
                    Price = dto.price,
                    CurrencyId = dto.currencyId,
                    CategoryId = dto.categoryId,
                    SubcategoryId = dto.subcategoryId,
                    CityId = dto.cityId,
                    ProductId = dto.productId,
                    Miliage = dto.miliage,
                    EngineCapacity = dto.engineCapacity,
                    ReleaseYear = dto.releaseYear,
                    IsNew = dto.isNew,
                    FuelTypeId = dto.fuelTypeId,
                    AutoColorId = dto.autoColorId,
                    TransmissionTypeId = dto.transmissionTypeId,
                    AutoModelId = dto.autoModelId
                };

                var result = await _mediator.Send(command);
                return NoContent();
            }
            catch (NoAccessException ex)
            {
                return Forbid(ex.Message);
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while processing the request: " + ex.Message);
            }
        }

        [HttpGet("autos")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllAutosAsync([FromQuery] GetAllAutoAttributesQueryDTO dto)
        {
            try
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                var query = new GetAllAutoAttributesQuery()
                {
                    UserId = userId,
                    Page = dto.page,
                    PageSize = dto.pageSize,
                    Title = dto.title,
                    CurrencyId = dto.currencyId,
                    PriceMin = dto.priceMin,
                    PriceMax = dto.priceMax,
                    CategoryId = dto.categoryId,
                    SubcategoryId = dto.subcategoryId,
                    CityId = dto.cityId,
                    OtherUserId = dto.otherUserId,
                    SortByPrice = dto.sortByPrice,
                    ReverseSort = dto.reverseSort,
                    MiliageMin = dto.miliageMin,
                    MiliageMax = dto.miliageMax,
                    EngineCapacityMin = dto.engineCapacityMin,
                    EngineCapacityMax = dto.engineCapacityMax,
                    ReleaseYearOlder = dto.releaseYearOlder,
                    ReleaseYearNewer = dto.releaseYearNewer,
                    IsNew = dto.isNew,
                    FuelTypesId = dto.fuelTypesId,
                    AutoColorsId = dto.autoColorsId,
                    TransmissionTypesId = dto.transmissionTypesId,
                    AutoBrandsId = dto.autoBrandsId,
                    AutoTypesId = dto.autoTypesId
                };
                var result = await _mediator.Send(query);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while processing the request: " + ex.Message);
            }
        }

        [HttpGet("autos/{productId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetByIdAutoAsync([FromRoute] GetByIdProductQueryDTO dto)
        {
            try
            {
                var query = new GetByIdAutoAttributeQuery()
                {
                    ProductId = dto.productId
                };

                var result = await _mediator.Send(query);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while processing the request: " + ex.Message);
            }
        }


        /// <summary>
        /// Clothes Attribute
        /// </summary>
        [HttpPost("clothes")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> AddClothesAsync(AddClothesAttributeCommandDTO dto)
        {
            try
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                var command = new AddClothesAttributeCommand()
                {
                    UserId = userId,
                    Title = dto.title,
                    Description = dto.description,
                    Price = dto.price,
                    CurrencyId = dto.currencyId,
                    CategoryId = dto.categoryId,
                    SubcategoryId = dto.subcategoryId,
                    CityId = dto.cityId,
                    IsNew = dto.isNew,
                    ClothesSeasonId = dto.clothesSeasonId,
                    ClothesSizeId = dto.clothesSizeId,
                    ClothesBrandViewId = dto.clothesBrandViewId
                };

                var result = await _mediator.Send(command);
                var locationUri = new Uri($"{Request.Scheme}://{Request.Host.ToUriComponent()}/clothes/{result.Id}");
                return Created(locationUri, result);
            }
            catch (ArgumentException ex)
            {
                return BadRequest("Invalid parameters: " + ex.Message);
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while processing the request: " + ex.Message);
            }
        }

        [HttpDelete("clothes")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> RemoveClothesAsync([FromRoute] RemoveClothesAttributeCommandDTO dto)
        {
            try
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                var command = new RemoveClothesAttributeCommand()
                {
                    UserId = userId,
                    ClothesAttributeId = dto.clothesAttributeId
                };

                var result = await _mediator.Send(command);
                return NoContent();
            }
            catch (NoAccessException ex)
            {
                return Forbid(ex.Message);
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while processing the request: " + ex.Message);
            }
        }

        [HttpPut("clothes")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateClothesAsync([FromQuery] UpdateClothesAttributeCommandDTO dto)
        {
            try
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                var command = new UpdateClothesAttributeCommand()
                {
                    UserId = userId,
                    Title = dto.title,
                    Description = dto.description,
                    Price = dto.price,
                    CurrencyId = dto.currencyId,
                    CategoryId = dto.categoryId,
                    SubcategoryId = dto.subcategoryId,
                    CityId = dto.cityId,
                    ProductId = dto.productId,
                    IsNew = dto.isNew,
                    ClothesSeasonId = dto.clothesSeasonId,
                    ClothesSizeId = dto.clothesSizeId,
                    ClothesBrandViewId = dto.clothesBrandViewId
                };

                var result = await _mediator.Send(command);
                return NoContent();
            }
            catch (NoAccessException ex)
            {
                return Forbid(ex.Message);
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while processing the request: " + ex.Message);
            }
        }

        [HttpGet("clothes")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllClothesAsync([FromQuery] GetAllClothesAttributesQueryDTO dto)
        {
            try
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                var query = new GetAllClothesAttributesQuery()
                {
                    UserId = userId,
                    Page = dto.page,
                    PageSize = dto.pageSize,
                    Title = dto.title,
                    CurrencyId = dto.currencyId,
                    PriceMin = dto.priceMin,
                    PriceMax = dto.priceMax,
                    CategoryId = dto.categoryId,
                    SubcategoryId = dto.subcategoryId,
                    CityId = dto.cityId,
                    OtherUserId = dto.otherUserId,
                    SortByPrice = dto.sortByPrice,
                    ReverseSort = dto.reverseSort,
                    IsNew = dto.isNew,
                    ClothesSeasonsId = dto.clothesSeasonsId,
                    ClothesSizesId = dto.clothesSizesId,
                    ClothesBrandsId = dto.clothesBrandsId,
                    ClothesViewsId = dto.clothesViewsId,
                    ClothesTypesId = dto.clothesTypesId,
                    ClothesGendersId = dto.clothesGendersId
                };

                var result = await _mediator.Send(query);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while processing the request: " + ex.Message);
            }
        }

        [HttpGet("clothes/{productId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetByIdClothesAsync([FromRoute] GetByIdProductQueryDTO dto)
        {
            try
            {
                var query = new GetByIdClothesAttributeQuery()
                {
                    ProductId = dto.productId
                };

                var result = await _mediator.Send(query);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while processing the request: " + ex.Message);
            }
        }


        /// <summary>
        /// Electronics Attribute
        /// </summary>
        [HttpPost("electronics")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> AddElectronicAsync(AddElectronicAttributeCommandDTO dto)
        {
            try
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                var command = new AddElectronicAttributeCommand()
                {
                    UserId = userId,
                    Title = dto.title,
                    Description = dto.description,
                    Price = dto.price,
                    CurrencyId = dto.currencyId,
                    CategoryId = dto.categoryId,
                    SubcategoryId = dto.subcategoryId,
                    CityId = dto.cityId,
                    IsNew = dto.isNew,
                    MemoryModelId = dto.memoryModelId,
                    ModelColorId = dto.modelColorId
                };

                var result = await _mediator.Send(command);
                var locationUri = new Uri($"{Request.Scheme}://{Request.Host.ToUriComponent()}/electronics/{result.Id}");
                return Created(locationUri, result);
            }
            catch (ArgumentException ex)
            {
                return BadRequest("Invalid parameters: " + ex.Message);
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while processing the request: " + ex.Message);
            }
        }

        [HttpDelete("electronics")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> RemoveElectronicAsync([FromRoute] RemoveElectronicAttributeCommandDTO dto)
        {
            try
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                var command = new RemoveElectronicAttributeCommand()
                {
                    UserId = userId,
                    ElectronicAttributeId = dto.electronicAttributeId
                };

                var result = await _mediator.Send(command);
                return NoContent();
            }
            catch (NoAccessException ex)
            {
                return Forbid(ex.Message);
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while processing the request: " + ex.Message);
            }
        }

        [HttpPut("electronics")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateElectronicAsync([FromQuery] UpdateElectronicAttributeCommandDTO dto)
        {
            try
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                var command = new UpdateElectronicAttributeCommand()
                {
                    UserId = userId,
                    Title = dto.title,
                    Description = dto.description,
                    Price = dto.price,
                    CurrencyId = dto.currencyId,
                    CategoryId = dto.categoryId,
                    SubcategoryId = dto.subcategoryId,
                    CityId = dto.cityId,
                    ProductId = dto.productId,
                    IsNew = dto.isNew,
                    MemoryModelId = dto.memoryModelId,
                    ModelColorId = dto.modelColorId
                };

                var result = await _mediator.Send(command);
                return NoContent();
            }
            catch (NoAccessException ex)
            {
                return Forbid(ex.Message);
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while processing the request: " + ex.Message);
            }
        }

        [HttpGet("electronics")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllElectronicsAsync([FromQuery] GetAllElectronicAttributesQueryDTO dto)
        {
            try
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                var query = new GetAllElectronicAttributesQuery()
                {
                    UserId = userId,
                    Page = dto.page,
                    PageSize = dto.pageSize,
                    Title = dto.title,
                    CurrencyId = dto.currencyId,
                    PriceMin = dto.priceMin,
                    PriceMax = dto.priceMax,
                    CategoryId = dto.categoryId,
                    SubcategoryId = dto.subcategoryId,
                    CityId = dto.cityId,
                    OtherUserId = dto.otherUserId,
                    SortByPrice = dto.sortByPrice,
                    ReverseSort = dto.reverseSort,
                    IsNew = dto.isNew,
                    MemoriesId = dto.memoriesId,
                    ColorsId = dto.colorsId,
                    ModelsId = dto.modelsId,
                    BrandsId = dto.brandsId,
                    TypesId = dto.typesId,
                };

                var result = await _mediator.Send(query);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while processing the request: " + ex.Message);
            }
        }

        [HttpGet("electronics/{productId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetByIdElectronicAsync([FromRoute] GetByIdProductQueryDTO dto)
        {
            try
            {
                var query = new GetByIdElectronicAttributeQuery()
                {
                    ProductId = dto.productId
                };

                var result = await _mediator.Send(query);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while processing the request: " + ex.Message);
            }
        }


        /// <summary>
        /// Items Attribute
        /// </summary>
        [HttpPost("items")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> AddItemAsync(AddItemAttributeCommandDTO dto)
        {
            try
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                var command = new AddItemAttributeCommand()
                {
                    UserId = userId,
                    Title = dto.title,
                    Description = dto.description,
                    Price = dto.price,
                    CurrencyId = dto.currencyId,
                    CategoryId = dto.categoryId,
                    SubcategoryId = dto.subcategoryId,
                    CityId = dto.cityId,
                    IsNew = dto.isNew,
                    ItemTypeId = dto.itemTypeId
                };

                var result = await _mediator.Send(command);
                var locationUri = new Uri($"{Request.Scheme}://{Request.Host.ToUriComponent()}/items/{result.Id}");
                return Created(locationUri, result);
            }
            catch (ArgumentException ex)
            {
                return BadRequest("Invalid parameters: " + ex.Message);
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while processing the request: " + ex.Message);
            }
        }

        [HttpDelete("items")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> RemoveItemAsync([FromRoute] RemoveItemAttributeCommandDTO dto)
        {
            try
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                var command = new RemoveItemAttributeCommand()
                {
                    UserId = userId,
                    ItemAttributeId = dto.itemAttributeId
                };

                var result = await _mediator.Send(command);
                return NoContent();
            }
            catch (NoAccessException ex)
            {
                return Forbid(ex.Message);
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while processing the request: " + ex.Message);
            }
        }

        [HttpPut("items")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateItemAsync([FromQuery] UpdateItemAttributeCommandDTO dto)
        {
            try
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                var command = new UpdateItemAttributeCommand()
                {
                    UserId = userId,
                    Title = dto.title,
                    Description = dto.description,
                    Price = dto.price,
                    CurrencyId = dto.currencyId,
                    CategoryId = dto.categoryId,
                    SubcategoryId = dto.subcategoryId,
                    CityId = dto.cityId,
                    ProductId = dto.productId,
                    IsNew = dto.isNew,
                    ItemTypeId = dto.itemTypeId
                };

                var result = await _mediator.Send(command);
                return NoContent();
            }
            catch (NoAccessException ex)
            {
                return Forbid(ex.Message);
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while processing the request: " + ex.Message);
            }
        }

        [HttpGet("items")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllItemsAsync([FromQuery] GetAllItemAttributesQueryDTO dto)
        {
            try
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                var query = new GetAllItemAttributesQuery()
                {
                    UserId = userId,
                    Page = dto.page,
                    PageSize = dto.pageSize,
                    Title = dto.title,
                    CurrencyId = dto.currencyId,
                    PriceMin = dto.priceMin,
                    PriceMax = dto.priceMax,
                    CategoryId = dto.categoryId,
                    SubcategoryId = dto.subcategoryId,
                    CityId = dto.cityId,
                    OtherUserId = dto.otherUserId,
                    SortByPrice = dto.sortByPrice,
                    ReverseSort = dto.reverseSort,
                    IsNew = dto.isNew,
                    ItemTypesId = dto.itemTypesId,
                };

                var result = await _mediator.Send(query);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while processing the request: " + ex.Message);
            }
        }

        [HttpGet("items/{productId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetByIdItemAsync([FromRoute] GetByIdProductQueryDTO dto)
        {
            try
            {
                var query = new GetByIdItemAttributeQuery()
                {
                    ProductId = dto.productId
                };

                var result = await _mediator.Send(query);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while processing the request: " + ex.Message);
            }
        }


        /// <summary>
        /// Real-Estates Attribute
        /// </summary>
        [HttpPost("real-estates")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> AddRealEstatesAsync(AddRealEstateAttributeCommandDTO dto)
        {
            try
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                var command = new AddRealEstateAttributeCommand()
                {
                    UserId = userId,
                    Title = dto.title,
                    Description = dto.description,
                    Price = dto.price,
                    CurrencyId = dto.currencyId,
                    CategoryId = dto.categoryId,
                    SubcategoryId = dto.subcategoryId,
                    CityId = dto.cityId,
                    Area = dto.area,
                    Rooms = dto.rooms,
                    IsRent = dto.isRent,
                    RealEstateTypeId = dto.realEstateTypeId
                };

                var result = await _mediator.Send(command);
                var locationUri = new Uri($"{Request.Scheme}://{Request.Host.ToUriComponent()}/real-estates/{result.Id}");
                return Created(locationUri, result);
            }
            catch (ArgumentException ex)
            {
                return BadRequest("Invalid parameters: " + ex.Message);
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while processing the request: " + ex.Message);
            }
        }

        [HttpDelete("real-estates")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> RemoveRealEstateAsync([FromRoute] RemoveRealEstateAttributeCommandDTO dto)
        {
            try
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                var command = new RemoveRealEstateAttributeCommand()
                {
                    UserId = userId,
                    RealEstateAttributeId = dto.realEstateAttributeId
                };

                var result = await _mediator.Send(command);
                return NoContent();
            }
            catch (NoAccessException ex)
            {
                return Forbid(ex.Message);
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while processing the request: " + ex.Message);
            }
        }

        [HttpPut("real-estates")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateRealEstateAsync([FromQuery] UpdateRealEstateAttributeCommandDTO dto)
        {
            try
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                var command = new UpdateRealEstateAttributeCommand()
                {
                    UserId = userId,
                    Title = dto.title,
                    Description = dto.description,
                    Price = dto.price,
                    CurrencyId = dto.currencyId,
                    CategoryId = dto.categoryId,
                    SubcategoryId = dto.subcategoryId,
                    CityId = dto.cityId,
                    ProductId = dto.productId,
                    Area = dto.area,
                    Rooms = dto.rooms,
                    IsRent = dto.isRent,
                    RealEstateTypeId = dto.realEstateTypeId
                };

                var result = await _mediator.Send(command);
                return NoContent();
            }
            catch (NoAccessException ex)
            {
                return Forbid(ex.Message);
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while processing the request: " + ex.Message);
            }
        }

        [HttpGet("real-estates")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllRealEstatesAsync([FromQuery] GetAllRealEstateAttributesQueryDTO dto)
        {
            try
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                var query = new GetAllRealEstateAttributesQuery()
                {
                    UserId = userId,
                    Page = dto.page,
                    PageSize = dto.pageSize,
                    Title = dto.title,
                    CurrencyId = dto.currencyId,
                    PriceMin = dto.priceMin,
                    PriceMax = dto.priceMax,
                    CategoryId = dto.categoryId,
                    SubcategoryId = dto.subcategoryId,
                    CityId = dto.cityId,
                    OtherUserId = dto.otherUserId,
                    SortByPrice = dto.sortByPrice,
                    ReverseSort = dto.reverseSort,
                    AreaMin = dto.areaMin,
                    AreaMax = dto.areaMax,
                    RoomsMin = dto.roomsMin,
                    RoomsMax = dto.roomsMax,
                    IsRent = dto.isRent,
                    RealEstateTypesId = dto.realEstateTypesId
                };

                var result = await _mediator.Send(query);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while processing the request: " + ex.Message);
            }
        }

        [HttpGet("real-estates/{productId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetByIdRealEstateAsync([FromRoute] GetByIdProductQueryDTO dto)
        {
            try
            {
                var query = new GetByIdRealEstateAttributeQuery()
                {
                    ProductId = dto.productId
                };

                var result = await _mediator.Send(query);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while processing the request: " + ex.Message);
            }
        }


        /// <summary>
        /// Tvs Attribute
        /// </summary>
        [HttpPost("tvs")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> AddTVAsync(AddTVAttributeCommandDTO dto)
        {
            try
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                var command = new AddTVAttributeCommand()
                {
                    UserId = userId,
                    Title = dto.title,
                    Description = dto.description,
                    Price = dto.price,
                    CurrencyId = dto.currencyId,
                    CategoryId = dto.categoryId,
                    SubcategoryId = dto.subcategoryId,
                    CityId = dto.cityId,
                    IsNew = dto.isNew,
                    IsSmart = dto.isSmart,
                    TVTypeId = dto.tvTypeId,
                    TVBrandId = dto.tvBrandId,
                    ScreenResolutionId = dto.screenResolutionId,
                    ScreenDiagonalId = dto.screenDiagonalId,
                };

                var result = await _mediator.Send(command);
                var locationUri = new Uri($"{Request.Scheme}://{Request.Host.ToUriComponent()}/tvs/{result.Id}");
                return Created(locationUri, result);
            }
            catch (ArgumentException ex)
            {
                return BadRequest("Invalid parameters: " + ex.Message);
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while processing the request: " + ex.Message);
            }
        }

        [HttpDelete("tvs")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> RemoveTVAsync([FromRoute] RemoveTVAttributeCommandDTO dto)
        {
            try
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                var command = new RemoveTVAttributeCommand()
                {
                    UserId = userId,
                    TVAttributeId = dto.tvAttributeId
                };

                var result = await _mediator.Send(command);
                return NoContent();
            }
            catch (NoAccessException ex)
            {
                return Forbid(ex.Message);
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while processing the request: " + ex.Message);
            }
        }

        [HttpPut("tvs")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateTVAsync([FromQuery] UpdateTVAttributeCommandDTO dto)
        {
            try
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                var command = new UpdateTVAttributeCommand()
                {
                    UserId = userId,
                    Title = dto.title,
                    Description = dto.description,
                    Price = dto.price,
                    CurrencyId = dto.currencyId,
                    CategoryId = dto.categoryId,
                    SubcategoryId = dto.subcategoryId,
                    CityId = dto.cityId,
                    ProductId = dto.productId,
                    IsNew = dto.isNew,
                    IsSmart = dto.isSmart,
                    TVTypeId = dto.tvTypeId,
                    TVBrandId = dto.tvBrandId,
                    ScreenResolutionId = dto.screenResolutionId,
                    ScreenDiagonalId = dto.screenDiagonalId
                };

                var result = await _mediator.Send(command);
                return NoContent();
            }
            catch (NoAccessException ex)
            {
                return Forbid(ex.Message);
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while processing the request: " + ex.Message);
            }
        }

        [HttpGet("tvs")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllTVsAsync([FromQuery] GetAllTVAttributesQueryDTO dto)
        {
            try
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                var query = new GetAllTVAttributesQuery()
                {
                    UserId = userId,
                    Page = dto.page,
                    PageSize = dto.pageSize,
                    Title = dto.title,
                    CurrencyId = dto.currencyId,
                    PriceMin = dto.priceMin,
                    PriceMax = dto.priceMax,
                    CategoryId = dto.categoryId,
                    SubcategoryId = dto.subcategoryId,
                    CityId = dto.cityId,
                    OtherUserId = dto.otherUserId,
                    SortByPrice = dto.sortByPrice,
                    ReverseSort = dto.reverseSort,
                    IsNew = dto.isNew,
                    IsSmart = dto.isSmart,
                    TVTypesId = dto.tvTypesId,
                    TVBrandsId = dto.tvBrandsId,
                    ScreenResolutionsId = dto.screenResolutionsId,
                    ScreenDiagonalsId = dto.screenDiagonalsId,
                };

                var result = await _mediator.Send(query);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while processing the request: " + ex.Message);
            }
        }

        [HttpGet("tvs/{productId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetByIdTVAsync([FromRoute] GetByIdProductQueryDTO dto)
        {
            try
            {
                var query = new GetByIdTVAttributeQuery()
                {
                    ProductId = dto.productId
                };

                var result = await _mediator.Send(query);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while processing the request: " + ex.Message);
            }
        }


        /// <summary>
        /// Favorite-Products Attribute
        /// </summary>
        [HttpPost("favorite-products")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> AddFavoriteProductAsync(AddFavoriteProductCommandDTO dto)
        {
            try
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                var command = new AddFavoriteProductCommand()
                {
                    UserId = userId,
                    ProductId = dto.productId,
                };

                var result = await _mediator.Send(command);
                var locationUri = new Uri($"{Request.Scheme}://{Request.Host.ToUriComponent()}/favorite-products/{result.Id}");
                return Created(locationUri, result);
            }
            catch (ArgumentException ex)
            {
                return BadRequest("Invalid parameters: " + ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while processing the request: " + ex.Message);
            }
        }

        [HttpDelete("favorite-products")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> RemoveFavoriteProductAsync([FromRoute] RemoveFavoriteProductCommandDTO dto)
        {
            try
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                var command = new RemoveFavoriteProductCommand()
                {
                    UserId = userId,
                    ProductId = dto.productId
                };

                var result = await _mediator.Send(command);
                return NoContent();
            }
            catch (NoAccessException ex)
            {
                return Forbid(ex.Message);
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while processing the request: " + ex.Message);
            }
        }

        [HttpGet("favorite-products")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllFavoriteProductsAsync([FromQuery] GetAllFavoriteProductsQueryDTO dto)
        {
            try
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                var query = new GetAllFavoriteProductsQuery()
                {
                    UserId = userId,
                    Page = dto.page,
                    PageSize = dto.pageSize,
                    Title = dto.title,
                    CurrencyId = dto.currencyId,
                    PriceMin = dto.priceMin,
                    PriceMax = dto.priceMax,
                    CategoryId = dto.categoryId,
                    SubcategoryId = dto.subcategoryId,
                    CityId = dto.cityId,
                    OtherUserId = dto.otherUserId,
                    SortByPrice = dto.sortByPrice,
                    ReverseSort = dto.reverseSort,
                    ProductId = dto.productId
                };

                var result = await _mediator.Send(query);
                return Ok(result);
            }
            catch (ArgumentException ex)
            {
                return BadRequest("Invalid parameters: " + ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while processing the request: " + ex.Message);
            }
        }


        /// <summary>
        /// Products Attribute
        /// </summary>
        [HttpGet("products")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllProductsAsync([FromQuery] GetAllProductsQueryDTO dto)
        {
            try
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                var query = new GetAllProductsQuery()
                {
                    UserId = userId,
                    Page = dto.page,
                    PageSize = dto.pageSize,
                    Title = dto.title,
                    CurrencyId = dto.currencyId,
                    PriceMin = dto.priceMin,
                    PriceMax = dto.priceMax,
                    CategoryId = dto.categoryId,
                    SubcategoryId = dto.subcategoryId,
                    CityId = dto.cityId,
                    OtherUserId = dto.otherUserId,
                    SortByPrice = dto.sortByPrice,
                    ReverseSort = dto.reverseSort
                };

                var result = await _mediator.Send(query);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while processing the request: " + ex.Message);
            }
        }

        [HttpDelete("products")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> RemoveProductAsync([FromRoute] RemoveProductCommandDTO dto)
        {
            try
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                var command = new RemoveProductCommand()
                {
                    UserId = userId,
                    ProductId = dto.productId
                };

                var result = await _mediator.Send(command);
                return NoContent();
            }
            catch (NoAccessException ex)
            {
                return Forbid(ex.Message);
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while processing the request: " + ex.Message);
            }
        }

        [HttpPatch("products")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> IncrementViewsAsync([FromRoute] IncrementProductViewsCommandDTO dto)
        {
            try
            {
                var command = new IncrementProductViewsCommand()
                {
                    ProductId = dto.productId
                };

                var result = await _mediator.Send(command);
                return NoContent();
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while processing the request: " + ex.Message);
            }
        }

        [HttpGet("cities")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllCitiesAsync()
        {
            try
            {
                GetAllCitiesQuery query = new GetAllCitiesQuery();

                var result = await _mediator.Send(query);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while processing the request: " + ex.Message);
            }
        }

        [HttpGet("currencies")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllCurrenciesAsync()
        {
            try
            {
                GetAllCurrenciesQuery query = new GetAllCurrenciesQuery();

                var result = await _mediator.Send(query);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while processing the request: " + ex.Message);
            }
        }

        [HttpGet("autos/fuel-types")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllFuelTypesAsync()
        {
            try
            {
                GetAllFuelTypesQuery query = new GetAllFuelTypesQuery();

                var result = await _mediator.Send(query);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while processing the request: " + ex.Message);
            }
        }

        [HttpGet("colors")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllColorsAsync([FromRoute] GetAllColorsQueryDTO dto)
        {
            try
            {
                var query = new GetAllColorsQuery()
                {
                    ModelId = dto.modelId
                };

                var result = await _mediator.Send(query);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while processing the request: " + ex.Message);
            }
        }

        [HttpGet("autos/transmission-types")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllTransmissionTypesAsync()
        {
            try
            {
                GetAllTransmissionTypesQuery query = new GetAllTransmissionTypesQuery();

                var result = await _mediator.Send(query);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while processing the request: " + ex.Message);
            }
        }

        [HttpGet("autos/brands")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllAutoBrandsAsync([FromRoute] GetAllAutoBrandsQueryDTO dto)
        {
            try
            {
                var query = new GetAllAutoBrandsQuery()
                {
                    AutoTypesId = dto.autoTypesId
                };

                var result = await _mediator.Send(query);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while processing the request: " + ex.Message);
            }
        }

        [HttpGet("tvs/brands")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllTVBrandsAsync()
        {
            try
            {
                GetAllTVBrandsQuery query = new GetAllTVBrandsQuery();

                var result = await _mediator.Send(query);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while processing the request: " + ex.Message);
            }
        }

        [HttpGet("tvs/screen-resolutions")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllScreenResolutionsAsync()
        {
            try
            {
                GetAllScreenResolutionsQuery query = new GetAllScreenResolutionsQuery();

                var result = await _mediator.Send(query);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while processing the request: " + ex.Message);
            }
        }

        [HttpGet("tvs/screen-diagonals")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllScreenDiagonalsAsync()
        {
            try
            {
                GetAllScreenDiagonalsQuery query = new GetAllScreenDiagonalsQuery();

                var result = await _mediator.Send(query);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while processing the request: " + ex.Message);
            }
        }

        [HttpGet("tvs/types")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllTVTypesAsync()
        {
            try
            {
                GetAllTVTypesQuery query = new GetAllTVTypesQuery();

                var result = await _mediator.Send(query);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while processing the request: " + ex.Message);
            }
        }

        [HttpGet("animals/breeds")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllAnimalBreedsAsync([FromRoute] GetAllAnimalBreedsQueryDTO dto)
        {
            try
            {
                var query = new GetAllAnimalBreedsQuery()
                {
                    AnimalTypesId = dto.animalTypesId
                };

                var result = await _mediator.Send(query);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while processing the request: " + ex.Message);
            }
        }

        [HttpGet("clothes/sizes")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllClothesSizesAsync(GetAllClothesSizesQueryDTO dto)
        {
            try
            {
                GetAllClothesSizesQuery query = new GetAllClothesSizesQuery()
                {
                    IsChild = dto.IsChild,
                    IsShoe = dto.IsShoe
                };

                var result = await _mediator.Send(query);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while processing the request: " + ex.Message);
            }
        }

        [HttpGet("clothes/seasons")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllClothesSeasonsAsync()
        {
            try
            {
                GetAllClothesSeasonsQuery query = new GetAllClothesSeasonsQuery();

                var result = await _mediator.Send(query);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while processing the request: " + ex.Message);
            }
        }

        [HttpGet("clothes/brands")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllClothesBrandsAsync([FromRoute] GetAllClothesBrandsQueryDTO dto)
        {
            try
            {
                var query = new GetAllClothesBrandsQuery()
                {
                    ClothesViewsId = dto.clothesViewsId
                };

                var result = await _mediator.Send(query);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while processing the request: " + ex.Message);
            }
        }

        [HttpGet("clothes/views")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllClothesViewsAsync([FromQuery] GetAllClothesViewsQueryDTO dto)
        {
            try
            {
                var query = new GetAllClothesViewsQuery()
                {
                    GenderId = dto.genderId,
                    ClothesTypeId = dto.clothesTypeId
                };

                var result = await _mediator.Send(query);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while processing the request: " + ex.Message);
            }
        }

        [HttpGet("clothes/genders")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllGendersAsync()
        {
            try
            {
                GetAllGendersQuery query = new GetAllGendersQuery();

                var result = await _mediator.Send(query);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while processing the request: " + ex.Message);
            }
        }

        [HttpGet("electronics/memories")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllMemoriesAsync([FromRoute] GetAllMemoriesQueryDTO dto)
        {
            try
            {
                var query = new GetAllMemoriesQuery()
                {
                    ModelId = dto.modelId
                };

                var result = await _mediator.Send(query);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while processing the request: " + ex.Message);
            }
        }

        [HttpGet("electronics/models")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllModelsAsync([FromQuery] GetAllModelsQueryDTO dto)
        {
            try
            {
                var query = new GetAllModelsQuery()
                {
                    ElectronicBrandsId = dto.electronicBrandsId,
                    ElectronicTypeId = dto.electronicTypeId
                };

                var result = await _mediator.Send(query);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while processing the request: " + ex.Message);
            }
        }

        [HttpGet("electronics/brands")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllElectronicBrandsAsync([FromRoute] GetAllElectronicBrandsQueryDTO dto)
        {
            try
            {
                var query = new GetAllElectronicBrandsQuery()
                {
                    ElectronicTypeId = dto.electronicTypeId
                };

                var result = await _mediator.Send(query);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while processing the request: " + ex.Message);
            }
        }

        [HttpGet("autos/models")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllAutoModelsAsync([FromQuery] GetAllAutoModelsQueryDTO dto)
        {
            try
            {
                var query = new GetAllAutoModelsQuery()
                {
                    AutoBrandsId = dto.autoBrandsId,
                    AutoTypesId = dto.autoTypesId
                };

                var result = await _mediator.Send(query);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while processing the request: " + ex.Message);
            }
        }

        [HttpGet("clothes/types")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllClothesTypesAsync([FromRoute] GetAllClothesTypesQueryDTO dto)
        {
            try
            {
                var query = new GetAllClothesTypesQuery()
                {
                    GenderId = dto.genderId
                };

                var result = await _mediator.Send(query);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while processing the request: " + ex.Message);
            }
        }

        [HttpGet("animals/types")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllAnimalTypesAsync()
        {
            try
            {
                var query = new GetAllAnimalTypesQuery();

                var result = await _mediator.Send(query);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while processing the request: " + ex.Message);
            }
        }

        [HttpGet("autos/types")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllAutoTypesAsync()
        {
            try
            {
                var query = new GetAllAutoTypesQuery();

                var result = await _mediator.Send(query);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while processing the request: " + ex.Message);
            }
        }

        [HttpGet("electronics/types")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllElectronicTypesAsync()
        {
            try
            {
                var query = new GetAllElectronicTypesQuery();

                var result = await _mediator.Send(query);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while processing the request: " + ex.Message);
            }
        }

        [HttpGet("items/types")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllItemTypesAsync()
        {
            try
            {
                var query = new GetAllItemTypesQuery();

                var result = await _mediator.Send(query);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while processing the request: " + ex.Message);
            }
        }

        [HttpGet("real-estates/types")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllRealEstateTypesAsync()
        {
            try
            {
                var query = new GetAllRealEstateTypesQuery();

                var result = await _mediator.Send(query);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while processing the request: " + ex.Message);
            }
        }

        [HttpHead]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Head()
        {
            return Ok();
        }

        [HttpOptions]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Options()
        {
            return Ok("x44 GET, x8 POST, x7 PUT, x9 DELETE, HEAD, OPTIONS");
        }
    }
}