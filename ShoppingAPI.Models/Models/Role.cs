using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingAPI.Data.Models
{
    public class Role :BaseModels
    {
        public string Name { get; set; }
        public virtual IEnumerable<UserRole>? UserRoles { get; set; }
    }
}
