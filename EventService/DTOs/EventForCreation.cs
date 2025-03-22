using System.ComponentModel.DataAnnotations;

namespace EventService.DTOs
{
    public class EventForCreation
    {
        [Required]
        public string NAME { get; set; }
        [Required]
        public string DESCRIPTION { get; set; }   
    }
}