using CarCatalogDAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarsCatalog.Model.DataProviders
{
    public class CarDataSingleton
    {
        private static CarDataSingleton cds;
        public static CarDataSingleton GetCarDataSingleton()
        {
            if (cds == null)
            {
                cds = new CarDataSingleton();              
            }
            return cds;
        }
        private CarDataSingleton()
        {
            Car = null;
            IsResultTrue = false;
        }
        
        public Car Car { get; set; }
        public bool IsResultTrue { get; set; }
    }
}
