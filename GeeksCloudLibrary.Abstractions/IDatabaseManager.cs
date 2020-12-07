using System.Collections.Generic;
using System.Threading.Tasks;
using GeeksCloudLibrary.Abstractions.Enums;
using GeeksCloudLibrary.Abstractions.Models.Databases;

namespace GeeksCloudLibrary.Abstractions
{
    public interface IDatabaseManager
    {
        Task CreateDatabaseAsync(CloudProvider provider, string infrastructureName,
            DatabaseServerType dbType, string name,
            Dictionary<string, string> options);

        Task DeleteDatabaseFileAsync(CloudProvider provider, string infrastructureName, string name);
        Task DeleteDatabaseFolderAsync(CloudProvider provider, string infrastructureName);
    }
}