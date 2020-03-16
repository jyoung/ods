namespace OutdoorShop.Catalog.Domain.Product
{
    using Dapper;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Linq;
    using System.Threading.Tasks;

    public interface IProductRepository
    {
        Task<IEnumerable<ProductEntity>> FetchFeaturedProducts();
        Task<ProductEntity> FetchById(long id);
    }

    public class ProductRepository : IProductRepository
    {
        private readonly IDbConnection db;

        public ProductRepository(IDbConnection db)
        {
            this.db = db;
        }

        public async Task<IEnumerable<ProductEntity>> FetchFeaturedProducts()
        {
            const string sql = "select * from catalog.products";

            var products = await db.QueryAsync<ProductEntity>(sql);

            return GetRandomSample(products.ToList(), 10);
        }

        public async Task<ProductEntity> FetchById(long id)
        {
            const string productSql = "select * from catalog.products where id = @Id";
            const string copySql = "select * from catalog.product_copy where product_id = @Id";
            const string imageSql = "select * from catalog.product_images where product_id = @Id";

            var sql = productSql + ";" + copySql + ";" + imageSql;

            ProductEntity product;

            using (var multi = await db.QueryMultipleAsync(sql, new { Id = id }))
            {
                product = multi.Read<ProductEntity>().FirstOrDefault();
                if (product != null)
                {
                    product.Copy = multi.Read<ProductCopyEntity>().First();
                    product.Images = multi.Read<ProductImageEntity>().ToList();
                }
            }
            

            return product;
        }

        private static IEnumerable<T> GetRandomSample<T>(IList<T> list, int sampleSize)
        {
            if (list == null) throw new ArgumentNullException("list");
            if (sampleSize > list.Count) throw new ArgumentException("sampleSize may not be greater than list count", "sampleSize");

            var indices = new Dictionary<int, int>(); int index;
            var rnd = new Random();

            for (int i = 0; i < sampleSize; i++)
            {
                int j = rnd.Next(i, list.Count);
                if (!indices.TryGetValue(j, out index)) index = j;

                yield return list[index];

                if (!indices.TryGetValue(i, out index)) index = i;
                indices[j] = index;
            }
        }
    }
}
