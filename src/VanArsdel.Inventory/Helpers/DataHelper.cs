﻿using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;

using VanArsdel.Inventory.Data;
using VanArsdel.Inventory.Models;

namespace VanArsdel.Inventory
{
    public class DataHelper
    {
        static public DataHelper Current { get; }

        static DataHelper()
        {
            Current = new DataHelper();
        }

        public IList<CountryCodeModel> CountryCodes { get; private set; }
        public IList<OrderStatusModel> OrderStatus { get; private set; }
        public IList<PaymentTypeModel> PaymentTypes { get; private set; }
        public IList<ShipperModel> Shippers { get; private set; }
        public IList<TaxTypeModel> TaxTypes { get; private set; }

        public async Task InitializeAsync(IDataProviderFactory providerFactory)
        {
            using (var dataProvider = providerFactory.CreateDataProvider())
            {
                var countryCodes = await dataProvider.GetCountryCodesAsync();
                CountryCodes = countryCodes.Select(r => new CountryCodeModel(r)).ToList();
                var orderStatus = await dataProvider.GetOrderStatusAsync();
                OrderStatus = orderStatus.Select(r => new OrderStatusModel(r)).ToList();
                var paymentTypes = await dataProvider.GetPaymentTypesAsync();
                PaymentTypes = paymentTypes.Select(r => new PaymentTypeModel(r)).ToList();
                var shippers = await dataProvider.GetShippersAsync();
                Shippers = shippers.Select(r => new ShipperModel(r)).ToList();
                var taxtTypes = await dataProvider.GetTaxTypesAsync();
                TaxTypes = taxtTypes.Select(r => new TaxTypeModel(r)).ToList();
            }
        }

        public string GetCountry(string id)
        {
            return CountryCodes.Where(r => r.CountryCodeID == id).Select(r => r.Name).FirstOrDefault();
        }

        public string GetOrderStatus(int id)
        {
            return OrderStatus.Where(r => r.Status == id).Select(r => r.Name).FirstOrDefault();
        }

        public string GetPaymentType(int id)
        {
            return PaymentTypes.Where(r => r.PaymentTypeID == id).Select(r => r.Name).FirstOrDefault();
        }

        public string Getshipper(int id)
        {
            return Shippers.Where(r => r.ShipperID == id).Select(r => r.Name).FirstOrDefault();
        }

        public string GetTaxDesc(int id)
        {
            // TODO: Update Database with correct values
            id = id * 10;
            return TaxTypes.Where(r => r.TaxTypeID == id).Select(r => $"{r.Rate} %").FirstOrDefault();
        }
        public decimal GetTaxRate(int id)
        {
            // TODO: Update Database with correct values
            id = id * 10;
            return TaxTypes.Where(r => r.TaxTypeID == id).Select(r => r.Rate).FirstOrDefault();
        }
    }
}
