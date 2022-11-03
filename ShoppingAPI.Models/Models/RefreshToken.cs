using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingAPI.Data.Models
{
    public class RefreshToken : BaseModels
    {
        [StringLength(100)]
        public string TokenId { get; set; }
        [DataType("varchar(max)")]
        public string Token { get; set; }
        [DataType("varchar(200)")]
        public string Refresh { get; set; }
        [DataType("varchar(50)")]
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
        public User User { get; set; }
    }
}
