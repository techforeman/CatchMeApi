using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CatchMe.Infrastructure.DTO
{
	public class EventDTO
	{

		public Guid Id { get; set; }
		public string Name { get; set; }
		public string Descirption { get; set; }
		public DateTime EndDate { get; set; }
		public DateTime StartDate { get; set; }
		public DateTime UpdateAt { get; set; }
		public int SeatsCount { get; set; }
	}
}
