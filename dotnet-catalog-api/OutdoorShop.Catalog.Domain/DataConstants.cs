namespace OutdoorShop.Catalog.Domain
{
    public static class DataConstants
    {
        public static class Schemas
        {
            public const string Catalog = "catalog";
        }

        public static class Tables
        {
            public const string Brands = "brands";
            public const string Categories = "categories";
            public const string Products = "products";
            public const string ProductCategories = "product_categories";
            public const string ProductCopy = "product_copy";
            public const string ProductImages = "product_images";
        }

        public static class Columns
        {
            public const string Id = "id";
            public const string Name = "name";
            public const string ImportId = "import_id";
            public const string ParentId = "parent_id";
            public const string ProductId = "product_id";
            public const string CategoryId = "category_id";
            public const string Title = "title";
            public const string ShortDescription = "short_description";
            public const string RetailPrice = "retail_price";
            public const string RetailCurrency = "retail_currency";
            public const string BrandId = "brand_id";
            public const string ItemNumber = "item_number";
            public const string SmallImageUrl = "small_image_url";
            public const string LargeImageUrl = "large_image_url";
            public const string LongDescription = "long_description";
            public const string Notes = "notes";
            public const string Bullets = "bullets";
        }

        public static class ForeignKeys
        {
            public const string ProductCateoriesToProducts = "fk_product_categories_to_products";
            public const string ProductCopyToProducts = "fk_product_copy_to_products";
            public const string ProductImagesToProducts = "fk_product_images_to_products";
        }
    }
}
