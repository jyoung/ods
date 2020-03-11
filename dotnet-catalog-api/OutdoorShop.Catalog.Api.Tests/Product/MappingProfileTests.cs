namespace OutdoorShop.Catalog.Api.Tests.Product
{
    using AutoMapper;
    using Product = OutdoorShop.Catalog.Api.Product ;
    using FeaturedProduct = OutdoorShop.Catalog.Api.FeaturedProduct;
    using Xunit;

    public class MappingProfileTests
    {
        [Fact]
        public void Mapping_Configuration_Is_Valid()
        {
            var configuration = new MapperConfiguration(cfg => {
                cfg.AddProfile<MappingProfile>();
                cfg.AddProfile<Product.MappingProfile>();
                cfg.AddProfile<FeaturedProduct.MappingProfile>();
            });

            configuration.AssertConfigurationIsValid();
        }
    }
}
