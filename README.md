# 1password-connect-sdk-csharp
**UNOFFICIAL 1PASSWORD CONNECT SDK**

## Local Development

### Setup 1Password Connect
[1Password Connect](https://support.1password.com/connect-api-reference/) requires you to have an instance of Connect running as well as a `1password-credentials.json` file and a valid `accessToken` which are generated when going through the [Secrets Automation Flow](https://support.1password.com/secrets-automation/).

For development purposes you can run Connect easily with Docker using the example `docker-compose.yaml` [provided here](https://support.1password.com/connect-api-reference/).

Create a new Console Application. Then clone this repo right beside it. 

```cli
dotnet new console -o OpConnect/OpConnectConsole
cd OpConnect
git clone https://github.com/zball/1password-connect-sdk-csharp.git OpConnectSdk
```

Setup Sln:
```cli
dotnet new sln
dotnet sln add .\OpConnectConsole\
dotnet sln add .\OpConnectSdk\OpConnectSdk\
dotnet restore
```

Next we need to reference the SDK into the Console App and install some packages into the Console App to setup DI:
```cli
dotnet add .\OpConnectConsole\ reference .\OpConnectSdk\OpConnectSdk\
cd OpConnectConsole
dotnet add package Microsoft.Extensions.Configuration --version 5.0.0
dotnet add package Microsoft.Extensions.Configuration.Json --version 5.0.0
dotnet add package Microsoft.Extensions.Configuration.Binder --version 5.0.0
```

Add appsettings.json to `OpConnectConsole`:
```json
{
  "OpConnect": {
    "Token": "<ACCESSTOKEN>",
    "BaseUrl": "http://localhost:8080/"
  }
}
```

Update `OpConnectConsole.csproj` to output `appsettings.json`. Add:
```xml
<ItemGroup>
    <None Update="appsettings.json">
        <CopyToOutputDirectory>PreserveNewest</CopyToOutputDirectory>
    </None>
</ItemGroup>
```

Update `Program.cs` to look something like:
```csharp
using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OpConnectSdk.Lib.Core.Interfaces;
using OpConnectSdk.Lib.Extensions;
using OpConnectSdk.Model;

namespace OpConnectConsole
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var services = new ServiceCollection();

            var env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
            var config = new ConfigurationBuilder()
                .AddJsonFile($"appsettings.json", true, true)
                .Build();      

            // Register OpConnect - Would be done same way in WebApp
            services.AddOpConnect(
                config.GetSection(OpConnectOptions.OpConnect).Get<OpConnectOptions>()
            );

            var serviceProvider = services.BuildServiceProvider();
            var opConnect = serviceProvider.GetService<IOpConnect>();

            // Fetch your vaults for this AccessToken
            var vaults = await opConnect.Vault.GetListAsync();
            Console.WriteLine(vaults?[0].Id);

            // Fetch your Vaults for this AccessToken w/ filter param:
            //var vaults = await opConnect.Vault.GetListAsync(filter: "name eq \"My Vault\"");

            // Fetch a single vault:
            // opConnect.Vault.GetAsync('<VAULT_UUID>');

            // Fetch items for a vault:
            // var vaultItems = await opConnect.Item.GetListAsync(
            //     vaultUuid: "<VAULT_UUID>"
            // );

            // Fetch Items for a vault with filter param:
            // var vaultItems = await opConnect.Item.GetListAsync(
            //     vaultUuid: "<VAULT_UUID>",
            //     filter: "title eq \"My Login\""
            // );

            // Fetch Item:
            // var vaultItem = await opConnect.Item.GetAsync(
            //     vaultUuid: "<VAULT_UUID>",
            //     itemUuid: "<ITEM_UUID>"
            // );
        }
    }
}
```

Move `1password-credentials.json` and `docker-compose.yaml` into OpConnect directory. 
Start OpConnect Docker containers:

From OpConnect dir:
```cli 
docker-compose up -d
```

Run Console Application to see list of vaults:
```cli
cd OpConnectConsole
dotnet run
```
