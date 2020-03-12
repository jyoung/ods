namespace OutdoorShop.Catalog.Seed
{
    using CsvHelper;
    using Npgsql;
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;

    public class PostgresDataLoader : IDataLoader
    {
        private readonly string connectionString;
        private IList<Data> data;


        public PostgresDataLoader(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public void Load()
        {
            throw new NotImplementedException();
        }

        public async Task LoadAsync()
        {
            LoadCSV();
            await CreateBrands();
            await CreateCategories();

            Console.ReadLine();
        }

        private void LoadCSV()
        {
            Console.Out.WriteLine("Loading CSV");

            using (var reader = new StreamReader("OutdoorShop.csv"))
            {
                var csvReader = new CsvReader(reader, CultureInfo.InvariantCulture);

                this.data = csvReader.GetRecords<Data>().ToList();

            }
        }

        private async Task CreateBrands()
        {
            const string sql = "insert into catalog.brands(name, import_id) values(@name, @importId)";
            
            var brands = data.GroupBy(x => x.BrandId)
                .Select(x => x.First())
                .ToList();

            using (var connection = new NpgsqlConnection(connectionString))
            {
                await connection.OpenAsync();
                using (var command = new NpgsqlCommand(sql, connection))
                {
                    foreach (var brand in brands)
                    {
                        command.Parameters.AddWithValue("name", brand.BrandName);
                        command.Parameters.AddWithValue("importId", brand.BrandId);

                        command.ExecuteNonQuery();
                        command.Parameters.Clear();
                    }
                }
            }
        }

        private async Task CreateCategories()
        {
            var parentCategoryIds = new Dictionary<long, long>();

            var categories = data.GroupBy(x => new { x.Category1Id, x.Category2Id})
                .Select(x => x.First())
                .ToList();

            const string parentSql = "insert into catalog.categories (name, import_id) values(@name, @importId)";
            const string childSql = "insert into catalog.categories (name, parent_id, import_id) values(@name, @parentId, @importId)";

            Console.WriteLine("Parent Categories");
            using (var connection = new NpgsqlConnection(connectionString))
            {
                await connection.OpenAsync();
                using (var command = new NpgsqlCommand(parentSql, connection))
                {
                    foreach (var parent in categories.GroupBy(x => x.Category1Id).Select(x => x.First()))
                    {
                        command.Parameters.AddWithValue("name", parent.Category1Name);
                        command.Parameters.AddWithValue("importId", parent.Category1Id);

                        command.ExecuteNonQuery();
                        command.Parameters.Clear();
                    }
                }
                
                // fetch all inserted parents
                using (var command = new NpgsqlCommand("select id, import_id from catalog.categories where parent_id is null", connection))
                {
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            parentCategoryIds.Add(reader.GetInt64(1), reader.GetInt64(0));
                        }
                    }
                }

                Console.WriteLine("Child Categories");
                using (var command = new NpgsqlCommand(childSql, connection))
                {
                    foreach (var child in categories.GroupBy(x => x.Category2Id).Select(x => x.First()))
                    {
                        
                        command.Parameters.AddWithValue("name", child.Category2Name);
                        command.Parameters.AddWithValue("parentId", parentCategoryIds[child.Category1Id]);
                        command.Parameters.AddWithValue("importId", child.Category2Id);

                        command.ExecuteNonQuery();
                        command.Parameters.Clear();
                    }
                }
            }
        }
    }
}
