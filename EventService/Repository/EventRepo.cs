using EventService.Interface;
using EventService.Models;

namespace EventService.Repository
{
    public class EventRepo : IEvent
    {
        private readonly AppDbContext context;
        public EventRepo(AppDbContext _context)
        {
            context = _context;
        }
        public void CreateCustomer(TBL_CUSTOMER customer)
        {
            ArgumentNullException.ThrowIfNull(customer);

            context.TBL_CUSTOMER.Add(customer);
        }

        public void CreateEvent(TBL_EVENT evt, int customerId)
        {
            ArgumentNullException.ThrowIfNull(evt);

            evt.CUSTOMERID = customerId;
            context.TBL_EVENT.Add(evt);
            SaveChanges();
        }

        public IEnumerable<TBL_CUSTOMER> GetAllCustomers()
        {
            return context.TBL_CUSTOMER.ToList();
        }

        public IEnumerable<TBL_EVENT> GetAllEvents()
        {
            return context.TBL_EVENT.ToList();
        }

        public TBL_CUSTOMER GetCustomerById(int customerId)
        {
            return context.TBL_CUSTOMER.First(c=> c.CUSTOMERID == customerId);
        }  

        public TBL_EVENT GetEventByCustomerIdAndEventId(int customerId, int eventId)
        {
            var evt = context.TBL_EVENT.Where(e => e.EVENTID == eventId && e.CUSTOMERID == customerId).First();

            return evt;
        }

        public IEnumerable<TBL_EVENT> GetEventsByCustomerId(int customerId)
        {
            return context.TBL_EVENT.Where(e => e.CUSTOMERID == customerId).ToList();
        }

        public bool IsCustomerExist(int customerId)
        {
            return context.TBL_CUSTOMER.Any(c => c.CUSTOMERID == customerId);
        }

        public bool SaveChanges()
        {
            return context.SaveChanges() > 0;
        }
    }
}