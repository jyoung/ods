namespace OutdoorShop.Catalog.Api.Product
{
    using AutoMapper;
    using OutdoorShop.Catalog.Domain.Product;

    public class MappingProfile : Profile
    {
        public MappingProfile() 
        {
            // document -> model
            //    CreateMap<ProductDocument, GetById.Model>();

            // entity -> model
            //CreateMap<ProductEntity, PriceModel>()
            //    .ForMember(dest => dest.Currency, opts => opts.MapFrom(src => src.RetailCurrency))
            //    .ForMember(dest => dest.Value, opts => opts.MapFrom(src => src.RetailPrice));

            //CreateMap<ProductEntity, ImageModel>()
            //    .ForMember(dest => dest.LargeUrl, opts => opts.MapFrom(src => src.LargeImageUrl))
            //    .ForMember(dest => dest.SmallUrl, opts => opts.MapFrom(src => src.SmallImageUrl));



            CreateMap<ProductEntity, GetById.Model>()
                .ForMember(dest => dest.Price, opts => opts.MapFrom(src => src))
                .ForMember(dest => dest.PrimaryImage, opts => opts.MapFrom(src => src))
                .ForMember(dest => dest.Copy, opts => opts.MapFrom(src => src.Copy));
                
                                
            
            // model -> document
        }
    }
}