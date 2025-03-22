namespace EventService.DTOs
{
    public class EventForReturn
    {
        public int eventId { get; set; }
        public int customerId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }   
        public DateTime dateTimeCreated { get; set; }
    }
}