namespace OpConnectSdk.Lib.Core.Services
{
    public abstract class ApiService
    {
        protected OpClient _httpClient;

        public ApiService(
            OpClient opClient
        )
        {
            _httpClient = opClient;
        }
    }
}