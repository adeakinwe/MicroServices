using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using CustomerService.AsyncDataServices;
using CustomerService.DTOs;
using CustomerService.Interface;
using CustomerService.Models;
using CustomerService.SyncDataServices.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;

namespace CustomerService.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class CustomerController: ControllerBase
    {
        private readonly ICustomerRepo _repo;
        private readonly IMapper _mapper;
        private readonly IEventDataClient _eventDataClient;
        private readonly IMessageBusClient _messageBusClient;

        public CustomerController(
            ICustomerRepo repo, 
            IMapper mapper, 
            IEventDataClient eventDataClient,
            IMessageBusClient messageBusClient
            )
        {
            _repo = repo;
            _mapper = mapper;
            _eventDataClient = eventDataClient;
            _messageBusClient = messageBusClient;
        }

        [HttpGet("all")]
        public ActionResult<List<CustomerForReturn>> GetAllCustomers()
        {
            var customers = _repo.GetAllCustomers();

            return Ok(_mapper.Map<List<CustomerForReturn>>(customers));
        }

        [HttpGet("{customerId:int}")]
        public ActionResult<List<CustomerForReturn>> GetCustomerById(int customerId)
        {
            var customer = _repo.GetCustomerById(customerId);

            if (customer == null) return NotFound();

            return Ok(_mapper.Map<CustomerForReturn>(customer));
        }

        [HttpPost("add-customer")]
        public async Task<ActionResult<CustomerForReturn>> AddCustomer(CustomerForCreation customer)
        {
            try 
            {
                var newCustomer = _mapper.Map<TBL_CUSTOMER>(customer);
                _repo.AddCustomer(newCustomer);
    
                var addedCustomer = _mapper.Map<CustomerForReturn>(newCustomer);

                //Send Sync Message
                try 
                {
                    await _eventDataClient.SendCustomerCreatedToEventService(addedCustomer);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"An error occurred while sending created customer to event service: {ex.Message}");
                }

                //Send Async Message to RabbitMQ
                try
                {
                    var customerPublishedModel = _mapper.Map<CustomerPublishedForCreation>(addedCustomer);
                    customerPublishedModel.Event = "Customer_Published";
                    _messageBusClient.PublishNewCustomer(customerPublishedModel);
                }
                catch(Exception ex)
                {
                    Console.WriteLine($"An error occurred while sending created customer asynchronously to RabbitMQ: {ex.Message}");
                }
                return CreatedAtRoute(new {Id = addedCustomer.customerId}, addedCustomer);
            } 
            catch (Exception e) 
            {
                return new ObjectResult(e.Message) { StatusCode = 500 };
            }
        }

        [HttpPut("update-customer/{customerId:int}")]
        public ActionResult<bool> UpdateCustomer(CustomerForUpdate customer, int customerId)
        {
            try 
            {
                _repo.UpdateCustomer(customer, customerId);
    
                return Ok();
            } 
            catch (Exception e) 
            {
                return new ObjectResult(e.Message) { StatusCode = 500 };
            }
        }

    }
}