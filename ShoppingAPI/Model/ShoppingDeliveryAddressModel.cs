using Microsoft.EntityFrameworkCore.Metadata.Internal;
using ShoppingAPI.Data.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShoppingAPI.Model
{
    public class ShoppingDeliveryAddressModel : BaseModels
    {
        public int UserId { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public string FullName { get; set; }
        public bool IsDefault { get; set; }
    }
}
