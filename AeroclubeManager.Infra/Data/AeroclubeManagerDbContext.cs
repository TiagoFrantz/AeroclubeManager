using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using AeroclubeManager.Core.Entities.User;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace AeroclubeManager.Infra.Data
{
    public class AeroclubeManagerDbContext : IdentityDbContext<ApplicationUser>
    {
        public AeroclubeManagerDbContext(DbContextOptions<AeroclubeManagerDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}
