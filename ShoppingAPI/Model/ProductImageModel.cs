using ShoppingAPI.Data.Models;

namespace ShoppingAPI.Model
{
    public class ProductImageModel : BaseModels
    {
        public string Image { get; set; }
        public int ProductId { get; set; }

        public int? ProductVariationId { get; set; }
    }
}
