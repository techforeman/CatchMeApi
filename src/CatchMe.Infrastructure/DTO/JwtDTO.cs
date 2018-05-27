using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CatchMe.Infrastructure.DTO
{
	public class JwtDTO
	{
		public string Token { get; set; }
		public long Expires { get; set; }
	}

}
