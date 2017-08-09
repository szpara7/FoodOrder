using FoodOrder.Interfaces.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FoodOrder.DAL;
using System.Data.Entity;

namespace FoodOrder.Abstract
{
    public class PriceRepository : IPriceRepository
    {
        private DbCtx context = new DbCtx();
        public void Add(Price objectT)
        {
            context.Entry(objectT).State = EntityState.Added;
            context.SaveChanges();
        }

        public void Edit(Price objectT)
        {
            context.Entry(objectT).State = EntityState.Modified;
            context.SaveChanges();
        }

        public IEnumerable<Price> GetAll()
        {
            return context.Prices.ToList();
        }

        public Price GetById(int? objectId)
        {
            return context.Prices.Where(t => t.PriceID == objectId).FirstOrDefault();
        }

        public bool Remove(int? objectId)
        {
            var price = GetById(objectId);
            price.IsDeleted = true;

            context.Entry(price).State = EntityState.Modified;
            context.SaveChanges();
            return true;
        }
    }
}