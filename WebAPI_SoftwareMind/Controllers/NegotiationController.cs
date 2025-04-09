using System.Diagnostics;
using WebAPI_SoftwareMind.Models;
using WebAPI_SoftwareMind.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using WebAPI_SoftwareMind.Filters.Action;
using WebAPI_SoftwareMind.Models.Entities.DTOs;

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

        [AllowAnonymous]
        [HttpPost("start")]
        [ServiceFilter(typeof(Negotiation_ValidateNagotiationCreateFilter))]
        public async Task<ActionResult<Negotiation>> Create([FromBody] NegotiationDTO negotiationDto)
        {
            var negotiationCreated =
                await _negotiationService.CreateNegotiationAsync(negotiationDto);

            return CreatedAtAction(nameof(GetNegotiationById), new { id = negotiationCreated.NegotiationId },
                negotiationCreated);
        }

        [AllowAnonymous]
        [HttpGet("{id}")]
        public async Task<ActionResult<Negotiation>> GetNegotiationById(int id)
        {
            var negotiation = await _negotiationService.GetNegotiationByIdAsync(id);
            if (negotiation == null)
                return NotFound();

            return Ok(negotiation);
        }
    }


}
