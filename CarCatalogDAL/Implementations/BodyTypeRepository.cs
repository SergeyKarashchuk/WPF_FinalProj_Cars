using CarCatalogDAL.Abstractions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarCatalogDAL.Implementations
{
    public class BodyTypeRepository : RepositoryBase, IRepository<BodyType>
    {
        public BodyTypeRepository(CarCatalogEntities db) : base(db) 
        {

        }

        public void Add(BodyType obj)
        {
            var entity = new BodyTypeDB
            {
                Name = obj.Name,
                Image = obj.Image
            };
            db.BodyType.Add(entity);
        }

        public void Update(BodyType obj)
        {
            var tmp = db.BodyType.FirstOrDefault(x => x.ID == obj.ID);
            if (tmp == null)
                return;
            tmp.Name = obj.Name;
            tmp.Image = obj.Image;
            db.Entry(obj).State = EntityState.Modified;
        }

        public BodyType Get(int id)
        {
            var entity = db.BodyType.Find(id);
            return new BodyType
            {
                ID = entity.ID,
                Name = entity.Name,
                Image = entity.Image,
            };
        }

        public BindingList<BodyType> GetAll()
        {
            db.BodyType.Load();
            return new ObservableCollection<BodyType>(db.BodyType.Select(x => new BodyType
            {
                ID = x.ID,
                Name = x.Name,
                Image = x.Image,
            })).ToBindingList();
        }

        public void Remove(BodyType obj)
        {
            var tmp = db.BodyType.FirstOrDefault(x => x.ID == obj.ID);
            if (tmp == null)
                return;
            db.Entry(tmp).State = EntityState.Deleted;
        }       
    }
}
