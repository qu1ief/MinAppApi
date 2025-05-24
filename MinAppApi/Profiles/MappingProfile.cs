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
            CreateMap<Event, EventGetDto>();
            CreateMap<EventCreateDto, Event>();
            CreateMap<EventUpdateDto, Event>();

            CreateMap<Organizer, OrganizerGetDto>();
            CreateMap<OrganizerCreateDto, Organizer>();
            CreateMap<OrganizerUpdateDto, Organizer>();

            CreateMap<Ticket, TicketGetDto>();
            CreateMap<TicketCreateDto, Ticket>();
            CreateMap<TicketUpdateDto, Ticket>();
        }
    }
}
