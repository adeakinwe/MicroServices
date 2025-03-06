using System.ComponentModel.DataAnnotations;

namespace EventService.Models
{
    public class TBL_CUSTOMER 
    {
        [Key]
        [Required]
        public int CUSTOMERID { get; set; }
        public required ICollection<TBL_EVENT> EVENTS { get; set; }
    }
}