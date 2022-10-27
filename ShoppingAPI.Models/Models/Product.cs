using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShoppingAPI.Data.Models
{
    public class Product :BaseModels
    {
        public Product()
        {
            ProductVariations = new HashSet<ProductVariation>();
            ProductImages = new HashSet<ProductImage>();
            Carts = new HashSet<Cart>();
        }
        [StringLength(200)]
        public string Name { get; set; }
        public int CategoryId { get; set; }
        public string Description { get; set; }
        [ForeignKey("CategoryId")]
        public virtual Category? Category { get; set; }
        public virtual ICollection<ProductVariation> ProductVariations { get; set; }
        public virtual ICollection<ProductImage> ProductImages { get; set; }
        public virtual ICollection<Cart>? Carts { get; set; }
    }
}
