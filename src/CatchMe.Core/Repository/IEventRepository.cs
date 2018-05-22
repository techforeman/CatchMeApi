using CatchMe.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CatchMe.Core.Repository
{
    public interface IEventRepository
	{
		 
		Task<Event> GetAsync(Guid id);
		Task<Event> GetAsync(string name);
		Task<IEnumerable<Event>> BrowseAsync(string name = "");
		Task AddAsync(Event @event);
		Task UpdateAsync(Event @event);
		Task DeleteAsync(Event @event);
	}
}

