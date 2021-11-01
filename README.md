**AN UNOFFICIAL 1PASSWORD CONNECT SDK**

This project is a WIP. Currently only supports: 
- Vaults
    - Get All Vaults w/ Search & Filtering
    - Get Single Vault
- Items
    - Get All Items By Vault w/ Search & Filtering
    - Get Sinlge Item
    - Create Item
    - Delete Item
- Server
    - Health Check
    - Heartbeat

To Setup: 

Install: [Nuget Install](https://www.nuget.org/packages/1PasswordConnectSDK/)

`Startup.cs`
```csharp
services.AddOpConnect(
    Configuration.GetSection(OpConnectOptions.OpConnect).Get<OpConnectOptions>()
);
```

`appsettings.json`
```json
"OpConnect": {
    "Token": "<ACCESS_TOKEN>",
    "BaseUrl": "http://localhost:8080/"
}
```

Inject service:
```csharp
private readonly IOpConnect _opConnect;

public WeatherForecastController(
    IOpConnect opConnect
)
{
    _opConnect = opConnect;
}

[HttpGet]
public async Task<IEnumerable<WeatherForecast>> Get()
{
    var vaults = await opConnect.Vault.GetListAsync();
    Console.WriteLine(vaults?[0].Id);

    ...
}
```

More documentation to come.