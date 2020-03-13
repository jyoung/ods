namespace OutdoorShop.Catalog.Api.Categories
{
    using MediatR;
    using OutdoorShop.Catalog.Domain.Category.Actions;
    using System.Linq;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;

    public class GetAll
    {
        public class Query : IRequest<IEnumerable<Model>>
        {

        }

        public class QueryHandler : IRequestHandler<Query, IEnumerable<Model>>
        {
            private readonly IMediator mediator;

            public QueryHandler(IMediator mediator)
            {
                this.mediator = mediator;
            }

            public async Task<IEnumerable<Model>> Handle(Query request, CancellationToken cancellationToken)
            {
                var parents = new List<Model>();

                var categories = await mediator.Send(new GetAllCategories.Query());
                              
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

                // TODO: Cache the parents model so we don't have to look them up again
            }
        }

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
