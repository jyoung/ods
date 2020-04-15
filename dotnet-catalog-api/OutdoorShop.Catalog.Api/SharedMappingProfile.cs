namespace OutdoorShop.Catalog.Api
{
    using AutoMapper;
    using OutdoorShop.Catalog.Api.SharedModels;
    using OutdoorShop.Catalog.Domain.Product;
    using System.Collections.Generic;
    using System.Linq;

    public class SharedMappingProfile : Profile
    {
        public SharedMappingProfile() 
        {
            // document -> model
            CreateMap<Brand, BrandModel>();
            CreateMap<Copy, CopyModel>();
            CreateMap<Image, ImageModel>();
            CreateMap<Category, CategoryModel>();
            CreateMap<Price, PriceModel>();

            //CreateMap<ProductEntity, PriceModel>()
            //    .ForMember(dest => dest.Currency, opts => opts.MapFrom(src => src.RetailCurrency))
            //    .ForMember(dest => dest.Value, opts => opts.MapFrom(src => src.RetailPrice));

            //CreateMap<ProductEntity, ImageModel>()
            //    .ForMember(dest => dest.LargeUrl, opts => opts.MapFrom(src => src.LargeImageUrl))
            //    .ForMember(dest => dest.SmallUrl, opts => opts.MapFrom(src => src.SmallImageUrl));

            //CreateMap<ProductImageEntity, ImageModel>()
            //   .ForMember(dest => dest.LargeUrl, opts => opts.MapFrom(src => src.LargeImageUrl))
            //   .ForMember(dest => dest.SmallUrl, opts => opts.MapFrom(src => src.SmallImageUrl));

            //CreateMap<ProductCopyEntity, CopyModel>()
            //    .ForMember(dest => dest.Description, opts => opts.MapFrom(src => src.LongDescription))
            //    .ForMember(dest => dest.Notes, opts => opts.MapFrom(src => src.Notes))
            //    .ForMember(dest => dest.Bullets, opts => opts.MapFrom(src => SplitBullets(src.Bullets))); 

            // model -> document
        }

        private static List<string> SplitBullets(string bullets)
        {
            return bullets.Split('|').ToList();
        }
    }
}