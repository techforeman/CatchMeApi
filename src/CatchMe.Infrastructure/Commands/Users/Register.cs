﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CatchMe.Infrastructure.Commands.Users
{
	public class Register
	{
		public Guid UserId { get; set; }

		public string Role { get; set; }
		public string Name { get; set; }
		public string Email { get; set; }
		public string Password { get; set; }

	}
}
