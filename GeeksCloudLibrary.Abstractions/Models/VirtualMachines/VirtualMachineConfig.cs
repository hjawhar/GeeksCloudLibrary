using GeeksCloudLibrary.Abstractions.Enums;

namespace GeeksCloudLibrary.Abstractions.Models.VirtualMachines
{
    public class VirtualMachineConfig : CreateCloudResourceConfig
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public VirtualMachineType virtualMachineType { get; set; }
    }
}