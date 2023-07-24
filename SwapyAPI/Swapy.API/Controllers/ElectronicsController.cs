using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swapy.API.Validators;
using Swapy.BLL.Domain.Electronics.Commands;
using Swapy.BLL.Domain.Electronics.Queries;
using Swapy.Common.Attributes;
using Swapy.Common.DTO.Electronics.Requests.Commands;
using Swapy.Common.DTO.Electronics.Requests.Queries;
using Swapy.Common.DTO.Products.Requests.Queries;
using Swapy.Common.Enums;
using Swapy.Common.Exceptions;
using System.Security.Claims;
using System.Text;

namespace Swapy.API.Controllers
{
    [ApiController]
    [Route("api/v1/Products/[controller]")]
    [Produces("application/json")]
    public class ElectronicsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ElectronicsController(IMediator mediator) => _mediator = mediator;

        [HttpGet("Ping")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> PingAsync()
        {
            try
            {
                return Ok("Ping");
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(ex.Message);
            }
            catch (UnconfirmedEmailException ex)
            {
                return Unauthorized(ex.Message);
            }
        }

        /// <summary>
        /// Electronics
        /// </summary>
        [HttpPost]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> AddElectronicAsync(AddElectronicAttributeCommandDTO dto)
        {
            try
            {
                var productValidator = new AddProductValidator();
                var productValidatorResult = productValidator.Validate(dto);

                if (!productValidatorResult.IsValid)
                {
                    StringBuilder builder = new StringBuilder();

                    foreach (var failure in productValidatorResult.Errors)
                    {
                        builder.Append($"Product Attribute property {failure.PropertyName} failed validation. Error: {failure.ErrorMessage}");
                    }

                    return BadRequest(builder.ToString());
                }


                var electronicsValidator = new AddElectronicsAttributeValidator();
                var electronicsValidatorResult = electronicsValidator.Validate(dto);

                if (!electronicsValidatorResult.IsValid)
                {
                    StringBuilder builder = new StringBuilder();

                    foreach (var failure in electronicsValidatorResult.Errors)
                    {
                        builder.Append($"Electronics Attribute property {failure.PropertyName} failed validation. Error: {failure.ErrorMessage}");
                    }

                    return BadRequest(builder.ToString());
                }

                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                var command = new AddElectronicAttributeCommand()
                {
                    UserId = userId,
                    IsNew = dto.IsNew,
                    Price = dto.Price,
                    Title = dto.Title,
                    CityId = dto.CityId,
                    CategoryId = dto.CategoryId,
                    CurrencyId = dto.CurrencyId,
                    Description = dto.Description,
                    ModelColorId = dto.ModelColorId,
                    MemoryModelId = dto.MemoryModelId,
                    SubcategoryId = dto.SubcategoryId
                };

                var result = await _mediator.Send(command);
                var locationUri = new Uri($"{Request.Scheme}://{Request.Host.ToUriComponent()}/electronics/{result.Id}");
                return Created(locationUri, result.ProductId);
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(ex.Message);
            }
            catch (UnconfirmedEmailException ex)
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

        [HttpPut]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateElectronicAsync([FromQuery] UpdateElectronicAttributeCommandDTO dto)
        {
            try
            {
                var productValidator = new UpdateProductValidator();
                var productValidatorResult = productValidator.Validate(dto);

                if (!productValidatorResult.IsValid)
                {
                    StringBuilder builder = new StringBuilder();

                    foreach (var failure in productValidatorResult.Errors)
                    {
                        builder.Append($"Product Attribute property {failure.PropertyName} failed validation. Error: {failure.ErrorMessage}");
                    }

                    return BadRequest(builder.ToString());
                }


                var electronicsValidator = new UpdateElectronicsAttributeValidator();
                var electronicsValidatorResult = electronicsValidator.Validate(dto);

                if (!electronicsValidatorResult.IsValid)
                {
                    StringBuilder builder = new StringBuilder();

                    foreach (var failure in electronicsValidatorResult.Errors)
                    {
                        builder.Append($"Electronics Attribute property {failure.PropertyName} failed validation. Error: {failure.ErrorMessage}");
                    }

                    return BadRequest(builder.ToString());
                }

                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                var command = new UpdateElectronicAttributeCommand()
                {
                    UserId = userId,
                    IsNew = dto.IsNew,
                    Price = dto.Price,
                    Title = dto.Title,
                    CityId = dto.CityId,
                    ProductId = dto.ProductId,
                    CurrencyId = dto.CurrencyId,
                    CategoryId = dto.CategoryId,
                    Description = dto.Description,
                    ModelColorId = dto.ModelColorId,
                    MemoryModelId = dto.MemoryModelId,
                    SubcategoryId = dto.SubcategoryId
                };

                var result = await _mediator.Send(command);
                return NoContent();
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(ex.Message);
            }
            catch (UnconfirmedEmailException ex)
            {
                return Unauthorized(ex.Message);
            }
            catch (NoAccessException ex)
            {
                return Unauthorized(ex.Message);
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
        [Localize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllElectronicsAsync([FromQuery] GetAllElectronicAttributesQueryDTO dto)
        {
            try
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                var query = new GetAllElectronicAttributesQuery()
                {
                    UserId = userId,
                    Page = dto.Page,
                    IsNew = dto.IsNew,
                    Title = dto.Title,
                    CityId = dto.CityId,
                    TypesId = dto.TypesId,
                    BrandsId = dto.BrandsId,
                    ColorsId = dto.ColorsId,
                    ModelsId = dto.ModelsId,
                    PageSize = dto.PageSize,
                    PriceMax = dto.PriceMax,
                    PriceMin = dto.PriceMin,
                    CategoryId = dto.CategoryId,
                    CurrencyId = dto.CurrencyId,
                    MemoriesId = dto.MemoriesId,
                    OtherUserId = dto.OtherUserId,
                    SortByPrice = dto.SortByPrice,
                    ReverseSort = dto.ReverseSort,
                    SubcategoryId = dto.SubcategoryId,
                    Language = (Language)HttpContext.Items["Language"]
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
        [Localize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetByIdElectronicAsync([FromRoute] GetByIdProductQueryDTO dto)
        {
            try
            {
                var query = new GetByIdElectronicAttributeQuery()
                {
                    ProductId = dto.ProductId,
                    Language = (Language)HttpContext.Items["Language"]
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


        /// <summary>
        /// Electronics Attributes
        /// </summary>
        [HttpGet("Memories/{ModelId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllMemoriesAsync([FromRoute] GetAllMemoriesQueryDTO dto)
        {
            try
            {
                var result = await _mediator.Send(new GetAllMemoriesQuery() { ModelId = dto.ModelId });
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
        public async Task<IActionResult> GetAllModelsAsync([FromQuery] GetAllModelsQueryDTO dto)
        {
            try
            {
                var query = new GetAllModelsQuery()
                {
                    ElectronicTypeId = dto.ElectronicTypeId,
                    ElectronicBrandsId = dto.ElectronicBrandsId
                };

                var result = await _mediator.Send(query);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while processing the request: " + ex.Message);
            }
        }

        [HttpGet("Brands/{ElectronicTypeId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllElectronicBrandsAsync([FromRoute] GetAllElectronicBrandsQueryDTO dto)
        {
            try
            {
                var result = await _mediator.Send(new GetAllElectronicBrandsQuery() { ElectronicTypeId = dto.ElectronicTypeId });
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while processing the request: " + ex.Message);
            }
        }

        [HttpGet("Types")]
        [Localize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllElectronicTypesAsync()
        {
            try
            {
                var result = await _mediator.Send(new GetAllElectronicTypesQuery() { Language = (Language)HttpContext.Items["Language"] });
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while processing the request: " + ex.Message);
            }
        }

        [HttpGet("Colors/{ModelId}")]
        [Localize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllColorsByModelAsync([FromRoute] GetAllColorsQueryDTO dto)
        {
            try
            {
                var query = new GetAllColorsByModelQuery()
                {
                    ModelId = dto.ModelId,
                    Language = (Language)HttpContext.Items["Language"]
                };
                
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
        public async Task<IActionResult> HeadAsync()
        {
            return Ok();
        }

        [HttpOptions]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> OptionsAsync()
        {
            return Ok("x7 GET, POST, PUT, DELETE, HEAD, OPTIONS");
        }
    }
}
