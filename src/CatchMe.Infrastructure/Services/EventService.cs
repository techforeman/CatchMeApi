using AutoMapper;
using CatchMe.Core.Domain;
using CatchMe.Core.Repository;
using CatchMe.Infrastructure.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CatchMe.Infrastructure.Services
{
	
	public class EventService : IEventService
	{
		private readonly IEventRepository _eventRepository;
		private readonly IMapper _mapper;

		public EventService(IEventRepository eventRepository, IMapper mapper)
		{
			_eventRepository = eventRepository;
			_mapper = mapper;
		}

		public async Task AddSeatAsync(Guid eventId, int amount, decimal price)
		{
			var @event = await _eventRepository.GetOrFailAsync(eventId);

			@event.AddSeats(amount, price);
			await _eventRepository.UpdateAsync(@event);



		}

		public async Task<IEnumerable<EventDTO>> BrowseAsync(string name = null)
		{
			var events = await _eventRepository.BrowseAsync(name);
			return _mapper.Map<IEnumerable<EventDTO>>(events);
		}

		public async Task CreateAsync(Guid id, string name, string description, DateTime startDate, DateTime endDate)
		{
			var @event = await _eventRepository.GetAsync(name);
			if (@event != null)
			{
				throw new Exception($"Event named: '{name}' already exist.");

			}
			@event = new Event(id, name, description, startDate, endDate);
			await _eventRepository.AddAsync(@event);


		}



		public async Task DeleteAsync(Guid id)
		{
			var @event = await _eventRepository.GetOrFailAsync(id);
			await _eventRepository.DeleteAsync(@event);
		}

		public async Task<EventDetailsDTO> GetAsync(string name)
		{
			var @event = await _eventRepository.GetAsync(name);

			return _mapper.Map<EventDetailsDTO>(@event);


		}

		public async Task<EventDetailsDTO> GetAsync(Guid id)
		{
			var @event = await _eventRepository.GetAsync(id);

			return _mapper.Map<EventDetailsDTO>(@event);
		}
		public async Task UpdateAsync(Guid id, string name, string description)
		{

			var @event = await _eventRepository.GetAsync(name);
			if (@event != null)
			{
				throw new Exception($"Event: '{name}' already exist.");

			}
			@event = await _eventRepository.GetOrFailAsync(id);
			@event.SetName(name);
			@event.SetDescription(description);
			await _eventRepository.UpdateAsync(@event);
		}

	}
}
