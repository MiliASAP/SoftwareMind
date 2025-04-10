using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using WebAPI_SoftwareMind.Filters.Action;
using WebAPI_SoftwareMind.Models.Entities.DTOs;
using WebAPI_SoftwareMind.Services.BusinessLogic;

namespace WebAPI_SoftwareMind.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NegotiationController : ControllerBase
    {
        private readonly INegotiationService _negotiationService;

        public NegotiationController(INegotiationService negotiationService)
        {
            _negotiationService = negotiationService;
        }
        /// <summary>
        /// Starts a new negotiation for a product.
        /// </summary>
        /// <param name="negotiationDto">Data transfer object for starting a negotiation</param>
        /// <returns>The created negotiation</returns>
        /// <response code="201">Negotiation successfully created</response>
        /// <response code="400">Invalid data or product not found</response>
        [AllowAnonymous]
        [HttpPost("start")]
        [ServiceFilter(typeof(ValidateProductIdFilter))]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [SwaggerResponse(StatusCodes.Status201Created, "Negotiation successfully created", typeof(Negotiation))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "Product not found or negotiation already exists")]
        public async Task<ActionResult<Negotiation>> Create([FromBody] NegotiationInsertProductDTO negotiationDto)
        {
            try
            {
                var negotiationCreated =
                    await _negotiationService.CreateNegotiationAsync(negotiationDto);
                return CreatedAtAction(nameof(GetNegotiationById), new { id = negotiationCreated.NegotiationId },
                    negotiationCreated);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Retrieves all negotiations.
        /// </summary>
        /// <returns>A list of negotiations</returns>
        /// <response code="200">Returns a list of negotiations</response>
        [HttpGet]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [SwaggerResponse(StatusCodes.Status200OK, "Successfully retrieved negotiations", typeof(IEnumerable<Negotiation>))]
        public async Task<ActionResult<IEnumerable<Negotiation>>> GetAll()
        {
            return await _negotiationService.GetAllNegotiationsAsync();
        }

        /// <summary>
        /// Retrieves a negotiation by its unique identifier.
        /// </summary>
        /// <param name="id">The negotiation's unique identifier</param>
        /// <returns>A single negotiation</returns>
        /// <response code="200">Returns the requested negotiation</response>
        /// <response code="404">Negotiation not found</response>
        [AllowAnonymous]
        [HttpGet("{id}")]
        [ServiceFilter(typeof(ValidateNegotiationIdFilter))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [SwaggerResponse(StatusCodes.Status200OK, "Successfully retrieved negotiation", typeof(Negotiation))]
        [SwaggerResponse(StatusCodes.Status404NotFound, "Negotiation not found")]
        public async Task<ActionResult<Negotiation>> GetNegotiationById(int id)
        {
            var negotiation = await _negotiationService.GetNegotiationByIdAsync(id);
            return Ok(negotiation);
        }
        /// <summary>
        /// Makes a decision on a negotiation (accept or reject).
        /// </summary>
        /// <param name="dto">The negotiation decision data</param>
        /// <returns>The updated negotiation</returns>
        /// <response code="200">Negotiation updated successfully</response>
        /// <response code="404">Negotiation not found</response>
        [Authorize]
        [ServiceFilter(typeof(ValidateNegotiationIdFilter))]
        [HttpPut("decide")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [SwaggerResponse(StatusCodes.Status200OK, "Negotiation successfully decided", typeof(Negotiation))]
        [SwaggerResponse(StatusCodes.Status404NotFound, "Negotiation not found")]
        public async Task<IActionResult> Decide([FromBody] NegotiationDecisionDTO dto)
        {
            var result = await _negotiationService.DecisionNegotiationAsync(dto);
            if (result == null)
                return NotFound("Negotiation not found or already decided.");

            return Ok(result);
        }

        /// <summary>
        /// Allows to retry negotiation by proposing a new price.
        /// </summary>
        /// <param name="negotiationDto" example = "{}">Negotiation details for retrying</param>
        /// <returns>The updated negotiation</returns>
        /// <response code="200">Negotiation updated successfully</response>
        /// <response code="400">Invalid data or negotiation already decided</response>
        [AllowAnonymous]
        [HttpPut("negotiate")]
        [ServiceFilter(typeof(ValidateNegotiationIdFilter))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [SwaggerResponse(StatusCodes.Status200OK, "Negotiation successfully updated", typeof(Negotiation))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "Negotiation not found or invalid status")]
        public async Task<ActionResult<Negotiation>> Negotiate([FromBody] NegotiationNegotiateDTO negotiationDto)
        {
            try
            {
                var negotiation = await _negotiationService.NegotiateAsync(negotiationDto);
                return Ok(negotiation);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message); 
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message); 
            }
            catch
            {
                return StatusCode(500, "Unexpected error occurred."); 
            }
        }
    }


}
