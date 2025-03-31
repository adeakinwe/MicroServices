using AutoMapper;
using CustomerService;
using EventService.SyncDataServices.Grpc;
using EventService.Models;
using Grpc.Net.Client;

namespace EventService.SyncDataServices.Grpc
{
    public class CustomerDataClient : ICustomerDataClient
    {
        private IConfiguration _config;
        private IMapper _mapper;

        public CustomerDataClient(IConfiguration config, IMapper mapper)
        {
            _config = config;
            _mapper = mapper;
        }
        public IEnumerable<TBL_CUSTOMER> ReturnAllCustomers()
        {
            Console.WriteLine($"Calling GRPC service {_config["GrpcCustomer"]}");

            var channel = GrpcChannel.ForAddress(_config["GrpcCustomer"]);
            var client = new GrpcCustomer.GrpcCustomerClient(channel);
            var request = new GetAllRequest();

            try
            {
                var reply = client.GetAllCustomers(request);
                return _mapper.Map<IEnumerable<TBL_CUSTOMER>>(reply.Customer);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Could not reach GRPC service {ex.Message}");
            }

            return new List<TBL_CUSTOMER>();
        }
    }
}