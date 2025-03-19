namespace EventService.DTOs
{
    public class EventForReturn
    {
        public int eventId { get; set; }
        public int customerId { get; set; }
        public required string Name { get; set; }
        public required string Description { get; set; }   
        public DateTime dateTimeCreated { get; set; }
    }
}