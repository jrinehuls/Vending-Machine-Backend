using Microsoft.AspNetCore.Mvc;
using System.Data;
using VendingMachine.Models.DTOs;
using VendingMachine.Models.Entities;
using VendingMachine.Services;

namespace VendingMachine.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SnackController : ControllerBase
    {
        private readonly ISnackService _snackService;

        public SnackController(ISnackService snackService)
        {
            _snackService = snackService;
        }

        [HttpGet("All", Name = "GetSnacks")]
        public async Task<ActionResult<List<SnackResponseDto>>> GetSnacks(long id)
        {
            return null;
        }

        [HttpPost("Purchase/snackId:long:min(1)", Name = "PurchaseSnack")]
        public async Task<ActionResult<SnackResponseDto>> PurchaseSnack([FromRoute] long snackId,
            [FromBody] PurchaseSnackRequestDto requestDto)
        {
            return null;
        }

    }
}
