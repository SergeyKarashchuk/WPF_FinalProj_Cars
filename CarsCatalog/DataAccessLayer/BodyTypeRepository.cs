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
    public class BodyTypeRepository : IRepository<BodyType>
    {
        CarsCatalogContext db;
        public BodyTypeRepository(CarsCatalogContext db)
        {
            this.db = db;
        }

        public void Add(BodyType obj)
        {
            db.BodyTypes.Add(obj);
        }

        public void Update(BodyType obj)
        {
            BodyType tmp = db.BodyTypes.FirstOrDefault(x => x.Id == obj.Id);
            if (tmp == null)
                return;
            db.Entry(tmp).State = EntityState.Modified;
        }

        public BodyType Get(int id)
        {
            return db.BodyTypes.Find(id);
        }

        public BindingList<BodyType> GetAll()
        {
            db.BodyTypes.Load();
            return db.BodyTypes.Local.ToBindingList();
        }

        public void Remove(BodyType obj)
        {
            BodyType tmp = db.BodyTypes.FirstOrDefault(x => x.Id == obj.Id);
            if (tmp == null)
                return;
            db.Entry(tmp).State = EntityState.Deleted;
        }       
    }
}
