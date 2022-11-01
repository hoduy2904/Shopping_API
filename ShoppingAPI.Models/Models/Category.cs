using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShoppingAPI.Data.Models
{
    public class Category : BaseModels
    {
        public Category()
        {
            Categories = new HashSet<Category>();
            Products= new HashSet<Product>();
        }
        [StringLength(50)]
        public string Name { get; set; }
        public int? CategoryId { get; set; }
        public string? Image { get; set; }
        [ForeignKey("CategoryId")]
        public Category? CategoryParrent { get; set; }
        public ICollection<Category> Categories { get; set; }
        public ICollection<Product> Products { get; set; }
    }
}
