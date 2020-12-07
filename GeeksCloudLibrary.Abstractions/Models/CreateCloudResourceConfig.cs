using GeeksCloudLibrary.Abstractions.Enums;

namespace GeeksCloudLibrary.Abstractions.Models
{
    public abstract class CreateCloudResourceConfig
    {
        public string Name { get; set; }
        public ResourceType Type { get; set; }
    }
}