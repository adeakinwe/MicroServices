using EventService.Models;

namespace EventService.Interface 
{
    public interface IEvent
    {
        bool SaveChanges();
        bool IsCustomerExist(int customerId);
        IEnumerable<TBL_CUSTOMER> GetAllCustomers();
        TBL_CUSTOMER GetCustomerById(int customerId);
        void CreateCustomer(TBL_CUSTOMER customer);

        IEnumerable<TBL_EVENT> GetAllEvents();
        IEnumerable<TBL_EVENT> GetEventsByCustomerId(int customerId);
        TBL_EVENT GetEventByEventIdAndCustomerId(int eventId, int customerId);
        void CreateEvent(TBL_EVENT evt, int customerId);
    }
}