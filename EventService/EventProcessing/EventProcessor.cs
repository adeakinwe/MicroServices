using System.Runtime.Serialization;
using System.Text.Json;
using AutoMapper;
using EventService.DTOs;
using EventService.Enum;
using EventService.Interface;
using EventService.Models;

namespace EventService.EventProcessing
{
    public class EventProcessor : IEventProcessor
    {
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly IMapper _mapper;

        public EventProcessor(IServiceScopeFactory scopeFactory, IMapper mapper)
        {
            _scopeFactory = scopeFactory;
            _mapper = mapper;
        }

        public void ProcessEvent(string msg)
        {
            var eventType = DetermineEvent(msg);
            if (eventType.Equals(EventType.CustomerPublished))
            {
                AddPlatform(msg);
            }
            else
            {
                Console.WriteLine("Event is not a customer published");
            }
        }

        private EventType DetermineEvent(string eventMessage)
        {
            Console.WriteLine("Determining event type");

            var eventType = JsonSerializer.Deserialize<GenericEventDto>(eventMessage);

            switch(eventType.Event)
            {
                case "Customer_Published":
                    Console.WriteLine("Customer published event detected");
                    return EventType.CustomerPublished;
                default:
                    Console.WriteLine("Event is not a customer published");
                    return EventType.Undetermined;
            }
        }
    
        private void AddPlatform(string publishedCustomer)
        {
            using (var scope = _scopeFactory.CreateScope()){
                var repo = scope.ServiceProvider.GetRequiredService<IEvent>();

                var deserializedPublishedCustomer = JsonSerializer.Deserialize<CustomerPublishedForReturn>(publishedCustomer);

                try
                {
                    var cust = _mapper.Map<TBL_CUSTOMER>(deserializedPublishedCustomer);
                    repo.CreateCustomer(cust);
                }
                catch(Exception ex)
                {
                    Console.WriteLine($"An error occured while adding customer {ex.Message}");
                }
            }
        }
    }
}