using System.Collections.Generic;

namespace SimpleStore.Core.Entities
{
    public class Category : Entity
    {
        public int CategoryId { get; set; }
        public int? ParentCategoryId { get; set; }
        public string Name { get; set; }

        public virtual Category ParentCategory { get; set; }
        public virtual IList<Item> Items { get; set; }
    }
}
