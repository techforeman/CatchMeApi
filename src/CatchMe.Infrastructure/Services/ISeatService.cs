using CatchMe.Infrastructure.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CatchMe.Infrastructure.Services
{
	public interface ISeatService
	{
		Task<SeatDTO> GetAsync(Guid userId, Guid eventId, Guid seatId);

		Task OrderAsync(Guid userId, Guid eventId, int amount);
		Task CanceleAsync(Guid userId, Guid eventId, int amount);

		Task<IEnumerable<SeatDetailsDTO>> GetForUserAsync(Guid userId);
	}
}
