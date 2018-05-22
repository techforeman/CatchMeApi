using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CatchMe.Infrastructure.Commands
{
	public class CreateEvent
	{
		public Guid EventId { get; set; }
		public string Name { get; set; }
		public string Descirption { get; set; }
		public DateTime EndDate { get; set; }
		public DateTime StartDate { get; set; }
		public int Seats { get; set; }



	}
}
