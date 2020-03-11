namespace OutdoorShop.Catalog.Api
{
    using AutoMapper;
    using OutdoorShop.Catalog.Api.Models;
    using OutdoorShop.Catalog.Domain.Product;

    public class MappingProfile : Profile
    {
        public MappingProfile() 
        {
            // document -> model
            CreateMap<Brand, BrandModel>();
            CreateMap<Copy, CopyModel>();
            CreateMap<Image, ImageModel>();
            CreateMap<Category, CategoryModel>();
            CreateMap<Price, PriceModel>();

            // model -> document
        }
    }
}