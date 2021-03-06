﻿using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;

namespace VanArsdel.Inventory.Data
{
    public partial class LocalDataProvider : IDataProvider
    {
        private LocalDb _datasource = null;

        public LocalDataProvider()
        {
            _datasource = new LocalDb();
        }

        public async Task<IList<CountryCode>> GetCountryCodesAsync()
        {
            return await _datasource.CountryCodes.ToListAsync();
        }

        public async Task<IList<OrderStatus>> GetOrderStatusAsync()
        {
            return await _datasource.OrderStatus.ToListAsync();
        }

        public async Task<IList<PaymentType>> GetPaymentTypesAsync()
        {
            return await _datasource.PaymentTypes.ToListAsync();
        }

        public async Task<IList<Shipper>> GetShippersAsync()
        {
            return await _datasource.Shippers.ToListAsync();
        }

        public async Task<IList<TaxType>> GetTaxTypesAsync()
        {
            return await _datasource.TaxTypes.ToListAsync();
        }

        public async Task<PageResult<Product>> GetProductsAsync(int pageIndex, int pageSize, string query)
        {
            IQueryable<Product> items = _datasource.Products;
            if (!String.IsNullOrEmpty(query))
            {
                items = items.Where(r => r.SearchTerms.Contains(query, StringComparison.OrdinalIgnoreCase));
            }
            int count = items.Count();
            int index = Math.Min(Math.Max(0, count - 1) / pageSize, pageIndex);
            var records = await items.Skip(index * pageSize).Take(pageSize).ToListAsync();
            return new PageResult<Product>(index, pageSize, count, records);
        }

        public async Task DeleteProduct(string id)
        {
            var item = _datasource.Products.Where(r => r.ProductID == id).FirstOrDefault();
            if (item != null)
            {
                _datasource.Products.Remove(item);
                await _datasource.SaveChangesAsync();
            }
            else
            {
                // TODO: Handle issue "Trying to delete unexisting item"
            }
        }

        #region Dispose
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_datasource != null)
                {
                    _datasource.Dispose();
                }
            }
        }
        #endregion
    }
}
