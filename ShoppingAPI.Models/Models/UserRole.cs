using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingAPI.Data.Models
{
    public class UserRole :BaseModels
    {
        public int UserId { get; set; }
        public int RoleId { get; set; }
        [ForeignKey("UserId")]
        public User? User { get; set; }
        [ForeignKey("RoleId")]
        public Role? Role { get; set; }
    }
}
