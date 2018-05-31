using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CatchMe.Infrastructure.Services;
using Microsoft.AspNetCore.Authorization;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace CatchMe.Api.Controllers
{
	[Authorize]
	[Route("events/{eventId}/seats")]
    public class SeatController : ApiControllerBase
    {
		private readonly ISeatService _seatService;

		public SeatController(ISeatService seatService)
		{
			_seatService = seatService;
		}

		[HttpGet("{seatId}")]
		public async Task<IActionResult> Get(Guid eventId, Guid seatId)
		{
			var seat = await _seatService.GetAsync(UserId, eventId, seatId);
			if (seat == null)
			{
				return NotFound();
			}
			return Json(seat);
		}



		[HttpPost("order/{amount}")]
		public async Task<IActionResult> Post(Guid eventId, int amount)
		{
			await _seatService.OrderAsync(UserId, eventId, amount);
			
			return NoContent();
			
		}



		[HttpDelete("cancele/{amount}")]
		public async Task<IActionResult> Delete(Guid eventId, int amount)
		{
			await _seatService.CanceleAsync(UserId, eventId, amount);

			return NoContent();

		}


	}
}
