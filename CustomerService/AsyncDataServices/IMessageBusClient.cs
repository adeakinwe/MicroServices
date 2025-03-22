using CustomerService.DTOs;

namespace CustomerService.AsyncDataServices
{
    public interface IMessageBusClient
    {
        void PublishNewCustomer(CustomerPublishedForCreation customerPublishedForCreation);
    }
}