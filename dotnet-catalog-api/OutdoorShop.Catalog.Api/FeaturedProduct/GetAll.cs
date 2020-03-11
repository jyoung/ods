namespace OutdoorShop.Catalog.Api.FeaturedProduct
{
    using System.Collections.Generic;
    using System.Runtime.Serialization;
    using System.Threading;
    using System.Threading.Tasks;
    using AutoMapper;
    using MediatR;
    using OutdoorShop.Catalog.Api.Models;
    using OutdoorShop.Catalog.Domain;
    using OutdoorShop.Catalog.Domain.Product;

    public class GetAll
    {
        public class Query : IRequest<IEnumerable<Model>>
        {

        }

        public class QueryHandler : IRequestHandler<Query, IEnumerable<Model>>
        {
            private readonly IDocumentRepository<FeaturedProductDocument> repository;
            private readonly IMapper mapper;

            public QueryHandler(IDocumentRepository<FeaturedProductDocument> repository, IMapper mapper)
            {
                this.repository = repository;
                this.mapper = mapper;
            }

            public async Task<IEnumerable<Model>> Handle(Query request, CancellationToken cancellationToken)
            {
                // TODO:
                //  check if documents are in cache
                //  if so, return cached documents
                //  if not, fetch featured products from featured products repository
                //  then for each featured product, fetch the product from the product repository
                //  build a collection of models based on the product
                //  cache the collection
                //  return the collection
                var documents = await repository.GetAllAsync();
                return mapper.Map<IEnumerable<Model>>(documents);
            }
        }

        [DataContract(Name = "FeaturedProduct")]
        public class Model
        {
            public int Id { get; set; }
            public string Title { get; set; }
            public string ShortDescription { get; set; }
            public PriceModel Price { get; set; }
            public PriceModel SalePrice { get; set; }
            public ImageModel PrimaryImage { get; set; } 
        }
    }
}