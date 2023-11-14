using CIS.Models;
using CIS.Models.Relations;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CIS.DAL
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {}
        public DbSet<User> Users { get; init; }
        public DbSet<Gender> Genders { get; init; }
        public DbSet<Category> Categories { get; init; }
        public DbSet<Scheme> Schemes { get; init; }
        public DbSet<Role> Roles { get; init; }
        public DbSet<BeneficiarySchemeApplied> BeneficiarySchemeApplied { get; init; }
        public DbSet<Beneficiary> Beneficiaries { get; init; }
        public DbSet<PhysicallyDisability> PhysicallyDisabilies { get; init; }



    }
}
