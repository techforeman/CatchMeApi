using CatchMe.Infrastructure.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CatchMe.Infrastructure.Services
{
	public interface IEventService
	{
		Task<EventDetailsDTO> GetAsync(Guid id);
		Task<EventDetailsDTO> GetAsync(string name);
		Task<IEnumerable<EventDTO>> BrowseAsync(string name = null);

		Task CreateAsync(Guid id, string name, string description, DateTime startDate, DateTime endaDate);
		Task AddSeatAsync(Guid eventId, int amount, decimal price);
		Task UpdateAsync(Guid id, string name, string description);
		Task DeleteAsync(Guid id);

	}
}
