using System.ComponentModel.DataAnnotations.Schema;

namespace ShoppingAPI.Models
{
    public class ProductImage : BaseModels
    {
        public string Image { get; set; }
        public int ProductId { get; set; }
        
        public int? ProductVariationId { get; set; }
        [ForeignKey("ProductVariationId")]
        public virtual ProductVariation? ProductVariation { get; set; }
        [ForeignKey("ProductId")]
        public virtual Product? Product { get; set; }

    }
}
