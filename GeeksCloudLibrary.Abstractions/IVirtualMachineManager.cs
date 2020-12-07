using System.Collections.Generic;
using System.Threading.Tasks;
using GeeksCloudLibrary.Abstractions.Enums;
using GeeksCloudLibrary.Abstractions.Models.VirtualMachines;

namespace GeeksCloudLibrary.Abstractions
{
    public interface IVirtualMachineManager
    {
        Task CreateVirtualMachineAsync(CloudProvider provider, string infrastructureName,
            VirtualMachineType dbType,
            string name, Dictionary<string, string> options);

        Task DeleteVirtualMachineFile(CloudProvider provider, string infrastructureName, string name);
        Task DeleteVirtualMachineFolder(CloudProvider provider, string infrastructureName);
    }
}