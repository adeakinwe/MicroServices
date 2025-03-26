using System.Threading.Tasks;
using AutoMapper;
using CustomerService.Interface;
using Grpc.Core;

namespace CustomerService.SyncDataServices.Grpc
{
    public class GrpcCustomerService : GrpcCustomer.GrpcCustomerBase
    {
        private readonly ICustomerRepo _repo;
        private readonly IMapper _mapper;

        public GrpcCustomerService(ICustomerRepo repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public override Task<CustomerResponse>GetAllCustomers(GetAllRequest req, ServerCallContext context)
        {
            var response = new CustomerResponse();
            var customers = _repo.GetAllCustomers();

            customers.ForEach(customer => {
                response.Customer.Add(_mapper.Map<GrpcCustomerModel>(customer));
            });

            return Task.FromResult(response);
        }
    }
}