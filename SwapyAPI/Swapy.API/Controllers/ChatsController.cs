using Microsoft.AspNetCore.Mvc;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Swapy.BLL.Domain.Chats.Commands;
using Swapy.Common.Exceptions;
using Swapy.BLL.Domain.Chats.Queries;
using Swapy.Common.DTO.Chats.Requests;
using System.Security.Claims;

namespace Swapy.API.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    [Produces("application/json")]
    public class ChatsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ChatsController(IMediator mediator) => _mediator = mediator;

        [HttpGet]
        [Route("ping")]
        [Authorize]
        public IActionResult Ping()
        {
            return Ok("ping");
        }

        [HttpPost]
        [Route("")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateChatAsync(CreateChatCommandDTO dto)
        {
            try
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                var command = new CreateChatCommand()
                {
                    UserId = userId,
                    ProductId= dto.ProductId,
                };

                var result = await _mediator.Send(command);
                var locationUri = new Uri($"{Request.Scheme}://{Request.Host.ToUriComponent()}/{result.Id}");
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

        [HttpPost]
        [Route("messages")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> SendMessageAsync(SendMessageCommandDTO dto)
        {
            try
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                var command = new SendMessageCommand()
                {
                    UserId = userId,
                    Text = dto.Text,
                    Image = dto.Image,
                    ChatId = dto.ChatId
                };

                var result = await _mediator.Send(command);
                var locationUri = new Uri($"{Request.Scheme}://{Request.Host.ToUriComponent()}/messages/{result.Id}");
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

        [HttpGet]
        [Route("chats/buyers/{userId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllBuyerChatsAsync()
        {
            try
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                var result = await _mediator.Send(new GetAllBuyerChatsQuery() { UserId = userId });
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while processing the request: " + ex.Message);
            }
        }

        [HttpGet]
        [Route("chats/sellers/{userId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllSellerChatsAsync()
        {
            try
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                var result = await _mediator.Send(new GetAllSellerChatsQuery() { UserId = userId });
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while processing the request: " + ex.Message);
            }
        }

        [HttpGet]
        [Route("chats/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetDetailChatAsync(GetDetailChatQueryDTO dto)
        {
            try
            {
                var result = await _mediator.Send(new GetDetailChatQueryDTO() { ChatId = dto.ChatId});
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
            return Ok("x4 GET, x2 POST, HEAD, OPTIONS");
        }
    }
}