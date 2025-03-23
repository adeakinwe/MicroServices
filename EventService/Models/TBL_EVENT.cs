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
        public string NAME { get; set; }
        public int CUSTOMERID { get; set; }
        public string DESCRIPTION { get; set; }
        public DateTime DATETIMECREATED { get; set; } = DateTime.Now;
        public TBL_CUSTOMER CUSTOMER { get; set; }
    }
}