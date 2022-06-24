using System;

namespace CulinaryApi.Core.Entieties
{
    public class User : Identity
    {
        public string Email { get; protected set; }
        public string PasswordHash { get; protected set; }

        public int RoleId { get; protected set; }
        public virtual Role Role { get; protected set; }

        protected User() { }
        public void SetEmail(string email)
        {
            Email = email ?? throw new ArgumentNullException("Email cannot be empty.");
        }

        public void SetRole(int roleId)
        {
            if(roleId<= 0)
            {
                throw new ArgumentOutOfRangeException($"User Role Id {roleId} cannot be lower than 1");
            }

            RoleId = roleId;
        }

        public void SetPasswordHash(string passHash) 
        {
            PasswordHash = passHash ?? throw new ArgumentNullException("Password hash cannot be empty.");
        }

        public static User CreateUser()
        {
            return new User();
        }
    }
}
