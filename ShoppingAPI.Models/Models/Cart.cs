using System.ComponentModel.DataAnnotations.Schema;

namespace ShoppingAPI.Data.Models
{
    public class Cart :BaseModels
    {
        public int ProductId { get; set; }
        public double Price { get; set; }
        public int ProductVarationId { get; set; }
        public int Number { get; set; }
        public int UserId { get; set; }
        [ForeignKey("ProductId")]
        public virtual Product? Product { get; set; }
        [ForeignKey("UserId")]
        public virtual User? User { get; set; }
    }
}
