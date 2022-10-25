using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShoppingAPI.Data.Models
{
    public class InfomationUser :BaseModels
    {
        public int UserId { get; set; }
        public string Address { get; set; }
        [StringLength(12)]
        public string PhoneNumber { get; set; }
        [ForeignKey("UserId")]
        public virtual User User { get; set; }
    }
}
