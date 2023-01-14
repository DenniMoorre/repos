using passport.Models;
using System.Collections;
using System.Collections.Generic;

namespace passport.Data
{
    public interface IPassportRepo
    {

        bool SaveChange();

        IEnumerable<Passport> GetAllPassports();

        Passport GetPassportById(int id);

        void CreatePassport(Passport pass);


    }
}
