namespace OutdoorShop.Catalog.Api.Categories
{
    using MediatR;
    using System.Linq;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Runtime.Serialization;
    using OutdoorShop.Catalog.Domain;
    using Microsoft.EntityFrameworkCore;

    public class GetAll
    {
        public class Query : IRequest<IEnumerable<Model>>
        {

        }

        public class QueryHandler : IRequestHandler<Query, IEnumerable<Model>>
        {
            private readonly CategoryContext db;

            public QueryHandler(CategoryContext db)
            {
                this.db = db;
            }

            public async Task<IEnumerable<Model>> Handle(Query request, CancellationToken cancellationToken)
            {
                var parents = new List<Model>();

                var categories = await db.Categories
                    .Where(x => x.Parent == null)
                    .Include(x => x.Children)
                    .ToListAsync();


                Model parent = null;

                foreach (var category in categories)
                {
                    if (category.Parent == null)
                    {
                        parent = new Model { Id = category.Id, Name = category.Name };
                        parents.Add(parent);
                    }

                    if (category.Children.Any() && parent != null)
                    {
                        parent.Children = category.Children.Select(x => new Model { Id = x.Id, Name = x.Name }).ToList();
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
