using CulinaryApi.Core.Entieties;
using CulinaryApi.Core.Repositories;
using CulinaryApi.Exceptions;
using CulinaryApi.Infrastructure.DTO;
using CulinaryApi.Infrastructure.DTO.Users;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace CulinaryApi.Infrastructure.Services.Users
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IPasswordHasher<User> _password;
        private readonly IJwtHandler _handler;


        public UserService(IUserRepository userRepository, IPasswordHasher<User> password, IJwtHandler handler)
        {
            _userRepository = userRepository;
            _password = password;
            _handler = handler;
        }
        public async Task<TokenDto> LoginAsync(LoginDto dto) 
        {
            var user = await _userRepository.GetAsync(dto.Email);

            if (user == null)
            {
                throw new BadRequestException("Invalid user name or password");
            }

            var result = _password.VerifyHashedPassword(user, user.PasswordHash, dto.Password);

            if(result == PasswordVerificationResult.Failed)
            {
                throw new BadRequestException("Invalid user name or password");
            }

            var jwt = _handler.CreateToken(user.Id, user.Role.Name);

            return new TokenDto
            {
                Token = jwt.Token,
                Expires = jwt.Expires,
                Role = user.Role.Name
            };
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
