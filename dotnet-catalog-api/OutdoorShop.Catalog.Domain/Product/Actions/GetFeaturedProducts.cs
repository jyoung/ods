namespace OutdoorShop.Catalog.Domain.Product.Actions
{
    using Dapper;
    using MediatR;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;

    public class GetFeaturedProducts
    {
        public class Query : IRequest<IEnumerable<ProductEntity>>
        {

        }

        public class QueryHandler : IRequestHandler<Query, IEnumerable<ProductEntity>>
        {
            private readonly IDbConnection db;

            public QueryHandler(IDbConnection db)
            {
                this.db = db;
            }

            public async Task<IEnumerable<ProductEntity>> Handle(Query request, CancellationToken cancellationToken)
            {
                var query = $@"select * from {DataConstants.Schemas.Catalog}.{DataConstants.Tables.Products}";
                var products = await db.QueryAsync<ProductEntity>(query);

                // fake a collection of featured products for the time being
                var featuredProducts = GetRandomSample(products.ToList(), 10);

                return featuredProducts.ToList();
            }
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
