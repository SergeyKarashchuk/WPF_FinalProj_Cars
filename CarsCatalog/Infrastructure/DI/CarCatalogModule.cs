using CarCatalogDAL.Abstractions;
using CarCatalogDAL.Implementations;
using Ninject.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarsCatalog.Infrastructure.DI
{
    public class CarCatalogModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IUnitOfWork>().To<UnitOfWork>().InSingletonScope();
            Bind<IApplicationNavigation>().To<ApplicationNavigation>().InSingletonScope();
        }
    }
}
