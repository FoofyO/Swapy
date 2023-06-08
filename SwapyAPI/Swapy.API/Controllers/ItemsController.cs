﻿using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swapy.BLL.Domain.Items.Commands;
using Swapy.BLL.Domain.Items.Queries;
using Swapy.Common.DTO.Items.Requests.Commands;
using Swapy.Common.DTO.Items.Requests.Queries;
using Swapy.Common.DTO.Products.Requests.Queries;
using Swapy.Common.Exceptions;
using System.Security.Claims;

namespace Swapy.API.Controllers
{
    [ApiController]
    [Route("api/v1/Products/[controller]")]
    [Produces("application/json")]
    public class ItemsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ItemsController(IMediator mediator) => _mediator = mediator;

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
        /// Items
        /// </summary>
        [HttpPost]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
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
                    IsNew = dto.IsNew,
                    Title = dto.Title,
                    Price = dto.Price,
                    CityId = dto.CityId,
                    ItemTypeId = dto.ItemTypeId,
                    CategoryId = dto.CategoryId,
                    CurrencyId = dto.CurrencyId,
                    Description = dto.Description,
                    SubcategoryId = dto.SubcategoryId
                };

                var result = await _mediator.Send(command);
                var locationUri = new Uri($"{Request.Scheme}://{Request.Host.ToUriComponent()}/items/{result.Id}");
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

        [HttpPut]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
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
                    IsNew = dto.IsNew,
                    Price = dto.Price,
                    Title = dto.Title,
                    CityId = dto.CityId,
                    ProductId = dto.ProductId,
                    ItemTypeId = dto.ItemTypeId,
                    CategoryId = dto.CategoryId,
                    CurrencyId = dto.CurrencyId,
                    Description = dto.Description,
                    SubcategoryId = dto.SubcategoryId
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
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllItemsAsync([FromQuery] GetAllItemAttributesQueryDTO dto)
        {
            try
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                var query = new GetAllItemAttributesQuery()
                {
                    UserId = userId,
                    Page = dto.Page,
                    IsNew = dto.IsNew,
                    Title = dto.Title,
                    CityId = dto.CityId,
                    PageSize = dto.PageSize,
                    PriceMin = dto.PriceMin,
                    PriceMax = dto.PriceMax,
                    CurrencyId = dto.CurrencyId,
                    CategoryId = dto.CategoryId,
                    OtherUserId = dto.OtherUserId,
                    SortByPrice = dto.SortByPrice,
                    ReverseSort = dto.ReverseSort,
                    ItemTypesId = dto.ItemTypesId,
                    SubcategoryId = dto.SubcategoryId,
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
        public async Task<IActionResult> GetByIdItemAsync([FromRoute] GetByIdProductQueryDTO dto)
        {
            try
            {
                var result = await _mediator.Send(new GetByIdItemAttributeQuery() { ProductId = dto.ProductId });
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
        /// Items Attributes
        /// </summary>
        [HttpGet("Sections")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllItemSectionsAsync()
        {
            try
            {
                var result = await _mediator.Send(new GetAllItemSectionsQuery());
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while processing the request: " + ex.Message);
            }
        }

        [HttpGet("Types/{ParentSubcategoryId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllItemTypesAsync([FromRoute] GetAllItemTypesQueryDTO dto)
        {
            try
            {
                var result = await _mediator.Send(new GetAllItemTypesQuery()
                {
                    ParentSubcategoryId = dto.ParentSubcategoryId
                });
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
