using System;
using System.Text.Json.Serialization;

namespace CustomerService.DTOs
{
    public class CustomerForUpdate
    {
        [JsonIgnore]
        public string customerCode { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string gender { get; set; }
        public string occupation { get; set; }
        public string address { get; set; }
        [JsonIgnore]
        public int lastUpdatedBy { get; set; } = 1;
        [JsonIgnore]
        public DateTime dateTimeUpdated { get; set; } = DateTime.Now;
    }
}