using FoodOrder.Abstract;
using FoodOrder.Interfaces;
using FoodOrder.Interfaces.Abstract;
using FoodOrder.Models;
using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace FoodOrder.Infrastructure
{
    public class NinjectControllerFactory : DefaultControllerFactory
    {
        private IKernel ninjectKernel;

        public NinjectControllerFactory()
        {
            ninjectKernel = new StandardKernel();
            AddBindings();
        }
        protected override IController GetControllerInstance(RequestContext requestContext, Type controllerType)
        {
            return controllerType == null ? null : (IController)ninjectKernel.Get(controllerType);
        }
        
        private void AddBindings()
        {
            ninjectKernel.Bind<ICategoryRepository>().To<CategoryRepository>();
            ninjectKernel.Bind<ICustomerRepository>().To<CustomerRepository>();
            ninjectKernel.Bind<IEmployeeRepository>().To<EmployeeRepository>();
            ninjectKernel.Bind<IOrderLineRepository>().To<OrderLineRepository>();
            ninjectKernel.Bind<IOrderRepository>().To<OrderRepository>();
            ninjectKernel.Bind<IPriceRepository>().To<PriceRepository>();
            ninjectKernel.Bind<IProductRepository>().To<ProductRepository>();
            ninjectKernel.Bind<IReviewRepository>().To<ReviewRepository>();

            ninjectKernel.Bind<IEmailSender>().To<EmailSender>();
        }
    }
}