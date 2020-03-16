namespace OutdoorShop.Catalog.Domain.Product
{
    public class ProductEntity : Entity
    {
        public string ItemNumber { get; set; }
        public string Title { get; set; }
        public string ShortDescription { get; set; }
        public decimal RetailPrice { get; set; }
        public string RetailCurrency { get; set; }
        public string SmallImageUrl { get; set; }
        public string LargeImageUrl { get; set; }     

        public ProductCopyEntity Copy { get; internal set; }
    }
}
