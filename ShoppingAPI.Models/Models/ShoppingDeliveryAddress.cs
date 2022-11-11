using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShoppingAPI.Data.Models
{
    public class ShoppingDeliveryAddress :BaseModels
    {
        public int UserId { get; set; }
        public string Address { get; set; }
        [Column(TypeName = "varchar(12)")]
        public string PhoneNumber { get; set; }
        [ForeignKey("UserId")]
        public User? User { get; set; }
        public string FullName { get; set; }
        public bool IsDefault { get; set; }
    }
}
