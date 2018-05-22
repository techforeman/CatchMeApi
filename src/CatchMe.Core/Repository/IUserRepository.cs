using CatchMe.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CatchMe.Core.Repository
{
	public interface IUserRepository
	{
		Task<User> GetAsync(Guid id);
		Task<User> GetAsync(string email);

		Task AddAsync(User user);
		Task UpdateAsync(User user);
		Task DeleteAsync(User user);
	}
}
