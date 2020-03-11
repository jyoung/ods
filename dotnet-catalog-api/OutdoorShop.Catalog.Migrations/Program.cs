namespace OutdoorShop.Catalog.Migrations
{
    using FluentMigrator.Runner;
    using Microsoft.Extensions.DependencyInjection;
    using System;

    class Program
    {
        static void Main(string[] args)
        {
            var serviceProvider = CreateServices();

            using (var scope = serviceProvider.CreateScope())
            {
                MigrateDatabase(scope.ServiceProvider);
            }
        }

        private static IServiceProvider CreateServices()
        {
            return new ServiceCollection()
                .AddFluentMigratorCore()
                .ConfigureRunner(r => {
                    r.AddPostgres();
                    r.WithGlobalConnectionString("");
                    r.ScanIn(typeof(Program).Assembly).For.Migrations();
                })
                .AddLogging(l => l.AddFluentMigratorConsole())
                .BuildServiceProvider(false);
        }

        private static void MigrateDatabase(IServiceProvider serviceProvider)
        {
            var runner = serviceProvider.GetRequiredService<IMigrationRunner>();

            runner.MigrateUp();
        }
    }
}
