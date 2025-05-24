using AutoMapper;
using MinAppApi.Dtos.Event;
using MinAppApi.Dtos.Organizer;
using MinAppApi.Dtos.Ticket;
using MinAppApi.Entities;

namespace MinAppApi.Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Event, EventGetDto>().ReverseMap();
            CreateMap<EventCreateDto, Event>().ReverseMap();
            CreateMap<EventUpdateDto, Event>().ReverseMap();

            CreateMap<Organizer, OrganizerGetDto>().ReverseMap();
            CreateMap<OrganizerCreateDto, Organizer>().ReverseMap();
            CreateMap<OrganizerUpdateDto, Organizer>().ReverseMap();

            CreateMap<Ticket, TicketGetDto>().ReverseMap();
            CreateMap<TicketCreateDto, Ticket>().ReverseMap();
            CreateMap<TicketUpdateDto, Ticket>().ReverseMap();
        }
    }
}
