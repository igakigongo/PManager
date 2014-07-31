using Ninject;
using PManager.Domain.Abstract;
using PManager.Domain.Concrete;
using System;
using System.Collections.Generic;
using System.Web.Mvc;
using PManager.Domain.ViewModels;

namespace PManager.WebUI.Infrastructure
{
    public class NinjectDependencyResolver: IDependencyResolver
    {
        private readonly IKernel _kernel;
        public NinjectDependencyResolver(IKernel kernelParam)
        {
            _kernel = kernelParam;
            AddBindings();
        }

        public object GetService(Type serviceType)
        {
            return _kernel.Get(serviceType);
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return _kernel.GetAll(serviceType);
        }

        private void AddBindings()
        {
            _kernel.Bind<IDataTransferObject>().To<JsonDTO>();
            _kernel.Bind<IReportViewModel>().To<ReportViewModel>();
        }
    }
}