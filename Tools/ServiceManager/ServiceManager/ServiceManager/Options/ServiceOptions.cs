namespace Governor.Server.Options
{
    public class ServiceOptions
    {
        public string Name { get; set; }
        public string Directory { get; set; }
        public string FileName { get; set; }
        public string Arguments { get; set; }
        public string Url { get; set; }
        public bool SharedShell { get; set; }
    }
}