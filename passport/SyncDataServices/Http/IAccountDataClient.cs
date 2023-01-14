using System.Threading.Tasks;
using passport.Dtos;

namespace passport.SyncDataServices.Http
{
    public interface IAccountDataClient
    {
        Task SendPassportToAccount(PassportReadDto pass); 
    }
}