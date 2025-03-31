using EventService.Models;

namespace EventService.SyncDataServices.Grpc
{
    public interface ICustomerDataClient
    {
        IEnumerable<TBL_CUSTOMER> ReturnAllCustomers();
    }
}