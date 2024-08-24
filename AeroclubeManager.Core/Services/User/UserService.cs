using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AeroclubeManager.Core.Entities.User;
using AeroclubeManager.Core.Interfaces.Repos.User;
using AeroclubeManager.Core.Interfaces.Services.User;

namespace AeroclubeManager.Core.Services.User
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository) {
        _userRepository = userRepository;
        }



        public async Task<bool> EditUser(string userId, ApplicationUser newUser)
        {
            return await _userRepository.EditUser(userId, newUser);
        }
    }
}
