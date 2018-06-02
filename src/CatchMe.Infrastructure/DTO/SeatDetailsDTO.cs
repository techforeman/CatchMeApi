using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CatchMe.Infrastructure.DTO
{
    public class SeatDetailsDTO : SeatDTO
    {
		public Guid EventId { get; set; }
		public string EventName { get; set; }
	}
}
