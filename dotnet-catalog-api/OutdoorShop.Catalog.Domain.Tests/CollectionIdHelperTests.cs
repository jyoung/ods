namespace OutdoorShop.Catalog.Domain.Tests
{
    using System;
    using OutdoorShop.Catalog.Domain.Product;
    using Shouldly;
    using Xunit;

    public class CollectionIdHelperTests
    {
        [Fact]
        public void GetId_returns_the_proper_pluralized_name()
        {
            var collectionId = CollectionIdHelper.GetId(typeof(ProductDocument));

            collectionId.ShouldBe("Products");
        }
    }
}