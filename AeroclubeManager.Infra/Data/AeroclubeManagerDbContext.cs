using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using AeroclubeManager.Core.Entities.User;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using AeroclubeManager.Core.Entities.FlightSchoolEntities;

namespace AeroclubeManager.Infra.Data
{
    public class AeroclubeManagerDbContext : IdentityDbContext<ApplicationUser>
    {
        public AeroclubeManagerDbContext(DbContextOptions<AeroclubeManagerDbContext> options) : base(options) { }

        public DbSet<FlightSchool> FlightSchools { get; set; }
        public DbSet<Airplane> Airplanes { get; set; }

        public DbSet<Airport> Airports { get; set; } 


        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<FlightSchool>()
                .HasMany(f => f.Planes)
                .WithOne(p => p.FlightSchoolAirplane)
                .HasForeignKey(p => p.FlightSchoolId);

            builder.Entity<FlightSchool>() // o aeroclube
                .HasOne(f => f.SchoolFlightAirport) // tem um aeroporto
                .WithOne(a => a.FlightSchool) // que tem um aeroclube
                .HasForeignKey<FlightSchool>(f => f.SchoolFlightAirportId) // que são ligados por SchoolFlightAirportId
                .OnDelete(DeleteBehavior.Cascade); // que quando o aeroclube for deletado, o aeroporto também sera

            builder.Entity<Airport>() // o aeroporto
                .HasOne(a => a.FlightSchool) // possui um aeroclube
                .WithOne(f => f.SchoolFlightAirport) // que possui um aeroporto
                .HasForeignKey<Airport>(a => a.FlightSchoolId) // o aeroporto possui uma chave estrangeira (FlightSchoolId)
                .OnDelete(DeleteBehavior.SetNull); // e quando o aeroporto for deletado, ele no aeroclube é nulo



        }
    }
}
