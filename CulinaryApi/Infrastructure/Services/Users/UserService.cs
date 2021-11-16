using CulinaryApi.Core.Entieties;
using CulinaryApi.Core.Repositories;
using CulinaryApi.Exceptions;
using CulinaryApi.Infrastructure.DTO.Users;
using Microsoft.AspNetCore.Identity;
using System;
using System.Threading.Tasks;

namespace CulinaryApi.Infrastructure.Services.Users
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IPasswordHasher<User> _password;

        public UserService(IUserRepository userRepository, IPasswordHasher<User> password)
        {
            _userRepository = userRepository;
            _password = password;
        }
        public Task<string> LoginAsync(LoginDto dto)
        {
            throw new NotImplementedException();
        }

        public async Task RegisterAsync(RegisterUserDto dto)
        {
            var user = await _userRepository.GetAsync(dto.Email);

            if (user != null)
            {
                throw new UserExistsException($"User with email : '{user.Email}' already exists");
            }

            var newUser = new User();
            newUser.SetEmail(dto.Email);
            newUser.SetRole(dto.RoleId);
            var hashedpass = _password.HashPassword(newUser, dto.Password);
            newUser.SetPasswordHash(hashedpass);

            await _userRepository.AddAsync(newUser);
        }
    }
}
