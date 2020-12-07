namespace GeeksCloudLibrary.Abstractions.Models.Databases
{
    public class MySqlConfig : CreateCloudResourceConfig
    {
        public string Server { get; set; }
        public string User { get; set; }
        public string Password { get; set; }
        public string Database { get; set; }
    }
}