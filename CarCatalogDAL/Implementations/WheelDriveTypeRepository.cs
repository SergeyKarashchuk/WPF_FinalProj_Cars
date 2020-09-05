using CarCatalogDAL.Abstractions;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data.Entity;
using System.Linq;

namespace CarCatalogDAL.Implementations
{
    public class WheelDriveTypeRepository : RepositoryBase, IRepository<WheelDriveType>
    {
        public WheelDriveTypeRepository(CarCatalogEntities db) : base(db) 
        {

        }

        public void Add(WheelDriveType obj)
        {
            var entity = new WheelDriveTypeDB
            {
                Name = obj.Name,
                Image = obj.Image
            };
            db.WheelDriveType.Add(entity);
        }

        public void Update(WheelDriveType obj)
        {
            var tmp = db.WheelDriveType.FirstOrDefault(x => x.ID == obj.ID);
            if (tmp == null)
                return;
            tmp.Name = obj.Name;
            tmp.Image = obj.Image;
            db.Entry(tmp).State = EntityState.Modified;
        }

        public WheelDriveType Get(int id)
        {
            var entity = db.WheelDriveType.Find(id);
            return new WheelDriveType
            {
                ID = entity.ID,
                Name = entity.Name,
                Image = entity.Image,
            };
        }

        public BindingList<WheelDriveType> GetAll()
        {
            db.WheelDriveType.Load();
            return new ObservableCollection<WheelDriveType>(db.WheelDriveType.Select(x => new WheelDriveType
            {
                ID = x.ID,
                Name = x.Name,
                Image = x.Image,
            })).ToBindingList();
        }

        public void Remove(WheelDriveType obj)
        {
            var tmp = db.WheelDriveType.FirstOrDefault(x => x.ID == obj.ID);
            if (tmp == null)
                return;
            db.Entry(tmp).State = EntityState.Deleted;
        }       
    }
}
