using Microsoft.Extensions.DependencyInjection;
using OpConnectSdk.Lib.Core;
using OpConnectSdk.Lib.Core.Interfaces;
using OpConnectSdk.Lib.Core.Services;
using OpConnectSdk.Model;

namespace OpConnectSdk.Lib.Extensions 
{
    public static class IServiceCollectionExtensions
    {
        public static IServiceCollection AddOpConnect(this IServiceCollection services, OpConnectOptions config)
        {

            services.AddSingleton<OpConnectOptions>(config);

            services.AddHttpClient<OpClient>();

            services.AddScoped<IVaultService, VaultService>();
            services.AddScoped<IItemService, ItemService>();

            services.AddScoped<IOpConnect, OpConnect>();

            return services;
        }
    }
}