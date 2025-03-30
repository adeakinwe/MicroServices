using AutoMapper;
using CustomerService.SyncDataServices.Grpc;
using EventService.Models;

namespace EventService.SyncDataServices.Grpc
{
    public class CustomerDataClient : ICustomerDataClient
    {
        public CustomerDataClient(IConfiguration config, IMapper mapper)
        {
            
        }
        public IEnumerable<TBL_CUSTOMER> ReturnAllCustomers()
        {
            throw new NotImplementedException();
        }
    }
}