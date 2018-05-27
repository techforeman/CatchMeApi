using AutoMapper;
using CatchMe.Core.Domain;
using CatchMe.Core.Repository;
using CatchMe.Infrastructure.DTO;
using CatchMe.Infrastructure.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CatchMe.Infrastructure.Services
{
	public class UserService : IUserService
	{
		private readonly IUserRepository _userRepository;
		private readonly IJwtHandler _jwtHandler;
		private readonly IMapper _mapper;

		public UserService(IUserRepository userRepository, IJwtHandler jwtHandler, IMapper mapper)
		{
			_userRepository = userRepository;
			_jwtHandler = jwtHandler;
			_mapper = mapper;
		}

		public async Task<TokenDTO> LoginAsync(string email, string password)
		{
			var user = await _userRepository.GetAsync(email);
			if (user == null)
			{
				throw new Exception($"Invalid credentials.");
			}
			if (user.Password != password)
			{
				throw new Exception($"Invalid credentials.");
			}
			var jwt = _jwtHandler.CreateToken(user.Id, user.Role);

			return new TokenDTO
			{
				Token = jwt.Token,
				Expires = jwt.Expires,
				Role = user.Role
			};

		}

		public async Task RegisterAsync(Guid userId, string email, string name, string password, string role = "user")
		{
			var user = await _userRepository.GetAsync(email);
			if (user != null)
			{
				throw new Exception($"User with this email: '{email}' already exist.");
			}
			user = new User(userId, role, name, email, password);
			await _userRepository.AddAsync(user);
		}

		public async Task<AccountDTO> GetAccountAsync(Guid userId)
		{
			var user = await _userRepository.GetOrFailAsync(userId);
			return _mapper.Map<AccountDTO>(user);
		}
	}
}
