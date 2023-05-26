using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swapy.BLL.Domain.Users.Commands;
using Swapy.BLL.Domain.Users.Queries;
using Swapy.Common.DTO.Users.Requests;
using Swapy.Common.Enums;
using Swapy.Common.Exceptions;
using System.Security.Claims;

namespace Swapy.API.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    [Produces("application/json")]
    public class UsersController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UsersController(IMediator mediator) => _mediator = mediator;

        [HttpGet("ping")]
        [Authorize]
        public async Task<IActionResult> Ping()
        {
            return Ok("ping");
        }


        /// <summary>
        /// Users
        /// </summary>
        [HttpGet("{userid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetById([FromRoute] GetByIdUserQueryDTO dto)
        {
            try
            {
                var result = await _mediator.Send(new GetByIdUserQuery() { UserId = dto.userid });
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
        public async Task<IActionResult> UpdateAsync(UpdateUserCommandDTO dto)
        {
            try
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                var command = new UpdateUserCommand
                {
                    UserId = userId,
                    Logo = dto.logo,
                    Email = dto.email,
                    LastName = dto.lastname,
                    FirstName = dto.firstname,
                    PhoneNumber = dto.phonenumber
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

        [HttpDelete]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> RemoveAsync()
        {
            try
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                var result = await _mediator.Send(new RemoveUserCommand() { UserId = userId });
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


        /// <summary>
        /// Likes 
        /// </summary>
        [HttpPost("likes/{recipientid}")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> AddLike([FromRoute] AddLikeCommandDTO dto)
        {
            try
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                var type = User.FindFirstValue(ClaimTypes.Role);
                var command = new AddLikeCommand()
                {
                    Type = (UserType)Enum.Parse(typeof(UserType), type),
                    UserId = userId,
                    RecipientId = dto.recipientid,
                };

                var result = await _mediator.Send(command);
                var locationUri = new Uri($"{Request.Scheme}://{Request.Host.ToUriComponent()}/likes/{result.Id}");
                return Created(locationUri, result);
            }
            catch (DuplicateValueException ex)
            {
                return Conflict(ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest("Invalid operation: " + ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while processing the request: " + ex.Message);
            }
        }

        [HttpDelete("likes/{recipientid}")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> RemoveLike([FromRoute] RemoveLikeCommandDTO dto)
        {
            try
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                var command = new RemoveLikeCommand()
                {
                    LikerId = userId,
                    RecipientId = dto.recipientid
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

        [HttpGet("likes/check/{recipientid}")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CheckLike([FromRoute] CheckLikeQueryDTO dto)
        {
            try
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                var query = new CheckLikeQuery()
                {
                    UserId = userId,
                    RecipientId = dto.recipientid
                };

                var result = await _mediator.Send(query);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while processing the request: " + ex.Message);
            }
        }


        /// <summary>
        /// Subscriptions
        /// </summary>
        [HttpPost("subscriptions/{recipientid}")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> AddSubscription([FromRoute] AddSubscriptionCommandDTO dto)
        {
            try
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                var type = User.FindFirstValue(ClaimTypes.Role);
                var command = new AddSubscriptionCommand()
                {
                    Type = (UserType)Enum.Parse(typeof(UserType), type),
                    UserId = userId,
                    RecipientId = dto.recipientid,
                };

                var result = await _mediator.Send(command);
                var locationUri = new Uri($"{Request.Scheme}://{Request.Host.ToUriComponent()}/subscriptions/{result.Id}");
                return Created(locationUri, result);
            }
            catch (DuplicateValueException ex)
            {
                return Conflict(ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest("Invalid operation: " + ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while processing the request: " + ex.Message);
            }
        }

        [HttpDelete("subscriptions/{recipientid}")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> RemoveSubscription([FromRoute] RemoveSubscriptionCommandDTO dto)
        {
            try
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                var command = new RemoveSubscriptionCommand()
                {
                    SubscriberId = userId,
                    RecipientId = dto.recipientid
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

        [HttpGet("subscriptions/check/{recipientid}")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CheckSubscription([FromRoute] CheckSubscriptionQueryDTO dto)
        {
            try
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                var query = new CheckSubscriptionQuery()
                {
                    UserId = userId,
                    RecipientId = dto.recipientid
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
        public async Task<IActionResult> Head()
        {
            return Ok();
        }

        [HttpOptions]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<string>> Options()
        {
            return Ok("x4 GET, x2 POST, PUT, DELETE, x2 DELETE, HEAD, OPTIONS");
        }
    }
}
