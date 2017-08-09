using FoodOrder.Interfaces.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FoodOrder.DAL;
using System.Data.Entity;

namespace FoodOrder.Abstract
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private DbCtx context = new DbCtx();
        public void Add(Employee objectT)
        {
            context.Entry(objectT).State = EntityState.Added;
            context.SaveChanges();
        }

        public void Edit(Employee objectT)
        {
            context.Entry(objectT).State = EntityState.Modified;
            context.SaveChanges();
        }

        public IEnumerable<Employee> GetAll()
        {
            return context.Employees.ToList();
        }

        public Employee GetById(int? objectId)
        {
            return context.Employees.Where(t => t.EmployeeID == objectId).FirstOrDefault();
        }

        public bool Remove(int? objectId)
        {
            var employee = GetById(objectId);
            employee.IsDeleted = true;

            context.Entry(employee).State = EntityState.Modified;
            context.SaveChanges();
            return true;
        }
    }
}