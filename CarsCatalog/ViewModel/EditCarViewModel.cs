using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using CarsCatalog.Model.DataProviders;
using System.Windows.Input;
using CarsCatalog.Infrastructure;
using System.Windows;
using CarsCatalog.ViewModel.StyleAndLanguage;
using CarCatalogDAL.Implementations;
using CarCatalogDAL;

namespace CarsCatalog.ViewModel
{
    public class EditCarViewModel : INotifyPropertyChanged
    {
        #region PropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        private void Notify([CallerMemberName]string property="")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }
        #endregion

        #region Properties
        private UnitOfWork uof;
        private CarDataSingleton cds;

        private Car car;
        public Car Car
        {
            get => car;
            set
            {
                car = value;
                Notify();
            }
        }

        public StyleLangCollection StyleLanguage { get; }

        public List<BodyType> BodyTypes { get; }
        public List<Manufacturer> Manufacturers { get; }
        public List<GearBoxType> GearBoxTypes { get; }
        public List<WheelDriveType> WheelDriveTypes { get; }
        #endregion

        #region Commands
        public ICommand AcceptCommand { get; }
        public ICommand CencelCommand { get; }
        public ICommand AddImageCommand { get; }
        #endregion

        #region Methods
        public EditCarViewModel()
        {
            StyleLanguage = StyleLangCollection.GetStyleLangCollection();
            uof = UnitOfWork.GetUnitOfWork();
            BodyTypes = uof.BodyTypes.GetAll().ToList();
            Manufacturers = uof.Manufacturers.GetAll().ToList();
            GearBoxTypes = uof.GearBoxTypes.GetAll().ToList();
            WheelDriveTypes = uof.WheelDriveTypes.GetAll().ToList();

            cds = CarDataSingleton.GetCarDataSingleton();
            Car = new Car();

            if (cds.Car == null)
            {
                cds.Car = new Car();
            }
            else
            {
                Car.Image = cds.Car.Image;
                Car.Model = cds.Car.Model;
                Car.Power = cds.Car.Power;
                Car.Price = cds.Car.Price;
                Car.WheelDriveType = cds.Car.WheelDriveType;
                Car.BodyType = cds.Car.BodyType;
                Car.GearBoxType = cds.Car.GearBoxType;
                car.WheelDriveType = cds.Car.WheelDriveType;
            }           

            AddImageCommand = new RelayCommand(AddImageMethod);
            AcceptCommand = new RelayCommand(AcceptMethod);
            CencelCommand = new RelayCommand(CencelMethod);
        }

        private void AddImageMethod(object o)
        {
            Car.Image = ImageCopyManager.CopyImageToFolder();
        }
        
        private void AcceptMethod(object o)
        {
            Window w = o as Window;
            cds.Car.BodyType = Car.BodyType;
            cds.Car.Manufacturer = Car.Manufacturer;
            cds.Car.GearBoxType = Car.GearBoxType;
            cds.Car.WheelDriveType = Car.WheelDriveType;
            cds.Car.Image = Car.Image;
            cds.Car.Model = Car.Model;
            cds.Car.Price = Car.Price;
            cds.IsResultTrue = true;
            w.Close();
        }

        private void CencelMethod(object o)
        {
            Window w = o as Window;
            cds.IsResultTrue = false;
            w.Close();
        }
        #endregion
    }
}
