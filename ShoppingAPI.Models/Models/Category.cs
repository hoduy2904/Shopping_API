using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShoppingAPI.Data.Models
{
    public class Category :BaseModels
    {
        public Category()
        {
            Categories = new HashSet<Category>();
        }
        [StringLength(50)]
        public string Name { get; set; }
        public int? CategoryId { get; set; }
        public string? Image { get; set; }
        [ForeignKey("CategoryId")]
        public virtual Category? CategoryParrent { get; set; }
        public virtual ICollection<Category> Categories { get; set; }
    }
}
