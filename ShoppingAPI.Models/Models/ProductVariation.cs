using System.ComponentModel.DataAnnotations.Schema;

namespace ShoppingAPI.Data.Models
{
    public class ProductVariation :BaseModels
    {
        public string? Name { get; set; }
        public int ProductId { get; set; }
        public int Number { get; set; }
        public double PriceOld { get; set; }
        public double PriceCurrent { get; set; }
        public string? Variation { get; set; }
        public virtual ProductVariation? ProductVariate { get; set; }
        public virtual IEnumerable<ProductVariation>? ProductVariations { get; set; }
        [ForeignKey("ProductId")]
        public virtual Product? Product { get; set; }
        public virtual IEnumerable<ProductImage>? ProductImages { get; set; }
        public virtual IEnumerable<Cart>? Carts { get; set; }
    }
}
