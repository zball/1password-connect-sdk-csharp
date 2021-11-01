namespace OpConnectSdk.Model
{
    public class ServerHealth
    {
        public string Name { get; set; }
        public string Vertsion { get; set; }
        public ServiceDependency[] Dependencies { get; set; }
    }
}