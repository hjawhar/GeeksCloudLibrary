using System;
using System.Threading.Tasks;
using GeeksCloudLibrary.Abstractions;
using GeeksCloudLibrary.Abstractions.Enums;
using GeeksCloudLibrary.Abstractions.Models.Databases;
using GeeksCloudLibrary.Abstractions.Models.VirtualMachines;
using GeeksCloudLibrary.IGSProvider;

namespace GeeksCloudLibrary.ConsoleApp
{
    public class Program
    {
        /**
         * Example of the implementation
         */
        public static async Task Main(string[] args)
        {
            IResourceManager _resourceManager = new IGSResourceManager(CloudProvider.Igs);

            await _resourceManager.CreateInfrastructure("UAT");
            await _resourceManager.CreateResourceAsync("UAT", new VirtualMachineConfig()
            {
                Name = "VMM1",
                Type = ResourceType.VirtualMachine,
                Username = "User",
                Password = "Password",
                virtualMachineType = VirtualMachineType.Linux
            });
            await _resourceManager.CreateResourceAsync("UAT", new MySqlConfig()
            {
                Name = "DIGITAL_OCEAN1",
                Type = ResourceType.Database,
                Server = "ExampleServer",
                User = "ExampleUser",
                Password = "ExamplePassword",
                Database = "ExampleDatabase"
            });


            await _resourceManager.CreateResourceAsync("UAT", new MySqlConfig()
            {
                Name = "DIGITAL_OCEAN2",
                Type = ResourceType.Database,
                Server = "ExampleServer",
                User = "ExampleUser",
                Password = "ExamplePassword",
                Database = "ExampleDatabase"
            });

            await _resourceManager.CreateInfrastructure("Test");
            await _resourceManager.CreateResourceAsync("Test", new VirtualMachineConfig()
            {
                Name = "VMM2",
                Type = ResourceType.VirtualMachine,
                Username = "User",
                Password = "Password",
                virtualMachineType = VirtualMachineType.Windows
            });

            await _resourceManager.CreateResourceAsync("Test", new SqlServerConfig()
            {
                Name = "DIGITAL_OCEAN3",
                Type = ResourceType.Database,
                Server = "ExampleServer",
                Database = "ExampleDatabase",
                UserId = "ExampleUserId",
                Password = "ExamplePassword",
                IntegratedSecurity = false,
                MultipleActiveResultSets = true,
                Encrypt = true,
            });

            await _resourceManager.DeleteInfrastructure("UAT");
        }
    }
}