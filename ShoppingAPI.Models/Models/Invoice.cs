using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingAPI.Data.Models
{
    public class Invoice : BaseModels
    {
        public Invoice()
        {
            InvoicesDetails = new HashSet<InvoicesDetails>();
        }
        public int UserId { get; set; }
        [StringLength(100)]
        public string FullName { get; set; }
        public string Address { get; set; }
        [DataType("varchar(12)")]
        public string PhoneNumber { get; set; }
        public DateTime? PaymentTime { get; set; }
        public DateTime? DeliveryTime { get; set; }
        public DateTime? CompletionTime { get; set; }
        [ForeignKey("UserId")]
        public User? User { get; set; }
        public ICollection<InvoicesDetails> InvoicesDetails { get; set; }
        [NotMapped]
        public double TotalMoney => InvoicesDetails.Sum(x => x.Total);
    }
}
