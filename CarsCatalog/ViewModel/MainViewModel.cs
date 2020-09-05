using CarsCatalog.Infrastructure;
using CarsCatalog.Model;
using CarsCatalog.Model.DataProviders;
using CarsCatalog.View;
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
using CarCatalogDAL;
using CarCatalogDAL.Implementations;

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
        public Car SelectedCar { get; set; }

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

        private void AddNewCar(object o)
        {
            CarDataSingleton cds = CarDataSingleton.GetCarDataSingleton();
            cds.Car = null;
            EditCarWindow ecw = new EditCarWindow();            
            ecw.ShowDialog();
            if (cds.IsResultTrue && cds.Car != null)
            {               
                uof.Cars.Add(cds.Car);
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
                uof.Cars.Update(cds.Car);
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
        #endregion
    }
}
