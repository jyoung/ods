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
            await CreateProducts();
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
            Console.WriteLine("Creating Brands ...");

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
            Console.WriteLine("Creating Categories ...");

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

        private async Task<IDictionary<long, long>> GetBrandIds()
        {
            var brandIds = new Dictionary<long, long>();

            using (var connection = new NpgsqlConnection(connectionString))
            {
                await connection.OpenAsync();
                using (var command = new NpgsqlCommand("select id, import_id from catalog.brands", connection))
                {
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            brandIds.Add(reader.GetInt64(1), reader.GetInt64(0));
                        }
                    }
                }
            }

            return brandIds;
        }

        private async Task<IDictionary<long, long>> GetCategoryIds()
        {
            var categoryIds = new Dictionary<long, long>();

            using (var connection = new NpgsqlConnection(connectionString))
            {
                await connection.OpenAsync();
                using (var command = new NpgsqlCommand("select id, import_id from catalog.categories", connection))
                {
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            categoryIds.Add(reader.GetInt64(1), reader.GetInt64(0));
                        }
                    }
                }
            }

            return categoryIds;
        }

        private async Task CreateProducts()
        {
            Console.WriteLine("Creating Products ...");

            const string productSql = @"insert into catalog.products 
(item_number, title, short_description, retail_price, retail_currency, small_image_url, large_image_url, brand_id) 
values(@itemNumber, @title, @shortDescription, @retailPrice, @retailCurrency, @smallUrl, @largeUrl, @brandId) returning id";
            const string categorySql = @"insert into catalog.product_categories (product_id, category_id) values(@productId, @categoryId)";
            const string imageSql = @"insert into catalog.product_images (product_id, small_image_url, large_image_url) values (@productId, @smallUrl, @largeUrl)";
            const string copySql = @"insert into catalog.product_copy (product_id, long_description, notes, bullets) values (@productId, @description, @notes, @bullets)";

            var brands = await GetBrandIds();
            var categories = await GetCategoryIds();

            //insert into pais(nombre, capital) values(@nombre, @capital) RETURNING id
            //Object res = query.ExecuteScalar();

            using (var connection = new NpgsqlConnection(connectionString))
            {
                await connection.OpenAsync();
                using (var productCommand = new NpgsqlCommand(productSql, connection))
                {
                    foreach (var product in data)
                    {
                        productCommand.Parameters.AddWithValue("itemNumber", product.ItemNumber);
                        productCommand.Parameters.AddWithValue("title", product.Title);
                        productCommand.Parameters.AddWithValue("shortDescription", "Sed rutrum et mi a consequat. Fusce a viverra nunc. Donec mattis sed eros sed viverra.");
                        productCommand.Parameters.AddWithValue("retailPrice", product.RetailPrice);
                        productCommand.Parameters.AddWithValue("retailCurrency", "US");
                        productCommand.Parameters.AddWithValue("smallUrl", product.PrimarySmallImage);
                        productCommand.Parameters.AddWithValue("largeUrl", product.PrimaryLargeImage);
                        productCommand.Parameters.AddWithValue("brandId", brands[product.BrandId]);

                        var productId = await productCommand.ExecuteScalarAsync();

                        productCommand.Parameters.Clear();
                                                
                        // product categories
                        using (var categoryCommand = new NpgsqlCommand(categorySql, connection))
                        {
                            categoryCommand.Parameters.AddWithValue("productId", productId);
                            categoryCommand.Parameters.AddWithValue("categoryId", categories[product.Category1Id]);

                            await categoryCommand.ExecuteNonQueryAsync();

                            categoryCommand.Parameters.Clear();

                            categoryCommand.Parameters.AddWithValue("productId", productId);
                            categoryCommand.Parameters.AddWithValue("categoryId", categories[product.Category2Id]);

                            await categoryCommand.ExecuteNonQueryAsync();

                            categoryCommand.Parameters.Clear();
                        }

                        // product images
                        using (var imageCommand = new NpgsqlCommand(imageSql, connection))
                        {
                            imageCommand.Parameters.AddWithValue("productId", productId);
                            imageCommand.Parameters.AddWithValue("smallUrl", product.AdditionalImage1Small);
                            imageCommand.Parameters.AddWithValue("largeUrl", product.AdditionalImage1Large);

                            await imageCommand.ExecuteNonQueryAsync();

                            imageCommand.Parameters.Clear();
                        }

                        // product copy
                        using (var copyCommand = new NpgsqlCommand(copySql, connection))
                        {
                            copyCommand.Parameters.AddWithValue("productId", productId);
                            copyCommand.Parameters.AddWithValue("description", product.CopyDescription);
                            copyCommand.Parameters.AddWithValue("notes", product.CopyNotes);
                            copyCommand.Parameters.AddWithValue("bullets", $"{product.CopyBullet1}|{product.CopyBullet2}|{product.CopyBullet3}");

                            await copyCommand.ExecuteNonQueryAsync();

                            copyCommand.Parameters.Clear();
                        }
                    }
                }
            }
        }
    }
}
