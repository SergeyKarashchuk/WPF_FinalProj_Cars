using CarsCatalog.DataAccessLayer.DAL_Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarsCatalog.DataAccessLayer
{
    public class CarsCatalogContext : DbContext
    {
        public CarsCatalogContext() : base("CarsCatalogConnection")
        {

        }

        public DbSet<Car> Cars { get; set; }
        public DbSet<Brand> Brands { get; set; }
        public DbSet<BodyType> BodyTypes { get; set; }
        public DbSet<Gearbox> Gearboxes { get; set; }
        public DbSet<WheelDrive> WheelDrives { get; set; }
    }
}
