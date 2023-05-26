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
        [HttpPost]
        [Route("animals")]
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
                    Title = dto.Title,
                    Description = dto.Description,
                    Price = dto.Price,
                    CurrencyId = dto.CurrencyId,
                    CategoryId = dto.CategoryId,
                    SubcategoryId = dto.SubcategoryId,
                    CityId = dto.CityId,
                    AnimalBreedId = dto.AnimalBreedId
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

        [HttpDelete]
        [Route("animals/{id}")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> RemoveAnimalAsync(RemoveAnimalAttributeCommandDTO dto)
        {
            try
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                var command = new RemoveAnimalAttributeCommand()
                {
                    UserId = userId,
                    AnimalAttributeId = dto.AnimalAttributeId
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

        [HttpPut]
        [Route("animals/{id}")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateAnimalAsync(UpdateAnimalAttributeCommandDTO dto)
        {
            try
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                var command = new UpdateAnimalAttributeCommand()
                {
                    UserId = userId,
                    Title = dto.Title,
                    Description = dto.Description,
                    Price = dto.Price,
                    CurrencyId = dto.CurrencyId,
                    CategoryId = dto.CategoryId,
                    SubcategoryId = dto.SubcategoryId,
                    CityId = dto.CityId,
                    AnimalAttributeId = dto.AnimalAttributeId,
                    AnimalBreedId = dto.AnimalBreedId
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

        [HttpGet]
        [Route("animals")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllAnimalsAsync(GetAllAnimalAttributesQueryDTO dto)
        {
            try
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                var query = new GetAllAnimalAttributesQuery()
                {
                    UserId = userId,
                    Page = dto.Page,
                    PageSize = dto.PageSize,
                    Title = dto.Title,
                    CurrencyId = dto.CurrencyId,
                    PriceMin = dto.PriceMin,
                    PriceMax = dto.PriceMax,
                    CategoryId = dto.CategoryId,
                    SubcategoryId = dto.SubcategoryId,
                    CityId = dto.CityId,
                    OtherUserId = dto.OtherUserId,
                    SortByPrice = dto.SortByPrice,
                    ReverseSort = dto.ReverseSort,
                    AnimalBreedsId = dto.AnimalBreedsId,
                    AnimalTypesId = dto.AnimalTypesId
                };

                var result = await _mediator.Send(query);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while processing the request: " + ex.Message);
            }
        }

        [HttpGet]
        [Route("animals/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetByIdAnimalAsync(GetByIdAnimalAttributeQueryDTO dto)
        {
            try
            {
                var query = new GetByIdAnimalAttributeQuery()
                {
                    AnimalAttributeId = dto.AnimalAttributeId
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
        [HttpPost]
        [Route("autos")]
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
                    Title = dto.Title,
                    Description = dto.Description,
                    Price = dto.Price,
                    CurrencyId = dto.CurrencyId,
                    CategoryId = dto.CategoryId,
                    SubcategoryId = dto.SubcategoryId,
                    CityId = dto.CityId,
                    Miliage = dto.Miliage,
                    EngineCapacity = dto.EngineCapacity,
                    ReleaseYear = dto.ReleaseYear,
                    IsNew = dto.IsNew,
                    FuelTypeId = dto.FuelTypeId,
                    AutoColorId = dto.AutoColorId,
                    TransmissionTypeId = dto.TransmissionTypeId,
                    AutoModelId = dto.AutoModelId
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

        [HttpDelete]
        [Route("autos/{id}")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> RemoveAutoAsync(RemoveAutoAttributeCommandDTO dto)
        {
            try
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                var command = new RemoveAutoAttributeCommand()
                {
                    UserId = userId,
                    AutoAttributeId = dto.AutoAttributeId
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

        [HttpPut]
        [Route("autos/{id}")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateAutoAsync(UpdateAutoAttributeCommandDTO dto)
        {
            try
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                var command = new UpdateAutoAttributeCommand()
                {
                    UserId = userId,
                    Title = dto.Title,
                    Description = dto.Description,
                    Price = dto.Price,
                    CurrencyId = dto.CurrencyId,
                    CategoryId = dto.CategoryId,
                    SubcategoryId = dto.SubcategoryId,
                    CityId = dto.CityId,
                    AutoAttributeId = dto.AutoAttributeId,
                    Miliage = dto.Miliage,
                    EngineCapacity = dto.EngineCapacity,
                    ReleaseYear = dto.ReleaseYear,
                    IsNew = dto.IsNew,
                    FuelTypeId = dto.FuelTypeId,
                    AutoColorId = dto.AutoColorId,
                    TransmissionTypeId = dto.TransmissionTypeId,
                    AutoModelId = dto.AutoModelId
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

        [HttpGet]
        [Route("autos")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllAutosAsync(GetAllAutoAttributesQueryDTO dto)
        {
            try
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                var query = new GetAllAutoAttributesQuery()
                {
                    UserId = userId,
                    Page = dto.Page,
                    PageSize = dto.PageSize,
                    Title = dto.Title,
                    CurrencyId = dto.CurrencyId,
                    PriceMin = dto.PriceMin,
                    PriceMax = dto.PriceMax,
                    CategoryId = dto.CategoryId,
                    SubcategoryId = dto.SubcategoryId,
                    CityId = dto.CityId,
                    OtherUserId = dto.OtherUserId,
                    SortByPrice = dto.SortByPrice,
                    ReverseSort = dto.ReverseSort,
                    MiliageMin = dto.MiliageMin,
                    MiliageMax = dto.MiliageMax,
                    EngineCapacityMin = dto.EngineCapacityMin,
                    EngineCapacityMax = dto.EngineCapacityMax,
                    ReleaseYearOlder = dto.ReleaseYearOlder,
                    ReleaseYearNewer = dto.ReleaseYearNewer,
                    IsNew = dto.IsNew,
                    FuelTypesId = dto.FuelTypesId,
                    AutoColorsId = dto.AutoColorsId,
                    TransmissionTypesId = dto.TransmissionTypesId,
                    AutoBrandsId = dto.AutoBrandsId,
                    AutoTypesId = dto.AutoTypesId
                };
                var result = await _mediator.Send(query);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while processing the request: " + ex.Message);
            }
        }

        [HttpGet]
        [Route("autos/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetByIdAutoAsync(GetByIdAutoAttributeQueryDTO dto)
        {
            try
            {
                var query = new GetByIdAutoAttributeQuery()
                {
                    AutoAttributeId = dto.AutoAttributeId
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
        [HttpPost]
        [Route("clothes")]
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
                    Title = dto.Title,
                    Description = dto.Description,
                    Price = dto.Price,
                    CurrencyId = dto.CurrencyId,
                    CategoryId = dto.CategoryId,
                    SubcategoryId = dto.SubcategoryId,
                    CityId = dto.CityId,
                    IsNew = dto.IsNew,
                    ClothesSeasonId = dto.ClothesSeasonId,
                    ClothesSizeId = dto.ClothesSizeId,
                    ClothesBrandViewId = dto.ClothesBrandViewId
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

        [HttpDelete]
        [Route("clothes/{id}")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> RemoveClothesAsync(RemoveClothesAttributeCommandDTO dto)
        {
            try
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                var command = new RemoveClothesAttributeCommand()
                {
                    UserId = userId,
                    ClothesAttributeId = dto.ClothesAttributeId
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

        [HttpPut]
        [Route("clothes/{id}")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateClothesAsync(UpdateClothesAttributeCommandDTO dto)
        {
            try
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                var command = new UpdateClothesAttributeCommand()
                {
                    UserId = userId,
                    Title = dto.Title,
                    Description = dto.Description,
                    Price = dto.Price,
                    CurrencyId = dto.CurrencyId,
                    CategoryId = dto.CategoryId,
                    SubcategoryId = dto.SubcategoryId,
                    CityId = dto.CityId,
                    ClothesAttributeId = dto.ClothesAttributeId,
                    IsNew = dto.IsNew,
                    ClothesSeasonId = dto.ClothesSeasonId,
                    ClothesSizeId = dto.ClothesSizeId,
                    ClothesBrandViewId = dto.ClothesBrandViewId
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

        [HttpGet]
        [Route("clothes/")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllClothesAsync(GetAllClothesAttributesQueryDTO dto)
        {
            try
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                var query = new GetAllClothesAttributesQuery()
                {
                    UserId = userId,
                    Page = dto.Page,
                    PageSize = dto.PageSize,
                    Title = dto.Title,
                    CurrencyId = dto.CurrencyId,
                    PriceMin = dto.PriceMin,
                    PriceMax = dto.PriceMax,
                    CategoryId = dto.CategoryId,
                    SubcategoryId = dto.SubcategoryId,
                    CityId = dto.CityId,
                    OtherUserId = dto.OtherUserId,
                    SortByPrice = dto.SortByPrice,
                    ReverseSort = dto.ReverseSort,
                    IsNew = dto.IsNew,
                    ClothesSeasonsId = dto.ClothesSeasonsId,
                    ClothesSizesId = dto.ClothesSizesId,
                    ClothesBrandsId = dto.ClothesBrandsId,
                    ClothesViewsId = dto.ClothesViewsId,
                    ClothesTypesId = dto.ClothesTypesId,
                    ClothesGendersId = dto.ClothesGendersId
                };

                var result = await _mediator.Send(query);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while processing the request: " + ex.Message);
            }
        }

        [HttpGet]
        [Route("clothes/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetByIdClothesAsync(GetByIdClothesAttributeQueryDTO dto)
        {
            try
            {
                var query = new GetByIdClothesAttributeQuery()
                {
                    ClothesAttributeId = dto.ClothesAttributeId
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
        [HttpPost]
        [Route("electronics")]
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
                    Title = dto.Title,
                    Description = dto.Description,
                    Price = dto.Price,
                    CurrencyId = dto.CurrencyId,
                    CategoryId = dto.CategoryId,
                    SubcategoryId = dto.SubcategoryId,
                    CityId = dto.CityId,
                    IsNew = dto.IsNew,
                    MemoryModelId = dto.MemoryModelId,
                    ModelColorId = dto.ModelColorId
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

        [HttpDelete]
        [Route("electronics/{id}")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> RemoveElectronicAsync(RemoveElectronicAttributeCommandDTO dto)
        {
            try
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                var command = new RemoveElectronicAttributeCommand()
                {
                    UserId = userId,
                    ElectronicAttributeId = dto.ElectronicAttributeId
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

        [HttpPut]
        [Route("electronics/{id}")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateElectronicAsync(UpdateElectronicAttributeCommandDTO dto)
        {
            try
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                var command = new UpdateElectronicAttributeCommand()
                {
                    UserId = userId,
                    Title = dto.Title,
                    Description = dto.Description,
                    Price = dto.Price,
                    CurrencyId = dto.CurrencyId,
                    CategoryId = dto.CategoryId,
                    SubcategoryId = dto.SubcategoryId,
                    CityId = dto.CityId,
                    ElectronicAttributeId = dto.ElectronicAttributeId,
                    IsNew = dto.IsNew,
                    MemoryModelId = dto.MemoryModelId,
                    ModelColorId = dto.ModelColorId
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

        [HttpGet]
        [Route("electronics")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllElectronicsAsync(GetAllElectronicAttributesQueryDTO dto)
        {
            try
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                var query = new GetAllElectronicAttributesQuery()
                {
                    UserId = userId,
                    Page = dto.Page,
                    PageSize = dto.PageSize,
                    Title = dto.Title,
                    CurrencyId = dto.CurrencyId,
                    PriceMin = dto.PriceMin,
                    PriceMax = dto.PriceMax,
                    CategoryId = dto.CategoryId,
                    SubcategoryId = dto.SubcategoryId,
                    CityId = dto.CityId,
                    OtherUserId = dto.OtherUserId,
                    SortByPrice = dto.SortByPrice,
                    ReverseSort = dto.ReverseSort,
                    IsNew = dto.IsNew,
                    MemoriesId = dto.MemoriesId,
                    ColorsId = dto.ColorsId,
                    ModelsId = dto.ModelsId,
                    BrandsId = dto.BrandsId,
                    TypesId = dto.TypesId,
                };

                var result = await _mediator.Send(query);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while processing the request: " + ex.Message);
            }
        }

        [HttpGet]
        [Route("electronics/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetByIdElectronicAsync(GetByIdElectronicAttributeQueryDTO dto)
        {
            try
            {
                var query = new GetByIdElectronicAttributeQuery()
                {
                    ElectronicAttributeId = dto.ElectronicAttributeId
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
        [HttpPost]
        [Route("items")]
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
                    Title = dto.Title,
                    Description = dto.Description,
                    Price = dto.Price,
                    CurrencyId = dto.CurrencyId,
                    CategoryId = dto.CategoryId,
                    SubcategoryId = dto.SubcategoryId,
                    CityId = dto.CityId,
                    IsNew = dto.IsNew,
                    ItemTypeId = dto.ItemTypeId
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

        [HttpDelete]
        [Route("items/{id}")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> RemoveItemAsync(RemoveItemAttributeCommandDTO dto)
        {
            try
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                var command = new RemoveItemAttributeCommand()
                {
                    UserId = userId,
                    ItemAttributeId = dto.ItemAttributeId
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

        [HttpPut]
        [Route("items/{id}")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateItemAsync(UpdateItemAttributeCommandDTO dto)
        {
            try
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                var command = new UpdateItemAttributeCommand()
                {
                    UserId = userId,
                    Title = dto.Title,
                    Description = dto.Description,
                    Price = dto.Price,
                    CurrencyId = dto.CurrencyId,
                    CategoryId = dto.CategoryId,
                    SubcategoryId = dto.SubcategoryId,
                    CityId = dto.CityId,
                    ItemAttributeId = dto.ItemAttributeId,
                    IsNew = dto.IsNew,
                    ItemTypeId = dto.ItemTypeId
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

        [HttpGet]
        [Route("items")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllItemsAsync(GetAllItemAttributesQueryDTO dto)
        {
            try
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                var query = new GetAllItemAttributesQuery()
                {
                    UserId = userId,
                    Page = dto.Page,
                    PageSize = dto.PageSize,
                    Title = dto.Title,
                    CurrencyId = dto.CurrencyId,
                    PriceMin = dto.PriceMin,
                    PriceMax = dto.PriceMax,
                    CategoryId = dto.CategoryId,
                    SubcategoryId = dto.SubcategoryId,
                    CityId = dto.CityId,
                    OtherUserId = dto.OtherUserId,
                    SortByPrice = dto.SortByPrice,
                    ReverseSort = dto.ReverseSort,
                    IsNew = dto.IsNew,
                    ItemTypesId = dto.ItemTypesId,
                };

                var result = await _mediator.Send(query);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while processing the request: " + ex.Message);
            }
        }

        [HttpGet]
        [Route("items/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetByIdItemAsync(GetByIdItemAttributeQueryDTO dto)
        {
            try
            {
                var query = new GetByIdItemAttributeQuery()
                {
                    ItemAttributeId = dto.ItemAttributeId
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
        [HttpPost]
        [Route("real-estates")]
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
                    Title = dto.Title,
                    Description = dto.Description,
                    Price = dto.Price,
                    CurrencyId = dto.CurrencyId,
                    CategoryId = dto.CategoryId,
                    SubcategoryId = dto.SubcategoryId,
                    CityId = dto.CityId,
                    Area = dto.Area,
                    Rooms = dto.Rooms,
                    IsRent = dto.IsRent,
                    RealEstateTypeId = dto.RealEstateTypeId
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

        [HttpDelete]
        [Route("real-estates/{id}")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> RemoveRealEstateAsync(RemoveRealEstateAttributeCommandDTO dto)
        {
            try
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                var command = new RemoveRealEstateAttributeCommand()
                {
                    UserId = userId,
                    RealEstateAttributeId = dto.RealEstateAttributeId
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

        [HttpPut]
        [Route("real-estates/{id}")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateRealEstateAsync(UpdateRealEstateAttributeCommandDTO dto)
        {
            try
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                var command = new UpdateRealEstateAttributeCommand()
                {
                    UserId = userId,
                    Title = dto.Title,
                    Description = dto.Description,
                    Price = dto.Price,
                    CurrencyId = dto.CurrencyId,
                    CategoryId = dto.CategoryId,
                    SubcategoryId = dto.SubcategoryId,
                    CityId = dto.CityId,
                    RealEstateAttributeId = dto.RealEstateAttributeId,
                    Area = dto.Area,
                    Rooms = dto.Rooms,
                    IsRent = dto.IsRent,
                    RealEstateTypeId = dto.RealEstateTypeId
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

        [HttpGet]
        [Route("real-estates")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllRealEstatesAsync(GetAllRealEstateAttributesQueryDTO dto)
        {
            try
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                var query = new GetAllRealEstateAttributesQuery()
                {
                    UserId = userId,
                    Page = dto.Page,
                    PageSize = dto.PageSize,
                    Title = dto.Title,
                    CurrencyId = dto.CurrencyId,
                    PriceMin = dto.PriceMin,
                    PriceMax = dto.PriceMax,
                    CategoryId = dto.CategoryId,
                    SubcategoryId = dto.SubcategoryId,
                    CityId = dto.CityId,
                    OtherUserId = dto.OtherUserId,
                    SortByPrice = dto.SortByPrice,
                    ReverseSort = dto.ReverseSort,
                    AreaMin = dto.AreaMin,
                    AreaMax = dto.AreaMax,
                    RoomsMin = dto.RoomsMin,
                    RoomsMax = dto.RoomsMax,
                    IsRent = dto.IsRent,
                    RealEstateTypesId = dto.RealEstateTypesId
                };

                var result = await _mediator.Send(query);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while processing the request: " + ex.Message);
            }
        }

        [HttpGet]
        [Route("real-estates/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetByIdRealEstateAsync(GetByIdRealEstateAttributeQueryDTO dto)
        {
            try
            {
                var query = new GetByIdRealEstateAttributeQuery()
                {
                    RealEstateAttributeId = dto.RealEstateAttributeId
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
        [HttpPost]
        [Route("tvs")]
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
                    Title = dto.Title,
                    Description = dto.Description,
                    Price = dto.Price,
                    CurrencyId = dto.CurrencyId,
                    CategoryId = dto.CategoryId,
                    SubcategoryId = dto.SubcategoryId,
                    CityId = dto.CityId,
                    IsNew = dto.IsNew,
                    IsSmart = dto.IsSmart,
                    TVTypeId = dto.TVTypeId,
                    TVBrandId = dto.TVBrandId,
                    ScreenResolutionId = dto.ScreenResolutionId,
                    ScreenDiagonalId = dto.ScreenDiagonalId,
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

        [HttpDelete]
        [Route("tvs/{id}")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> RemoveTVAsync(RemoveTVAttributeCommandDTO dto)
        {
            try
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                var command = new RemoveTVAttributeCommand()
                {
                    UserId = userId,
                    TVAttributeId = dto.TVAttributeId
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

        [HttpPut]
        [Route("tvs/{id}")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateTVAsync(UpdateTVAttributeCommandDTO dto)
        {
            try
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                var command = new UpdateTVAttributeCommand()
                {
                    UserId = userId,
                    Title = dto.Title,
                    Description = dto.Description,
                    Price = dto.Price,
                    CurrencyId = dto.CurrencyId,
                    CategoryId = dto.CategoryId,
                    SubcategoryId = dto.SubcategoryId,
                    CityId = dto.CityId,
                    TVAttributeId = dto.TVAttributeId,
                    IsNew = dto.IsNew,
                    IsSmart = dto.IsSmart,
                    TVTypeId = dto.TVTypeId,
                    TVBrandId = dto.TVTypeId,
                    ScreenResolutionId = dto.ScreenResolutionId,
                    ScreenDiagonalId = dto.ScreenDiagonalId
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

        [HttpGet]
        [Route("tvs")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllTVsAsync(GetAllTVAttributesQueryDTO dto)
        {
            try
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                var query = new GetAllTVAttributesQuery()
                {
                    UserId = userId,
                    Page = dto.Page,
                    PageSize = dto.PageSize,
                    Title = dto.Title,
                    CurrencyId = dto.CurrencyId,
                    PriceMin = dto.PriceMin,
                    PriceMax = dto.PriceMax,
                    CategoryId = dto.CategoryId,
                    SubcategoryId = dto.SubcategoryId,
                    CityId = dto.CityId,
                    OtherUserId = dto.OtherUserId,
                    SortByPrice = dto.SortByPrice,
                    ReverseSort = dto.ReverseSort,
                    IsNew = dto.IsNew,
                    IsSmart = dto.IsSmart,
                    TVTypesId = dto.TVTypesId,
                    TVBrandsId = dto.TVBrandsId,
                    ScreenResolutionsId = dto.ScreenResolutionsId,
                    ScreenDiagonalsId = dto.ScreenDiagonalsId,
                };

                var result = await _mediator.Send(query);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while processing the request: " + ex.Message);
            }
        }

        [HttpGet]
        [Route("tvs/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetByIdTVAsync(GetByIdTVAttributeQueryDTO dto)
        {
            try
            {
                var query = new GetByIdTVAttributeQuery()
                {
                    TVAttributeId = dto.TVAttributeId
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
        [HttpPost]
        [Route("favorite-products")]
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
                    ProductId = dto.ProductId,
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

        [HttpDelete]
        [Route("favorite-products/{id}")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> RemoveFavoriteProductAsync(RemoveFavoriteProductCommandDTO dto)
        {
            try
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                var command = new RemoveFavoriteProductCommand()
                {
                    UserId = userId,
                    FavoriteProductId = dto.FavoriteProductId
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

        [HttpGet]
        [Route("favorite-products")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllFavoriteProductsAsync(GetAllFavoriteProductsQueryDTO dto)
        {
            try
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                var query = new GetAllFavoriteProductsQuery()
                {
                    UserId = userId,
                    Page = dto.Page,
                    PageSize = dto.PageSize,
                    Title = dto.Title,
                    CurrencyId = dto.CurrencyId,
                    PriceMin = dto.PriceMin,
                    PriceMax = dto.PriceMax,
                    CategoryId = dto.CategoryId,
                    SubcategoryId = dto.SubcategoryId,
                    CityId = dto.CityId,
                    OtherUserId = dto.OtherUserId,
                    SortByPrice = dto.SortByPrice,
                    ReverseSort = dto.ReverseSort,
                    ProductId = dto.ProductId
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

        [HttpGet]
        [Route("favorite-products/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetByIdFavoriteProductAsync(GetByIdFavoriteProductQueryDTO dto)
        {
            try
            {
                var query = new GetByIdFavoriteProductQuery()
                {
                    FavoriteProductId = dto.FavoriteProductId
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
        /// Products Attribute
        /// </summary>
        [HttpGet]
        [Route("products")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllProductsAsync(GetAllProductsQueryDTO dto)
        {
            try
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                var query = new GetAllProductsQuery()
                {
                    UserId = userId,
                    Page = dto.Page,
                    PageSize = dto.PageSize,
                    Title = dto.Title,
                    CurrencyId = dto.CurrencyId,
                    PriceMin = dto.PriceMin,
                    PriceMax = dto.PriceMax,
                    CategoryId = dto.CategoryId,
                    SubcategoryId = dto.SubcategoryId,
                    CityId = dto.CityId,
                    OtherUserId = dto.OtherUserId,
                    SortByPrice = dto.SortByPrice,
                    ReverseSort = dto.ReverseSort
                };

                var result = await _mediator.Send(query);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while processing the request: " + ex.Message);
            }
        }

        [HttpDelete]
        [Route("products/{id}")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> RemoveProductAsync(RemoveProductCommandDTO dto)
        {
            try
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                var command = new RemoveProductCommand()
                {
                    UserId = userId,
                    ProductId = dto.ProductId
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

        [HttpPatch]
        [Route("products/{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> IncrementViewsAsync(IncrementProductViewsCommandDTO dto)
        {
            try
            {
                var command = new IncrementProductViewsCommand()
                {
                    ProductId = dto.ProductId
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

        [HttpGet]
        [Route("cities")]
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

        [HttpGet]
        [Route("currencies")]
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

        [HttpGet]
        [Route("autos/fuel-types")]
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

        [HttpGet]
        [Route("colors")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllColorsAsync(GetAllColorsQueryDTO dto)
        {
            try
            {
                var query = new GetAllColorsQuery()
                {
                    ModelId = dto.ModelId
                };

                var result = await _mediator.Send(query);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while processing the request: " + ex.Message);
            }
        }

        [HttpGet]
        [Route("autos/transmission-types")]
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

        [HttpGet]
        [Route("autos/brands")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllAutoBrandsAsync(GetAllAutoBrandsQueryDTO dto)
        {
            try
            {
                var query = new GetAllAutoBrandsQuery()
                {
                    AutoTypesId = dto.AutoTypesId
                };

                var result = await _mediator.Send(query);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while processing the request: " + ex.Message);
            }
        }

        [HttpGet]
        [Route("tvs/brands")]
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

        [HttpGet]
        [Route("tvs/screen-resolutions")]
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

        [HttpGet]
        [Route("tvs/screen-diagonals")]
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

        [HttpGet]
        [Route("tvs/types")]
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

        [HttpGet]
        [Route("animals/breeds")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllAnimalBreedsAsync(GetAllAnimalBreedsQueryDTO dto)
        {
            try
            {
                var query = new GetAllAnimalBreedsQuery()
                {
                    AnimalTypesId = dto.AnimalTypesId
                };

                var result = await _mediator.Send(query);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while processing the request: " + ex.Message);
            }
        }

        [HttpGet]
        [Route("clothes/sizes")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllClothesSizesAsync()
        {
            try
            {
                GetAllClothesSizesQuery query = new GetAllClothesSizesQuery();

                var result = await _mediator.Send(query);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while processing the request: " + ex.Message);
            }
        }

        [HttpGet]
        [Route("clothes/seasons")]
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

        [HttpGet]
        [Route("clothes/brands")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllClothesBrandsAsync(GetAllClothesBrandsQueryDTO dto)
        {
            try
            {
                var query = new GetAllClothesBrandsQuery()
                {
                    ClothesViewsId = dto.ClothesViewsId
                };

                var result = await _mediator.Send(query);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while processing the request: " + ex.Message);
            }
        }

        [HttpGet]
        [Route("clothes/views")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllClothesViewsAsync(GetAllClothesViewsQueryDTO dto)
        {
            try
            {
                var query = new GetAllClothesViewsQuery()
                {
                    GenderId = dto.GenderId,
                    ClothesTypeId = dto.ClothesTypeId
                };

                var result = await _mediator.Send(query);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while processing the request: " + ex.Message);
            }
        }

        [HttpGet]
        [Route("clothes/genders")]
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

        [HttpGet]
        [Route("electronics/memories")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllMemoriesAsync(GetAllMemoriesQueryDTO dto)
        {
            try
            {
                var query = new GetAllMemoriesQuery()
                {
                    ModelId = dto.ModelId
                };

                var result = await _mediator.Send(query);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while processing the request: " + ex.Message);
            }
        }

        [HttpGet]
        [Route("electronics/models")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllModelsAsync(GetAllModelsQueryDTO dto)
        {
            try
            {
                var query = new GetAllModelsQuery()
                {
                    ElectronicBrandsId = dto.ElectronicBrandsId,
                    ElectronicTypeId = dto.ElectronicTypeId
                };

                var result = await _mediator.Send(query);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while processing the request: " + ex.Message);
            }
        }

        [HttpGet]
        [Route("electronics/brands")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllElectronicBrandsAsync(GetAllElectronicBrandsQueryDTO dto)
        {
            try
            {
                var query = new GetAllElectronicBrandsQuery()
                {
                    ElectronicTypeId = dto.ElectronicTypeId
                };

                var result = await _mediator.Send(query);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while processing the request: " + ex.Message);
            }
        }

        [HttpGet]
        [Route("autos/models")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllAutoModelsAsync(GetAllAutoModelsQueryDTO dto)
        {
            try
            {
                var query = new GetAllAutoModelsQuery()
                {
                    AutoBrandsId = dto.AutoBrandsId,
                    AutoTypesId = dto.AutoTypesId
                };

                var result = await _mediator.Send(query);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while processing the request: " + ex.Message);
            }
        }

        [HttpGet]
        [Route("clothes/types")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllClothesTypesAsync(GetAllClothesTypesQueryDTO dto)
        {
            try
            {
                var query = new GetAllClothesTypesQuery()
                {
                    GenderId = dto.GenderId
                };

                var result = await _mediator.Send(query);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while processing the request: " + ex.Message);
            }
        }

        [HttpGet]
        [Route("animals/types")]
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

        [HttpGet]
        [Route("autos/types")]
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

        [HttpGet]
        [Route("electronics/types")]
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

        [HttpGet]
        [Route("items/types")]
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

        [HttpGet]
        [Route("real-estates/types")]
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
        public async Task<ActionResult<string>> Options()
        {
            return Ok("x44 GET, x8 POST, x7 PUT, x9 DELETE, HEAD, OPTIONS");
        }
    }
}