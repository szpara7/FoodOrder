using FoodOrder.Interfaces.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FoodOrder.DAL;
using System.Data.Entity;

namespace FoodOrder.Abstract
{
    public class ReviewRepository : IReviewRepository
    {
        private DbCtx context = new DbCtx();
        public void Add(Review objectT)
        {
            context.Entry(objectT).State = EntityState.Added;
            context.SaveChanges();
        }

        public void Edit(Review objectT)
        {
            context.Entry(objectT).State = EntityState.Modified;
            context.SaveChanges();
        }

        public IEnumerable<Review> GetAll()
        {
            return context.Reviews.ToList();
        }

        public Review GetById(int? objectId)
        {
            return context.Reviews.Where(t => t.ReviewID == objectId).FirstOrDefault();
        }

        public bool Remove(int? objectId)
        {
            var review = GetById(objectId);
            review.IsDeleted = true;

            context.Entry(review).State = EntityState.Modified;
            context.SaveChanges();
            return true;
        }
    }
}