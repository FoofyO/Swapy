using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swapy.API.Validators;
using Swapy.BLL.Domain.Products.Commands;
using Swapy.BLL.Domain.Products.Queries;
using Swapy.BLL.Domain.Shops.Commands;
using Swapy.BLL.Domain.Users.Commands;
using Swapy.Common.Attributes;
using Swapy.Common.DTO.Products.Requests.Commands;
using Swapy.Common.DTO.Products.Requests.Queries;
using Swapy.Common.Enums;
using Swapy.Common.Exceptions;
using System.Security.Claims;
using System.Text;

namespace Swapy.API.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    [Produces("application/json")]
    public class ProductsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ProductsController(IMediator mediator) => _mediator = mediator;

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
        /// Products
        /// </summary>
        [HttpGet]
        [Localize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllProductsAsync([FromQuery] GetAllProductsQueryDTO dto)
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
                    IsDisable = dto.IsDisable,
                    OtherUserId = dto.OtherUserId,
                    SortByPrice = dto.SortByPrice,
                    ReverseSort = dto.ReverseSort,
                    Language = (Language)HttpContext.Items["Language"]
                };

                var result = await _mediator.Send(query);
                return Ok(result);
            }
            catch (NoAccessException ex)
            {
                return NotFound(ex.Message);
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

        [HttpDelete("{ProductId}")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
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
                    ProductId = dto.ProductId
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

        [HttpPatch("Views/{ProductId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> IncrementViewsAsync([FromRoute] IncrementProductViewsCommandDTO dto)
        {
            try
            {
                var result = await _mediator.Send(new IncrementProductViewsCommand() { ProductId = dto.ProductId });
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

        [HttpPatch("Enabling/{ProductId}")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> SwitchEnablingAsync([FromRoute] SwitchProductEnablingCommandDTO dto)
        {
            try
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                var command = new SwitchProductEnablingCommand()
                {
                    UserId = userId,
                    ProductId = dto.ProductId
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

        [HttpPost("PreviewUploadImages")]
        [Authorize]
        [Consumes("multipart/form-data")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> PreviewUploadImageAsync([FromForm] UploadImagesCommandDTO dto)
        {
            try
            {
                var validator = new ImagePreviewUploadValidator();
                var validatorResult = validator.Validate(dto);
                if (!validatorResult.IsValid)
                {
                    StringBuilder builder = new StringBuilder();

                    foreach (var failure in validatorResult.Errors)
                    {
                        builder.Append($"Product image preview upload property {failure.PropertyName} failed validation. Error: {failure.ErrorMessage}");
                    }

                    return BadRequest(builder.ToString());
                }

                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

                var result = await _mediator.Send(new PreviewUploadImageCommand()
                {
                    UserId = userId,
                    Images = dto.Files
                });
                return Ok(result);
            }
            catch (NoAccessException ex)
            {
                return Unauthorized(ex.Message);
            }
            catch (UnconfirmedEmailException ex)
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

        [HttpPost("UploadImages")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UploadImageAsync(UploadImageCommandDTO dto)
        {
            try
            {
                var validator = new ImageUploadValidator();
                var validatorResult = validator.Validate(dto);
                if (!validatorResult.IsValid)
                {
                    StringBuilder builder = new StringBuilder();

                    foreach (var failure in validatorResult.Errors)
                    {
                        builder.Append($"Product image upload property {failure.PropertyName} failed validation. Error: {failure.ErrorMessage}");
                    }

                    return BadRequest(builder.ToString());
                }

                var command = new UploadImageCommand()
                {
                    ProductId = dto.ProductId,
                    Paths = dto.Paths
                };

                var result = await _mediator.Send(command);
                return Ok(result);
            }
            catch (NoAccessException ex)
            {
                return Unauthorized(ex.Message);
            }
            catch (UnconfirmedEmailException ex)
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

        /// <summary>
        /// Favorite products
        /// </summary>
        [HttpPost("FavoriteProducts/{ProductId}")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> AddFavoriteProductAsync([FromRoute] AddFavoriteProductCommandDTO dto)
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
                return Created(locationUri, result.Id);
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
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while processing the request: " + ex.Message);
            }
        }

        [HttpDelete("FavoriteProducts/{ProductId}")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
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
                    ProductId = dto.ProductId
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

        [HttpGet("FavoriteProducts")]
        [Authorize]
        [Localize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllFavoriteProductsAsync([FromQuery] GetAllFavoriteProductsQueryDTO dto)
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
                    ProductId = dto.ProductId,
                    Language = (Language)HttpContext.Items["Language"]
                };

                var result = await _mediator.Send(query);
                return Ok(result);
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
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while processing the request: " + ex.Message);
            }
        }


        /// <summary>
        /// Product Attributes
        /// </summary>
        [HttpGet("Cities")]
        [Localize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllCitiesAsync()
        {
            try
            {
                var result = await _mediator.Send(new GetAllCitiesQuery() { Language = (Language)HttpContext.Items["Language"] });
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while processing the request: " + ex.Message);
            }
        }

        [HttpGet("Currencies")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllCurrenciesAsync()
        {
            try
            {
                var result = await _mediator.Send(new GetAllCurrenciesQuery());
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while processing the request: " + ex.Message);
            }
        }

        [HttpGet("Colors")]
        [Localize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllColorsAsync()
        {
            try
            {
                var result = await _mediator.Send(new GetAllColorsQuery() { Language = (Language)HttpContext.Items["Language"] });
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
            return Ok("x6 GET, POST, PATCH, x2 DELETE, HEAD, OPTIONS");
        }
    }
}