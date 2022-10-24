using System.ComponentModel.DataAnnotations.Schema;

namespace ShoppingAPI.Models
{
    public class Product :BaseModels
    {
        public string Name { get; set; }
        public int CategoryId { get; set; }
        [ForeignKey("CategoryId")]
        public virtual Category? Category { get; set; }
        public virtual IEnumerable<ProductVariation>? ProductVariations { get; set; }
        public virtual IEnumerable<ProductImage>? ProductImages { get; set; }
    }
}
