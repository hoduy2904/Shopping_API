using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShoppingAPI.Data.Models
{
    public class Product : BaseModels
    {
        public Product()
        {
            ProductVariations = new HashSet<ProductVariation>();
            ProductImages = new HashSet<ProductImage>();
            Carts = new HashSet<Cart>();
            InvoicesDetails = new HashSet<InvoicesDetails>();
            ProductRatings = new HashSet<ProductRating>();
        }
        [Column(TypeName = "varchar(50)")]
        public string SKUS { get; set; }
        [StringLength(200)]
        public string Name { get; set; }
        public int CategoryId { get; set; }
        public string Description { get; set; }
        [ForeignKey("CategoryId")]
        public Category? Category { get; set; }
        public ICollection<ProductVariation> ProductVariations { get; set; }
        public ICollection<ProductImage> ProductImages { get; set; }
        public ICollection<Cart>? Carts { get; set; }
        public ICollection<InvoicesDetails> InvoicesDetails { get; set; }
        public ICollection<ProductRating> ProductRatings { get; set; }
    }
}
