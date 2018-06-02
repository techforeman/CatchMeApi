using AutoMapper;
using CatchMe.Core.Domain;
using CatchMe.Infrastructure.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CatchMe.Infrastructure.Mapper
{
	public static class AutoMapperConfig
	{
		public static IMapper Initialize()
			=> new MapperConfiguration(cfg =>
			{
				cfg.CreateMap<Event, EventDTO>()
				.ForMember(x => x.SeatsCount, m => m.MapFrom(p => p.Seats.Count()))
				.ForMember(x => x.AvailableSeatsCount, m => m.MapFrom(p => p.AvailabledSets.Count()))
				.ForMember(x => x.OrderedSeatsCount, m => m.MapFrom(p => p.OrderedSets.Count()));
				cfg.CreateMap<Event, EventDetailsDTO>();
				cfg.CreateMap<User, AccountDTO>();
				cfg.CreateMap<Seat, SeatDTO>();
				cfg.CreateMap<Seat, SeatDetailsDTO>();
			})
			.CreateMapper();

	}

}
