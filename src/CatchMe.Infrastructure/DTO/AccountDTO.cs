using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CatchMe.Infrastructure.DTO
{
	public class AccountDTO
	{
		public Guid Id { get; set; }
		public string Role { get; protected set; }
		public string Name { get; protected set; }
		public string Email { get; protected set; }
	}
}
