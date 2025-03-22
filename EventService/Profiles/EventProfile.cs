using AutoMapper;
using EventService.DTOs;
using EventService.Models;

namespace EventService.Profiles
{
    public class EventProfile : Profile
    {
        public EventProfile()
        {
            //Source => Target
            CreateMap<TBL_CUSTOMER, CustomerForReturn>();
            CreateMap<EventForCreation, TBL_EVENT>();
            CreateMap<TBL_EVENT, EventForReturn>();
            CreateMap<CustomerPublishedForReturn, TBL_CUSTOMER>()
                .ForMember(dest => dest.CUSTOMERID, opt => opt.MapFrom(src => src.customerId));
        }
    }
}