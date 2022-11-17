using ShoppingAPI.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingAPI.Models
{
    public class CartModel : BaseModels
    {
        public int ProductId { get; set; }
        public int ProductVarationId { get; set; }
        public int Number { get; set; }
        public int UserId { get; set; }
    }
}
