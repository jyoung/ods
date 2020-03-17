namespace OutdoorShop.Catalog.Api.Categories
{
    using MediatR;
    using System.Linq;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Runtime.Serialization;
    using OutdoorShop.Catalog.Domain.Category;
    using Microsoft.Extensions.Caching.Distributed;
    using System.Text.Json;
    using System;

    public class GetAll
    {
        public class Query : IRequest<IEnumerable<Model>>
        {

        }

        public class QueryHandler : IRequestHandler<Query, IEnumerable<Model>>
        {
            private readonly ICategoryRepository repository;
            private readonly IDistributedCache cache;

            public QueryHandler(ICategoryRepository repository, IDistributedCache cache)
            {
                this.repository = repository;
                this.cache = cache;
            }

            public async Task<IEnumerable<Model>> Handle(Query request, CancellationToken cancellationToken)
            {
                const string cacheKey = "ods-categories";

                List<Model> categories;

                var serializedCategories = await cache.GetStringAsync(cacheKey);

                if (serializedCategories != null)
                {
                    categories = JsonSerializer.Deserialize<List<Model>>(serializedCategories);
                }
                else
                {
                    categories = await GetCategories();
                    serializedCategories = JsonSerializer.Serialize(categories);

                    var cacheOptions = new DistributedCacheEntryOptions()
                                                .SetSlidingExpiration(TimeSpan.FromMinutes(5))
                                                .SetAbsoluteExpiration(DateTime.UtcNow.AddHours(6));

                    await cache.SetStringAsync(cacheKey, serializedCategories, cacheOptions);
                }
                
                return categories;
            }

            private async Task<List<Model>> GetCategories()
            {
                var parents = new List<Model>();

                var categories = await repository.FetchAllAsync();

                // convert the flat categories list to a heirarchy
                var lookup = categories.ToLookup(x => x.ParentId);

                Model parent = null;

                foreach (var category in categories)
                {
                    if (category.ParentId.HasValue == false)
                    {
                        parent = new Model { Id = category.Id, Name = category.Name };
                        parents.Add(parent);
                    }

                    if (lookup.Contains(category.Id) && parent != null)
                    {
                        parent.Children = lookup[category.Id].Select(x => new Model { Id = x.Id, Name = x.Name }).ToList();
                    }
                }

                return parents;
            }
        }

        [DataContract(Name = "CategoryTreeItem")]
        public class Model
        {
            public long Id { get; set; }
            public string Name { get; set; }

            public List<Model> Children { get; set; }

            public Model()
            {
                Children = new List<Model>();
            }
        }
    }
}
