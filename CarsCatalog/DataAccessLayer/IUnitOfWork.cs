using CarsCatalog.DataAccessLayer.DAL_Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarsCatalog.DataAccessLayer
{
    public interface IUnitOfWork
    {
        IRepository<Car> Cars { get; }
        IRepository<Brand> Brands { get; }
        IRepository<BodyType> BodyTypes { get; }
        IRepository<Gearbox> Gearboxes { get; }
        IRepository<WheelDrive> WheelDrives { get; }
        void SaveChanges();
    }
}
