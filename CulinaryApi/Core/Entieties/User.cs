namespace CulinaryApi.Core.Entieties
{
    public class User
    {
        public int Id { get; protected set; }
        public string Email { get; protected set; }
        public string PasswordHash { get; protected set; }

        public int RoleId { get; protected set; }
        public virtual Role Role { get; protected set; }
    }
}
