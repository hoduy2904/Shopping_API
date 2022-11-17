using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingAPI.Common.Models
{
    public class ProductRatingImageRequest
    {
        public int ProductImageRatingId { get; set; }
        public IFormFile Image { get; set; }
    }
}
