﻿using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swapy.BLL.Domain.Shops.Queries;
using Swapy.Common.Exceptions;
using Swapy.BLL.Domain.Shops.Commands;
using Swapy.Common.DTO.Shops.Requests;
using System.Security.Claims;
using Swapy.API.Validators;
using System.Text;

namespace Swapy.API.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    [Produces("application/json")]
    public class ShopsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ShopsController(IMediator mediator) => _mediator = mediator;

        [HttpGet("Ping")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> Ping()
        {
            try
            {
                return Ok("ping");
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(ex.Message);
            }
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAll([FromQuery] GetAllShopsQueryDTO dto)
        {
            try
            {
                var query = new GetAllShopsQuery()
                {
                    Page = dto.Page,
                    Title = dto.Title,
                    PageSize = dto.PageSize,
                    ReverseSort = dto.ReverseSort,
                    SortByViews = dto.SortByViews
                };

                var result = await _mediator.Send(query);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while processing the request: " + ex.Message);
            }
        }

        [HttpGet("{UserId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetById([FromRoute] GetByIdShopQueryDTO dto)
        {
            try
            {
                var result = await _mediator.Send(new GetByIdShopQuery() { UserId = dto.UserId});
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

        [HttpPut]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateAsync(UpdateShopCommandDTO dto)
        {
            try
            {
                var validator = new ShopUpdateValidator();
                var validatorResult = validator.Validate(dto);
                if (!validatorResult.IsValid)
                {
                    StringBuilder builder = new StringBuilder();

                    foreach (var failure in validatorResult.Errors)
                    {
                        builder.Append($"Shop property {failure.PropertyName} failed validation. Error: {failure.ErrorMessage}");
                    }

                    return BadRequest(builder.ToString());
                }

                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                var command = new UpdateShopCommand()
                {
                    UserId = userId,
                    Banner = dto.Banner,
                    Slogan = dto.Slogan,
                    Location = dto.Location,
                    ShopName = dto.ShopName,
                    WorkDays = dto.WorkDays,
                    Description = dto.Description,
                    EndWorkTime = dto.EndWorkTime,
                    StartWorkTime = dto.StartWorkTime
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
            return Ok("x3 GET, PUT, HEAD, OPTIONS");
        }
    }
}
