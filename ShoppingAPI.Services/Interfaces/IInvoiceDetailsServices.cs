using ShoppingAPI.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingAPI.Services.Interfaces
{
    public interface IInvoiceDetailsServices
    {
        Task<InvoicesDetails> GetInvoiceDetailsAsync(int id);
        IQueryable<InvoicesDetails> GetInvoicesDetailsByInvoiceId(int InvoiceId);
        IQueryable<InvoicesDetails> GetInvoicesDetails();
        Task InsertInvoiceDetailsAsync(InvoicesDetails invoicesDetails);
        Task UpdateInvoiceDetailsAsync(InvoicesDetails invoicesDetails);
        Task DeleteInvoiceDetailsAsync(int id);
        Task InsertInvoiceDetailRangesAsync(IEnumerable<InvoicesDetails> invoicesDetails);
        Task UpdateInvoiceDetailsRangeAsync(IEnumerable<InvoicesDetails> invoicesDetails);
        Task DeleteInvoiceDetailsRangeAsync(IEnumerable<InvoicesDetails> invoicesDetails);
        IQueryable<InvoicesDetails> Where(Expression<Func<InvoicesDetails, bool>> expression);
    }
}
