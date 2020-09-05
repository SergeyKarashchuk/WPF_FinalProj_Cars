using CarCatalogDAL.Abstractions;
using CarCatalogDAL.Models;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data.Entity;
using System.Linq;

namespace CarCatalogDAL.Implementations
{
    public class CarRepository : RepositoryBase, IRepository<Car>
    {
        public CarRepository(CarCatalogEntities db) : base(db)
        {
            this.db = db;
        }

        public void Add(Car obj)
        {
            var entity = new CarDB
            {
                BodyTypeID = obj.BodyType?.ID,
                ManufacturerID = obj.Manufacturer?.ID,
                WheelDriveTypeID = obj.WheelDriveType?.ID,
                GearBoxTypeID = obj.GearBoxType?.ID,
                Model = obj.Model,
                Image = obj.Image,
                Power = obj.Power,
                Price = obj.Price,
            };
            db.Car.Add(entity);
        }

        public void Update(Car obj)
        {
            var tmp = db.Car.FirstOrDefault(x => x.ID == obj.ID);
            if (tmp == null)
                return;
            tmp.BodyTypeID = obj.BodyType?.ID;
            tmp.ManufacturerID = obj.Manufacturer?.ID;
            tmp.WheelDriveTypeID = obj.WheelDriveType?.ID;
            tmp.GearBoxTypeID = obj.GearBoxType?.ID;
            tmp.Model = obj.Model;
            tmp.Image = obj.Image;
            tmp.Power = obj.Power;
            tmp.Price = obj.Price;
            db.Entry(tmp).State = EntityState.Modified;            
        }

        public Car Get(int id)
        {
            var entity = db.Car.Find(id);
            var car = new Car
            {
                ID = entity.ID,
                Model = entity.Model,
                Image = entity.Image,
                Power = entity.Power,
                Price = entity.Price,
            };
            if (entity.BodyType != null)
                car.BodyType = new BodyType
                {
                    ID = entity.BodyType.ID,
                    Name = entity.BodyType.Name,
                    Image = entity.BodyType.Image,
                };
            if (entity.WheelDriveType != null)
                car.WheelDriveType = new WheelDriveType
                {
                    ID = entity.WheelDriveType.ID,
                    Name = entity.WheelDriveType.Name,
                    Image = entity.WheelDriveType.Image,
                };
            if (entity.Manufacturer != null)
                car.Manufacturer = new Manufacturer
                {
                    ID = entity.Manufacturer.ID,
                    Name = entity.Manufacturer.Name,
                    Image = entity.Manufacturer.Image,
                };
            if (entity.GearBoxType != null)
                car.GearBoxType = new GearBoxType
                {
                    ID = entity.GearBoxType.ID,
                    Name = entity.GearBoxType.Name,
                    Image = entity.GearBoxType.Image,
                };

            return car;
        }

        public BindingList<Car> GetAll()
        {
            db.Car.Load();
            var list = db.Car.Select(x => new Car
            {
                ID = x.ID,
                Model = x.Model,
                Image = x.Image,
                Power = x.Power,
                Price = x.Price,
                BodyType = !x.BodyTypeID.HasValue ? null : new BodyType
                {
                    ID = x.BodyType.ID,
                    Name = x.BodyType.Name,
                    Image = x.BodyType.Image,
                },
                Manufacturer = !x.ManufacturerID.HasValue ? null : new Manufacturer
                {
                    ID = x.Manufacturer.ID,
                    Name = x.Manufacturer.Name,
                    Image = x.Manufacturer.Image,
                },
                WheelDriveType = !x.WheelDriveTypeID.HasValue ? null : new WheelDriveType
                {
                    ID = x.WheelDriveType.ID,
                    Name = x.WheelDriveType.Name,
                    Image = x.WheelDriveType.Image,
                },
                GearBoxType = !x.GearBoxTypeID.HasValue ? null : new GearBoxType
                {
                    ID = x.GearBoxType.ID,
                    Name = x.GearBoxType.Name,
                    Image = x.GearBoxType.Image,
                },
            }).ToList();
            return new ObservableCollection<Car>(list).ToBindingList();
        }

        public void Remove(Car obj)
        {
            var tmp = db.Car.FirstOrDefault(x => x.ID == obj.ID);
            if (tmp == null)
                return;
            db.Entry(tmp).State = EntityState.Deleted;
        }
    }
}
