using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingAPI.Data.Models
{
    public class RefreshToken : BaseModels
    {
        public string Token { get; set; }
        public string Refresh { get; set; }
        public string IPAdress { get; set; }
        public DateTime Expired { get; set; }
        public int UserId { get; set; }
        [NotMapped]
        public bool IsExpired
        {
            get
            {
                return DateTime.UtcNow > Expired;
            }
        }
        public virtual User User { get; set; }
    }
}
