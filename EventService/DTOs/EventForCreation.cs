using System.ComponentModel.DataAnnotations;

namespace EventService.DTOs
{
    public class EventForCreation
    {
        [Required]
        public required string NAME { get; set; }
        [Required]
        public required string DESCRIPTION { get; set; }   
    }
}