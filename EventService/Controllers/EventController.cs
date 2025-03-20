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
    [Route("api/v1/[controller]")]
    [ApiController]
    public class EventController : ControllerBase
    {
        private readonly IEvent _repo;
        private readonly IMapper _mapper;

        public EventController(IEvent repo, IMapper mapper)
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

        [HttpGet("all-events")]
        public ActionResult<IEnumerable<EventForReturn>> GetAllEvents()
        {
            var events = _repo.GetAllEvents();
            return Ok(_mapper.Map<IEnumerable<EventForReturn>>(events));
        }

        [HttpGet("customer-events/{customerId}")]
        public ActionResult<IEnumerable<EventForReturn>> GetEventsByCustomerId(int customerId)
        {
            if (!_repo.IsCustomerExist(customerId))
            {
                return NotFound();
            }

            var customerEvents = _repo.GetEventsByCustomerId(customerId);

            return Ok(_mapper.Map<IEnumerable<EventForReturn>>(customerEvents));
        }

        [HttpGet("customer-event/{customerId}/{eventId}", Name = "GetEventByCustomerIdAndEventId")]
        public ActionResult<EventForReturn> GetEventByCustomerIdAndEventId(int customerId, int eventId)
        {
            if (!_repo.IsCustomerExist(customerId))
            {
                return NotFound();
            }

            var event_ = _repo.GetEventByCustomerIdAndEventId(customerId, eventId);

            if (event_ == null) return NotFound();

            return Ok(_mapper.Map<EventForReturn>(event_));
        }

        [HttpPost("create-event")]
        public ActionResult<EventForReturn> CreateEvent(EventForCreation eventForCreation, int customerId)
        {
            if (!_repo.IsCustomerExist(customerId))
            {
                return NotFound();
            }

            var event_ = _mapper.Map<TBL_EVENT>(eventForCreation);

            _repo.CreateEvent(event_, customerId);

            return CreatedAtRoute(nameof(GetEventByCustomerIdAndEventId), new {customerId = customerId, eventId = event_.EVENTID});
        }

        [HttpPost]
        public ActionResult CreatedCustomer()
        {
            Console.WriteLine("created customer successfully posted to event service");
            
            return Ok();
        }

    }
}