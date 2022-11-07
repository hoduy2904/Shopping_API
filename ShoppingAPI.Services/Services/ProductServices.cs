﻿using Microsoft.EntityFrameworkCore;
using ShoppingAPI.Data.Models;
using ShoppingAPI.REPO.Repository;
using ShoppingAPI.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingAPI.Services.Services
{
    public class ProductServices : IProductServices
    {
        private readonly IRepository<Product> repository;
        public ProductServices(IRepository<Product> repository)
        {
            this.repository = repository;
        }

        public async Task DeleteProduct(int id)
        {
            var product = await repository.GetAsync(id);
            await repository.DeleteAsync(product);
        }

        public async Task<Product> GetProductAsync(int id)
        {
            return await repository.Where(x => x.Id == id && x.IsTrash == false)
                .Include(pi => pi.ProductImages)
                .Include(pv => pv.ProductVariations)
                .Include(pct => pct.Category)
                .Include(pr => pr.ProductRatings)
                .ThenInclude(pri => pri.productRatingImages)
                .SingleOrDefaultAsync();
        }

        public async Task<IEnumerable<Product>> GetProductsAsync()
        {
            return repository.Where(x => x.IsTrash == false)
                .Include(pi => pi.ProductImages)
                .Include(pv => pv.ProductVariations)
                .Include(pct => pct.Category)
                .AsEnumerable();
        }

        public async Task InsertProduct(Product product)
        {
            await repository.InsertAsync(product);
        }

        public async Task UpdateProduct(Product product)
        {
            await repository.UpdateAsync(product);
        }
        public IQueryable<Product> Where(Expression<Func<Product, bool>> expression)
        {
            return repository.Where(expression);
        }
    }
}
