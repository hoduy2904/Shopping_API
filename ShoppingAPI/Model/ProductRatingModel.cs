using ShoppingAPI.Data.Models;

namespace ShoppingAPI.Model
{
    public class ProductRatingModel : BaseModels
    {
        public string Description { get; set; }
        public int ProductId { get; set; }
        public int? ProductVariationId { get; set; }
        public int Rating { get; set; }
        public bool isEdit { get; set; }
        public int ProductRatingImage { get; set; }
        public int? ProductRatingId { get; set; }
        public int UserId { get; set; }
    }
}
