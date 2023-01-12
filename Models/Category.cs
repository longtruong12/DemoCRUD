using Microsoft.Build.Framework;

namespace CRUDApp.Models
{
    public class Category
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public int Status { get; set; }

        public virtual ICollection<Product> Product { get; set; }
    }
}
