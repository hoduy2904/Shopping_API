using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingAPI.Common.Models
{
    public class RegisterRequest
    {
        public string? FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public bool Sex { get; set; }
        public string IdentityCard { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
