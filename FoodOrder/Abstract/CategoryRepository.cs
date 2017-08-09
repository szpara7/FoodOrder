using FoodOrder.Interfaces.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FoodOrder.DAL;
using System.Data.Entity;

namespace FoodOrder.Abstract
{
    
    public class CategoryRepository : ICategoryRepository
    {
        private DbCtx context = new DbCtx();

        public void Add(Category objectT)
        {
            context.Entry(objectT).State = EntityState.Added;
            context.SaveChanges();
        }

        public void Edit(Category objectT)
        {
            context.Entry(objectT).State = EntityState.Modified;
            context.SaveChanges();
        }

        public IEnumerable<Category> GetAll()
        {
            return context.Categories.ToList();
        }

        public Category GetById(int? objectId)
        {
            return context.Categories.Where(t => t.CategoryID == objectId).FirstOrDefault();
        }

        public bool Remove(int? objectId)
        {
            var category = GetById(objectId);
            category.isDeleted = true;

            context.Entry(category).State = EntityState.Modified;
            context.SaveChanges();
            return true;            
        }
    }
}