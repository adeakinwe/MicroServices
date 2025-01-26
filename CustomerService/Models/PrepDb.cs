using System;
using System.Linq;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace CustomerService.Models
{
    public static class PrepDb
    {
        public static void PrepPopulate(IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                SeedData(serviceScope.ServiceProvider.GetService<AppDbContext>());
            }
        }

        private static void SeedData(AppDbContext context)
        {
            if (!context.TBL_CUSTOMER.Any())
            {
                context.TBL_CUSTOMER.AddRange(
                    new TBL_CUSTOMER() {CUSTOMERCODE="CA101", FIRSTNAME="Steve", LASTNAME="Johnson", GENDER="Male", OCCUPATION="Developer", ADDRESS="Lagos", CREATEDBY=1, DATETIMECREATED=DateTime.Now},
                    new TBL_CUSTOMER() {CUSTOMERCODE="CA102", FIRSTNAME="Felicia", LASTNAME="Williams", GENDER="Female", OCCUPATION="Banker", ADDRESS="Ibadan", CREATEDBY=1, DATETIMECREATED=DateTime.Now}
                );
            };

            context.SaveChanges();
        }
    }
}