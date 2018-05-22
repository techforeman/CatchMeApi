using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CatchMe.Core.Domain
{
	public class Event : Entity
	{
		private ISet<Seat> _seats = new HashSet<Seat>();
		public string Name { get; protected set; }
		public string Descirption { get; protected set; }
		public DateTime CreateAt { get; protected set; }
		public DateTime EndDate { get; protected set; }
		public DateTime StartDate { get; protected set; }
		public DateTime UpdateAt { get; protected set; }

		public IEnumerable<Seat> Seats => _seats;

		public IEnumerable<Seat> OrderedSets => Seats.Where(x => x.Ordered);
		public IEnumerable<Seat> AvailabledSets => Seats.Except(OrderedSets);






		protected Event()
		{

		}

		public Event(Guid id, string name, string description,
			DateTime startDate, DateTime endDate)
		{

			Id = id;
			SetName(name);
			SetDescription(description);
			Descirption = description;
			StartDate = DateTime.UtcNow;
			EndDate = endDate;
			CreateAt = DateTime.UtcNow;
			UpdateAt = DateTime.UtcNow;
		}
		public void SetName(string name)
		{
			if (string.IsNullOrWhiteSpace(name))
			{
				throw new Exception($"Event with id: '{Id}' cannot have an empty name.");
			}
			Name = name;
			UpdateAt = DateTime.UtcNow;
		}

		public void SetDescription(string description)
		{
			if (string.IsNullOrWhiteSpace(description))
			{
				throw new Exception($"Event with id: '{Id}' cannot have an empty description  ");
			}
			Descirption = description;
			UpdateAt = DateTime.UtcNow;
		}


		public void AddSeats(int amount)
		{
			var seating = _seats.Count + 1;
			for (var i = 0; i < amount; i++)
			{

				_seats.Add(new Seat(this, seating + 1));
				seating++;
			}
		}


		public void OrderSeats(User user, int amount)
		{
			if (AvailabledSets.Count() < amount)
			{
				throw new Exception($"Not enough available seats to order.");
			}
			var seats = AvailabledSets.Take(amount);
			foreach (var seat in seats)
			{
				seat.Order(user);
			}
		}

		public void CancelOrderedSeats(User user, int amount)
		{

			var seats = OrderedSets.Where(x => x.UserId == user.Id);
			if (seats.Count() < amount)
			{
				throw new Exception($"Not enought ordered seats to be canceled.");
			}

			foreach (var seat in seats)
			{
				seat.Cancel();
			}
		}
	}
}
