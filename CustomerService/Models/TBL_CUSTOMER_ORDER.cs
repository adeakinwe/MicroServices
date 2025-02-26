using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CustomerService.Models
{
    [Table("TBL_CUSTOMER_ORDER")]
    public class TBL_CUSTOMER_ORDER
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CUSTOMERORDERID { get; set; }
        [ForeignKey("TBL_CUSTOMER")]
        [Required]
        public int CUSTOMERID { get; set; }
        [Required]
        public int ORDERRECEIVEDBY { get; set; }
        [Required]
        public DateTime ORDERDATE { get; set; }
        public string DESCRIPTION { get; set; }
        [Required]
        public decimal AMOUNT { get; set; }
        [Required]
        public string DELIVERYADDRESS { get; set; }
        public int DELETED { get; set; }
        public TBL_CUSTOMER CUSTOMER { get; set;}
    }
}