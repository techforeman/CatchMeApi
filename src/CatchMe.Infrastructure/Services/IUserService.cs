using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CatchMe.Infrastructure.Services
{
	public interface IUserService
	{
		Task<AccountDTO> GetAccountAsync(Guid userId);
		Task RegisterAsync(Guid userId, string email, string name, string password,
			string role = "user");

		Task<TokenDTO> LoginAsync(string email, string password);
	}
}
