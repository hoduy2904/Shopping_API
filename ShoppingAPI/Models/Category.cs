using System.ComponentModel.DataAnnotations.Schema;

namespace ShoppingAPI.Models
{
    public class Category :BaseModels
    {
        public string Name { get; set; }
        public int? CategoryId { get; set; }
        public string? Image { get; set; }
        [ForeignKey("CategoryId")]
        public virtual Category? CategoryParrent { get; set; }
        public virtual IEnumerable<Category>? Categories { get; set; }
    }
}
