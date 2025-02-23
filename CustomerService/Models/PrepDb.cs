using System;
using System.Linq;
using System.Threading;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace CustomerService.Models
{
    public static class PrepDb
    {
        public static void PrepPopulate(IApplicationBuilder app, bool isProd)
        {
            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                Console.WriteLine("Starting database migration...");
                try
                {
                    var context = serviceScope.ServiceProvider.GetRequiredService<AppDbContext>();
                    SeedData(context, isProd);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error during database migration: {ex}");
                    throw;  // Preserve stack trace
                }
            }
        }

        private static void SeedData(AppDbContext context, bool isProd)
        {
            if (isProd)
            {
                Console.WriteLine("Running database migration...");
                bool migrationSuccess = false;
                int maxRetries = 5;
                int retryDelay = 5000; // 5 seconds

                for (int i = 0; i < maxRetries; i++)
                {
                    try
                    {
                        context.Database.Migrate();
                        migrationSuccess = true;
                        break;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Migration attempt {i + 1} failed: {ex.Message}");
                        Thread.Sleep(retryDelay); // Wait before retrying
                    }
                }

                if (!migrationSuccess)
                {
                    Console.WriteLine("Migration failed after multiple attempts. Exiting...");
                    return;
                }
            }

            if (!context.TBL_CUSTOMER.Any())
            {
                Console.WriteLine("Seeding customer data...");
                context.TBL_CUSTOMER.AddRange(
                    new TBL_CUSTOMER() { CUSTOMERCODE = "CA101", FIRSTNAME = "Steve", LASTNAME = "Johnson", GENDER = "Male", OCCUPATION = "Developer", ADDRESS = "Lagos", CREATEDBY = 1, DATETIMECREATED = DateTime.UtcNow },
                    new TBL_CUSTOMER() { CUSTOMERCODE = "CA102", FIRSTNAME = "Felicia", LASTNAME = "Williams", GENDER = "Female", OCCUPATION = "Banker", ADDRESS = "Ibadan", CREATEDBY = 1, DATETIMECREATED = DateTime.UtcNow }
                );

                context.SaveChanges();
                Console.WriteLine("Customer data seeded successfully.");
            }
            else
            {
                Console.WriteLine("Customer data already exists. Skipping seeding.");
            }
        }
    }
}