using System.Threading.Tasks;

namespace ServiceSupport.Infrastructure.Services
{
    public interface IDataInitializer: IService
    {
         Task SeedAsync(); 
    }
}