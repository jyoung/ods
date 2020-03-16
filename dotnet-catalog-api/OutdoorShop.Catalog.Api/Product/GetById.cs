namespace OutdoorShop.Catalog.Api.Product
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.Serialization;
    using System.Threading;
    using System.Threading.Tasks;
    using AutoMapper;
    using MediatR;
    using Microsoft.EntityFrameworkCore;
    using OutdoorShop.Catalog.Api.SharedModels;
    using OutdoorShop.Catalog.Domain;
    using OutdoorShop.Catalog.Domain.Product;

    public class GetById
    {
        public class Query : IRequest<Model>
        {
            public long Id { get; set; }
        }

        public class QueryHandler : IRequestHandler<Query, Model>
        {
            private readonly CategoryContext db;
            private readonly IMapper mapper;

            public QueryHandler(CategoryContext db, IMapper mapper)
            {
                this.db = db;
                this.mapper = mapper;
            }

            public async Task<Model> Handle(Query request, CancellationToken cancellationToken)
            {
                var product = await db.Products
                    .Include(x => x.Copy)
                    .FirstOrDefaultAsync(x => x.Id == request.Id);

                return mapper.Map<Model>(product);
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
            //public BrandModel Brand { get; set; }
            public CopyModel Copy { get; set; }
            public PriceModel Price { get; set; }
            public ImageModel PrimaryImage { get; set; }
            //public List<ImageModel> AdditionalImages { get; set; }
            //public List<CategoryModel> Categories { get; set; }
        }
    }
}