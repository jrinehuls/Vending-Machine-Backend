using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;
using VendingMachine.Filters;
using VendingMachine.Models.DTOs;
using VendingMachine.Models.DTOs.Funds;
using VendingMachine.Models.DTOs.Snack;
using VendingMachine.Services;

namespace VendingMachine.Controllers
{
    [ApiController]
    [SnackFilter]
    [EnableCors]
    [Route("api/[controller]")]
    public class SnackController : ControllerBase
    {
        private readonly ISnackService _snackService;

        public SnackController(ISnackService snackService)
        {
            _snackService = snackService;
        }

        [HttpGet("All", Name = "GetSnacks")]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType<List<SnackResponseDto>>(200)]
        [ProducesResponseType(500)]
        public async Task<ActionResult<List<SnackResponseDto>>> GetSnacks()
        {
            return Ok(await _snackService.GetAllSnacksAsync());
        }


        [HttpGet("{id:long:min(1)}", Name = "GetSnackById")]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType<List<SnackResponseDto>>(200)]
        [ProducesResponseType<ErrorResponse>(404)]
        [ProducesResponseType(500)]
        public async Task<ActionResult<List<SnackResponseDto>>> GetSnackById([FromRoute] long id)
        {
            return Ok(await _snackService.GetSnackByIdAsync(id));
        }

        [HttpPost("Purchase/{snackId:long:min(1)}", Name = "PurchaseSnack")]
        [Consumes(MediaTypeNames.Application.Json)]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType<SnackChangeResponseDto>(200)]
        [ProducesResponseType<ValidationProblemDetails>(400)]
        [ProducesResponseType<ErrorResponse>(400)]
        [ProducesResponseType<ErrorResponse>(404)]
        [ProducesResponseType<ErrorResponse>(422)]
        [ProducesResponseType(500)]
        public async Task<ActionResult<SnackChangeResponseDto>> PurchaseSnack([FromRoute] long snackId,
            [FromBody] FundsRequestDto requestDto)
        {
            return Ok(await _snackService.PurchaseSnackAsync(snackId, requestDto));
        }

    }
}
