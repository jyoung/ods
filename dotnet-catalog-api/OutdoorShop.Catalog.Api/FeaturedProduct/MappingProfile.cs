namespace OutdoorShop.Catalog.Api.FeaturedProduct
{
    using System;
    using AutoMapper;
    using OutdoorShop.Catalog.Api.SharedModels;
    using OutdoorShop.Catalog.Domain.Product;

    public class MappingProfile : Profile
    {
        public MappingProfile() 
        {
            // document -> model
            // CreateMap<FeaturedProductDocument, GetAll.Model>();

            // entity -> model
            //CreateMap<ProductEntity, PriceModel>()
            //    .ForMember(dest => dest.Currency, opts => opts.MapFrom(src => src.RetailCurrency))
            //    .ForMember(dest => dest.Value, opts => opts.MapFrom(src => src.RetailPrice));

            //CreateMap<ProductEntity, ImageModel>()
            //    .ForMember(dest => dest.LargeUrl, opts => opts.MapFrom(src => src.LargeImageUrl))
            //    .ForMember(dest => dest.SmallUrl, opts => opts.MapFrom(src => src.SmallImageUrl));

            CreateMap<ProductEntity, GetAll.Model>()
                .ForMember(dest => dest.Price, opts => opts.MapFrom(src => src))
                .ForMember(dest => dest.PrimaryImage, opts => opts.MapFrom(src => src));

            // model -> document
        }
    }
}