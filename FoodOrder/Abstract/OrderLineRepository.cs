using FoodOrder.Interfaces.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FoodOrder.DAL;
using System.Data.Entity;

namespace FoodOrder.Abstract
{
    public class OrderLineRepository : IOrderLineRepository
    {
        private DbCtx context = new DbCtx();
        public void Add(OrderLine objectT)
        {
            context.Entry(objectT).State = EntityState.Added;
            context.SaveChanges();
        }

        public void Edit(OrderLine objectT)
        {
            context.Entry(objectT).State = EntityState.Modified;
            context.SaveChanges();
        }

        public IEnumerable<OrderLine> GetAll()
        {
            return context.OrderLines.ToList();
        }

        public OrderLine GetById(int? objectId)
        {
            return context.OrderLines.Where(t => t.OrderLineID == objectId).FirstOrDefault();
        }

        public bool Remove(int? objectId)
        {
            var orderLine = GetById(objectId);
            orderLine.IsDeleted = true;

            context.Entry(orderLine).State = EntityState.Modified;
            context.SaveChanges();
            return true;
        }
    }
}