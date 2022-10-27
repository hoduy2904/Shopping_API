using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShoppingAPI.Data.Models
{
    public class Product :BaseModels
    {
        [StringLength(200)]
        public string Name { get; set; }
        public int CategoryId { get; set; }
        public string Description { get; set; }
        [ForeignKey("CategoryId")]
        public virtual Category? Category { get; set; }
        public virtual IEnumerable<ProductVariation>? ProductVariations { get; set; }
        public virtual IEnumerable<ProductImage>? ProductImages { get; set; }
        public virtual IEnumerable<Cart>? Carts { get; set; }
    }
}
