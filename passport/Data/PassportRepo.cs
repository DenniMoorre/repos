using passport.Models;
using System;
using System.Collections.Generic;
using System.Linq;
namespace passport.Data
{
    public class PassportRepo : IPassportRepo
    {
        private readonly AppDbContext _context;



        public PassportRepo(AppDbContext context) 
        {
            _context = context;
        }
        public void CreatePassport(Passport pass)
        {
           if (pass == null)
            {
                throw new ArgumentNullException(nameof(pass)); 
            }

            _context.Passports.Add(pass);
        }

        public IEnumerable<Passport> GetAllPassports()
        {
           return _context.Passports.ToList();
        }

        public Passport GetPassportById(int id)
        {
            return _context.Passports.FirstOrDefault(p => p.Id == id);
        }

        public bool SaveChange()
        {
            return (_context.SaveChanges() >= 0);
        }
    }
}
