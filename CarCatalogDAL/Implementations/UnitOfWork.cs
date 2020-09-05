using CarCatalogDAL.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarCatalogDAL.Implementations
{
    public class UnitOfWork : IUnitOfWork
    {
        public IRepository<Car> Cars { get; }
        public IRepository<Manufacturer> Manufacturers { get; }
        public IRepository<BodyType> BodyTypes { get; }
        public IRepository<GearBoxType> GearBoxTypes { get; }
        public IRepository<WheelDriveType> WheelDriveTypes { get; }

        public void SaveChanges()
        {
            context.SaveChanges();
        }

        private CarCatalogEntities context;
        public UnitOfWork()
        {
            context = new CarCatalogEntities();
            Cars = new CarRepository(context);
            Manufacturers = new ManufacturerRepository(context);
            BodyTypes = new BodyTypeRepository(context);
            GearBoxTypes = new GearBoxTypeRepository(context);
            WheelDriveTypes = new WheelDriveTypeRepository(context);
        }
    }
}