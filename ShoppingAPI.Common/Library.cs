using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingAPI.Common
{
    public class Library
    {
        public static bool isAdmin(string roleName)
        {
            if (roleName.Equals("SuperAdmin") || roleName.Equals("Admin"))
                return true;
            return false;
        }
    }
}
