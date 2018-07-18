using CatchMe.Core.Domain;
using CatchMe.Core.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CatchMe.Infrastructure.Repository
{
	public class EventRepository : IEventRepository
	{
		private static readonly ISet<Event> _events = new HashSet<Event>()
		{
			//new Event(Guid.NewGuid(), "Event 1", "Event descirption 1",
			//	DateTime.UtcNow.AddHours(2), DateTime.UtcNow.AddHours(10)),
			//new Event(Guid.NewGuid(), "Event 2", "Event descirption 2",
			//	DateTime.UtcNow.AddHours(5), DateTime.UtcNow.AddHours(15))

		};

		public async Task AddAsync(Event @event)
		{
			_events.Add(@event);
			await Task.CompletedTask;
		}

		public async Task<IEnumerable<Event>> BrowseAsync(string name = "")
		{
			var events = _events.AsEnumerable();
			if (!string.IsNullOrWhiteSpace(name))
			{
				events = events.Where(x => x.Name.ToLowerInvariant()
				  .Contains(name.ToLowerInvariant()));
			}
			return await Task.FromResult(events);
		}

		public async Task DeleteAsync(Event @event)
		{
			_events.Remove(@event);
			await Task.CompletedTask;
		}

		public async Task<Event> GetAsync(string name)
		=> await Task.FromResult(_events.SingleOrDefault(n =>
			n.Name.ToLowerInvariant() == name.ToLowerInvariant()));

		public async Task<Event> GetAsync(Guid id)
		=> await Task.FromResult(_events.SingleOrDefault(x => x.Id == id));

		public async Task UpdateAsync(Event @event)
		{
			await Task.CompletedTask;
		}
	}
}
