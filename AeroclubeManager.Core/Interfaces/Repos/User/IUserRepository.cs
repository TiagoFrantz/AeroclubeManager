using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AeroclubeManager.Core.Entities.User;

namespace AeroclubeManager.Core.Interfaces.Repos.User
{
    public interface IUserRepository
    {
        public Task<bool> EditUser(string userId, ApplicationUser newUser);
    }
}
