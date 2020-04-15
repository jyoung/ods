namespace OutdoorShop.Catalog.Seed
{
    using System;
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
                .AddSingleton<IDataLoader>(x => {
                    var options = x.GetService<IOptions<AzureAccountDetails>>();
                    return new CosmosDbDataLoader(new Uri(options.Value.CosmosDbEndpoint), options.Value.CosmosDbKey);
                    //return new PostgresDataLoader(Configuration["postgres-user"]);
                })
                .BuildServiceProvider();

            var serviceProvider = services.BuildServiceProvider();
            
            var dataLoader = serviceProvider.GetService<IDataLoader>();

            dataLoader.LoadAsync().Wait();
        
          }
    }
}

