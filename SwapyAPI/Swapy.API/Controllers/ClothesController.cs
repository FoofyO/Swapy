﻿using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swapy.API.Validators;
using Swapy.BLL.Domain.Clothes.Commands;
using Swapy.BLL.Domain.Clothes.Queries;
using Swapy.Common.Attributes;
using Swapy.Common.DTO.Clothes.Requests.Commands;
using Swapy.Common.DTO.Clothes.Requests.Queries;
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
    public class ClothesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ClothesController(IMediator mediator) => _mediator = mediator;

        /// <summary>
        /// Clothes
        /// </summary>
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

        [HttpPost]
        [Authorize]
        [Consumes("multipart/form-data")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> AddClothesAsync(AddClothesAttributeCommandDTO dto)
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


                var clothesValidator = new AddClothesAttributeValidator();
                var clothesValidatorResult = clothesValidator.Validate(dto);

                if (!clothesValidatorResult.IsValid)
                {
                    StringBuilder builder = new StringBuilder();

                    foreach (var failure in clothesValidatorResult.Errors)
                    {
                        builder.Append($"Clothes Attribute property {failure.PropertyName} failed validation. Error: {failure.ErrorMessage}");
                    }

                    return BadRequest(builder.ToString());
                }


                var imageValidator = new AddImageUploadValidator();
                var imageValidatorResult = imageValidator.Validate(dto);
                if (!imageValidatorResult.IsValid)
                {
                    StringBuilder builder = new StringBuilder();

                    foreach (var failure in imageValidatorResult.Errors)
                    {
                        builder.Append($"Product image upload property {failure.PropertyName} failed validation. Error: {failure.ErrorMessage}");
                    }

                    return BadRequest(builder.ToString());
                }


                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                var command = new AddClothesAttributeCommand()
                {
                    UserId = userId,
                    IsNew = dto.IsNew,
                    Price = dto.Price,
                    Title = dto.Title,
                    CityId = dto.CityId,
                    CategoryId = dto.CategoryId,
                    CurrencyId = dto.CurrencyId,
                    Description = dto.Description,
                    SubcategoryId = dto.SubcategoryId,
                    ClothesSizeId = dto.ClothesSizeId,
                    ClothesSeasonId = dto.ClothesSeasonId,
                    ClothesBrandViewId = dto.ClothesBrandViewId,
                    Files = dto.Files
                };

                var result = await _mediator.Send(command);
                var locationUri = new Uri($"{Request.Scheme}://{Request.Host.ToUriComponent()}/clothes/{result.Id}");
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
        [Consumes("multipart/form-data")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateClothesAsync([FromQuery] UpdateClothesAttributeCommandDTO dto)
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


                var clothesValidator = new UpdateClothesAttributeValidator();
                var clothesValidatorResult = clothesValidator.Validate(dto);

                if (!clothesValidatorResult.IsValid)
                {
                    StringBuilder builder = new StringBuilder();

                    foreach (var failure in clothesValidatorResult.Errors)
                    {
                        builder.Append($"Clothes Attribute property {failure.PropertyName} failed validation. Error: {failure.ErrorMessage}");
                    }

                    return BadRequest(builder.ToString());
                }


                var imageValidator = new UpdateImageUploadValidator();
                var imageValidatorResult = imageValidator.Validate(dto);
                if (!imageValidatorResult.IsValid)
                {
                    StringBuilder builder = new StringBuilder();

                    foreach (var failure in imageValidatorResult.Errors)
                    {
                        builder.Append($"Product image upload property {failure.PropertyName} failed validation. Error: {failure.ErrorMessage}");
                    }

                    return BadRequest(builder.ToString());
                }


                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                var command = new UpdateClothesAttributeCommand()
                {
                    UserId = userId,
                    IsNew = dto.IsNew,
                    Price = dto.Price,
                    Title = dto.Title,
                    CityId = dto.CityId,
                    ProductId = dto.ProductId,
                    CategoryId = dto.CategoryId,
                    CurrencyId = dto.CurrencyId,
                    Description = dto.Description,
                    ClothesSizeId = dto.ClothesSizeId,
                    SubcategoryId = dto.SubcategoryId,
                    ClothesSeasonId = dto.ClothesSeasonId,
                    ClothesBrandViewId = dto.ClothesBrandViewId
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
        public async Task<IActionResult> GetAllClothesAsync([FromQuery] GetAllClothesAttributesQueryDTO dto)
        {
            try
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                var query = new GetAllClothesAttributesQuery()
                {
                    UserId = userId,
                    Page = dto.Page,
                    IsNew = dto.IsNew,
                    Title = dto.Title,
                    CityId = dto.CityId,
                    PageSize = dto.PageSize,
                    PriceMax = dto.PriceMax,
                    PriceMin = dto.PriceMin,
                    CategoryId = dto.CategoryId,
                    CurrencyId = dto.CurrencyId,
                    OtherUserId = dto.OtherUserId,
                    SortByPrice = dto.SortByPrice,
                    ReverseSort = dto.ReverseSort,
                    SubcategoryId = dto.SubcategoryId,
                    ClothesSizesId = dto.ClothesSizesId,
                    ClothesTypesId = dto.ClothesTypesId,
                    ClothesViewsId = dto.ClothesViewsId,
                    ClothesBrandsId = dto.ClothesBrandsId,
                    ClothesGendersId = dto.ClothesGendersId,
                    ClothesSeasonsId = dto.ClothesSeasonsId,
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
        public async Task<IActionResult> GetByIdClothesAsync([FromRoute] GetByIdProductQueryDTO dto)
        {
            try
            {
                var query = new GetByIdClothesAttributeQuery()
                {
                    UserId = (string)HttpContext.Items["Check"],
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
        /// Autos Attributes
        /// </summary>
        [HttpGet("Sizes")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllClothesSizesAsync([FromQuery] GetAllClothesSizesQueryDTO dto)
        {
            try
            {
                var query = new GetAllClothesSizesQuery()
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

        [HttpGet("Seasons")]
        [Localize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllClothesSeasonsAsync()
        {
            try
            {
                var result = await _mediator.Send(new GetAllClothesSeasonsQuery() { Language = (Language)HttpContext.Items["Language"] });
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while processing the request: " + ex.Message);
            }
        }

        [HttpGet("Brands{ClothesViewsId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllClothesBrandsAsync([FromRoute] GetAllClothesBrandsQueryDTO dto)
        {
            try
            {
                var result = await _mediator.Send(new GetAllClothesBrandsQuery() { ClothesViewsId = dto.ClothesViewsId });
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while processing the request: " + ex.Message);
            }
        }

        [HttpGet("Views")]
        [Localize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllClothesViewsAsync([FromQuery] GetAllClothesViewsQueryDTO dto)
        {
            try
            {
                var query = new GetAllClothesViewsQuery()
                {
                    GenderId = dto.GenderId,
                    ClothesTypeId = dto.ClothesTypeId,
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

        [HttpGet("Genders")]
        [Localize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllGendersAsync()
        {
            try
            {
                var result = await _mediator.Send(new GetAllGendersQuery() { Language = (Language)HttpContext.Items["Language"] });
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while processing the request: " + ex.Message);
            }
        }

        [HttpGet("Types{GenderId}")]
        [Localize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllClothesTypesAsync([FromRoute] GetAllClothesTypesQueryDTO dto)
        {
            try
            {
                var query = new GetAllClothesTypesQuery()
                {
                    GenderId = dto.GenderId,
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
            return Ok("x9 GET, POST, PUT, DELETE, HEAD, OPTIONS");
        }
    }
}
