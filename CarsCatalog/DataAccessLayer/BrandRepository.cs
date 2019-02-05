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
    public class BrandRepository : IRepository<Brand>
    {
        CarsCatalogContext db;
        public BrandRepository(CarsCatalogContext db)
        {
            this.db = db;
        }

        public void Add(Brand obj)
        {
            db.Brands.Add(obj);
        }

        public void Update(Brand obj)
        {
            Brand tmp = db.Brands.FirstOrDefault(x => x.Id == obj.Id);
            if (tmp == null)
                return;
            db.Entry(tmp).State = EntityState.Modified;
        }

        public Brand Get(int id)
        {
            return db.Brands.Find(id);
        }

        public BindingList<Brand> GetAll()
        {
            db.Brands.Load();
            return db.Brands.Local.ToBindingList();
        }

        public void Remove(Brand obj)
        {
            Brand tmp = db.Brands.FirstOrDefault(x => x.Id == obj.Id);
            if (tmp == null)
                return;
            db.Entry(tmp).State = EntityState.Deleted;
        }
    }
}
