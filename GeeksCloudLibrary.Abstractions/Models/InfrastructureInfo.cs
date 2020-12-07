using System.Collections.Generic;

namespace GeeksCloudLibrary.Abstractions.Models
{
    public class InfrastructureInfo
    {
        public string Name { get; set; }
        public IEnumerable<CloudResourceInfo> Resources { get; set; }
    }
}