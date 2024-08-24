using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AeroclubeManager.Core.Entities.User;
using AeroclubeManager.Core.Interfaces.Repos.User;
using AeroclubeManager.Infra.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace AeroclubeManager.Infra.Repositories.User
{
    public class UserRepository : IUserRepository
    {
        private readonly UserManager<ApplicationUser> _userManager;
        public UserRepository(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<bool> EditUser(string userId, ApplicationUser newUser)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return false;
            }

            user.FirstName = newUser.FirstName;
            user.LastName = newUser.LastName;
            user.Document = newUser.Document;
            user.PerfilImage = newUser.PerfilImage;
            user.DateOfBirth = newUser.DateOfBirth;


            var result = await _userManager.UpdateAsync(user);
            return result.Succeeded;
        }
    }
}
