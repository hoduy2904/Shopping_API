using Microsoft.EntityFrameworkCore.Metadata.Internal;
using ShoppingAPI.Data.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ShoppingAPI.Model
{
    public class UserModel : BaseModels
    {
        [StringLength(50)]
        public string? FristName { get; set; }
        [StringLength(50)]
        public string LastName { get; set; }
        public bool Sex { get; set; }
        [Column(TypeName = "varchar(18)")]
        public string? IdentityCard { get; set; }
        [Column(TypeName = "varchar(50)")]
        public string Username { get; set; }
        [Column(TypeName = "varchar(200)")]
        public string Password { get; set; }
        [Column(TypeName = "varchar(100)")]
        public string? Email { get; set; }
        public string? RoleName { get; set; }
    }
}
