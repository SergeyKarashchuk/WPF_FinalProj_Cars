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
    class WheelDriveRepository : IRepository<WheelDrive>
    {
        CarsCatalogContext db;
        public WheelDriveRepository(CarsCatalogContext db)
        {
            this.db = db;
        }
        public void Add(WheelDrive obj)
        {
            db.WheelDrives.Add(obj);
        }
        public void Update(WheelDrive obj)
        {
            WheelDrive tmp = db.WheelDrives.FirstOrDefault(x => x.Id == obj.Id);
            if (tmp == null) 
            {
                return;
            }
            db.Entry(tmp).State = EntityState.Modified;
        }

        public WheelDrive Get(int id)
        {
            return db.WheelDrives.Find(id);
        }

        public BindingList<WheelDrive> GetAll()
        {
            db.WheelDrives.Load();
            return db.WheelDrives.Local.ToBindingList();
        }

        public void Remove(WheelDrive obj)
        {
            WheelDrive tmp = db.WheelDrives.FirstOrDefault(x => x.Id == obj.Id);
            if (tmp == null)
                return;
            db.Entry(tmp).State = EntityState.Deleted;
        }       
    }
}
