namespace OutdoorShop.Catalog.Domain.Category.Actions
{
    using Dapper;
    using MediatR;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;

    public class GetAllCategories
    {
        public class Query : IRequest<IEnumerable<CategoryEntity>>
        {

        }

        public class QueryHandler : IRequestHandler<Query, IEnumerable<CategoryEntity>>
        {
            private readonly IDbConnection db;

            public QueryHandler(IDbConnection db)
            {
                this.db = db;
            }
            public async Task<IEnumerable<CategoryEntity>> Handle(Query request, CancellationToken cancellationToken)
            {
                var parents = new List<CategoryEntity>();

                var query = $@"select * from {DataConstants.Schemas.Catalog}.{DataConstants.Tables.Categories}";

                var categories = await db.QueryAsync<CategoryEntity>(query);

                var lookup = categories.ToLookup(x => x.ParentId);

                foreach (var category in categories)
                {
                    if (category.ParentId.HasValue == false)
                    {
                        parents.Add(category);
                    }

                    if (lookup.Contains(category.Id))
                    {
                        category.Children = lookup[category.Id].ToList();
                    }
                }

                return parents;
            }
        }
    }
}
