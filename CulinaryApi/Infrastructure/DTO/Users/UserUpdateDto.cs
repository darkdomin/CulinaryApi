namespace CulinaryApi.Infrastructure.DTO.Users
{
    public class UserUpdateDto
    {
       // public string Email { get; set; }
        public string Password { get; set; }
        public string NewPassword { get; set; }
        public string ConfirmPassword { get; set; }
        public string Role { get; set; }
    }
}
