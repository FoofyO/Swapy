﻿using Castle.Core.Internal;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swapy.BLL.Domain.Auth.Commands;
using Swapy.Common.Attributes;
using Swapy.Common.DTO.Auth.Requests;
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

        [HttpPost("Login")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> LoginAsync(LoginCommandDTO dto)
        {
            try
            {
                var command = new LoginCommand()
                {
                    EmailOrPhone = dto.EmailOrPhone,
                    Password = dto.Password
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

        [HttpPost("Register/User")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UserRegistrationAsync(UserRegistrationCommandDTO dto)
        {
            try
            {
                var command = new UserRegistrationCommand()
                {
                    FirstName = dto.FirstName,
                    LastName = dto.LastName,
                    Email = dto.Email,
                    PhoneNumber = dto.PhoneNumber,
                    Password = dto.Password
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

        [HttpPost("Register/Shop")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> ShopRegistrationAsync(ShopRegistrationCommandDTO dto)
        {
            try
            {
                var command = new ShopRegistrationCommand()
                {
                    ShopName = dto.ShopName,
                    Email = dto.Email,
                    PhoneNumber = dto.PhoneNumber,
                    Password = dto.Password
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

        [HttpPut("RefreshAccessToken")]
        [AuthorizeIgnore]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateTokenAsync()
        {
            try
            {
                var accessToken = User.FindFirstValue(ClaimTypes.Hash);
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                if(userId.IsNullOrEmpty()) throw new UnauthorizedAccessException("Unauthorized");
                var command = new UpdateUserTokenCommand()
                {
                    UserId = userId,
                    OldAccessToken = accessToken,
                };

                var result = await _mediator.Send(command);
                return Ok(result);
            }
            catch (Exception ex) when (ex is UnauthorizedAccessException || ex is TokenExpiredException)
            {
                return Unauthorized(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while processing the request: " + ex.Message);
            }
        }

        [HttpGet("Logout")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
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
            catch(UnauthorizedAccessException ex)
            {
                return Unauthorized(ex.Message);
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

        [HttpGet("Check")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Check([FromQuery] CheckCommandDTO dto)
        {
            try
            {
                if (!string.IsNullOrEmpty(dto.Email) && string.IsNullOrEmpty(dto.PhoneNumber) && string.IsNullOrEmpty(dto.ShopName))
                {
                    var command = new EmailCommand()
                    {
                        Email = dto.Email
                    };

                    var result = await _mediator.Send(command);
                    return Ok(result);
                }
                else if (string.IsNullOrEmpty(dto.Email) && !string.IsNullOrEmpty(dto.PhoneNumber) && string.IsNullOrEmpty(dto.ShopName))
                {
                    var command = new PhoneNumberCommand()
                    {
                        PhoneNumber = dto.PhoneNumber
                    };

                    var result = await _mediator.Send(command);
                    return Ok(result);
                }
                else if (string.IsNullOrEmpty(dto.Email) && string.IsNullOrEmpty(dto.PhoneNumber) && !string.IsNullOrEmpty(dto.ShopName))
                {
                    var command = new ShopNameCommand()
                    {
                        ShopName = dto.ShopName
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

        [HttpPatch("ChangePassword")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> ChangePassword(ChangePasswordCommandDTO dto)
        {
            try
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                var command = new ChangePasswordCommand()
                {
                    UserId = userId,
                    NewPassword = dto.NewPassword,
                    OldPassword = dto.OldPassword
                };

                var result = await _mediator.Send(command);
                return Ok();
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
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
            return Ok("x4 GET, x3 POST, PATCH, HEAD, OPTIONS");
        }
    }
}
