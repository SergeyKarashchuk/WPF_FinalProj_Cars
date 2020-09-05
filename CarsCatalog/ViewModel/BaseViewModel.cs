using CarCatalogDAL.Abstractions;
using CarCatalogDAL.Models;
using CarsCatalog.Infrastructure;
using CarsCatalog.ViewModel.StyleAndLanguage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BaseNotifyEntity = CarsCatalog.Infrastructure.BaseNotifyEntity;

namespace CarsCatalog.ViewModel
{
    public abstract class BaseViewModel : BaseNotifyEntity
    {
        public StyleLangCollection StyleLanguage { get; }
        protected readonly IUnitOfWork uof;
        protected readonly IApplicationNavigation navigation;
        public BaseViewModel()
        {
            uof = DependencyResolver.Resolve<IUnitOfWork>();
            StyleLanguage = StyleLangCollection.GetStyleLangCollection();
            navigation = DependencyResolver.Resolve<IApplicationNavigation>();
        }

        public virtual void Remap()
        {

        }
    }
}
