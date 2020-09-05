using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using CarsCatalog.Infrastructure;
using CarCatalogDAL;

namespace CarsCatalog.ViewModel.Filter
{
    public class FilterSortItems : INotifyPropertyChanged
    {
        public ICommand Sort { get; }

        public event PropertyChangedEventHandler PropertyChanged;
        private void Notify([CallerMemberName]string property = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }

        private IEnumerable<Car> carsList;
        public FilterCollections FilterCollections { get; set; }

        private ObservableCollection<Car> observableCollection;       
        public ObservableCollection<Car> CarsObservableCollection
        {
            get => observableCollection;
            set
            {
                observableCollection = value;
                Notify();
            }
        }      

        public FilterSortItems(IEnumerable<Car> carsList)
        {
            this.carsList = carsList;
            FilterCollections = new FilterCollections(FilterCollections_PropertyChanged);
            Sort = new RelayCommand(SortMethod, x => observableCollection.Count > 0);
        }

        #region RemapFiltersState
        private void RemapFilterInAllCollections()
        {
            RemapChecksInBrands();
            RemapChecksInBodyTypes();
            RemapChecksInGearboxes();
            RemapChecksInWheelDrives();
        }

        private void RemapChecksInBrands()
        {
            foreach (var item in FilterCollections.ManufacturerChecks)
            {
                if (carsList.FirstOrDefault(x => x.Manufacturer?.Name == item.Name) == null)
                {
                    item.IsEnabled = false;
                    item.IsChecked = false;
                }
            }
        }

        private void RemapChecksInBodyTypes()
        {
            foreach (var item in FilterCollections.BodyTypesChecks)
            {
                if (carsList.FirstOrDefault(x => x.BodyType?.Name == item.Name) == null)
                {
                    item.IsEnabled = false;
                    item.IsChecked = false;
                }
            }
        }

        private void RemapChecksInGearboxes()
        {
            foreach (var item in FilterCollections.GearBoxTypesChecks)
            {
                if (carsList.FirstOrDefault(x => x.GearBoxType?.Name == item.Name) == null)
                {
                    item.IsEnabled = false;
                    item.IsChecked = false;
                }
            }
        }

        private void RemapChecksInWheelDrives()
        {
            foreach (var item in FilterCollections.WheelDriveChecks)
            {
                if (carsList.FirstOrDefault(x => x.WheelDriveType?.Name == item.Name) == null)
                {
                    item.IsEnabled = false;
                    item.IsChecked = false;
                }
            }
        }
        #endregion

        #region FiltersMethods
        public void ClearFilters(IEnumerable<Car> carsList)
        {
            this.carsList = carsList;
            FilterCollections.RemapCheckBoxLists();
            RemapFilterInAllCollections();
            CarsObservableCollection = new ObservableCollection<Car>(carsList);

        }

        public void FilterCollections_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (!(e.PropertyName == "IsEnabled" || e.PropertyName == "Name"))
            {
                CheckBoxItem check_item = sender as CheckBoxItem;
                RemapElementsInCollection(check_item.CheckBoxItemType);
            }
        }

        public void RemapElementsInCollection(CheckBoxItemType typeOfFiltrate)
        {
            RemapFilterInAllCollections();           


            List<Car> NewCollection = new List<Car>(carsList);

            switch (typeOfFiltrate)
            {
                case CheckBoxItemType.Brand:
                    FiltrateByBrand(NewCollection);
                    FiltrateByBodyType(NewCollection);
                    FiltrateByGearBox(NewCollection);
                    FiltrateByWheelDrive(NewCollection);
                    break;
                case CheckBoxItemType.BodyType:
                    FiltrateByBodyType(NewCollection);
                    FiltrateByBrand(NewCollection);
                    FiltrateByGearBox(NewCollection);
                    FiltrateByWheelDrive(NewCollection);
                    break;
                case CheckBoxItemType.Gearbox:
                    FiltrateByGearBox(NewCollection);
                    FiltrateByBrand(NewCollection);
                    FiltrateByBodyType(NewCollection);
                    FiltrateByWheelDrive(NewCollection);

                    break;
                case CheckBoxItemType.WheelDrive:
                    FiltrateByWheelDrive(NewCollection);
                    FiltrateByBrand(NewCollection);
                    FiltrateByBodyType(NewCollection);
                    FiltrateByGearBox(NewCollection);
                    break;
                default:
                    break;
            }
            CarsObservableCollection = new ObservableCollection<Car>(NewCollection);
        }

