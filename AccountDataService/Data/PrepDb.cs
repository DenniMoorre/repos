using System;
using System.Collections.Generic;
using AccountDataService.Models;
using AccountDataService.SyncDataServices.Grpc;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace AccountDataService.Data
{
    public static class PrepDb
    {
        public static void PrepPopulation(IApplicationBuilder applicationBuilder)
        {
            using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
            {
                var grpcClient = serviceScope.ServiceProvider.GetService<IPassportDataClient>();

                var passports = grpcClient.ReturnAllPassports();

                SeedData(serviceScope.ServiceProvider.GetService<IAccountDataRepo>(), passports);
            }
        }
        
        private static void SeedData(IAccountDataRepo repo, IEnumerable<Passport> passports)
        {
            Console.WriteLine("Seeding new platforms...");

            foreach (var pass in passports)
            {
                if(!repo.ExternalPassportExists(pass.ExternalID))
                {
                    repo.CreatePassport(pass);
                }
                repo.SaveChanges();
            }
        }
    }
}