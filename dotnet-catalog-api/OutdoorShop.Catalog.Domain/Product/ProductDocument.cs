namespace OutdoorShop.Catalog.Domain.Product
{
    using System.Collections.Generic;

    public class ProductDocument : DocumentBase
    {
        public string Title { get; set; }
        public string ShortDescription { get; set; }
        public Brand Brand { get; set; }
        public Copy Copy { get; set; }
        public Price Price { get; set; }
        public Image PrimaryImage { get; set; }
        public List<Image> AdditionalImages { get; set; }
        public List<Category> Categories { get; set; }

        protected ProductDocument()
        {
            Brand = new Brand();
            Price = new Price();
            Copy = new Copy();
            PrimaryImage = new Image();
            AdditionalImages = new List<Image>();
            Categories = new List<Category>();
        }
        public ProductDocument(string id, string title) : this()
        {
            Id = id;
            Title = title;
        }
    }
}