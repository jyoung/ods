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
        }

        public static class Columns
        {
            public static string Id = "id";
            public static string Name = "name";
            public static string ImportId = "import_id";
            public static string ParentId = "parent_id";
        }
    }
}
