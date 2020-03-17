namespace OutdoorShop.Catalog.Migrations
{
    using FluentMigrator.Runner;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Npgsql;
    using System;

    class Program
    {
        public static IConfigurationRoot Configuration { get; set; }

        static void Main(string[] args)
        {
            var builder = new ConfigurationBuilder();

            builder.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
            builder.AddUserSecrets<Program>();

            Configuration = builder.Build();

            var serviceProvider = CreateServices();

            using (var scope = serviceProvider.CreateScope())
            {
                MigrateDatabase(scope.ServiceProvider);
            }
        }

        private static IServiceProvider CreateServices()
        {
            return new ServiceCollection()
                .AddOptions()
                .AddFluentMigratorCore()
                .ConfigureRunner(r => {
                    r.AddPostgres();
                    r.WithGlobalConnectionString(Configuration["postgres-admin"]);
                    r.ScanIn(typeof(Program).Assembly).For.Migrations();
                })
                .AddLogging(l => l.AddFluentMigratorConsole())
                .BuildServiceProvider(false);
        }

        private static void MigrateDatabase(IServiceProvider serviceProvider)
        {
            var runner = serviceProvider.GetRequiredService<IMigrationRunner>();
            runner.MigrateUp();
            GrantPermissions();
        }

        private static void GrantPermissions()
        {
            const string usageSql = "grant usage on schema catalog to ods;";
            const string permissionsSql = "grant select, insert, update, delete on all tables in schema catalog to ods with grant option;";
            const string sequencesSql = "grant usage, select on all sequences in schema catalog to ods;";

            var combinedSql = string.Join(usageSql, permissionsSql, sequencesSql);

            using (var connection = new NpgsqlConnection(Configuration["postgres-admin"]))
            {
                connection.Open();
                using (var command = new NpgsqlCommand(combinedSql, connection))
                {
                    command.ExecuteNonQuery();
                }
            }
        }
    }
}
