using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CatchMe.Infrastructure.Services;
using CatchMe.Infrastructure.Commands;
using Microsoft.AspNetCore.Authorization;
using CatchMe.Infrastructure.Commands.Events;
using Microsoft.Extensions.Caching.Memory;
using CatchMe.Infrastructure.DTO;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace CatchMe.Api.Controllers
{
	[Route("[controller]")]
	public class EventsController : Controller
	{
		private readonly IEventService _eventService;
		private readonly IMemoryCache _cache;

		public EventsController(IEventService eventService, IMemoryCache cache)
		{
			_eventService = eventService;
			_cache = cache;
		}

		[HttpGet]
		public async Task<IActionResult> Get(string name)
		{
			var events = _cache.Get<IEnumerable<EventDTO>>("events");
			if (events ==null)
			{
				Console.WriteLine("Fetching from service.");
				events = await _eventService.BrowseAsync(name);
				_cache.Set("events", events, TimeSpan.FromMinutes(1));
				
			}
			else
			{
				Console.WriteLine("Fetching from cache.");

			}
			
			return Json(events);
		}

		[HttpGet("{eventId}")]
		public async Task<IActionResult> Get(Guid eventId)
		{

			var @event = await _eventService.GetAsync(eventId);
			if (@event == null)
			{
				return NotFound();
			}
			return Json(@event);
		}

		[HttpPost]
		[Authorize(Policy = "HasAdminRole")]
		public async Task<IActionResult> Post([FromBody] CreateEvent cmd)
		{
			cmd.EventId = Guid.NewGuid();
			await _eventService.CreateAsync(cmd.EventId, cmd.Name, cmd.Descirption, cmd.StartDate, cmd.EndDate);
			await _eventService.AddSeatAsync(cmd.EventId, cmd.Seats, cmd.Price);
			return Created($"/events/{cmd.EventId}", null);
		}

		[HttpPut("{eventId}")]
		[Authorize(Policy = "HasAdminRole")]
		public async Task<IActionResult> Put(Guid eventId, [FromBody] UpdateEvent cmd)
		{
			await _eventService.UpdateAsync(eventId, cmd.Name, cmd.Description);
			return NoContent();
		}
		[HttpDelete("{eventId}")]
		[Authorize(Policy = "HasAdminRole")]
		public async Task<IActionResult> Delete(Guid eventId)
		{
			await _eventService.DeleteAsync(eventId);
			return NoContent();
		}



	}
}
