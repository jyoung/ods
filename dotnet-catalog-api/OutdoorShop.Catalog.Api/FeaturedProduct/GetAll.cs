namespace OutdoorShop.Catalog.Api.FeaturedProduct
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.Serialization;
    using System.Threading;
    using System.Threading.Tasks;
    using AutoMapper;
    using MediatR;
    using Microsoft.Extensions.Caching.Distributed;
    using OutdoorShop.Catalog.Api.SharedModels;
    using OutdoorShop.Catalog.Domain.Product;
    using System.Text.Json;
    using OutdoorShop.Catalog.Domain;

    public class GetAll
    {
        public class Query : IRequest<IEnumerable<Model>>
        {

        }

        public class QueryHandler : IRequestHandler<Query, IEnumerable<Model>>
        {
            private readonly IDocumentRepository<FeaturedProductDocument> repository;
            private readonly IMapper mapper;
            private readonly IDistributedCache cache;

            public QueryHandler(IDocumentRepository<FeaturedProductDocument> repository, IMapper mapper, IDistributedCache cache)
            {
                this.repository = repository;
                this.mapper = mapper;
                this.cache = cache;
            }

            public async Task<IEnumerable<Model>> Handle(Query request, CancellationToken cancellationToken)
            {
                //  check if documents are in cache
                //  if so, return cached documents
                //  if not, fetch featured products from featured products repository
                //  then for each featured product, fetch the product from the product repository
                //  build a collection of models based on the product
                //  cache the collection
                //  return the collection
                //var products = await db.Products.ToListAsync();

                const string cacheKey = "ods-featured-products";

                var serializedProducts = await cache.GetStringAsync(cacheKey);

                if (serializedProducts != null)
                {
                    var cachedProducts = JsonSerializer.Deserialize<List<ProductEntity>>(serializedProducts);
                    return mapper.Map<IEnumerable<Model>>(cachedProducts);
                }
                else
                {
                    var featuredProducts = await repository.GetAllAsync();

                    serializedProducts = JsonSerializer.Serialize(featuredProducts);

                    var cacheOptions = new DistributedCacheEntryOptions()
                                                .SetSlidingExpiration(TimeSpan.FromMinutes(5))
                                                .SetAbsoluteExpiration(DateTime.UtcNow.AddHours(6));

                    await cache.SetStringAsync(cacheKey, serializedProducts, cacheOptions);

                    return mapper.Map<IEnumerable<Model>>(featuredProducts);
                }
            }
        }

        [DataContract(Name = "FeaturedProduct")]
        public class Model
        {
            public long Id { get; set; }
            public string Title { get; set; }
            public string ShortDescription { get; set; }
            public PriceModel Price { get; set; }
            public ImageModel PrimaryImage { get; set; } 
        }
    }
}