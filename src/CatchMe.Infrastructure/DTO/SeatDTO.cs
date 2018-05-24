using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CatchMe.Infrastructure.DTO
{
	public class SeatDTO
	{
		public Guid Id { get; set; }
		public int Seating { get; set; }
		public Guid? UserId { get; set; }
		public string UserName { get; set; }
		public DateTime? OrderedAt { get; set; }
		public bool Ordered { get; set; }
		public decimal Price { get; protected set; }

	}
}
