using CatchMe.Core.Domain;
using CatchMe.Core.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CatchMe.Infrastructure.Extensions
{
	public static class RepositoryExtensions
	{
		public static async Task<Event> GetOrFailAsync(this IEventRepository repository, Guid id)
		{
			var @event = await repository.GetAsync(id);
			if (@event == null)
			{
				throw new Exception($"Event with id: '{id}' does not exist");
			}
			return @event;
		}


		public static async Task<User> GetOrFailAsync(this IUserRepository repository, Guid id)
		{
			var user = await repository.GetAsync(id);
			if (user == null)
			{
				throw new Exception($"User with id: '{id}' does not exist");
			}
			return user;
		}





		public static async Task<Seat> GetSeatOrFailAsync(this IEventRepository repository, Guid eventId,
			Guid seatId)
		{
			var @event = await repository.GetAsync(eventId);
			var seat = @event.Seats.SingleOrDefault(x => x.Id == seatId);
			if (seat == null)
			{
				throw new Exception($"Seat with id: '{seatId}' does not exist in '{@event.Name}'");
			}
			return seat;
		}

	}
}
