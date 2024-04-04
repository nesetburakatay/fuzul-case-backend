using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Services
{
    public interface IUserService
    {
        Task<User> AddUserAsync(User user);
        bool DeleteUser(long Id);
        Task<User> GetUserByIdAsync(long Id);
        Task<User> GetUserByNameAsync(string name);
        Task<User> GetUserByRefreshTokenAsync(string RefreshToken);
        User UpdateUser(User user);
        Task<bool> IsUserExistAsync(User user);
    }
}
