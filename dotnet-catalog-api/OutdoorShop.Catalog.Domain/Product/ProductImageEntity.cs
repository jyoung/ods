namespace OutdoorShop.Catalog.Domain.Product
{
    public class ProductImageEntity : Entity
    {
        public long ProductId { get; set; }
        public string SmallImageUrl { get; set; }
        public string LargeImageUrl { get; set; }
    }
}
