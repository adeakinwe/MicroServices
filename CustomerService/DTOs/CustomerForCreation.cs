using System;
using System.Text.Json.Serialization;

namespace CustomerService.DTOs
{
    public class CustomerForCreation
    {
        public string customerCode { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string gender { get; set; }
        public string occupation { get; set; }
        public string address { get; set; }
        [JsonIgnore]
        public int createdBy { get; set; } = 1;
        [JsonIgnore]
        public DateTime dateTimeCreated { get; set; } = DateTime.Now;
    }
}