using ShoppingAPI.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingAPI.Services.Interfaces
{
    public interface IInvoiceServices
    {
        Task<Invoice> GetInvoiceAsync(int id);
        IQueryable<Invoice> GetInvoicesByUserId(int UserId);
        IQueryable<Invoice> GetInvoices();
        Task InsertInvoiceAsync(Invoice invoice);
        Task UpdateInvoiceAsync(Invoice invoice);
        Task DeleteInvoiceAsync(int id);
        IQueryable<Invoice> Where(Expression<Func<Invoice, bool>> expression);
    }
}
