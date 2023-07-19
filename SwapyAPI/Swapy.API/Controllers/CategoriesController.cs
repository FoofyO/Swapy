﻿using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swapy.BLL.Domain.Categories.Queries;
using Swapy.Common.Attributes;
using Swapy.Common.DTO.Categories.Requests.Queries;
using Swapy.Common.Enums;
using Swapy.Common.Exceptions;

namespace Swapy.API.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    [Produces("application/json")]
    public class CategoriesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CategoriesController(IMediator mediator) => _mediator = mediator;

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
        }

        [HttpGet]
        [Localize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllCategoriesAsync()
        {
            try
            {
                var result = await _mediator.Send(new GetAllCategoriesQuery() { Language = (Language)HttpContext.Items["Language"] });
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while processing the request: " + ex.Message);
            }
        }

        [HttpGet("Subcategories/{CategoryId}")]
        [Localize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetSubcategoriesByCategoryAsync([FromRoute] GetAllSubcategoriesByCategoryQueryDTO dto)
        {
            try
            {
                var query = new GetAllSubcategoriesByCategoryQuery()
                {
                    CategoryId = dto.CategoryId,
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

        [HttpGet("Subcategories/{SubcategoryId}")]
        [Localize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetSubcategoriesBySubcategoryAsync([FromRoute] GetAllSubcategoriesBySubcategoryQueryDTO dto)
        {
            try
            {
                var query = new GetAllSubcategoriesBySubcategoryQuery()
                {
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
            return Ok("x4 GET, HEAD, OPTIONS");
        }
    }
}
