using NLog;
using System;
using System.Collections.Generic;
using System.Linq;

namespace WareHouse
{
    class WareHouseService
    {
        private List<IProduct> Products { get; set; }
        private ProductContext ProductContext;
        Logger logger = LogManager.GetCurrentClassLogger();

        public WareHouseService()
        {
            Products = new List<IProduct>();
            ProductContext = new ProductContext();
        }

        public void AddProduct(IProduct product)
        {
            try
            {
                if (product == null)
                {
                    throw new Exception("Product could not be null");
                }

                Products.Add(product);

                if (product is Milk)
                {
                    ProductContext.Milks.Add(product as Milk);
                }

                if (product is Bread)
                {
                    ProductContext.Breads.Add(product as Bread);
                }

                if (product is Water)
                {
                    ProductContext.Waters.Add(product as Water);
                }

                ProductContext.SaveChanges();
            }
            catch (Exception exception)
            {
                logger.Error(exception.Message);
            }
        }

        public void RemoveProduct(string name)
        {
            Products.Remove(Products.FirstOrDefault(x => x.Name == name));
            var milkForRemove = ProductContext.Milks?.FirstOrDefault(x => x.Name == name);
            var breadForemove = ProductContext.Breads?.FirstOrDefault(x => x.Name == name);
            var waterforremove = ProductContext.Waters?.FirstOrDefault(x => x.Name == name);

            if (milkForRemove != null)
            {
                ProductContext.Milks.Remove(milkForRemove);
            }

            if (breadForemove != null)
            {
                ProductContext.Breads.Remove(breadForemove);
            }

            if (waterforremove != null)
            {
                ProductContext.Waters.Remove(waterforremove);
            }

            ProductContext.SaveChanges();
        }

        public List<IProduct> GetProducts()
        {
            var collection = new List<IProduct>();
            collection.AddRange(ProductContext.Milks.ToList());
            collection.AddRange(ProductContext.Breads.ToList());
            collection.AddRange(ProductContext.Waters.ToList());

            return collection.ToList();
        }

    }
}
