namespace OutdoorShop.Catalog.Api.Product
{
    using System.Collections.Generic;
    using System.Runtime.Serialization;
    using System.Threading;
    using System.Threading.Tasks;
    using AutoMapper;
    using MediatR;
    using OutdoorShop.Catalog.Api.SharedModels;
    using OutdoorShop.Catalog.Domain;
    using OutdoorShop.Catalog.Domain.Product;

    public class GetById
    {
        public class Query : IRequest<Model>
        {
            public string Id { get; set; }
        }

        public class QueryHandler : IRequestHandler<Query, Model>
        {
            private readonly IDocumentRepository<ProductDocument> repository;
            private readonly IMapper mapper;

            public QueryHandler(IDocumentRepository<ProductDocument> repository, IMapper mapper)
            {
                this.repository = repository;
                this.mapper = mapper;
            }

            public async Task<Model> Handle(Query request, CancellationToken cancellationToken)
            {
                var document = await repository.GetAsync(request.Id);
                return mapper.Map<Model>(document);
            }
        }

        /// <summary>
        /// Product Model
        /// </summary>
        [DataContract(Name = "ProductDetail")]
        public class Model
        {
            public int Id { get; set; }
            public string Title { get; set; }
            public string ShortDescription { get; set; }
            public BrandModel Brand { get; set; }
            public CopyModel Copy { get; set; }
            public PriceModel Price { get; set; }
            public PriceModel SalePrice { get; set; }
            public ImageModel PrimaryImage { get; set; }
            public List<ImageModel> AdditionalImages { get; set; }
            public List<CategoryModel> Categories { get; set; }
        }
    }
}