using ShoppingAPI.Data.Models;
using System.ComponentModel.DataAnnotations;

namespace ShoppingAPI.Model
{
    public class InvoiceDetailModel : BaseModels
    {
        public int? InvoiceId { get; set; }
        public int? ProductId { get; set; }
        [StringLength(200)]
        public string ProductName { get; set; }
        public int? ProductVariationId { get; set; }
        public double Price { get; set; }
        public int Numbers { get; set; }
        public string? Image { get; set; }
    }
}
