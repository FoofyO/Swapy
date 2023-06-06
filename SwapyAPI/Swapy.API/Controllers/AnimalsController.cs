using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swapy.BLL.Domain.Animals.Commands;
using Swapy.BLL.Domain.Animals.Queries;
using Swapy.Common.DTO.Animals.Requests.Commands;
using Swapy.Common.DTO.Animals.Requests.Queries;
using Swapy.Common.DTO.Products.Requests.Queries;
using Swapy.Common.Exceptions;
using System.Security.Claims;

namespace Swapy.API.Controllers
{
    [ApiController]
    [Route("api/v1/Products/[controller]")]
    [Produces("application/json")]
    public class AnimalsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AnimalsController(IMediator mediator) => _mediator = mediator;

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
        /// Animals
        /// </summary>
        [HttpPost]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> AddAnimalAsync(AddAnimalAttributeCommandDTO dto)
        {
            try
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                var command = new AddAnimalAttributeCommand()
                {
                    UserId = userId,
                    Title = dto.Title,
                    Description = dto.Description,
                    Price = dto.Price,
                    CurrencyId = dto.CurrencyId,
                    CategoryId = dto.CategoryId,
                    SubcategoryId = dto.SubcategoryId,
                    CityId = dto.CityId,
                    AnimalBreedId = dto.AnimalBreedId
                };

                var result = await _mediator.Send(command);
                var locationUri = new Uri($"{Request.Scheme}://{Request.Host.ToUriComponent()}/animals/{result.Id}");
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
        public async Task<IActionResult> UpdateAnimalAsync([FromQuery] UpdateAnimalAttributeCommandDTO dto)
        {
            try
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                var command = new UpdateAnimalAttributeCommand()
                {
                    UserId = userId,
                    Title = dto.Title,
                    Description = dto.Description,
                    Price = dto.Price,
                    CurrencyId = dto.CurrencyId,
                    CategoryId = dto.CategoryId,
                    SubcategoryId = dto.SubcategoryId,
                    CityId = dto.CityId,
                    ProductId = dto.ProductId,
                    AnimalBreedId = dto.AnimalBreedId
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
        public async Task<IActionResult> GetAllAnimalsAsync([FromQuery] GetAllAnimalAttributesQueryDTO dto)
        {
            try
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                var query = new GetAllAnimalAttributesQuery()
                {
                    UserId = userId,
                    Page = dto.Page,
                    PageSize = dto.PageSize,
                    Title = dto.Title,
                    CurrencyId = dto.CurrencyId,
                    PriceMin = dto.PriceMin,
                    PriceMax = dto.PriceMax,
                    CategoryId = dto.CategoryId,
                    SubcategoryId = dto.SubcategoryId,
                    CityId = dto.CityId,
                    OtherUserId = dto.OtherUserId,
                    SortByPrice = dto.SortByPrice,
                    ReverseSort = dto.ReverseSort,
                    AnimalBreedsId = dto.AnimalBreedsId,
                    AnimalTypesId = dto.AnimalTypesId
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
        public async Task<IActionResult> GetByIdAnimalAsync([FromRoute] GetByIdProductQueryDTO dto)
        {
            try
            {
                var result = await _mediator.Send(new GetByIdAnimalAttributeQuery() { ProductId = dto.ProductId });
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
        /// Animal Attributes
        /// </summary>
        [HttpGet("Breeds{AnimalTypesId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllAnimalBreedsAsync([FromRoute] GetAllAnimalBreedsQueryDTO dto)
        {
            try
            {
                var result = await _mediator.Send(new GetAllAnimalBreedsQuery() { AnimalTypesId = dto.AnimalTypesId });
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while processing the request: " + ex.Message);
            }
        }

        [HttpGet("Types")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllAnimalTypesAsync()
        {
            try
            {
                var result = await _mediator.Send(new GetAllAnimalTypesQuery());
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
            return Ok("x5 GET, POST, PUT, DELETE, HEAD, OPTIONS");
        }
    }
}
