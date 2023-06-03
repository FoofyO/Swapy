using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swapy.BLL.Domain.Autos.Commands;
using Swapy.BLL.Domain.Autos.Queries;
using Swapy.Common.DTO.Autos.Requests.Commands;
using Swapy.Common.DTO.Autos.Requests.Queries;
using Swapy.Common.DTO.Products.Requests.Queries;
using Swapy.Common.Exceptions;
using System.Security.Claims;

namespace Swapy.API.Controllers
{
    [ApiController]
    [Route("api/v1/Products/[controller]")]
    [Produces("application/json")]
    public class AutosController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AutosController(IMediator mediator) => _mediator = mediator;

        [HttpGet("Ping")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> Ping()
        {
            try
            {
                return Ok("Ping");
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(ex.Message);
            }
        }

        /// <summary>
        /// Autos
        /// </summary>
        [HttpPost]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
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
                return Created(locationUri, result.ProductId);
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(ex.Message);
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
        [Authorize]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
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
                    AutoAttributeId = dto.AutoAttributeId
                };

                var result = await _mediator.Send(command);
                return NoContent();
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(ex.Message);
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
        [Authorize]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
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
                    Title = dto.Title,
                    Description = dto.Description,
                    Price = dto.Price,
                    CurrencyId = dto.CurrencyId,
                    CategoryId = dto.CategoryId,
                    SubcategoryId = dto.SubcategoryId,
                    CityId = dto.CityId,
                    ProductId = dto.ProductId,
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
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(ex.Message);
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
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllAutosAsync([FromQuery] GetAllAutoAttributesQueryDTO dto)
        {
            try
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                var query = new GetAllAutoAttributesQuery()
                {
                    UserId = userId,
                    Page = dto.Page,
                    IsNew = dto.IsNew,
                    Title = dto.Title,
                    CityId = dto.CityId,
                    PageSize = dto.PageSize,
                    PriceMax = dto.PriceMax,
                    PriceMin = dto.PriceMin,
                    MiliageMin = dto.MiliageMin,
                    MiliageMax = dto.MiliageMax,
                    CategoryId = dto.CategoryId,
                    CurrencyId = dto.CurrencyId,
                    AutoTypesId = dto.AutoTypesId,
                    OtherUserId = dto.OtherUserId,
                    SortByPrice = dto.SortByPrice,
                    ReverseSort = dto.ReverseSort,
                    FuelTypesId = dto.FuelTypesId,
                    AutoColorsId = dto.AutoColorsId,
                    AutoBrandsId = dto.AutoBrandsId,
                    SubcategoryId = dto.SubcategoryId,
                    ReleaseYearOlder = dto.ReleaseYearOlder,
                    ReleaseYearNewer = dto.ReleaseYearNewer,
                    EngineCapacityMin = dto.EngineCapacityMin,
                    EngineCapacityMax = dto.EngineCapacityMax,
                    TransmissionTypesId = dto.TransmissionTypesId
                };
                var result = await _mediator.Send(query);
                return Ok(result);
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

        [HttpGet("{ProductId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetByIdAutoAsync([FromRoute] GetByIdProductQueryDTO dto)
        {
            try
            {
                var result = await _mediator.Send(new GetByIdAutoAttributeQuery() { ProductId = dto.ProductId });
                return Ok(result);
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


        /// <summary>
        /// Autos Attributes
        /// </summary>
        [HttpGet("FuelTypes")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllFuelTypesAsync()
        {
            try
            {
                var result = await _mediator.Send(new GetAllFuelTypesQuery());
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while processing the request: " + ex.Message);
            }
        }

        [HttpGet("TransmissionTypes")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllTransmissionTypesAsync()
        {
            try
            {
                var result = await _mediator.Send(new GetAllTransmissionTypesQuery());
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while processing the request: " + ex.Message);
            }
        }

        [HttpGet("Brands")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllAutoBrandsAsync([FromRoute] GetAllAutoBrandsQueryDTO dto)
        {
            try
            {
                var result = await _mediator.Send(new GetAllAutoBrandsQuery() { AutoTypesId = dto.AutoTypesId });
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while processing the request: " + ex.Message);
            }
        }

        [HttpGet("Models")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllAutoModelsAsync([FromQuery] GetAllAutoModelsQueryDTO dto)
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

        [HttpGet("Types")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllAutoTypesAsync()
        {
            try
            {
                var result = await _mediator.Send(new GetAllAutoTypesQuery());
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
            return Ok("x8 GET, POST, PUT, DELETE, HEAD, OPTIONS");
        }
    }
}
