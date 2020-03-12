namespace OutdoorShop.Catalog.Migrations
{
    public static class Constants
    {
        public static class Schemas 
        {
            public static string Catalog = "catalog";
        }

        public static class Tables
        {
            public static string Brands = "brands";
            public static string Categories = "categories";
            public static string Products = "products";
            public static string ProductCategories = "product_categories";
            public static string ProductCopy = "product_copy";
            public static string ProductImages = "product_images";
        }

        public static class Columns
        {
            public static string Id = "id";
            public static string Name = "name";
            public static string ImportId = "import_id";
            public static string ParentId = "parent_id";
            public static string ProductId = "product_id";
            public static string CategoryId = "category_id";
            public static string Title = "title";
            public static string ShortDescription = "short_description";
            public static string RetailPrice = "retail_price";
            public static string RetailCurrency = "retail_currency";
            public static string BrandId = "brand_id";
            public static string ItemNumber = "item_number";
            public static string SmallImageUrl = "small_image_url";
            public static string LargeImageUrl = "large_image_url";
            public static string LongDescription = "long_description";
            public static string Notes = "notes";
            public static string Bullets = "bullets";
        }

        public static class ForeignKeys
        {
            public static string ProductCateoriesToProducts = "fk_product_categories_to_products";
            public static string ProductCopyToProducts = "fk_product_copy_to_products";
            public static string ProductImagesToProducts = "fk_product_images_to_products";
        }
    }
}
