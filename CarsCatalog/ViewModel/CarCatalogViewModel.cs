﻿using CarCatalogDAL;
using CarsCatalog.Infrastructure;
using CarsCatalog.View;
using CarsCatalog.ViewModel.Filter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CarsCatalog.ViewModel
{
    public class CarCatalogViewModel : BaseViewModel
    {
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

        private List<Car> carlist;
        public IconProperty FilterProperty { get; set; }
        public IconProperty SortProperty { get; set; }

        public FilterSortItems ElementsWithFilters { get; set; }
        public Car SelectedCar { get; set; }

        #endregion

        #region Methods
        public CarCatalogViewModel()
        {
            RemoveCar = new RelayCommand(RemoveCarMethod, x => SelectedCar != null);
            ExitCommand = new RelayCommand(ExitMethod);
            AddCar = new RelayCommand(o => AddOrUpdateCar());
            EditCar = new RelayCommand(o => AddOrUpdateCar(), x => SelectedCar != null);
            EditSpecificationsCommand = new RelayCommand(EditSpecificationsMethod);

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
            List<Car> car_list = new List<Car>(uof.Cars.GetAll());

            return car_list;
        }

        private void RemoveCarMethod(object o)
        {
            var dal_car = uof.Cars.Get(SelectedCar.ID);
            uof.Cars.Remove(dal_car);
            uof.SaveChanges();
            RemapCollections();
        }

        private void AddOrUpdateCar()
        {
            var ecw = new EditCarUC(SelectedCar?.ID);
            navigation.OpenNewWindow(ecw);
        }

        private void ExitMethod(object o)
        {
            //(o as Window)?.Close();
        }

        private void SaveMethod(object obj)
        {
            uof.SaveChanges();
        }

        private void EditSpecificationsMethod(object o)
        {
            navigation.OpenNewWindow(new EditSpecificationUC());
        }

        public override void Remap()
        {
            RemapCollections();
        }
        #endregion
    }
}
