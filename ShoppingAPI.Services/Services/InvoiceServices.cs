using Microsoft.EntityFrameworkCore;
using ShoppingAPI.Data.Models;
using ShoppingAPI.REPO.Repository;
using ShoppingAPI.Services.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingAPI.Services.Services
{
    public class InvoiceServices : IInvoiceServices
    {
        private readonly IRepository<Invoice> repository;
        public InvoiceServices(IRepository<Invoice> repository)
        {
            this.repository = repository;
        }
        public async Task DeleteInvoiceAsync(int id)
        {
            var invoice = await repository.GetAsync(id);
            await repository.DeleteAsync(invoice);
        }

        public IQueryable<Invoice> GetInvoices()
        {
            return repository.GetAll();
        }

        public async Task<Invoice> GetInvoiceAsync(int id)
        {
            var invoice = await repository
                .Where(x => x.Id == id && x.IsTrash == false)
                .Include(id => id.InvoicesDetails)
                .ThenInclude(p => p.Product)
                .Include(id => id.InvoicesDetails)
                .ThenInclude(pv => pv.ProductVariation)
                .Include(u => u.User)
                .SingleOrDefaultAsync();
            return invoice;
        }

        public IQueryable<Invoice> GetInvoicesByUserId(int UserId)
        {
            var invoices = repository
                .Where(x => x.UserId == UserId && x.IsTrash == false)
                .Include(id => id.InvoicesDetails)
                .ThenInclude(p => p.Product)
                .Include(id => id.InvoicesDetails)
                .ThenInclude(pv => pv.ProductVariation)
                .Include(u => u.User);
            return invoices;
        }

        public async Task InsertInvoiceAsync(Invoice invoice)
        {
            await repository.InsertAsync(invoice);
        }

        public async Task UpdateInvoiceAsync(Invoice invoice)
        {
            await repository.UpdateAsync(invoice);
        }

        public IQueryable<Invoice> Where(Expression<Func<Invoice, bool>> expression)
        {
            return repository.Where(expression);
        }
    }
}
