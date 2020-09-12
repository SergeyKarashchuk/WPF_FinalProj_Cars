using CarCatalogDAL;
using CarsCatalog.Infrastructure;
using CarsCatalog.View;
using CarsCatalog.ViewModel.Filter;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
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
        public ICommand ReloadCatalogCommand { get; }
        public ICommand ExitCommand { get; }
        #endregion

        #region Properties

        private List<Car> carlist;
        public IconProperty FilterProperty { get; set; }
        public IconProperty SortProperty { get; set; }

        public FilterSortItems ElementsWithFilters { get; set; }
        public Car SelectedCar { get; set; }
        public ObservableCollection<Car> CarCatalog { get; set; }

        #endregion

        #region Methods
        public CarCatalogViewModel()
        {
            RemoveCar = new RelayCommand(RemoveCarMethod, x => SelectedCar != null);
            ExitCommand = new RelayCommand(ExitMethod);
            AddCar = new RelayCommand(o => AddOrUpdateCar(true));
            EditCar = new RelayCommand(o => AddOrUpdateCar(false), x => SelectedCar != null);
            EditSpecificationsCommand = new RelayCommand(EditSpecificationsMethod);
            ReloadCatalogCommand = new RelayCommand(o => RemapCollections());
            FilterProperty = new IconProperty();
            SortProperty = new IconProperty();

            carlist = GetCarListAsync().Result;
            ElementsWithFilters = new FilterSortItems(carlist);
            ElementsWithFilters.ClearFilters(carlist);
            CarCatalog = new ObservableCollection<Car>();
        }

        private void RemapCollections()
        {
            CarCatalog.Clear();
            navigation.SetAwaiter(true);
            Task.Factory.StartNew(async () =>
            {
                await Task.Delay(TimeSpan.FromSeconds(1)).ConfigureAwait(false);
                carlist = await GetCarListAsync();
                await Task.Delay(TimeSpan.FromSeconds(1)).ConfigureAwait(false);
                ExecuteOperationInSyncThread(() =>
                {
                    carlist.ForEach(x => CarCatalog.Add(x));
                    navigation.SetAwaiter(false);
                });
            });
        }

        private Task<List<Car>> GetCarListAsync()
        {
            var task = Task.Factory.StartNew(() => new List<Car>(uof.Cars.GetAll()));
            return task;
        }

        private void RemoveCarMethod(object o)
        {
            var dal_car = uof.Cars.Get(SelectedCar.ID);
            uof.Cars.Remove(dal_car);
            uof.SaveChanges();
            RemapCollections();
        }

        private void AddOrUpdateCar(bool isNew)
        {
            var ecw = new EditCarUC();
            navigation.OpenNewWindow(ecw, !isNew ? SelectedCar?.ID : null);
        }

        private void ExitMethod(object o)
        {
            Application.Current.Shutdown();
        }

        private void SaveMethod(object obj)
        {
            uof.SaveChanges();
        }

        private void EditSpecificationsMethod(object o)
        {
            navigation.OpenNewWindow(new EditSpecificationUC());
        }

        public override void Remap(object remapParam = null)
        {
            RemapCollections();
        }
        #endregion
    }
}
