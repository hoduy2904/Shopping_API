using ShoppingAPI.Data.Models;

namespace ShoppingAPI.Model
{
    public class ProductVariationModel : BaseModels
    {
        public string? Name { get; set; }
        public int ProductId { get; set; }
        public int Number { get; set; }
        public double PriceOld { get; set; }
        public double PriceCurrent { get; set; }
        public int? VariationId { get; set; }
    }
}
