using CatchMe.Infrastructure.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CatchMe.Infrastructure.Services
{
	public interface IJwtHandler
	{
		JwtDTO CreateToken(Guid userId, string role);
	}
}
