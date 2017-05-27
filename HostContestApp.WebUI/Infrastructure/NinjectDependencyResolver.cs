using System;
using System.Collections.Generic;
using System.Configuration;
using System.Web.Mvc;
using Moq;
using Ninject;
using HostContestApp.Domain.Abstract;
using HostContestApp.Domain.Entities;
using HostContestApp.Domain.Concrete;

namespace HostContestApp.WebUI.Infrastructure
{
    public class NinjectDependencyResolver : IDependencyResolver
    {
        private IKernel kernel;

        public NinjectDependencyResolver(IKernel kernelParam)
        {
            kernel = kernelParam;
            AddBindings();
        }

        public object GetService(Type serviceType)
        {
            return kernel.TryGet(serviceType);
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return kernel.GetAll(serviceType);
        }

        private void AddBindings()
        {
            // Здесь размещаются привязки
            Mock<IFormRepository> mock = new Mock<IFormRepository>();
            mock.Setup(m => m.Forms).Returns(new List<Form>
             {
                 new Form { FormId = 1, Date = DateTime.Today, Description = "desc1",
                     ResponsiblePersonName = "RespPers1",  TypeId = 1, ClosingDate = DateTime.Today
                           },
                 new Form { FormId = 2, Date = DateTime.Today, Description = "desc2",
                     ResponsiblePersonName = "RespPers2",  TypeId = 2, ClosingDate = DateTime.Today
                           }
              });
            //kernel.Bind<IFormRepository>().ToConstant(mock.Object);
            kernel.Bind<IFormRepository>().To<EFFormRepository>();
        }
    }
}