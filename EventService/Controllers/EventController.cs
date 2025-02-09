using System;
using System.Collections.Generic;
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
        public CustomerController()
        {
            
        }

        [HttpGet("")]
        public ActionResult<int> GetRandomCustomerId()
        {
            Random rand = new Random();
            return Ok(rand.Next(1, 21));
        }

        [HttpPost]
        public ActionResult CreatedCustomer()
        {
            Console.WriteLine("created customer successfully posted to event service");
            
            return Ok();
        }

    }
}