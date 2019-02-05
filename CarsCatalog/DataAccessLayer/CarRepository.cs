using CarsCatalog.DataAccessLayer.DAL_Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarsCatalog.DataAccessLayer
{
    public class CarRepository : IRepository<Car>
    {
        CarsCatalogContext db;
        public CarRepository(CarsCatalogContext db)
        {
            this.db = db;
        }

        public void Add(Car obj)
        {
            db.Cars.Add(obj);
        }

        public void Update(Car obj)
        {
            Car tmp = db.Cars.FirstOrDefault(x => x.Id == obj.Id);
            if (tmp == null)
                return;
            db.Entry(obj).State = EntityState.Modified;            
        }

        public Car Get(int id)
        {
            return db.Cars.Find(id);
        }

        public BindingList<Car> GetAll()
        {
            db.Cars.Load();
            return db.Cars.Local.ToBindingList();
        }

        public void Remove(Car obj)
        {
            Car tmp = db.Cars.FirstOrDefault(x => x.Id == obj.Id);
            if (tmp == null)
                return;
            db.Entry(tmp).State = EntityState.Deleted;
        }
    }
}
