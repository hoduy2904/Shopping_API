
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
        }
        public string? FristName { get; set; }
        public string LastName { get; set; }
        public bool Sex { get; set; }
        public string? IdentityCard { get; set; }
        public string Username { get; set; }
        public string PasswordHash { get; set; }
        public string? Email { get; set; }
        public virtual ICollection<ShoppingDeliveryAddress> ShoppingDeliveryAddresses { get; set; }
        public virtual ICollection<Cart> Carts { get; set; }
        public virtual ICollection<UserRole> UserRoles { get; set; }
        public virtual ICollection<RefreshToken> RefreshTokens { get; set; }
    }
}
