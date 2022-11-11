using ShoppingAPI.Data.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingAPI.Models
{
    public class CategoryModel : BaseModels
    {
        [StringLength(50)]
        public string Name { get; set; }
        public int? CategoryId { get; set; }
        public string? Image { get; set; }
    }
}
