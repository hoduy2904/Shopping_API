using System.ComponentModel.DataAnnotations.Schema;

namespace ShoppingAPI.Data.Models
{
    public class Cart :BaseModels
    {
        public int ProductId { get; set; }
        public int? ProductVarationId { get; set; }
        public int Number { get; set; }
        public int UserId { get; set; }
        [ForeignKey("ProductId")]
        public Product? Product { get; set; }
        [ForeignKey("UserId")]
        public User? User { get; set; }
        [ForeignKey("ProductVarationId")]
        public ProductVariation? ProductVariation { get; set; }
    }
}
