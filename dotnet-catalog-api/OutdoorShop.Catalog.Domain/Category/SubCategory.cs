namespace OutdoorShop.Catalog.Domain.Category
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class SubCategory
    {
                public string Id { get; set; }
        public string Name { get; set; }

        public SubCategory() { }

        public SubCategory(string id, string name)
        {
            this.Id = id;
            this.Name = name;
        }

    }
}
