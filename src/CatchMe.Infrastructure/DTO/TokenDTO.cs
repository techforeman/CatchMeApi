using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CatchMe.Infrastructure.DTO
{
	public class TokenDTO
	{
		public string Token { get; set; }
		public long Expires { get; set; }
		public string Role { get; set; }
	}
}
