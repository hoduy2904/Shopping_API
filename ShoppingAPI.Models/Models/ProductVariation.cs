using System.ComponentModel.DataAnnotations.Schema;

namespace ShoppingAPI.Data.Models
{
    public class ProductVariation : BaseModels
    {
        public ProductVariation()
        {
            ProductVariations = new HashSet<ProductVariation>();
            ProductImages = new HashSet<ProductImage>();
            Carts = new HashSet<Cart>();
            InvoicesDetails = new HashSet<InvoicesDetails>();
        }
        public string? Name { get; set; }
        public int ProductId { get; set; }
        public int Number { get; set; }
        public double PriceOld { get; set; }
        public double PriceCurrent { get; set; }
        public int? VariationId { get; set; }
        [ForeignKey("VariationId")]
        public ProductVariation? ProductVariate { get; set; }
        public ICollection<ProductVariation> ProductVariations { get; set; }
        [ForeignKey("ProductId")]
        public Product? Product { get; set; }
        public ICollection<ProductImage> ProductImages { get; set; }
        public ICollection<Cart> Carts { get; set; }
        public ICollection<InvoicesDetails> InvoicesDetails { get; set; }
    }
}
