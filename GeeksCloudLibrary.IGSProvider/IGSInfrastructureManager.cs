using System;
using System.IO;
using System.Threading.Tasks;
using GeeksCloudLibrary.Abstractions;
using GeeksCloudLibrary.Abstractions.Enums;
using GeeksCloudLibrary.Abstractions.Models;

namespace GeeksCloudLibrary.IGSProvider
{
    public class IGSInfrastructureManager : IInfrastructureManager
    {
        public Task CreateInfrastructureAsync(CloudProvider provider, string infrastructureName)
        {
            var providerName = Helpers.Helpers.GetProviderName(provider);
            var parentDirectory = Helpers.Helpers.GetParentDirectory();
            var targetPath = $"{parentDirectory}/{providerName}/{infrastructureName}";
            if (Directory.Exists(targetPath))
            {
                throw new Exception("Infrastructure already exists");
            }
            else
            {
                return Task.Run(() =>
                {
                    Directory.CreateDirectory(targetPath);
                    var di = new DirectoryInfo(targetPath);
                    di.CreateSubdirectory("Databases");
                    di.CreateSubdirectory($"VirtualMachine");
                });
            }
        }


        public Task<InfrastructureInfo> GetInfrastructureAsync(CloudProvider provider, string name)
        {
            throw new NotImplementedException();
        }

        public InfrastructureInfo GetInfrastructure(CloudProvider provider, string name)
        {
            throw new System.NotImplementedException();
        }
    }
}