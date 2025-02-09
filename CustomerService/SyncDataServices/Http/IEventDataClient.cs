using System.Threading.Tasks;
using CustomerService.DTOs;

namespace CustomerService.SyncDataServices.Http
{
    public interface IEventDataClient
    {
        Task SendCustomerCreatedToEventService(CustomerForReturn cust);
    }
}