namespace OutdoorShop.Catalog.Domain.Tests.Product
{
    using Domain.Product;
    using Shouldly;
    using Xunit;

    public class ProductDocumentTests
    {
        [Fact]
        public void Constructor_creates_all_objects()
        {
            var product = new ProductDocument("0", "");

            product.Brand.ShouldNotBeNull();
            product.Price.ShouldNotBeNull();
            product.Copy.ShouldNotBeNull();
            product.SalePrice.ShouldNotBeNull();
            product.Categories.ShouldNotBeNull();
            product.AdditionalImages.ShouldNotBeNull();
        }

        [Fact]
        public void Constructor_initializes_properties()
        {
            var product = new ProductDocument("1234", "My Cool Product");

            product.Id.ShouldBe("1234");
            product.Title.ShouldBe("My Cool Product");
        }
    }
}