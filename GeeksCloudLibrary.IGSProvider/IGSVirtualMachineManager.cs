using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using GeeksCloudLibrary.Abstractions;
using GeeksCloudLibrary.Abstractions.Enums;
using GeeksCloudLibrary.Abstractions.Models.VirtualMachines;

namespace GeeksCloudLibrary.IGSProvider
{
    public class IGSVirtualMachineManager : IVirtualMachineManager
    {
        public async Task CreateVirtualMachineAsync(CloudProvider provider,
            string infrastructureName, VirtualMachineType dbType,
            string name, Dictionary<string, string> options)
        {
            var parentDirectory = Helpers.Helpers.GetParentDirectory();
            var providerName = Helpers.Helpers.GetProviderName(provider);

            var infrastructurePath = $"{parentDirectory}/{providerName}/{infrastructureName}";
            var fileName = $"{infrastructurePath}/VirtualMachine/{name}.json";

            if (!Directory.Exists(infrastructurePath))
            {
                throw new Exception("Infrastructure does not exist");
            }
            else
            {
                if (File.Exists(fileName))
                {
                    throw new Exception("File exists with the same name");
                }
                else
                {
                    using (FileStream fs = File.Create(fileName, 1024))
                    {
                        byte[] info = new UTF8Encoding(true).GetBytes(Helpers.Helpers.ConvertDictionaryToJson(options));
                        await fs.WriteAsync(info, 0, info.Length);
                    }
                }
            }
        }

        public Task DeleteVirtualMachineFile(CloudProvider provider, string infrastructureName, string name)
        {
            var parentDirectory = Helpers.Helpers.GetParentDirectory();
            var providerName = Helpers.Helpers.GetProviderName(provider);
            var infrastructurePath = $"{parentDirectory}/{providerName}/{infrastructureName}";
            var fileName = $"{infrastructurePath}/VirtualMachine/{name}.json";
            if (File.Exists(fileName))
            {
                return Task.Run(() => { File.Delete(fileName); });
            }
            else
            {
                throw new Exception("File does not exist");
            }
        }

        public Task DeleteVirtualMachineFolder(CloudProvider provider, string infrastructureName)
        {
            var parentDirectory = Helpers.Helpers.GetParentDirectory();
            var providerName = Helpers.Helpers.GetProviderName(provider);
            var infrastructurePath = $"{parentDirectory}/{providerName}/{infrastructureName}";
            var fileName = $"{infrastructurePath}/VirtualMachine";
            if (Directory.Exists(fileName))
            {
                return Task.Run(() => { Directory.Delete(fileName, true); });
            }
            else
            {
                throw new Exception("File does not exist");
            }
        }
    }
}