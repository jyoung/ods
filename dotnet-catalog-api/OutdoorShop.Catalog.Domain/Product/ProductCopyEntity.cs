﻿namespace OutdoorShop.Catalog.Domain.Product
{
    public class ProductCopyEntity : Entity
    {
        public long ProductId { get; set; }
        public string LongDescription { get; set; }
        public string Notes { get; set; }
        public string Bullets { get; set; }
    }
}
