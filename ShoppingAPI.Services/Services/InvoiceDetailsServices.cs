using Microsoft.EntityFrameworkCore;
using ShoppingAPI.Data.Models;
using ShoppingAPI.REPO.Repository;
using ShoppingAPI.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingAPI.Services.Services
{
    public class InvoiceDetailsServices : IInvoiceDetailsServices
    {
        private readonly IRepository<InvoicesDetails> repository;
        public InvoiceDetailsServices(IRepository<InvoicesDetails> repository)
        {
            this.repository = repository;
        }

        public async Task DeleteInvoiceDetailsAsync(int id)
        {
            var invoiceDetails = await repository.GetAsync(id);
            await repository.DeleteAsync(invoiceDetails);
        }

        public async Task DeleteInvoiceDetailsRangeAsync(IEnumerable<InvoicesDetails> invoicesDetails)
        {
            await repository.DeleteRangeAsync(invoicesDetails);
        }

        public async Task<InvoicesDetails> GetInvoiceDetailsAsync(int id)
        {
            return await repository.GetAsync(id);
        }

        public async Task<IEnumerable<InvoicesDetails>> GetInvoicesDetails()
        {
            return await repository.GetAllAsync();
        }

        public IQueryable<InvoicesDetails> GetInvoicesDetailsByInvoiceId(int InvoiceId)
        {
            var invoiceDetails = repository.Where(id => id.InvoiceId == InvoiceId && id.IsTrash == false)
                .Include(p => p.Product)
                .Include(pv => pv.ProductVariation);
            return invoiceDetails;
        }

        public async Task InsertInvoiceDetailRangesAsync(IEnumerable<InvoicesDetails> invoicesDetails)
        {
            await repository.InsertRangeAsync(invoicesDetails);
        }

        public async Task InsertInvoiceDetailsAsync(InvoicesDetails invoicesDetails)
        {
            await repository.InsertAsync(invoicesDetails);
        }

        public async Task UpdateInvoiceDetailsAsync(InvoicesDetails invoicesDetails)
        {
            await repository.UpdateAsync(invoicesDetails);
        }

        public async Task UpdateInvoiceDetailsRangeAsync(IEnumerable<InvoicesDetails> invoicesDetails)
        {
            await repository.UpdateRangeAsync(invoicesDetails);
        }

        public IQueryable<InvoicesDetails> Where(Expression<Func<InvoicesDetails, bool>> expression)
        {
            return repository.Where(expression);
        }
    }
}
