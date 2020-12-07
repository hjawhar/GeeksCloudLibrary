using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using GeeksCloudLibrary.Abstractions;
using GeeksCloudLibrary.Abstractions.Enums;
using GeeksCloudLibrary.Abstractions.Models;
using GeeksCloudLibrary.Abstractions.Models.Databases;
using GeeksCloudLibrary.Abstractions.Models.VirtualMachines;

namespace GeeksCloudLibrary.IGSProvider
{
    public class IGSResourceManager : IResourceManager
    {
        private readonly IDatabaseManager _databaseManager;
        private readonly IVirtualMachineManager _virtualMachineManager;
        private readonly IInfrastructureManager _infrastructureManager;
        private readonly CloudProvider _cloudProvider;

        public IGSResourceManager(CloudProvider cloudProvider)
        {
            _cloudProvider = cloudProvider;
            switch (cloudProvider)
            {
                case CloudProvider.Igs:
                    // TODO - Dependency Injection could be used instead of the following
                    _databaseManager = new IGSDatabaseManager();
                    _virtualMachineManager = new IGSVirtualMachineManager();
                    _infrastructureManager = new IGSInfrastructureManager();
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(cloudProvider), cloudProvider, null);
            }

            if (!ProviderExists(cloudProvider))
            {
                CreateProvider(cloudProvider);
            }
        }

        public async Task CreateResourceAsync(string infrastructureName,
            CreateCloudResourceConfig config)
        {
            PropertyInfo[] infos = config.GetType().GetProperties();
            Dictionary<string, string> options = new Dictionary<string, string>();
            foreach (PropertyInfo info in infos)
            {
                options.Add(info.Name, info.GetValue(config, null).ToString());
            }


            switch (config.Type)
            {
                case ResourceType.VirtualMachine:
                    if (config is VirtualMachineConfig vmconfig)
                    {
                        await _virtualMachineManager.CreateVirtualMachineAsync(GetCurrentCloudProvider(),
                            infrastructureName,
                            vmconfig.virtualMachineType,
                            vmconfig.Name, options);
                    }

                    break;
                case ResourceType.Database:

                    if (config is MySqlConfig)
                    {
                        await _databaseManager.CreateDatabaseAsync(GetCurrentCloudProvider(), infrastructureName,
                            DatabaseServerType.MySql, config.Name, options);
                    }
                    else if (config is SqlServerConfig)
                    {
                        await _databaseManager.CreateDatabaseAsync(GetCurrentCloudProvider(), infrastructureName,
                            DatabaseServerType.SqlServer,
                            config.Name, options);
                    }
                    else
                    {
                        throw new Exception();
                    }

                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public Task DeleteResourceAsync(string infrastructureName, string name)
        {
            throw new System.NotImplementedException();
        }

        public Task CreateProvider(CloudProvider provider)
        {
            var providerName = Helpers.Helpers.GetProviderName(provider);
            var parentDirectory = Helpers.Helpers.GetParentDirectory();
            var targetPath = $"{parentDirectory}/{providerName}";
            return Task.Run(() => { Directory.CreateDirectory(targetPath); });
        }

        private bool ProviderExists(CloudProvider provider)
        {
            var providerName = Helpers.Helpers.GetProviderName(provider);
            var parentDirectory = Helpers.Helpers.GetParentDirectory();
            var targetPath = $"{parentDirectory}/{providerName}";
            return Directory.Exists(targetPath);
        }

        public async Task CreateInfrastructure(string infrastructureName)
        {
            await _infrastructureManager.CreateInfrastructureAsync(GetCurrentCloudProvider(), infrastructureName);
        }

        public CloudProvider GetCurrentCloudProvider()
        {
            return _cloudProvider;
        }

        public Task<InfrastructureInfo> GetInfrastructureAsync(string name)
        {
            throw new System.NotImplementedException();
        }


        public async Task DeleteInfrastructure(string infrastructureName)
        {
            var providerName = Helpers.Helpers.GetProviderName(_cloudProvider);
            var parentDirectory = Helpers.Helpers.GetParentDirectory();
            var targetPath = $"{parentDirectory}/{providerName}/{infrastructureName}";
            if (!Directory.Exists(targetPath))
            {
                throw new Exception("Infrastructure does not exist");
            }
            else
            {
                var directories = Directory.GetDirectories(targetPath);
                if (directories.Length == 0)
                {
                    throw new Exception("Infrastructure is empty");
                }
                else
                {
                    await _virtualMachineManager.DeleteVirtualMachineFolder(GetCurrentCloudProvider(),
                        infrastructureName);
                    await _databaseManager.DeleteDatabaseFolderAsync(GetCurrentCloudProvider(), infrastructureName);
                }
            }
        }
    }
}