using FoodOrder.Abstract;
using FoodOrder.Interfaces.Abstract;
using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FoodOrder.Infrastructure
{
    public class NinjectDependencyResolver : IDependencyResolver
    {
        private IKernel ninjectKernel;

        public NinjectDependencyResolver()
        {
            ninjectKernel = new StandardKernel();
            AddBindings();
        }
        public object GetService(Type serviceType)
        {
            return ninjectKernel.TryGet(serviceType);
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return ninjectKernel.GetAll(serviceType);
        }

        private void AddBindings()
        {
            ninjectKernel.Bind<IEmployeeRepository>().To<EmployeeRepository>();
        }
    }
}