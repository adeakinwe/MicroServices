using EventService.Interface;
using EventService.SyncDataServices.Grpc;

namespace EventService.Models
{
    public static  class PrepDb
    {
        public static void PrepPopulate(IApplicationBuilder applicationBuilder)
        {
        using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope()) 
        {
            var grpcClient = serviceScope.ServiceProvider.GetService<ICustomerDataClient>();

            var customers = grpcClient.ReturnAllCustomers();

            SeedData(serviceScope.ServiceProvider.GetService<IEvent>(), customers);
        }

        }

        private static void SeedData(IEvent repo, IEnumerable<TBL_CUSTOMER> customers)
        {
            Console.WriteLine("seeding new customers...");

            foreach (var cust in customers)
            {
                if (!repo.IsCustomerExist(cust.CUSTOMERID))
                {
                    repo.CreateCustomer(cust);
                }

                repo.SaveChanges();
            }
        }
    }
}