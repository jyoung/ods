namespace OutdoorShop.Catalog.Domain.Product
{
    using System.Collections.Generic;

    public class Copy
    {
        public string Description { get; set; }
        public string Notes { get; set; }
        public List<string> Bullets { get; set; }

        public Copy()
        {
            Bullets = new List<string>();
        }
    }
}