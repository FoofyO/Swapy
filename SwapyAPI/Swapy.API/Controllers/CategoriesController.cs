using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swapy.BLL.Domain.Categories.Queries;
using Swapy.Common.DTO.Categories.Requests.Queries;
using Swapy.Common.Entities;

namespace Swapy.API.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    [Produces("application/json")]
    public class CategoriesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CategoriesController(IMediator mediator) => _mediator = mediator;

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
        public async Task<IActionResult> GetAllCategoriesAsync()
        {
            try
            {
                GetAllCategoriesQuery query = new GetAllCategoriesQuery();

                var result = await _mediator.Send(query);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while processing the request: " + ex.Message);
            }
        }

        [HttpGet]
        [Route("subcategories/{category}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetSubcategoriesByCategoryAsync(GetAllSubcategoriesByCategoryQueryDTO dto)
        {
            try
            {
                var query = new GetAllSubcategoriesByCategoryQuery() 
                {
                    CategoryId = dto.CategoryId
                };

                var result = await _mediator.Send(query);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while processing the request: " + ex.Message);
            }
        }

        [HttpGet]
        [Route("subcategories/{subcategory}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetSubcategoriesBySubcategoryAsync(GetAllSubcategoriesBySubcategoryQueryDTO dto)
        {
            try
            {
                var query = new GetAllSubcategoriesBySubcategoryQuery()
                {
                    SubcategoryId = dto.SubcategoryId
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
            return Ok("x4 GET, HEAD, OPTIONS");
        }
    }
}
