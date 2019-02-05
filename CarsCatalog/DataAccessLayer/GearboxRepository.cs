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
    class GearboxRepository : IRepository<Gearbox>
    {
        CarsCatalogContext db;
        public GearboxRepository(CarsCatalogContext db)
        {
            this.db = db;
        }
        public void Add(Gearbox obj)
        {
            db.Gearboxes.Add(obj);
        }
        public void Update(Gearbox obj)
        {
            Gearbox tmp = db.Gearboxes.FirstOrDefault(x => x.Id == obj.Id);
            if (tmp == null)
                return;
            db.Entry(tmp).State = EntityState.Modified;
        }

        public Gearbox Get(int id)
        {
            return db.Gearboxes.Find(id);
        }

        public BindingList<Gearbox> GetAll()
        {
            db.Gearboxes.Load();
            return db.Gearboxes.Local.ToBindingList();
        }

        public void Remove(Gearbox obj)
        {
            Gearbox tmp = db.Gearboxes.FirstOrDefault(x => x.Id == obj.Id);
            if (tmp == null)
                return;
            db.Entry(tmp).State = EntityState.Deleted;
        }       
    }
}
