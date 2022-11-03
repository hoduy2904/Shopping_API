using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingAPI.Data.Models
{
    public class InvoicesDetails : BaseModels
    {
        public int? InvoiceId { get; set; }
        public int? ProductId { get; set; }
        [StringLength(200)]
        public string ProductName { get; set; }
        public int? ProductVariationId { get; set; }
        public double Price { get; set; }
        public double Numbers { get; set; }
        public string? Image { get; set; }
        [NotMapped]
        public double Total => (Price * Numbers);
        [ForeignKey("InvoiceId")]
        public Invoice? Invoice { get; set; }
        [ForeignKey("ProductId")]
        public Product? Product { get; set; }
        [ForeignKey("ProductVariationId")]
        public ProductVariation? ProductVariation { get; set; }
    }
}
