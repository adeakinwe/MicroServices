using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CustomerService.Models
{
    [Table("TBL_CUSTOMER")]
    public class TBL_CUSTOMER
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CUSTOMERID { get; set; }
        [Required]
        public string CUSTOMERCODE { get; set; }
        [Required]
        public string FIRSTNAME { get; set; }
        [Required]
        public string LASTNAME { get; set; }
        [Required]
        [MaxLength(10)]
        public string GENDER { get; set; }
        [Required]
        public string OCCUPATION { get; set; }
        [Required]
        public string ADDRESS { get; set; }
        [Required]
        public int CREATEDBY { get; set; }
        [Required]
        public DateTime DATETIMECREATED { get; set; }
        public int? LASTUPDATEDBY { get; set; }
        public DateTime? DATETIMEUPDATED { get; set; }
    }
}