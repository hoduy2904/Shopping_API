using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingAPI.Data.Models
{
    public class ProductRating : BaseModels
    {
        public ProductRating()
        {
            productRatingImages = new HashSet<ProductRatingImage>();
        }
        public string Description { get; set; }
        public int ProductId { get; set; }
        public int? ProductVariationId { get; set; }
        public int Rating { get; set; }
        public bool isEdit { get; set; }
        public int? ProductRatingId { get; set; }

        public int? InvoiceId { get; set; }
        public int UserId { get; set; }
        [ForeignKey("UserId")]
        public User? User { get; set; }
        [ForeignKey("InvoiceId")]
        public Invoice? Invoice { get; set; }
        [ForeignKey("ProductId")]
        public Product? Product { get; set; }
        [ForeignKey("ProductVariationId")]
        public ProductVariation? ProductVariation { get; set; }
        [ForeignKey("ProductRatingId")]
        public ProductRating? ProductRatingReply { get; set; }
        public ICollection<ProductRatingImage> productRatingImages { get; set; }
    }
}
