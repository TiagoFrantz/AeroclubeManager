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
using AeroclubeManager.Core.Entities.Review;

namespace AeroclubeManager.Infra.Data
{
    public class AeroclubeManagerDbContext : IdentityDbContext<ApplicationUser>
    {
        public AeroclubeManagerDbContext(DbContextOptions<AeroclubeManagerDbContext> options) : base(options) { }

        public DbSet<FlightSchool> FlightSchools { get; set; }
        public DbSet<UserFlightSchoolRole> UserFlightSchoolRoles { get; set; }
        public DbSet<FlightSchoolReview> FlightSchoolReviews { get; set; }
        public DbSet<UserFlightSchool> UserFlightSchools { get; set; }
        public DbSet<Flight> Flights { get; set; }
        public DbSet<Airplane> Airplanes { get; set; }
        public DbSet<AirplaneImage> AirplaneImages { get; set; }
        public DbSet<Airport> Airports { get; set; } 

        public DbSet<FlightSchoolLink> FlightSchoolLinks { get; set; } 


        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<FlightSchool>()
                .HasMany(f => f.Planes)
                .WithOne(p => p.FlightSchool)
                .HasForeignKey(p => p.FlightSchoolId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<FlightSchool>() // o aeroclube
                .HasOne(f => f.SchoolFlightAirport) // tem um aeroporto
                .WithOne(a => a.FlightSchool) // que tem um aeroclube
                .HasForeignKey<Airport>(a => a.FlightSchoolId) // que são ligados por SchoolFlightAirportId
                .OnDelete(DeleteBehavior.Cascade); // que quando o aeroclube for deletado, o aeroporto também sera

          

            builder.Entity<FlightSchool>() // o aeroclube
                .HasMany(f => f.Links) // possui links
                .WithOne(l => l.FlightSchool) // que possuem referencia ao aeroclube
                .HasForeignKey(l => l.FlightSchoolId) //com o id do aeroclube
                .OnDelete(DeleteBehavior.Cascade); // que se deletado, deletará os links

            builder.Entity<FlightSchool>()
                .HasMany(f => f.Flights)
                .WithOne(flight => flight.FlightSchool)
                .HasForeignKey(flight => flight.FlightSchoolId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<FlightSchool>()
                .HasMany(f => f.Reviews)
                .WithOne(r => r.FlightSchool)
                .HasForeignKey(r => r.SchoolFlightId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<ApplicationUser>()
                .HasMany(u => u.FlightSchoolReviews)
                .WithOne(r => r.User)
                .HasForeignKey(r => r.UserId)
                .OnDelete(DeleteBehavior.Cascade);


            builder.Entity<FlightSchool>()
                .HasMany(f => f.Users)
                .WithOne(u => u.FlightSchool)
                .HasForeignKey(u => u.FlightSchoolId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<ApplicationUser>()
                .HasMany(au => au.FlightSchools)
                .WithOne(u => u.User)
                .HasForeignKey(u => u.UserId)
                .OnDelete(DeleteBehavior.Cascade);


            builder.Entity<UserFlightSchool>()
                .HasMany(u => u.FlightSchoolRoles)
                .WithOne(fl => fl.User)
                .HasForeignKey(fl => fl.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<Airplane>()
                .HasMany(a => a.Images)
                .WithOne(img => img.Airplane)
                .HasForeignKey(img => img.AirplaneId)
                .OnDelete(DeleteBehavior.Cascade);


            builder.Entity<Flight>()
                .HasOne(f => f.Instructor)
                .WithMany()
                .HasForeignKey(f => f.InstructorId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Flight>()
                .HasOne(f => f.Student)
                .WithMany()
                .HasForeignKey(f => f.StudentId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Flight>()
                .HasOne(f => f.FlightAirplane)
                .WithMany()
                .HasForeignKey(f => f.AirplaneId)
                .OnDelete(DeleteBehavior.Restrict);



        }
    }
}
