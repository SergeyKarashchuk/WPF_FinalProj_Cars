using CarsCatalog.DataAccessLayer;
using CarsCatalog.Infrastructure;
using CarsCatalog.Model;
using CarsCatalog.Model.DataProviders;
using CarsCatalog.View;
using dal_models = CarsCatalog.DataAccessLayer.DAL_Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows;
using CarsCatalog.ViewModel.Filter;
using Themes;
using CarsCatalog.ViewModel.StyleAndLanguage;

namespace CarsCatalog.ViewModel
{
    public class MainViewModel : INotifyPropertyChanged
    {
        #region PropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        private void Notify([CallerMemberName]string property="")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }
        #endregion

        #region Commands
        public ICommand AddCar { get; }
        public ICommand EditCar { get; }
        public ICommand RemoveCar { get; }
        public ICommand EditSpecificationsCommand { get; }
        public ICommand Filter { get; }
        public ICommand Sort { get; }
        public ICommand ExitCommand { get; }
        #endregion

        #region Properties
        public StyleLangCollection StyleLanguage { get; }
        private UnitOfWork uof;
        private List<Car> carlist;
        public IconProperty FilterProperty { get; set; }
        public IconProperty SortProperty { get; set; }

        public FilterSortItems ElementsWithFilters { get; set; }        
        #endregion

        #region Methods
        public MainViewModel()
        {
            StyleLanguage = StyleLangCollection.GetStyleLangCollection();

            RemoveCar = new RelayCommand(RemoveCarMethod, x => SelectedCar != null);
            ExitCommand = new RelayCommand(ExitMethod);
            AddCar = new RelayCommand(AddNewCar);
            EditCar = new RelayCommand(EditSelectedCar, x => SelectedCar != null);
            EditSpecificationsCommand = new RelayCommand(EditSpecificationsMethod);          

            uof = UnitOfWork.GetUnitOfWork();

            FilterProperty = new IconProperty();
            SortProperty = new IconProperty();

            carlist = GetCarList();
            ElementsWithFilters = new FilterSortItems(carlist);
            ElementsWithFilters.ClearFilters(carlist);
        }

        private void RemapCollections()
        {
            carlist = GetCarList();
            ElementsWithFilters.ClearFilters(carlist);
        }

        private List<Car> GetCarList()
        {
            List<Car> car_list = new List<Car>(uof
                .Cars
                    .GetAll()
                        .Select(x => new Car(
                            x.Id,
                            x.Model,
                            x.Image,
                            x.Power,
                            x.Price,
                            uof.BodyTypes.GetAll().FirstOrDefault(y => y.Id == x.BodyTypeId),
                            uof.Brands.GetAll().FirstOrDefault(y => y.Id == x.BrandId),
                            uof.Gearboxes.GetAll().FirstOrDefault(y => y.Id == x.GearboxId),
                            uof.WheelDrives.GetAll().FirstOrDefault(y => y.Id == x.WheelDriveId),
                            Car_PropertyChanged)));
            return car_list;
        }      

        private void RemoveCarMethod(object o)
        {
            dal_models.Car dal_car = uof.Cars.Get(SelectedCar.Id);
            uof.Cars.Remove(dal_car);
            uof.SaveChanges();
            RemapCollections();
        }

        private void AddNewCar(object o)
        {
            CarDataSingleton cds = CarDataSingleton.GetCarDataSingleton();
            cds.Car = null;
            EditCarWindow ecw = new EditCarWindow();            
            ecw.ShowDialog();
            if (cds.IsResultTrue)
            {               
                dal_models.Car dal_car = CopyModelCarToDalCar(cds.Car);
                uof.Cars.Add(dal_car);
                uof.SaveChanges();
                RemapCollections();
            }
        }

        private void EditSelectedCar(object o)
        {
            CarDataSingleton cds = CarDataSingleton.GetCarDataSingleton();
            cds.Car = SelectedCar;

            EditCarWindow ecw = new EditCarWindow();
            ecw.ShowDialog();
            if (cds.IsResultTrue)
            {
                dal_models.Car dal_car = CopyModelCarToDalCar(cds.Car, uof.Cars.Get(cds.Car.Id));
                uof.Cars.Update(dal_car);
                uof.SaveChanges();
                RemapCollections();
            }
        }

        private void ExitMethod(object o)
        {
            try
            {
                Window w = o as Window;
                w.Close();
            }
            catch
            {

            }
        }

        private dal_models.Car CopyModelCarToDalCar(Car obj, dal_models.Car dal_car = null)
        {
            if (dal_car == null)
            {
                dal_car = new dal_models.Car();
            }

            dal_car.BrandId = obj.Brand.Id;
            dal_car.BodyTypeId = obj.BodyType.Id;
            dal_car.GearboxId = obj.Gearbox.Id;
            dal_car.WheelDriveId = obj.WheelDrive.Id;
            dal_car.Image = obj.Image;
            dal_car.Model = obj.Model;
            dal_car.Price = obj.Price;
            dal_car.Power = obj.Power;

            return dal_car;
        }

        private void SaveMethod(object obj)
        {
            uof.SaveChanges();
        }

        private void EditSpecificationsMethod(object o)
        {            
            EditSpecificationWindow editSpecificationWindow = new EditSpecificationWindow();
            editSpecificationWindow.ShowDialog();
            RemapCollections();
        }      

        public void Car_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            Car car = sender as Car;
            DataAccessLayer.DAL_Models.Car dal_car = uof.Cars.Get(car.Id);

            CopyModelCarToDalCar(car, dal_car);          

            uof.Cars.Update(dal_car);           
        }

        public Car SelectedCar { get; set; }
        #endregion
    }
}
