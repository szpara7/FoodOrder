using FoodOrder.Interfaces.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FoodOrder.DAL;
using System.Data.Entity;

namespace FoodOrder.Abstract
{
    public class OrderRepository : IOrderRepository
    {
        private DbCtx context = new DbCtx();
        public void Add(Order objectT)
        {
            context.Entry(objectT).State = EntityState.Added;
            context.SaveChanges();
        }

        public void Edit(Order objectT)
        {
            context.Entry(objectT).State = EntityState.Modified;
            context.SaveChanges();
        }

        public IEnumerable<Order> GetAll()
        {
            return context.Orders.ToList();
        }

        public Order GetById(int? objectId)
        {
            return context.Orders.Where(t => t.OrderID == objectId).FirstOrDefault();
        }

        public bool Remove(int? objectId)
        {
            return true;
        }
    }
}