using System.Collections.Generic;
using AccountDataService.Models;

namespace AccountDataService.SyncDataServices.Grpc
{
    public interface IPassportDataClient
    {
        IEnumerable<Passport> ReturnAllPassports();
    }
}