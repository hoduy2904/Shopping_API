
namespace ShoppingAPI.Data.Models
{
    public class User : BaseModels
    {
        public string? FristName { get; set; }
        public string LastName { get; set; }
        public bool Sex { get; set; }
        public string? IdentityCard { get; set; }
        public string Username { get; set; }
        public string PasswordHash { get; set; }
        public string? Email { get; set; }
        public virtual IEnumerable<InfomationUser>? InfomationUsers { get; set; }
        public virtual IEnumerable<Cart>? Carts { get; set; }
        public virtual IEnumerable<UserRole> UserRoles { get; set; }
        public virtual IEnumerable<RefreshToken>? RefreshTokens { get; set; }
    }
}
