using System.Threading.Tasks;
using GeeksCloudLibrary.Abstractions.Enums;
using GeeksCloudLibrary.Abstractions.Models;

namespace GeeksCloudLibrary.Abstractions
{
    public interface IInfrastructureManager
    {
        Task CreateInfrastructureAsync(CloudProvider provider, string infrastructureName);
        Task<InfrastructureInfo> GetInfrastructureAsync(CloudProvider provider, string name);
    }
}