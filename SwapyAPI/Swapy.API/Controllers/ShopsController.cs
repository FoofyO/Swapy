using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swapy.BLL.Domain.Shops.Queries;
using Swapy.Common.Exceptions;
using Swapy.BLL.Domain.Shops.Commands;
using Swapy.Common.DTO.Shops.Requests;
using System.Security.Claims;

namespace Swapy.API.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    [Produces("application/json")]
    public class ShopsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ShopsController(IMediator mediator) => _mediator = mediator;

        [HttpGet("ping")]
        [Authorize]
        public async Task<IActionResult> Ping()
        {
            return Ok("ping");
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
                    Page = dto.page,
                    Title = dto.title,
                    PageSize = dto.pagesize,
                    ReverseSort = dto.reversesort,
                    SortByViews = dto.sortbyviews
                };

                var result = await _mediator.Send(query);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while processing the request: " + ex.Message);
            }
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetById([FromRoute] GetByIdShopQueryDTO dto)
        {
            try
            {
                var result = await _mediator.Send(new GetByIdShopQuery() { ShopId = dto.id});
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
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateAsync(UpdateShopCommandDTO dto)
        {
            try
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                var command = new UpdateShopCommand()
                {
                    UserId = userId,
                    ShopName = dto.shopName,
                    Description = dto.description,
                    Location = dto.location,
                    Slogan = dto.slogan,
                    Banner = dto.banner,
                    WorkDays = dto.workDays,
                    StartWorkTime = dto.startWorkTime,
                    EndWorkTime = dto.endWorkTime
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
            return Ok("x2 GET, POST, PUT, HEAD, OPTIONS");
        }
    }
}
