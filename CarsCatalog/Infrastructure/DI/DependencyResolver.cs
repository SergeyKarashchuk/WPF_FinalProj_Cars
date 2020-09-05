using CarsCatalog.Infrastructure.DI;
using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;

namespace CarsCatalog
{
    public class DependencyResolver
    {
        private static readonly StandardKernel kernel;
        static DependencyResolver()
        {
            kernel = new StandardKernel(new CarCatalogModule());
        }

        public static T Resolve<T>()
        {
            return kernel.Get<T>();
        }
    }
}
