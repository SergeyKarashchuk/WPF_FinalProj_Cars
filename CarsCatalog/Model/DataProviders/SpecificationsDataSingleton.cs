using CarsCatalog.DataAccessLayer;
using CarsCatalog.Model.Specifications;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarsCatalog.Model.DataProviders
{
    public class SpecificationsDataSingleton
    {
        private UnitOfWork uof;
        private static SpecificationsDataSingleton data;
        public static SpecificationsDataSingleton GetSpecificationsDataSingleton()
        {
            if (data == null)
            {
                data = new SpecificationsDataSingleton();
            }
            return data;
        }

        private SpecificationsDataSingleton()
        {
            uof = UnitOfWork.GetUnitOfWork();

            BodyTypes = new ObservableCollection<Specification>(uof
                .BodyTypes
                    .GetAll()
                        .Select(x => new BodyType(
                            x.Id, x.Name, x.Image, BodyType_PropertyChanged)));

            Brands = new ObservableCollection<Specification>(uof
                .Brands
                    .GetAll()
                        .Select(x => new Brand(
                            x.Id, x.Name, x.Image, Brand_PropertyChanged)));

            Gearboxes = new ObservableCollection<Specification>(uof
                .Gearboxes
                    .GetAll()
                        .Select(x => new Gearbox(
                            x.Id, x.Name, x.Image, Geadbox_PropertyChanged)));

            WheelDrives = new ObservableCollection<Specification>(uof
                .WheelDrives
                    .GetAll()
                        .Select(x => new WheelDrive(
                            x.Id, x.Name, x.Image, WheelDrive_PropertyChanged)));


        }

        public void BodyType_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            BodyType bodyType = sender as BodyType;
            DataAccessLayer.DAL_Models.BodyType dal_body = uof.BodyTypes.Get(bodyType.Id);
            dal_body.Name = bodyType.Name;
            dal_body.Image = bodyType.Image;           
            uof.BodyTypes.CreateOrUpdate(dal_body);
            uof.SaveChanges();
        }

        public void Brand_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            Brand brand = sender as Brand;
            DataAccessLayer.DAL_Models.Brand dal_brand = uof.Brands.Get(brand.Id);
            dal_brand.Name = brand.Name;
            dal_brand.Image = brand.Image;
            uof.Brands.CreateOrUpdate(dal_brand);
            uof.SaveChanges();
        }

        public void Geadbox_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            Gearbox gear = sender as Gearbox;
            DataAccessLayer.DAL_Models.Gearbox dal_gear = uof.Gearboxes.Get(gear.Id);
            dal_gear.Name = gear.Name;
            dal_gear.Image = gear.Image;
            uof.Gearboxes.CreateOrUpdate(dal_gear);
            uof.SaveChanges();
        }

        public void WheelDrive_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            WheelDrive wd = sender as WheelDrive;
            DataAccessLayer.DAL_Models.WheelDrive dal_gear = uof.WheelDrives.Get(wd.Id);
            dal_gear.Name = wd.Name;
            dal_gear.Image = wd.Image;
            uof.WheelDrives.CreateOrUpdate(dal_gear);
            uof.SaveChanges();
        }     

        public ObservableCollection<Specification> BodyTypes { get; set; }
        public ObservableCollection<Specification> Brands { get; set; }
        public ObservableCollection<Specification> Gearboxes { get; set; }
        public ObservableCollection<Specification> WheelDrives { get; set; }
    }
}
