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
    - Replace Item
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

SCIM Filter builder:
```csharp
var filter = new FilterBuilder<Item>().Group()
                 .Field(item => item.Title)
                 .Eq("test")
                 .Or()
                 .Field(item => item.Title)
                 .Eq("test2")
                 .GroupEnd()
                 .And()
                 .Group()
                 .Field(item => item.Id)
                 .Eq("test3")
                 .Or()
                 .Field(item => item.Id)
                 .Eq("test4")
                 .GroupEnd();
Console.WriteLine(filter.ToString()); // (title eq test or title eq test2) and (id eq test3 or id eq test4)
```

More documentation to come.
