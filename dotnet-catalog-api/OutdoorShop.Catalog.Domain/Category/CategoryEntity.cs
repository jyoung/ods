namespace OutdoorShop.Catalog.Domain.Category
{
    using System.Collections.Generic;

    public class CategoryEntity : Entity
    {
        public CategoryEntity Parent { get; set; }
        
        public string Name { get; set; }

        public IReadOnlyList<CategoryEntity> Children { get; set; }
    }
}
