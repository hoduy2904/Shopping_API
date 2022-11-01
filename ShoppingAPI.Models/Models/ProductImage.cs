using System.ComponentModel.DataAnnotations.Schema;

namespace ShoppingAPI.Data.Models
{
    public class ProductImage : BaseModels
    {
        public string Image { get; set; }
        public int ProductId { get; set; }
        
        public int? ProductVariationId { get; set; }
        [ForeignKey("ProductVariationId")]
        public ProductVariation? ProductVariation { get; set; }
        [ForeignKey("ProductId")]
        public Product? Product { get; set; }

    }
}
