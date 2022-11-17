using ShoppingAPI.Data.Models;

namespace ShoppingAPI.Model
{
    public class UserRoleModel : BaseModels
    {
        public int UserId { get; set; }
        public int RoleId { get; set; }
    }
}
