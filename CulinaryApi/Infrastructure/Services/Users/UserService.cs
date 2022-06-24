using AutoMapper;
using CulinaryApi.Core.Entieties;
using CulinaryApi.Core.Repositories;
using CulinaryApi.Exceptions;
using CulinaryApi.Infrastructure.DTO.Users;
using CulinaryApi.Infrastructure.Extensions;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;

using System.Threading.Tasks;

namespace CulinaryApi.Infrastructure.Services.Users
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IPasswordHasher<User> _password;
        private readonly IJwtHandler _handler;
        private readonly IMapper _mapper;
        private readonly CulinaryDbContext dbContext;

        public UserService(IUserRepository userRepository, IPasswordHasher<User> password, IJwtHandler handler, IMapper mapper,
             TestEmail testEmail, CulinaryDbContext dbContext) 
        {
            _userRepository = userRepository;
            _password = password;
            _handler = handler;
            _mapper = mapper;
            this.dbContext = dbContext; 
        }

        public async Task ChangeRole(UserUpdateRole dto)
        {
            var user = await GetUser(dto.UserId);
            UserServiceExtension.UserVerification(user);

            var role = CulinarySeeder.GetRoles()
                                     .FirstOrDefault(r => r.Name.ToLowerInvariant() == dto.Role.ToLowerInvariant());
            user.SetRole(role.Id);
            await _userRepository.UpdateAsync();
        }

        public async Task<IEnumerable<UserDto>> Get()
        {
            var users = await _userRepository.GetAsync();
            var usersDto = users.Select(u => new UserDto(u.Id, u.Email, null){});
            return usersDto;
        }

        public async Task<UserDto> Get(int id)
        {
            var user = await GetUser(id);
            UserServiceExtension.UserVerification(user);
            var userDto = _mapper.Map<UserDto>(user);
            return userDto;
        }

        public async Task<UserDto> LoginAsync(LoginDto dto)
        {
            var user = await _userRepository.GetAsync(dto.Email);

            if (user == null)
            {
                throw new BadRequestException("Invalid user name or password");
            }

            var result = _password.VerifyHashedPassword(user, user.PasswordHash, dto.Password);
            UserServiceExtension.PasswordVerification(result);

            var token = _handler.CreateToken(user);
            var userDto = new UserDto(user.Id, user.Email, token);

            return userDto;
        }

        public async Task RegisterAsync(RegisterUserDto dto)
        {
            var user = await _userRepository.GetAsync(dto.Email);

            if (user != null)
            {
                throw new UserExistsException($"User with email : '{user.Email}' already exists");
            }

            var newUser = User.CreateUser();
            newUser.SetEmail(dto.Email);

            var hashedpass = _password.HashPassword(newUser, dto.Password);
            newUser.SetPasswordHash(hashedpass);

            var role = new Role(dto.RoleId);
            var collection = dbContext.Roles;
            var roleId = role.SetRole(role.Name, collection);
            newUser.SetRole(roleId);

            await _userRepository.AddAsync(newUser);
        }

        public async Task RemoveAsync(int id)
        {
            User user = await GetUser(id);
            UserServiceExtension.UserVerification(user);
            await _userRepository.DeleteAsync(user);
        }

        public async Task UpdateAsync(UserUpdateDto dto, int id)
        {
            var user = await GetUser(id);
            UserServiceExtension.UserVerification(user);

            if (dto.NewPassword == null )
            {
                throw new ArgumentNullException("Hasło nie może być puste");
            }

            if(dto.NewPassword != dto.ConfirmPassword)
            {
                throw new Exception("Hasła się nie zgadzają");
            }

            var result = _password.VerifyHashedPassword(user, user.PasswordHash, dto.Password);
            UserServiceExtension.PasswordVerification(result);

            var hashedPass = _password.HashPassword(user, dto.NewPassword);
            user.SetPasswordHash(hashedPass);

            // user.SetEmail(dto.Email);

            if (dto.Role != null && user.Role.Name == "admin")
            {
                var role = CulinarySeeder.GetRoles()
                                         .FirstOrDefault(r => r.Name.ToLowerInvariant() == dto.Role);
                user.SetRole(role.Id);
            }

            await _userRepository.UpdateAsync();
        }

        // --- methods -- //

        private async Task<User> GetUser(int id)
        {
            return await _userRepository.GetAsync(id);
        }
    }
}
