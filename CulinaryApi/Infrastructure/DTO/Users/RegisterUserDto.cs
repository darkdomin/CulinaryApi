using System.ComponentModel.DataAnnotations;

namespace CulinaryApi.Infrastructure.DTO.Users
{
    public class RegisterUserDto
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public int RoleId { get; set; } = 2;
    }
}
