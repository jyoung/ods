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
           // CreateCategories();
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
            const string sql = "insert into catalog.brands(id, name) values(@id, @name)";
            
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
                        command.Parameters.AddWithValue("id", brand.BrandId);
                        command.Parameters.AddWithValue("name", brand.BrandName);

                        command.ExecuteNonQuery();
                    }
                }
            }
        }

        private void CreateCategories()
        {
            var categories = data.GroupBy(x => new { x.Category1Id, x.Category2Id})
                .Select(x => x.First())
                .ToList();

            foreach (var category in categories)
            {
                Console.WriteLine($"Category Id1:{category.Category1Id.ToString()} Id2:{category.Category2Id.ToString()} Name:{category.Category1Name}");
            }
        }
    }
}
