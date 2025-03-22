using AutoMapper;
using CustomerService.DTOs;
using CustomerService.Models;

namespace CustomerService.Profiles
{
    public  class CustomerProfile : Profile
    {
        public CustomerProfile()
        {
            //Source => Target
            CreateMap<TBL_CUSTOMER, CustomerForReturn>();
            CreateMap<CustomerForCreation, TBL_CUSTOMER>();
            CreateMap<CustomerForReturn,CustomerPublishedForCreation>();
        }
    }
}