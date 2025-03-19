using AutoMapper;
using EventService.DTOs;
using EventService.Models;

namespace EventService.Profiles
{
    public class EventProfile : Profile
    {
        public EventProfile()
        {
            CreateMap<TBL_CUSTOMER, CustomerForReturn>();
            CreateMap<EventForCreation, TBL_EVENT>();
            CreateMap<TBL_EVENT, EventForReturn>();
        }
    }
}