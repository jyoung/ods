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
                var categories = await mediator.Send(new GetAllCategories.Query());

                var models = categories.Select(x => new Model { Id = x.Id, Name = x.Name }).ToList();

                // TODO: Cache the models so we don't have to look them up again

                return models;
            }
        }

        public class Model
        {
            public long Id { get; set; }
            public string Name { get; set; }
        }
    }
}
