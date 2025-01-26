using System;

namespace CustomerService.DTOs
{
    public class CustomerForReturn
    {
        public int customerId { get; set; }
        public string customerCode { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string gender { get; set; }
        public string occupation { get; set; }
        public string address { get; set; }
        public DateTime dateTimeCreated { get; set; }
    }
}