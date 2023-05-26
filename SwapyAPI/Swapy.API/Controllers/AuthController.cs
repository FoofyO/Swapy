using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swapy.BLL.Domain.Auth.Commands;
using Swapy.Common.DTO.Auth.Requests;
using Swapy.Common.DTO.Auth.Responses;
using Swapy.Common.Exceptions;
using System.ComponentModel.DataAnnotations;
using System.Security.Authentication;
using System.Security.Claims;

namespace Swapy.API.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    [Produces("application/json")]
    public class AuthController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AuthController(IMediator mediator) => _mediator = mediator;

        [HttpGet("ping")]
        [Authorize]
        public async Task<IActionResult> Ping()
        {
            return Ok("ping");
        }

        [HttpPost("login")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<AuthResponseDTO>> LoginAsync(LoginCommandDTO dto)
        {
            try
            {
                var command = new LoginCommand()
                {
                    EmailOrPhone = dto.emailorphone,
                    Password = dto.password
                };

                var result = await _mediator.Send(command);
                return Ok(result);
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (AuthenticationException ex)
            {
                return Unauthorized(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while processing the request: " + ex.Message);
            }
        }


        [HttpPost("register/user")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<AuthResponseDTO>> UserRegistrationAsync(UserRegistrationCommandDTO dto)
        {
            try
            {
                var command = new UserRegistrationCommand()
                {
                    FirstName = dto.firstname,
                    LastName = dto.lastname,
                    Email = dto.email,
                    PhoneNumber = dto.phonenumber,
                    Password = dto.password
                };

                var result = await _mediator.Send(command);
                return Ok(result);
            }
            catch (EmailOrPhoneTakenException ex)
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


        [HttpPost("register/shop")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<AuthResponseDTO>> ShopRegistrationAsync(ShopRegistrationCommandDTO dto)
        {
            try
            {
                var command = new ShopRegistrationCommand()
                {
                    ShopName = dto.shopname,
                    Email = dto.email,
                    PhoneNumber = dto.phonenumber,
                    Password = dto.password
                };

                var result = await _mediator.Send(command);
                return Ok(result);
            }
            catch (EmailOrPhoneTakenException ex)
            {
                return Conflict(ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                return StatusCode(500, "Invalid operation: " + ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while processing the request: " + ex.Message);
            }
        }


        [HttpGet("refresh/{oldRefreshToken}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<AuthResponseDTO>> RefreshAsync(UpdateUserTokenCommandDTO dto)
        {
            try
            {
                var accessToken = User.FindFirstValue(ClaimTypes.Hash);
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                var refreshToken = User.FindFirstValue(ClaimTypes.Authentication);
                var command = new UpdateUserTokenCommand()
                {
                    UserId = userId,
                    OldAccessToken = accessToken,
                    OldRefreshToken = refreshToken
                };

                var result = await _mediator.Send(command);
                return Ok(result);
            }
            catch (TokenExpiredException ex)
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


        [HttpGet("logout/{refreshToken}")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> LogoutAsync()
        {
            try
            {
                var refreshToken = User.FindFirstValue(ClaimTypes.Authentication);
                var command = new LogoutCommand()
                {
                    RefreshToken = refreshToken  
                };

                var result = await _mediator.Send(command);
                return NoContent();
            }
            catch (Exception ex)
            {
                if (ex is ValidationException)
                {
                    return BadRequest(ex.Message);
                }

                return StatusCode(500, "An error occurred while processing the request: " + ex.Message);
            }
        }


        [HttpGet("check")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<bool>> Check([FromQuery] CheckCommandDTO dto)
        {
            try
            {
                if (!string.IsNullOrEmpty(dto.email) && !string.IsNullOrEmpty(dto.phonenumber) && string.IsNullOrEmpty(dto.shopname))
                {
                    var command = new EmailCommand()
                    {
                        Email = dto.email
                    };

                    var result = await _mediator.Send(command);
                    return Ok(result);
                }
                else if (string.IsNullOrEmpty(dto.email) && !string.IsNullOrEmpty(dto.phonenumber) && string.IsNullOrEmpty(dto.shopname))
                {
                    var command = new PhoneNumberCommand()
                    {
                        PhoneNumber = dto.phonenumber
                    };

                    var result = await _mediator.Send(command);
                    return Ok(result);
                }
                else if (string.IsNullOrEmpty(dto.email) && string.IsNullOrEmpty(dto.phonenumber) && !string.IsNullOrEmpty(dto.shopname))
                {
                    var command = new ShopNameCommand()
                    {
                        ShopName = dto.shopname
                    };

                    var result = await _mediator.Send(command);
                    return Ok(result);
                }
                else throw new Exception("You can't send more than 1 parameter");
            }
            catch (Exception ex)
            {
                if (ex is ValidationException)
                {
                    return BadRequest(ex.Message);
                }

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
            return Ok("x4 GET, x3 POST, HEAD, OPTIONS");
        }
    }
}
