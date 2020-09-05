using CarCatalogDAL.Abstractions;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data.Entity;
using System.Linq;

namespace CarCatalogDAL.Implementations
{
    public class GearBoxTypeRepository : RepositoryBase, IRepository<GearBoxType>
    {
        public GearBoxTypeRepository(CarCatalogEntities db) : base(db)
        {

        }

        public void Add(GearBoxType obj)
        {
            var entity = new GearBoxTypeDB
            {
                Name = obj.Name,
                Image = obj.Image
            };
            db.GearBoxType.Add(entity);
        }
        public void Update(GearBoxType obj)
        {
            var tmp = db.GearBoxType.FirstOrDefault(x => x.ID == obj.ID);
            if (tmp == null)
                return;
            tmp.Name = obj.Name;
            tmp.Image = obj.Image;
            db.Entry(tmp).State = EntityState.Modified;
        }

        public GearBoxType Get(int id)
        {
            var entity = db.GearBoxType.Find(id);
            return new GearBoxType
            {
                ID = entity.ID,
                Name = entity.Name,
                Image = entity.Image,
            };
        }

        public BindingList<GearBoxType> GetAll()
        {
            db.GearBoxType.Load();
            return new ObservableCollection<GearBoxType>(db.GearBoxType.Select(x => new GearBoxType
            {
                ID = x.ID,
                Name = x.Name,
                Image = x.Image,
            })).ToBindingList();
        }

        public void Remove(GearBoxType obj)
        {
            var tmp = db.GearBoxType.FirstOrDefault(x => x.ID == obj.ID);
            if (tmp == null)
                return;
            db.Entry(tmp).State = EntityState.Deleted;
        }       
    }
}
