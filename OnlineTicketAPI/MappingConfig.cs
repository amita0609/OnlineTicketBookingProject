using AutoMapper;
using OnlineTicketData.Models;
using OnlineTicketData.Models.DTO;

namespace OnlineTicketAPI
{
    public class MappingConfig :Profile
    {
        public MappingConfig()
        {
            CreateMap<Event, EventDTO>().ReverseMap();
            CreateMap<TicketBooking ,TicketBookingDTO>().ReverseMap();
        }
    }
}
