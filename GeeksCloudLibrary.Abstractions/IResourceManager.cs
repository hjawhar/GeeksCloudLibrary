using System.Threading.Tasks;
using GeeksCloudLibrary.Abstractions.Enums;
using GeeksCloudLibrary.Abstractions.Models;

namespace GeeksCloudLibrary.Abstractions
{
    public interface IResourceManager
    {
        Task CreateResourceAsync(string infrastructureName, CreateCloudResourceConfig config);

        Task DeleteResourceAsync(string infrastructureName, string name);
        Task DeleteInfrastructure(string infrastructureName);

        Task CreateProvider(CloudProvider provider);
        Task CreateInfrastructure(string infrastructureName);
        CloudProvider GetCurrentCloudProvider();
    }
}