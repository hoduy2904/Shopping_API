using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingAPI.Data.Models
{
    public class ProductRatingImage : BaseModels
    {
        public string Image { get; set; }
        public int ProductRatingId { get; set; }
        [ForeignKey("ProductRatingId")]
        public ProductRating? ProductRating { get; set; }
    }
}
