using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingAPI.Data.Models
{
    public class Role :BaseModels
    {
        public Role()
        {
            UserRoles = new HashSet<UserRole>();
        }
        [StringLength(50)]
        public string Name { get; set; }
        public ICollection<UserRole>? UserRoles { get; set; }
    }
}
