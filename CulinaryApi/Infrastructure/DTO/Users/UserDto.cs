namespace CulinaryApi.Infrastructure.DTO.Users
{
    public class UserDto
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Token { get; set; }

        public UserDto(int id, string email,  string token)
        {
            Id = id;
            Email = email;
            Token = token;
        }

    }
}
