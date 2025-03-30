using EventService.Models;

namespace CustomerService.SyncDataServices.Grpc
{
    public interface ICustomerDataClient
    {
        IEnumerable<TBL_CUSTOMER> ReturnAllCustomers();
    }
}