using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace CulinaryApi.Core.Entieties
{
    public class Role : Identity
    {
        public string Name { get; protected set; }

        protected Role()
        {

        }
        public Role( string name)
        {
            SetRole(name);
        }

        private static int FindId(string role, DbSet<Role> collection)
        {
            return collection.FirstOrDefault(r => r.Name == role).Id;
        }

        public int SetRole(string role, DbSet<Role> collection)
        {
            return FindId(role, collection); 
        }

        public void SetRole(string roleName)
        {
            Name = roleName ?? throw new ArgumentNullException("Role name cannot be empty.");
        }
    }
}