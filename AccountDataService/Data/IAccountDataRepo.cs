using System.Collections.Generic;
using AccountDataService.Models;

namespace AccountDataService.Data
{
    public interface IAccountDataRepo
    {
        bool SaveChanges();

        // Platforms
        IEnumerable<Passport> GetAllPassports();
        void CreatePassport(Passport pass);
        bool PassportExits(int passportId);
        bool ExternalPassportExists(int externalPassportId);

        // Commands
        IEnumerable<AccountData> GetAccountDataForPassport(int passportId);
        Passport GetAccountData(int passportId, int accountDataId);
        void CreateAccountData(int passportId, AccountData accountData);
    }
}