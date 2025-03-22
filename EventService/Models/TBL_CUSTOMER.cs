using System.ComponentModel.DataAnnotations;

namespace EventService.Models
{
    public class TBL_CUSTOMER 
    {
        [Key]
        [Required]
        public int ID { get; set; }
        public int CUSTOMERID { get; set; }
        public string CUSTOMERCODE { get; set; }
        public ICollection<TBL_EVENT> EVENTS { get; set; }
    }
}