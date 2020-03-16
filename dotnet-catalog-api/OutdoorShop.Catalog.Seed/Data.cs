namespace OutdoorShop.Catalog.Seed
{
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
    }
}