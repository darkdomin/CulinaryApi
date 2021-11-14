using System.Threading.Tasks;

namespace CulinaryApi.Infrastructure.Services
{
    public interface IDataInitializer
    {
        Task SeedAsync();
    }
}
