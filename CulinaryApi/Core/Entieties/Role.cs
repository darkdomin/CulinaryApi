namespace CulinaryApi.Core.Entieties
{
    public class Role
    {
        public int Id { get; protected set; }
        public string Name { get; protected set; }

        protected Role()
        {

        }
        public Role(string name)
        {
            Name = name;
        }
    }
}