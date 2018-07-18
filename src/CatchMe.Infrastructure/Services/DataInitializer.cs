using CatchMe.Infrastructure.Services;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CatchMe.Infrastructure.Services
{
	public class DataInitializer : IDataInitializer
	{
		private static readonly Logger Logger = LogManager.GetCurrentClassLogger();
		private readonly IUserService _userService;
		private readonly IEventService _eventService;

		public DataInitializer(IUserService userService, IEventService eventService)
		{
			_userService = userService;
			_eventService = eventService;
		}

		public async Task SeedAsync()
		{
			Logger.Info("Initializin data...");
			var tasks = new List<Task>();
			tasks.Add(_userService.RegisterAsync(Guid.NewGuid(), "userId@emailcom", "default user", "secret"));
			tasks.Add(_userService.RegisterAsync(Guid.NewGuid(), "majsterjunior@gmail.com", "Junior", "secret", "admin"));
			Logger.Info("Created users: user, admin");

			for (var i=1; i<=10; i++)
			{
				var eventId = Guid.NewGuid();
				var name = $"Meeting number {i}";
				var description = $"This meeting was created only for dev tests.";
				var startDate = DateTime.UtcNow.AddHours(3);
				var endDate = startDate.AddHours(3);
				tasks.Add(_eventService.CreateAsync(eventId, name, description, startDate, endDate));
				tasks.Add(_eventService.AddSeatAsync(eventId, 15, 0));
				Logger.Info($"Created meeting number {i}");
			}
			await Task.WhenAll(tasks);
			Logger.Info($"Data was initialized.");
		}
	}
}
