using System.Collections.Generic;

namespace OutdoorShop.Catalog.Domain.Category
{
    public class CategoryEntity : Entity
    {
        public long? ParentId { get; set; }
        
        public string Name { get; set; }

        public IEnumerable<CategoryEntity> Children { get; set; }
    }
}
