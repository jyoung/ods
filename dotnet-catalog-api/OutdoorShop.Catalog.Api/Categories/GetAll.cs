namespace OutdoorShop.Catalog.Api.Categories
{
    using MediatR;
    using System.Linq;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Runtime.Serialization;
    using OutdoorShop.Catalog.Domain.Category;

    public class GetAll
    {
        public class Query : IRequest<IEnumerable<Model>>
        {

        }

        public class QueryHandler : IRequestHandler<Query, IEnumerable<Model>>
        {
            private readonly ICategoryRepository repository;

            public QueryHandler(ICategoryRepository repository)
            {
                this.repository = repository;
            }

            public async Task<IEnumerable<Model>> Handle(Query request, CancellationToken cancellationToken)
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
