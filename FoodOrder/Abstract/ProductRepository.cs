using FoodOrder.Interfaces.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FoodOrder.DAL;
using System.Data.Entity;

namespace FoodOrder.Abstract
{
    public class ProductRepository : IProductRepository
    {
        private DbCtx context = new DbCtx();
        public void Add(Product objectT)
        {
            context.Entry(objectT).State = EntityState.Added;
            context.SaveChanges();
        }

        public void Edit(Product objectT)
        {
            context.Entry(objectT).State = EntityState.Modified;
            context.SaveChanges();
        }

        public IEnumerable<Product> GetAll()
        {
            return context.Products.ToList();
        }

        public Product GetById(int? objectId)
        {
            return context.Products.Where(t => t.ProductID == objectId).FirstOrDefault();
        }

        public bool Remove(int? objectId)
        {
            var product = GetById(objectId);
            product.IsDeleted = true;

            context.Entry(product).State = EntityState.Modified;
            context.SaveChanges();
            return true;
        }
    }
}