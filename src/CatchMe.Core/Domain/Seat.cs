using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CatchMe.Core.Domain
{
	public class Seat : Entity
	{
		public Guid EventId { get; protected set; }
		public int Seating { get; protected set; }
		public Guid? UserId { get; protected set; }
		public string UserName { get; protected set; }
		public DateTime? OrderedAt { get; protected set; }
		public bool Ordered => UserId.HasValue;
		public decimal Price { get; protected set; }

		protected Seat()
		{

		}

		public Seat(Event @event, int seating, decimal price)
		{
			EventId = @event.Id;
			Seating = seating;
			Price = price;

		}

		public void Order(User user)
		{
			if (Ordered)
			{
				throw new Exception($"Seat was already ordered.");
			}
			UserId = user.Id;
			UserName = user.Name;
			OrderedAt = DateTime.UtcNow;
		}

		public void Cancel()
		{
			if (!Ordered)
			{
				throw new Exception($"Seat cannot be canceled.");
			}
			UserId = null;
			UserName = null;
			OrderedAt = null;
		}

	}
}

