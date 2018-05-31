using AutoMapper;
using CatchMe.Core.Repository;
using CatchMe.Infrastructure.DTO;
using CatchMe.Infrastructure.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CatchMe.Infrastructure.Services
{
	public class SeatService : ISeatService
	{
		private readonly IEventRepository _eventRepository;
		private readonly IMapper _mapper;
		private readonly IUserRepository _userRepository;

		public SeatService(IUserRepository userRepository,
			IEventRepository eventRepository, IMapper mapper)
		{
			_mapper = mapper;
			_eventRepository = eventRepository;
			_userRepository = userRepository;
		}



		public async Task<SeatDTO> GetAsync(Guid userId, Guid eventId, Guid seatId)
		{
			var user = await _userRepository.GetOrFailAsync(userId);
			var seat = await _eventRepository.GetSeatOrFailAsync(eventId, seatId);

			return _mapper.Map<SeatDTO>(seat);

		}

		public async Task OrderAsync(Guid userId, Guid eventId, int amount)
		{
			var user = await _userRepository.GetOrFailAsync(userId);
			var @event = await _eventRepository.GetOrFailAsync(eventId);
			@event.OrderSeats(user, amount);
			await _eventRepository.UpdateAsync(@event);

		}

		public async Task CanceleAsync(Guid userId, Guid eventId, int amount)
		{
			var user = await _userRepository.GetOrFailAsync(userId);
			var @event = await _eventRepository.GetOrFailAsync(eventId);
			@event.CancelOrderedSeats(user, amount);
			await _eventRepository.UpdateAsync(@event);
		}

		public async Task<IEnumerable<SeatDTO>> GetForUserAsync(Guid userId)
		{
			var user = await _userRepository.GetOrFailAsync(userId);
			var events = await _eventRepository.BrowseAsync();
			var seats = events.SelectMany(x => x.GetSeatsOrderedByUser(user));

			return _mapper.Map<IEnumerable<SeatDTO>>(seats);
		}
	}
}
