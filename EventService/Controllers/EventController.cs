using System;
using System.Collections.Generic;
using AutoMapper;
using EventService.DTOs;
using EventService.Interface;
using EventService.Models;


//using AutoMapper;
//using CustomerService.DTOs;
//using CustomerService.Interface;
//using CustomerService.Models;
using Microsoft.AspNetCore.Mvc;

namespace EventService.Controllers
{
    [Route("api/v1/event/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly IEvent _repo;
        private readonly IMapper _mapper;

        public CustomerController(IEvent repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        [HttpGet("")]
        public ActionResult<int> GetRandomCustomerId()
        {
            Random rand = new Random();
            return Ok(rand.Next(1, 21));
        }

        [HttpGet("all-customers")]
        public ActionResult<IEnumerable<TBL_CUSTOMER>> GetAllCustomers()
        {
            var customers = _repo.GetAllCustomers();
            return Ok(_mapper.Map<IEnumerable<CustomerForReturn>>(customers));
        }
        [HttpPost]
        public ActionResult CreatedCustomer()
        {
            Console.WriteLine("created customer successfully posted to event service");
            
            return Ok();
        }

    }
}