namespace OutdoorShop.Catalog.Domain.Product
{
    using System;
    
    public class FeaturedProductDocument : DocumentBase, IEquatable<FeaturedProductDocument>
    {
        public string Title { get; set; }
        public string ShortDescription { get; set;}
        public Price Price { get; set; }
        public Price SalePrice { get; set; }   
        public Image PrimaryImage {get; set;}

        public FeaturedProductDocument(string id, string title)
        {
            this.Id = id;
            this.Title = title;
        }
        
        protected FeaturedProductDocument()
        {
            PrimaryImage = new Image();
            Price = new Price();
            SalePrice = new Price();
        }

        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            
            FeaturedProductDocument fp = obj as FeaturedProductDocument;

            if (fp == null) return false;
            else return Equals(fp);
        }

        public override int GetHashCode() => Int32.Parse(Id);

        public bool Equals(FeaturedProductDocument other)
        {
            if (other == null) return false;
            return (this.Id.Equals(other.Id));
        }
    }
}