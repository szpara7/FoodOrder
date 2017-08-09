using FoodOrder.Interfaces.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FoodOrder.DAL;
using System.Data.Entity;

namespace FoodOrder.Abstract
{
    public class CustomerRepository : ICustomerRepository
    {
        private DbCtx context = new DbCtx();
        public void Add(Customer objectT)
        {
            context.Entry(objectT).State = EntityState.Added;
            context.SaveChanges();
        }

        public void Edit(Customer objectT)
        {
            context.Entry(objectT).State = EntityState.Modified;
            context.SaveChanges();
        }

        public IEnumerable<Customer> GetAll()
        {
            return context.Customers.ToList();
        }

        public Customer GetById(int? objectId)
        {
            return context.Customers.Where(t => t.CustomerID == objectId).FirstOrDefault();
        }

        public bool Remove(int? objectId)
        {
            var customer = GetById(objectId);
            customer.IsDeleted = true;

            context.Entry(customer).State = EntityState.Modified;
            context.SaveChanges();
            return true;
        }
    }
}