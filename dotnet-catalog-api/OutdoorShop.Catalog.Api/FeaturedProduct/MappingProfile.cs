namespace OutdoorShop.Catalog.Api.FeaturedProduct
{
    using System;
    using AutoMapper;
    using OutdoorShop.Catalog.Domain.Product;

    public class MappingProfile : Profile
    {
        public MappingProfile() 
        {
            // document -> model
            CreateMap<FeaturedProductDocument, GetAll.Model>();
            
            // model -> document
        }
    }
}