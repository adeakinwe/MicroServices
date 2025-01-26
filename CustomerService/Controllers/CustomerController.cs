using System;
using System.Collections.Generic;
using AutoMapper;
using CustomerService.DTOs;
using CustomerService.Interface;
using CustomerService.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;

namespace CustomerService.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class CustomerController: ControllerBase
    {
        private readonly ICustomerRepo repo;
        private readonly IMapper mapper;

        public CustomerController(ICustomerRepo _repo, IMapper _mapper)
        {
            repo = _repo;
            mapper = _mapper;
        }

        [HttpGet("all")]
        public ActionResult<List<CustomerForReturn>> GetAllCustomers()
        {
            var customers = repo.GetAllCustomers();

            return Ok(mapper.Map<List<CustomerForReturn>>(customers));
        }

        [HttpGet("{customerId:int}")]
        public ActionResult<List<CustomerForReturn>> GetCustomerById(int customerId)
        {
            var customer = repo.GetCustomerById(customerId);

            if (customer == null) return NotFound();

            return Ok(mapper.Map<CustomerForReturn>(customer));
        }

        [HttpPost("add-customer")]
        public ActionResult<CustomerForReturn> AddCustomer(CustomerForCreation customer)
        {
            try 
            {
                var newCustomer = mapper.Map<TBL_CUSTOMER>(customer);
                repo.AddCustomer(newCustomer);
    
                var addedCustomer = mapper.Map<CustomerForReturn>(newCustomer);
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
                repo.UpdateCustomer(customer, customerId);
    
                return Ok();
            } 
            catch (Exception e) 
            {
                return new ObjectResult(e.Message) { StatusCode = 500 };
            }
        }

    }
}