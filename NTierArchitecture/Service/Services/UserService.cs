using Core.Entities;
using Core.Enums;
using Core.Repositories;
using Core.Services;
using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }



        public async Task<User> AddUserAsync(User user)
        {
            user.Role = Role.USER;
            await _userRepository.xAddAsync(user);
            await _userRepository.xSaveChangesAsync();
            return user;
        }

        public bool DeleteUser(long Id)
        {
            _userRepository.xDeleteById(Id);
            _userRepository.xSaveChanges();
            return true;
        }

        public async Task<User> GetUserByIdAsync(long Id)
        {
            return await _userRepository.xGetByIdAsync(Id);
        }

        public async Task<User> GetUserByNameAsync(string name)
        {
            return await _userRepository.xGetFirstOrDefaultAsync(x => x.Name == name);
        }

        public async Task<User> GetUserByRefreshTokenAsync(string RefreshToken)
        {
            return await _userRepository.xGetFirstOrDefaultAsync(x => x.RefreshToken == RefreshToken);
        }

        public async Task<bool> IsUserExistAsync(User user)
        {

            if (await _userRepository.xGetFirstOrDefaultAsync(x => x.Id == user.Id) != null)
                return true;

            else if (await _userRepository.xGetFirstOrDefaultAsync(x => x.Name == user.Name) != null)
                return true;

            return false;
        }

        public User UpdateUser(User user)
        {
            User updatedUser = _userRepository.xUpdate(user);
            _userRepository.xSaveChanges();
            return updatedUser;
        }
    }
}
