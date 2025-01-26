using System;
using System.Collections.Generic;
using System.Linq;
using CustomerService.DTOs;
using CustomerService.Interface;
using CustomerService.Models;

namespace CustomerService.Repository 
{
    public class CustomerRepo : ICustomerRepo
    {
        private readonly AppDbContext context;
        public CustomerRepo(AppDbContext _context)
        {
            context = _context;
        }
        public void AddCustomer(TBL_CUSTOMER customer)
        {
            if (customer == null)
            {
                throw new ArgumentNullException(nameof(customer));
            }

            if (context.TBL_CUSTOMER.Any(c => c.CUSTOMERCODE.ToUpper() == customer.CUSTOMERCODE.ToUpper()))
            {
                throw new Exception($"Customer Code '{customer.CUSTOMERCODE}' already exist");
            }
            
            context.TBL_CUSTOMER.Add(customer);
            context.SaveChanges();
        }

        public List<TBL_CUSTOMER> GetAllCustomers()
        {
            var customers = context.TBL_CUSTOMER.ToList();
            return customers;
        }

        public TBL_CUSTOMER GetCustomerById(int customerId)
        {
            var customer = context.TBL_CUSTOMER.Where(c => c.CUSTOMERID.Equals(customerId)).FirstOrDefault();
            return customer;
        }

        public void UpdateCustomer(CustomerForUpdate customer, int customerId)
        {
            if (customer == null) 
                throw new ArgumentNullException(nameof(customer));

            var customerToUpdate = context.TBL_CUSTOMER.Find(customerId) ?? throw new Exception("Customer not found");

            customerToUpdate.CUSTOMERCODE = customer.customerCode;
            customerToUpdate.FIRSTNAME = customer.firstName;
            customerToUpdate.LASTNAME = customer.lastName;
            customerToUpdate.ADDRESS = customer.address;
            customerToUpdate.OCCUPATION = customer.occupation;
            customerToUpdate.GENDER = customer.gender;

            context.SaveChanges();
        }
    }
}