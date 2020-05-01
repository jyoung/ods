namespace OutdoorShop.Product.Api.Tests
{
    using Shouldly;
    using Xunit;

    public class SanityCheck
	{
		[Fact]
		public void AllGood()
		{
			(1 == 1).ShouldBe(true);
		}
	}
}
