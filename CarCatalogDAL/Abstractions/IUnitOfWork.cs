using CarCatalogDAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarCatalogDAL.Abstractions
{
    public interface IUnitOfWork
    {
        IRepository<Car> Cars { get; }
        IRepository<Manufacturer> Manufacturers { get; }
        IRepository<BodyType> BodyTypes { get; }
        IRepository<GearBoxType> GearBoxTypes { get; }
        IRepository<WheelDriveType> WheelDriveTypes { get; }
        void SaveChanges();
    }
}
