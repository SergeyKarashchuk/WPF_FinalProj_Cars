using CarCatalogDAL.Abstractions;
using CarCatalogDAL.Models;
using CarsCatalog.ViewModel.StyleAndLanguage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarsCatalog.ViewModel
{
    public class BaseViewModel : BaseNotifyEntity
    {
        public StyleLangCollection StyleLanguage { get; }
        protected IUnitOfWork uof;
        public BaseViewModel()
        {
            uof = DependencyResolver.Resolve<IUnitOfWork>();
            StyleLanguage = StyleLangCollection.GetStyleLangCollection();
        }

    }
}
