
using System.ComponentModel.DataAnnotations;

namespace ShoppingAPI.Data.Models
{
    public class User : BaseModels
    {
        public User()
        {
            ShoppingDeliveryAddresses = new HashSet<ShoppingDeliveryAddress>();
            Carts = new HashSet<Cart>();
            UserRoles = new HashSet<UserRole>();
            RefreshTokens = new HashSet<RefreshToken>();
            Invoices = new HashSet<Invoice>();
        }
        [StringLength(50)]
        public string? FristName { get; set; }
        [StringLength(50)]
        public string LastName { get; set; }
        public bool Sex { get; set; }
        [DataType("varchar(18)")]
        public string? IdentityCard { get; set; }
        [DataType("varchar(50)")]
        public string Username { get; set; }
        [DataType("varchar(200)")]
        public string PasswordHash { get; set; }
        [DataType("varchar(100)")]
        public string? Email { get; set; }
        public ICollection<ShoppingDeliveryAddress> ShoppingDeliveryAddresses { get; set; }
        public ICollection<Cart> Carts { get; set; }
        public ICollection<UserRole> UserRoles { get; set; }
        public ICollection<RefreshToken> RefreshTokens { get; set; }
        public ICollection<Invoice> Invoices { get; set; }
    }
}
