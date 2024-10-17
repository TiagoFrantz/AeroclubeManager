using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AeroclubeManager.Core.Entities.FlightSchoolEntities;
using AeroclubeManager.Core.Entities.User;
using AeroclubeManager.Core.Interfaces.Repos.FlightSchoolRepos;
using AeroclubeManager.Infra.Data;
using Azure.Core.GeoJson;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace AeroclubeManager.Infra.Repositories.FlightSchoolRepos
{
    public class FlightSchoolRepository : IFlightSchoolRepository
    {
        private readonly AeroclubeManagerDbContext _context;

        public FlightSchoolRepository(AeroclubeManagerDbContext context)
        {
            _context = context;
        }

       

        public async Task<FlightSchool?> CreateFlightSchool(FlightSchool flightSchool)
        {
             if(flightSchool == null)
            {
                return null;
            }

             _context.FlightSchools.Add(flightSchool);

            var result = await _context.SaveChangesAsync();

            if(result == 0)
            {
                return null;
            }

            return flightSchool;
        }

        public async Task<bool> DeleteFlightSchool(Guid flightSchoolId)
        { 
           if(flightSchoolId == null)
            {
                return false;
            }

            var flightSchool = await _context.FlightSchools.FindAsync(flightSchoolId);

            if (flightSchool == null) {
                return false;
            }

           _context.FlightSchools.Remove(flightSchool);
            var result =  await _context.SaveChangesAsync();
            return result > 0;
        }

        public async Task<List<FlightSchool>?> GetAllFlightSchools()
        {
         var allFlightSchools = await _context.FlightSchools
        .Include(fs => fs.Planes)
        .Include(fs => fs.Flights)
        .Include(fs => fs.Users)
        .Include(fs => fs.Reviews)
        .Include(fs => fs.Links)
        .Include(fs => fs.SchoolFlightAirport)
        .ToListAsync();


             if(allFlightSchools == null)
            {
                return null;
            }

            return allFlightSchools;
        }

        public async Task<FlightSchool?> GetFlightSchoolById(Guid flightSchoolId)
        {
            if (flightSchoolId == null)
            {
                return null;
            }

            var flightSchool = await _context.FlightSchools
                .Include(fs => fs.Planes)
                .Include(fs => fs.Flights)
            .Include(fs => fs.Users).ThenInclude(u => u.FlightSchoolRoles)
                .Include(fs => fs.Reviews)
                .Include(fs => fs.Links)
                .Include(fs => fs.SchoolFlightAirport) 
                .FirstOrDefaultAsync(fs => fs.Id == flightSchoolId);


            return flightSchool;
        }

        public async Task<List<FlightSchool>> GetFlightSchoolsByUserId(string userId)
        {
            var flightSchools =  await _context.FlightSchools
            .Include(fs => fs.Planes)
            .Include(fs => fs.Flights)
            .Include(fs => fs.Users).ThenInclude(u => u.FlightSchoolRoles)
            .Include(fs => fs.Reviews)
            .Include(fs => fs.Links)
            .Include(fs => fs.SchoolFlightAirport)
            .Where(f => f.Users.Any(uf => uf.UserId == userId))
            .ToListAsync();


            return flightSchools;
        }

        public async Task<List<FlightSchool>?> GetFlightSchoolsContaining(string name)
        {
            if (name.IsNullOrEmpty())
            {
                return null;
            }

            var flightSchools = await _context.FlightSchools
          .Include(fs => fs.Planes)
          .Include(fs => fs.Flights)
          .Include(fs => fs.Users).ThenInclude(u => u.FlightSchoolRoles)
          .Include(fs => fs.Reviews)
          .Include(fs => fs.Links)
          .Include(fs => fs.SchoolFlightAirport)
          .Where(f => f.Name.Contains(name))
          .ToListAsync();

            if(flightSchools.Any() == false)
            {
                return null;
            }

            return flightSchools;

        }

        public async Task<bool?> DeleteAllFlightSchoolLinks(Guid flightSchoolId)
        {
            if (flightSchoolId == Guid.Empty)
            {
                return null;
            }

            var links = _context.FlightSchoolLinks.Where(link => link.FlightSchoolId == flightSchoolId).ToList();

            if (links != null && links.Count > 0)
            {
                _context.FlightSchoolLinks.RemoveRange(links);
                var result = await _context.SaveChangesAsync();
            }

                return true;

        }

        public async Task<List<FlightSchoolLink>?> UpdateFlightSchoolLinks(Guid flightSchoolId, List<FlightSchoolLink> links)
        {
            if (flightSchoolId == Guid.Empty)
            {
                return null;
            }


            FlightSchool flightSchool = await GetFlightSchoolById(flightSchoolId);

            if (flightSchool == null)
            {
                return null;
            }

            await DeleteAllFlightSchoolLinks(flightSchoolId);

            foreach (var link in links)
            {
                var newLink = new FlightSchoolLink
                {
                    Url = link.Url,
                    Text = link.Text,
                    FlightSchoolId = flightSchoolId,
                    FlightSchool = flightSchool
                };

                _context.FlightSchoolLinks.Add(newLink);

            }

            try{
                await _context.SaveChangesAsync();

            } catch (Exception ex) {
                Console.WriteLine(ex.ToString());
            }

            return await _context.FlightSchoolLinks.ToListAsync();

        }

        public async Task<UserFlightSchool?> GetUserFlightSchoolByUserId(Guid flightSchoolId, string userId)
        {
            if(flightSchoolId == Guid.Empty || userId.IsNullOrEmpty())
            {
                return null;
            }

            FlightSchool flightSchool = await GetFlightSchoolById(flightSchoolId);

            if(flightSchool == null)
            {
                return null;
            }

            UserFlightSchool user = flightSchool.Users.FirstOrDefault(uf => uf.UserId == userId);


            if (user == null)
            {
                return null;
            }

            return user;
            }

            public async Task<FlightSchool?> UpdateFlightSchool(FlightSchool flightSchool)
        {
            if(flightSchool == null)
            {
                return null;
            }

            var flightSchoolToUpdate = await GetFlightSchoolById(flightSchool.Id);

            if(flightSchoolToUpdate == null) {
                return null; 
            }

            flightSchoolToUpdate.Name = flightSchool.Name;
            flightSchoolToUpdate.Description = flightSchool.Description;
            flightSchoolToUpdate.Document = flightSchool.Document;
            flightSchoolToUpdate.Reviews = flightSchool.Reviews;
            flightSchoolToUpdate.Users = flightSchool.Users;
            flightSchoolToUpdate.Planes = flightSchool.Planes;
            flightSchoolToUpdate.LicenseNumber = flightSchool.LicenseNumber;
            flightSchoolToUpdate.LogoUrl = flightSchool.LogoUrl;
            flightSchoolToUpdate.IsApproved = flightSchool.IsApproved;
            flightSchoolToUpdate.SchoolFlightAirport = flightSchool.SchoolFlightAirport;
            flightSchoolToUpdate.Flights = flightSchool.Flights;

            try
            {   var result = await _context.SaveChangesAsync();

            }
            catch (Exception error)
            {
                Console.WriteLine(error);    
            }
            await UpdateFlightSchoolLinks(flightSchool.Id, flightSchool.Links.ToList());




            return flightSchoolToUpdate;
        }
    }
}
