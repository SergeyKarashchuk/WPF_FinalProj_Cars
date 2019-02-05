using CarsCatalog.DataAccessLayer.DAL_Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace CarsCatalog.DataAccessLayer
{
    public class UnitOfWork : IUnitOfWork
    {
        public IRepository<Car> Cars { get; }     
        public IRepository<Brand> Brands { get; }
        public IRepository<BodyType> BodyTypes { get; }
        public IRepository<Gearbox> Gearboxes { get; }
        public IRepository<WheelDrive> WheelDrives { get; }

        public void SaveChanges()
        {
            context.SaveChanges();
        }

        private CarsCatalogContext context;

        private static UnitOfWork unit;
        public static UnitOfWork GetUnitOfWork()
        {
            if (unit == null)
            {
                unit = new UnitOfWork();
            }
            return unit;
        }

        private UnitOfWork()
        {            
            context = new CarsCatalogContext();
            Cars = new CarRepository(context);
            Brands = new BrandRepository(context);
            BodyTypes = new BodyTypeRepository(context);
            Gearboxes = new GearboxRepository(context);
            WheelDrives = new WheelDriveRepository(context);
        }          
    }
}
