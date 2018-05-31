using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CatchMe.Infrastructure.Services;
using Microsoft.AspNetCore.Authorization;
using CatchMe.Infrastructure.Commands.Users;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace CatchMe.Api.Controllers
{
	public class AccountController : ApiControllerBase
	{
		private readonly IUserService _userService;
		private readonly ISeatService _seatsService;

		public AccountController(IUserService userService, ISeatService seatsService)
		{
			_userService = userService;
			_seatsService = seatsService;
		}


		[HttpGet]
		[Authorize]
		public async Task<IActionResult> Get()
		=> Json(await _userService.GetAccountAsync(UserId));

		[HttpGet("seats")]
		public async Task<IActionResult> GetSeats()
		=> Json(await _seatsService.GetForUserAsync(UserId));

		[HttpPost("register")]
		public async Task<IActionResult> Post([FromBody]Register cmd)
		{
			await _userService.RegisterAsync(Guid.NewGuid(), cmd.Email, cmd.Name, cmd.Password, cmd.Role);

			return Created("/account", null);
		}


		[HttpPost("login")]
		public async Task<IActionResult> Post([FromBody]Login cmd)
		=> Json(await _userService.LoginAsync(cmd.Email, cmd.Password));

	}
}
