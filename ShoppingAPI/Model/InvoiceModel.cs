using Microsoft.EntityFrameworkCore.Metadata.Internal;
using ShoppingAPI.Data.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ShoppingAPI.Model
{
    public class InvoiceModel : BaseModels
    {
        public int UserId { get; set; }
        [StringLength(100)]
        public string FullName { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime? PaymentTime { get; set; }
        public DateTime? DeliveryTime { get; set; }
        public DateTime? CompletionTime { get; set; }
    }
}
