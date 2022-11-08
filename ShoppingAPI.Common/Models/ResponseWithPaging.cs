using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingAPI.Common.Models
{
    public class ResponseWithPaging : ResponseApi
    {
        public int PageCount { get; set; }
        public int PageNumber { get; set; }
        public int TotalItems { get; set; }
    }
}
