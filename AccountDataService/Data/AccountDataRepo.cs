using System;
using System.Collections.Generic;
using System.Linq;
using AccountDataService.Models;

namespace AccountDataService.Data
{
    public class AccountDataRepo : IAccountDataRepo
    {
        private readonly AppDbContext _context;

        public AccountDataRepo(AppDbContext context)
        {
            _context = context;
        }

        public void CreateAccountData(int passportId, AccountData accountData)
        {
            if (accountData == null)
            {
                throw new ArgumentNullException(nameof(accountData));
            }

            accountData.PassportId = passportId;
            _context.AccountData.Add(accountData);
        }

        public void CreatePassport(Passport pass)
        {
            if(pass == null)
            {
                throw new ArgumentNullException(nameof(pass));
            }
            _context.Passports.Add(pass);
        }

        public bool ExternalPassportExists(int externalPassportId)
        {
            return _context.Passports.Any(p => p.ExternalID == externalPassportId);
        }

        public IEnumerable<Passport> GetAllPassports()
        {
            return _context.Passports.ToList();
        }

        public AccountData GetAccount(int passportId, int accountDataId)
        {
            return _context.AccountData
                .Where(c => c.PassportId == passportId && c.Id == accountDataId).FirstOrDefault();
        }

        public IEnumerable<AccountData> GetAccountDataForPassport(int passportId)
        {
            return _context.AccountData
                .Where(c => c.PassportId == passportId)
                .OrderBy(c => c.Passport.Name);
        }

        public bool PassportExits(int passportId)
        {
            return _context.Passports.Any(p => p.Id == passportId);
        }

        public bool SaveChanges()
        {
            return (_context.SaveChanges() >= 0);
        }

        public Passport GetAccountData(int passportId, int accountDataId)
        {
            throw new NotImplementedException();
        }
    }
}