using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using GeeksCloudLibrary.Abstractions;
using GeeksCloudLibrary.Abstractions.Enums;
using GeeksCloudLibrary.Abstractions.Models.Databases;

namespace GeeksCloudLibrary.IGSProvider
{
    public class IGSDatabaseManager : IDatabaseManager
    {
        public async Task CreateDatabaseAsync(CloudProvider provider, string infrastructureName,
            DatabaseServerType dbType, string name,
            Dictionary<string, string> options)
        {
            var parentDirectory = Helpers.Helpers.GetParentDirectory();
            var providerName = Helpers.Helpers.GetProviderName(provider);

            var infrastructurePath = $"{parentDirectory}/{providerName}/{infrastructureName}";
            var fileName = $"{infrastructurePath}/Databases/{name}.json";


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

        public Task DeleteDatabaseFileAsync(CloudProvider provider, string infrastructureName, string name)
        {
            var parentDirectory = Helpers.Helpers.GetParentDirectory();
            var providerName = Helpers.Helpers.GetProviderName(provider);
            var infrastructurePath = $"{parentDirectory}/{providerName}/{infrastructureName}";
            var fileName = $"{infrastructurePath}/Databases/{name}.json";
            if (File.Exists(fileName))
            {
                return Task.Run(() => { Directory.Delete(fileName, true); });
            }
            else
            {
                throw new Exception("File does not exist");
            }
        }

        public Task DeleteDatabaseFolderAsync(CloudProvider provider, string infrastructureName)
        {
            var parentDirectory = Helpers.Helpers.GetParentDirectory();
            var providerName = Helpers.Helpers.GetProviderName(provider);
            var infrastructurePath = $"{parentDirectory}/{providerName}/{infrastructureName}";
            var fileName = $"{infrastructurePath}/Databases";
            if (Directory.Exists(fileName))
            {
                return Task.Run(() => { Directory.Delete(fileName, true); });
            }
            else
            {
                throw new Exception("Directory does not exist");
            }
        }
    }
}