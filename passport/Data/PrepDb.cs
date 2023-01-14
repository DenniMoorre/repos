using System;
using System.Linq;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using passport.Models;

namespace passport.Data
{
    public static class PrepDb
    {
        public static void PrepPopulation(IApplicationBuilder app, bool isProd)
        {
            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                SeedData(serviceScope.ServiceProvider.GetService<AppDbContext>(), isProd);
            }
        }

        private static void SeedData(AppDbContext context, bool isProd)
        {
            if (isProd)
            {
                Console.WriteLine("--> Attempting to apply migrations...");
                try
                {
                    context.Database.Migrate();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"--> Could not run migrations: {ex.Message}");
                }
            }

            if (!context.Passports.Any())
            {
                Console.WriteLine("--> Seeding Data...");

                context.Passports.AddRange(
                    new Passport() { PassportNumber = 1273648, AccountData = "Cool", Status = "Nice", AuthorityThatIssued = "asdf" },
                    new Passport() { PassportNumber = 6539900, AccountData = "Ghsjdfhf", Status = "Not Nice", AuthorityThatIssued = "asdf" },
                    new Passport() { PassportNumber = 9758123, AccountData = "asdfdff", Status = "Very Nice", AuthorityThatIssued = "asdf" }
                );

                context.SaveChanges();
            }
            else
            {
                Console.WriteLine("--> We already have data");
            }
        }
    }
}














