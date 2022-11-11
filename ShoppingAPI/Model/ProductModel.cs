using ShoppingAPI.Data.Models;
using System.ComponentModel.DataAnnotations;

namespace ShoppingAPI.Model
{
    public class ProductModel : BaseModels
    {
        public string SKUS { get; set; }
        [StringLength(200)]
        public string Name { get; set; }
        public int CategoryId { get; set; }
        public string Description { get; set; }
    }
}
