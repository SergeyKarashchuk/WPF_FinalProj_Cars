using CarCatalogDAL.Abstractions;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data.Entity;
using System.Linq;

namespace CarCatalogDAL.Implementations
{
    public class ManufacturerRepository : RepositoryBase, IRepository<Manufacturer>
    {
        public ManufacturerRepository(CarCatalogEntities db) : base(db) 
        {

        }

        public void Add(Manufacturer obj)
        {
            var entity = new ManufacturerDB
            {
                Name = obj.Name,
                Image = obj.Image
            };
            db.Manufacturer.Add(entity);
        }

        public void Update(Manufacturer obj)
        {
            var tmp = db.Manufacturer.FirstOrDefault(x => x.ID == obj.ID);
            if (tmp == null)
                return;
            tmp.Name = obj.Name;
            tmp.Image = obj.Image;
            db.Entry(tmp).State = EntityState.Modified;
        }

        public Manufacturer Get(int id)
        {
            var entity = db.Manufacturer.Find(id);
            return new Manufacturer
            {
                ID = entity.ID,
                Name = entity.Name,
                Image = entity.Image,
            };
        }

        public BindingList<Manufacturer> GetAll()
        {
            db.Manufacturer.Load();
            return new ObservableCollection<Manufacturer>(db.Manufacturer.Select(x => new Manufacturer
            {
                ID = x.ID,
                Name = x.Name,
                Image = x.Image,
            })).ToBindingList();
        }

        public void Remove(Manufacturer obj)
        {
            var tmp = db.Manufacturer.FirstOrDefault(x => x.ID == obj.ID);
            if (tmp == null)
                return;
            db.Entry(tmp).State = EntityState.Deleted;
        }
    }
}
