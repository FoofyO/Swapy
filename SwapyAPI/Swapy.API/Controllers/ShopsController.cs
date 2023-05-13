using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swapy.BLL.Domain.Products.Queries;
using Swapy.BLL.Domain.Shops.Queries;
using Swapy.Common.Exceptions;
using Swapy.BLL.Domain.Shops.Commands;

namespace Swapy.API.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    [Produces("application/json")]
    public class ShopsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ShopsController(IMediator mediator) => _mediator = mediator;

        [HttpGet]
        [Route("ping")]
        [Authorize]
        public IActionResult Ping()
        {
            return Ok("ping");
        }

        [HttpGet]
        [Route("")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAll(GetAllProductsQuery query)
        {
            try
            {
                var result = await _mediator.Send(query);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while processing the request: " + ex.Message);
            }
        }

        [HttpGet]
        [Route("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetById(GetByIdShopQuery query)
        {
            try
            {
                var result = await _mediator.Send(query);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while processing the request: " + ex.Message);
            }
        }

        [HttpPut]
        [Route("{id}")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateAsync(UpdateShopCommand command)
        {
            try
            {
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
        [Route("")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult Head()
        {
            return Ok();
        }

        [HttpOptions]
        [Route("")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<string> Options()
        {
            return Ok("x3 GET, x1 PUT, HEAD, OPTIONS");
        }
    }
}
