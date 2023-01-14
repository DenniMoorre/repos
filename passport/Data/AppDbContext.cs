using Microsoft.EntityFrameworkCore;
using System;
using System.Runtime.Intrinsics;
using passport.Models;

namespace passport.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> opt) : base(opt)
        {

        }

        public DbSet<Passport> Passports
        {
            get;set;
        }
    }
}
