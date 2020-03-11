namespace OutdoorShop.Catalog.Seed
{
    using System;
    using System.Threading.Tasks;
    using Microsoft.Azure.Documents.Client;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Options;

    public class Program
    {
        public static IConfigurationRoot Configuration { get; set; }

        static void Main(string[] args)
        {
            var builder = new ConfigurationBuilder();

            builder.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
            builder.AddUserSecrets<Program>();

            Configuration = builder.Build();

            IServiceCollection services = new ServiceCollection();

            services
                .Configure<AzureAccountDetails>(Configuration.GetSection(nameof(AzureAccountDetails)))
                .AddOptions()
                .AddSingleton<ISecretRevealer, SecretRevealer>()
                .AddSingleton<ICosmosDbDataLoader>(x => {
                    var options = x.GetService<IOptions<AzureAccountDetails>>();
                    return new CosmosDbDataLoader(new Uri(options.Value.CosmosDbEndpoint), options.Value.CosmosDbKey);
                })
                .BuildServiceProvider();

            var serviceProvider = services.BuildServiceProvider();
            
            var dataLoader = serviceProvider.GetService<ICosmosDbDataLoader>();

            dataLoader.Load()
                .Wait();
          }
    }
}