        private void FiltrateByBrand(List<Car> NewCollection)
        {            
            IEnumerable<CheckBoxItem> cheks = FilterCollections.ManufacturerChecks.Where(x => x.IsEnabled == true && x.IsChecked == true);

            if (cheks.ToList().Count > 0)
            {
                IEnumerable<CheckBoxItem> uncheks = FilterCollections.ManufacturerChecks.Where(x => x.IsEnabled == true && x.IsChecked == false);
                foreach (var item in uncheks)
                {
                    NewCollection.RemoveAll(x => x.Manufacturer.Name == item.Name);
                }
            }          
        }

        private void FiltrateByBodyType(List<Car> NewCollection)
        {
            IEnumerable<CheckBoxItem> cheks = FilterCollections.BodyTypesChecks.Where(x => x.IsEnabled == true && x.IsChecked == true);
            if (cheks.ToList().Count > 0)
            {
                IEnumerable<CheckBoxItem> uncheks = FilterCollections.BodyTypesChecks.Where(x => x.IsEnabled == true && x.IsChecked == false);
                foreach (var item in uncheks)
                {
                    NewCollection.RemoveAll(x => x.BodyType.Name == item.Name);
                }
            }
        }

        private void FiltrateByGearBox(List<Car> NewCollection)
        {
            IEnumerable<CheckBoxItem> cheks = FilterCollections.GearBoxTypesChecks.Where(x => x.IsEnabled == true && x.IsChecked == true);
            if (cheks.ToList().Count > 0)
            {
                IEnumerable<CheckBoxItem> uncheks = FilterCollections.GearBoxTypesChecks.Where(x => x.IsEnabled == true && x.IsChecked == false);
                foreach (var item in uncheks)
                {
                    NewCollection.RemoveAll(x => x.GearBoxType.Name == item.Name);
                }
            }
        }

        private void FiltrateByWheelDrive(List<Car> NewCollection)
        {
            IEnumerable<CheckBoxItem> cheks = FilterCollections.WheelDriveChecks.Where(x => x.IsEnabled == true && x.IsChecked == true);
            if (cheks.ToList().Count > 0)
            {
                IEnumerable<CheckBoxItem> uncheks = FilterCollections.WheelDriveChecks.Where(x => x.IsEnabled == true && x.IsChecked == false);
                foreach (var item in uncheks)
                {
                    NewCollection.RemoveAll(x => x.WheelDriveType.Name == item.Name);
                }
            }
        }      
        #endregion

        #region SortMethods
        private void SortMethod(object o)
        {
            switch (o.ToString())
            {
                case "Manufacturer":
                    CarsObservableCollection = new ObservableCollection<Car>(CarsObservableCollection.OrderBy(x => x.Manufacturer.Name));
                    break;

                case "GearBoxType":
                    CarsObservableCollection = new ObservableCollection<Car>(CarsObservableCollection.OrderBy(x => x.GearBoxType.Name));
                    break;

                case "WheelDriveType":
                    CarsObservableCollection = new ObservableCollection<Car>(CarsObservableCollection.OrderBy(x => x.WheelDriveType.Name));
                    break;

                case "PriceIncreases":
                    CarsObservableCollection = new ObservableCollection<Car>(CarsObservableCollection.OrderBy(x => x.Price));
                    break;

                case "PriceDecreases":
                    CarsObservableCollection = new ObservableCollection<Car>(CarsObservableCollection.OrderByDescending(x => x.Price));
                    break;

                default:
                    break;
            }
        }
       
        #endregion
    }
}
