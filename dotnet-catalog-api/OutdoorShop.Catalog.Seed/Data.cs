namespace OutdoorShop.Catalog.Seed
{
    using System;
    using OutdoorShop.Catalog.Domain.Product;

    public class Data
    {
        public string ItemNumber { get; set; }
        public int Category1Id { get; set; }
        public string Category1Name { get; set; }
        public int Category2Id { get; set; }
        public string Category2Name { get; set; }
        public string Title { get; set; }
        public int BrandId { get; set; }
        public string BrandName { get; set; }
        public string CopyDescription { get; set; }
        public string CopyNotes { get; set; }
        public string CopyBullet1 { get; set; }
        public string CopyBullet2 { get; set; }
        public string CopyBullet3 { get; set; }
        public string PrimarySmallImage { get; set; }
        public string PrimaryLargeImage { get; set; }
        public string AdditionalImage1Small { get; set; }
        public string AdditionalImage1Large { get; set; }
        public decimal RetailPrice { get; set; }
        public decimal SalePrice { get; set; }

        public ProductDocument CreateDocument(int index)
        {
            // cheap, naive way to make a unique id
            var id = $"{Category1Id}{Category2Id}{BrandId}{index}";

            var document = new ProductDocument(id, Title);   
            document.Categories.Add(new Category{Id = Category1Id.ToString(), Name = Category1Name });
            document.Categories.Add(new Category{Id = Category2Id.ToString(), Name = Category2Name });

            document.Brand.Id = BrandId.ToString();
            document.Brand.Name = BrandName;

            document.ShortDescription = CopyDescription.Substring(0, 57);

            document.Copy.Description = CopyDescription;
            document.Copy.Notes = CopyNotes;
            document.Copy.Bullets.Add(CopyBullet1);
            document.Copy.Bullets.Add(CopyBullet2);
            document.Copy.Bullets.Add(CopyBullet3);

            document.Price = new Price {Currency = "USD", Value = RetailPrice};
            document.SalePrice = new Price {Currency = "USD", Value = SalePrice};

            document.PrimaryImage.SmallUrl = PrimarySmallImage;
            document.PrimaryImage.LargeUrl = PrimaryLargeImage;

            document.AdditionalImages.Add(new Image() {SmallUrl =  AdditionalImage1Small, LargeUrl = AdditionalImage1Large});

            return document;
        }
    }
}