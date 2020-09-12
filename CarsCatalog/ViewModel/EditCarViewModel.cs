using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using CarsCatalog.Infrastructure;
using System.Windows;
using CarsCatalog.ViewModel.StyleAndLanguage;
using CarCatalogDAL.Implementations;
using CarCatalogDAL;
using CarCatalogDAL.Models;
using System.Collections.ObjectModel;
using System;

namespace CarsCatalog.ViewModel
{
    public class EditCarViewModel : BaseViewModel
    {
        #region Properties

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
            BodyTypes = uof.BodyTypes.GetAll().ToList();
            Manufacturers = uof.Manufacturers.GetAll().ToList();
            GearBoxTypes = uof.GearBoxTypes.GetAll().ToList();
            WheelDriveTypes = uof.WheelDriveTypes.GetAll().ToList();
            AddImageCommand = new RelayCommand(AddImageMethod);
            AcceptCommand = new RelayCommand(AcceptMethod);
            CencelCommand = new RelayCommand(CencelMethod);
            Car = new Car();
        }

        private void AddImageMethod(object o)
        {
            Car.Image = ImageCopyManager.CopyImageToFolder();
        }
        
        private void AcceptMethod(object o)
        {
            if (Car.ID == 0)
                uof.Cars.Add(Car);
            else
                uof.Cars.Update(Car);
            uof.SaveChanges();
            navigation.ClosePage();
        }

        private void CencelMethod(object o)
        {
            navigation.ClosePage();
        }

        public override void Remap(object remapParam = null)
        { 
            if (remapParam is int?)
            {
                var entity = uof.Cars.Get((remapParam as int?).Value);
                Car = entity;
            }
        }
        #endregion
    }
}
