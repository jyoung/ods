namespace OutdoorShop.Catalog.Api.FeaturedProduct
{
    using AutoMapper;
    using OutdoorShop.Catalog.Domain.Product;

    public class MappingProfile : Profile
    {
        public MappingProfile() 
        {
            // document -> model
            CreateMap<FeaturedProductDocument, GetAll.Model>();

            // entity -> model
            //CreateMap<ProductEntity, GetAll.Model>()
            //    .ForMember(dest => dest.Price, opts => opts.MapFrom(src => src))
            //    .ForMember(dest => dest.PrimaryImage, opts => opts.MapFrom(src => src));

            // model -> document
        }
    }
}