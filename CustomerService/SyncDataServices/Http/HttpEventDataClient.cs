using System;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using CustomerService.DTOs;
using Microsoft.Extensions.Configuration;

namespace CustomerService.SyncDataServices.Http
{
    public class HttpEventDataClient : IEventDataClient
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _config;

        public HttpEventDataClient(HttpClient httpClient, IConfiguration config)
        {
            _httpClient = httpClient;
            _config = config;
        }
        public async Task SendCustomerCreatedToEventService(CustomerForReturn cust)
        {
            var httpContent = new StringContent(
                JsonSerializer.Serialize(cust),
                Encoding.UTF8,
                "application/json"
            );

            var response = await _httpClient.PostAsync($"{_config["eventservicebaseurl"]}/api/v1/customer", httpContent); 

            if (response.IsSuccessStatusCode){
                Console.WriteLine("Post to event service successfull!");
            }
            else {
                Console.WriteLine("Post to event service failed!");
            }
        }
    }
}