using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CatchMe.Infrastructure.Commands.Events
{
	public class UpdateEvent
	{
		public Guid EventId { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }
	}
}
