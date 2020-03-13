namespace OutdoorShop.Catalog.Api
{
    using AutoMapper;
    using OutdoorShop.Catalog.Api.SharedModels;
    using OutdoorShop.Catalog.Domain.Product;

    public class SharedMappingProfile : Profile
    {
        public SharedMappingProfile() 
        {
            // document -> model
            CreateMap<Brand, BrandModel>();
            CreateMap<Copy, CopyModel>();
          //  CreateMap<Image, ImageModel>();
            CreateMap<Category, CategoryModel>();
            CreateMap<Price, PriceModel>();

            // model -> document
        }
    }
}