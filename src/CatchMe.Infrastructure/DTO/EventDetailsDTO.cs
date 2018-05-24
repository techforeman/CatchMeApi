using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CatchMe.Infrastructure.DTO
{
	public class EventDetailsDTO : EventDTO
	{

		public IEnumerable<SeatDTO> Seats { get; set; }

	}

}
