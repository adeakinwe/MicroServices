using System.Collections.Generic;
using CustomerService.DTOs;
using CustomerService.Models;

namespace CustomerService.Interface
{
    public interface ICustomerRepo
    {
        List<TBL_CUSTOMER> GetAllCustomers();
        TBL_CUSTOMER GetCustomerById(int customerId);
        void AddCustomer(TBL_CUSTOMER customer);
        void UpdateCustomer(CustomerForUpdate customer, int customerId);
    }
}