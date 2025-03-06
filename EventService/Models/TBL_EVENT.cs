using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EventService.Models
{
    [Table("TBL_EVENT")]
    public class TBL_EVENT
    {
        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int EVENTID { get; set; }
        public required string NAME { get; set; }
        public int CUSTOMERID { get; set; }
        public required string DESCRIPTION { get; set; }
        public DateTime DATECREATED { get; set; } = DateTime.Now;
        public required TBL_CUSTOMER CUSTOMER { get; set; }
    }
}