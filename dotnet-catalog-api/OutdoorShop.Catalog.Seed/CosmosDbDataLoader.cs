namespace OutdoorShop.Catalog.Seed
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.IO;
    using System.Threading;
    using System.Threading.Tasks;
    using CsvHelper;
    using Microsoft.Azure.Documents;
    using Microsoft.Azure.Documents.Client;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Serialization;
    using OutdoorShop.Catalog.Domain;
    using OutdoorShop.Catalog.Domain.Category;
    using OutdoorShop.Catalog.Domain.Product;

    public interface ICosmosDbDataLoader
    {
        Task Load();
    }

    public class CosmosDbDataLoader : ICosmosDbDataLoader
    {
        private const string DatabaseId = "Catalog";

        private readonly Uri endpoint;
        private readonly string key;

        private IList<ProductDocument> Documents = new List<ProductDocument>();

        private DocumentClient DocumentClient;

        public CosmosDbDataLoader(Uri endpoint, string key)
        {
            this.endpoint = endpoint;
            this.key = key;
        }

        public async Task Load()
        {
            Console.Out.WriteLine("Loading Data");

            LoadCSV();

            DocumentClient = new DocumentClient(endpoint, key, serializerSettings: new JsonSerializerSettings{
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            });
            
            await CreateDatabaseIfNotExistsAsync(DocumentClient);

            await CreateProducts();
            await CreateFeaturedProducts();
            await CreateCategories();
        }

        private void LoadCSV()
        {
            var index = 1000;

            Console.Out.WriteLine("Loading CSV");

            using (var reader = new StreamReader("OutdoorShop.csv"))
            {
                var csvReader = new CsvReader(reader, CultureInfo.InvariantCulture);
                
                foreach (var product in csvReader.GetRecords<Product>())
                {
                    Documents.Add(product.CreateDocument(index));

                    index++;
                }
            }
        }

        private async Task CreateProducts()
        {
            Console.Out.WriteLine("Creating Products");
            
            var collectionId = CollectionIdHelper.GetId(typeof(ProductDocument));

            await CreateCollectionIfNotExistsAsync(DocumentClient, collectionId);

            foreach (var document in Documents)
            {
                await DocumentClient.CreateDocumentAsync(UriFactory.CreateDocumentCollectionUri(DatabaseId, collectionId), document);
                Console.Out.WriteLine($"Created {document.Id} {document.Title}");
            }
        }

        private async Task CreateFeaturedProducts()
        {
            Console.Out.WriteLine("Creating Featured Products");
            var products = new List<FeaturedProductDocument>();

            var collectionId = CollectionIdHelper.GetId(typeof(FeaturedProductDocument));

            // randomly select 10 products to be "featured"
            var index = 0;
            var count = Documents.Count;
            var random = new Random();

            do 
            {
                var r = random.Next(count);
                var product = Documents[r];

                var featuredProduct = new FeaturedProductDocument(product.Id, product.Title) 
                {
                    PrimaryImage = product.PrimaryImage,
                    Price = product.Price,
                    SalePrice = product.SalePrice,
                    ShortDescription = product.ShortDescription
                };

                if (products.Contains(featuredProduct) == false)
                {
                    products.Add(featuredProduct);
                    index++;
                }

            } while (index < 10);
            
            await CreateCollectionIfNotExistsAsync(DocumentClient, collectionId);

            foreach (var fp in products)
            {
                 await DocumentClient.CreateDocumentAsync(UriFactory.CreateDocumentCollectionUri(DatabaseId, collectionId), fp);
                Console.Out.WriteLine($"Created {fp.Id} {fp.Title}");
            }
        }

        private async Task CreateCategories()
        {
            Console.Out.WriteLine("Creating Categories");

            var categories = new Dictionary<string, CategoryDocument>();

            var collectionId = CollectionIdHelper.GetId(typeof(CategoryDocument));

            await CreateCollectionIfNotExistsAsync(DocumentClient, collectionId);

            foreach (var document in Documents)
            {
                var primaryCategory = document.Categories[0];
                CategoryDocument categoryDocument;

                if (categories.ContainsKey(primaryCategory.Id))
                {
                    categoryDocument = categories[primaryCategory.Id];
                }
                else
                {
                    categoryDocument = new CategoryDocument(primaryCategory.Id, primaryCategory.Name);
                    categories.Add(primaryCategory.Id, categoryDocument);
                }

                var secondaryCategory = document.Categories[1];
                categoryDocument.AddSubCategory(new CategoryDocument(secondaryCategory.Id, secondaryCategory.Name));
            }

            foreach (var category in categories)
            {
                await DocumentClient.CreateDocumentAsync(UriFactory.CreateDocumentCollectionUri(DatabaseId, collectionId), category.Value);
                Console.Out.WriteLine($"Created {category.Key} {category.Value.Name}");
            }
        }

        private async Task CreateDatabaseIfNotExistsAsync(DocumentClient client)
        {
            try
            {
                await client.ReadDatabaseAsync(UriFactory.CreateDatabaseUri(DatabaseId));
            }
            catch (DocumentClientException docEx)
            {
                if (docEx.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    await client.CreateDatabaseAsync(new Database { Id = DatabaseId });
                }
                else
                {
                    throw;
                }
            }
        }

        private async Task CreateCollectionIfNotExistsAsync(DocumentClient client, string collectionId)
        {
            try
            {
                await client.ReadDocumentCollectionAsync(UriFactory.CreateDocumentCollectionUri(DatabaseId, collectionId));
            }
            catch (DocumentClientException docEx)
            {
                if (docEx.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    await client.CreateDocumentCollectionAsync(
                        UriFactory.CreateDatabaseUri(DatabaseId),
                        new DocumentCollection { Id = collectionId },
                        new RequestOptions { OfferThroughput = 400 });
                }
                else
                {
                    throw;
                }
            }
        }
    }
}