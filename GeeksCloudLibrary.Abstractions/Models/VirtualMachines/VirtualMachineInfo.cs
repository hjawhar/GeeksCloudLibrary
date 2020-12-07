using GeeksCloudLibrary.Abstractions.Enums;

namespace GeeksCloudLibrary.Abstractions.Models.VirtualMachines
{
    public class VirtualMachineInfo : CloudResourceInfo
    {
        public VirtualMachineType virtualMachineType { get; set; }
    }
}