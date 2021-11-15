namespace CulinaryApi.Core.Entieties
{
    public class User
    {
        public int Id { get; protected set; }
        public string Email { get; protected set; }
        public string PasswordHash { get; protected set; }

        public int RoleId { get; protected set; }
        public virtual Role Role { get; protected set; }


        public void SetEmail(string email)
        {
            Email = email;
        }

        public void SetPasswordHash(string passwordHash)
        {
            PasswordHash = passwordHash;
        }

        public void SetRole(int roleId)
        {
            if(roleId > 2)
            {
                RoleId = 1;
            }
            RoleId = roleId;
        }
    }
}
