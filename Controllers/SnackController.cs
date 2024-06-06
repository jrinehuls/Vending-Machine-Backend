using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Net.Mime;
using VendingMachine.Filters;
using VendingMachine.Models.DTOs;
using VendingMachine.Models.DTOs.Snack;
using VendingMachine.Models.Entities;
using VendingMachine.Services;

namespace VendingMachine.Controllers
{
    [ApiController]
    [SnackFilter]
    [Route("[controller]")]
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
            return Ok(await _snackService.GetSnacksAsync());
        }

        [HttpPost("Purchase/snackId:long:min(1)", Name = "PurchaseSnack")]
        [Consumes(MediaTypeNames.Application.Json)]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType<SnackResponseDto>(200)]
        [ProducesResponseType<ValidationProblemDetails>(400)]
        [ProducesResponseType<ErrorResponse>(400)]
        [ProducesResponseType<ErrorResponse>(404)]
        [ProducesResponseType<ErrorResponse>(422)]
        [ProducesResponseType(500)]
        public async Task<ActionResult<SnackResponseDto>> PurchaseSnack([FromRoute] long snackId,
            [FromBody] PurchaseSnackRequestDto requestDto)
        {
            return Ok(await _snackService.PurchaseSnackAsync(snackId, requestDto));
        }

    }
}
