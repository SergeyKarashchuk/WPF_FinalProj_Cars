using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using CarsCatalog.Infrastructure;
using CarCatalogDAL;
using System;

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
        private List<CheckBoxItemType> CheckBoxItemTypes => Enum.GetValues(typeof(CheckBoxItemType)).Cast<CheckBoxItemType>().ToList();
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
            FilterCollections = new FilterCollections();
            FilterCollections.CheckBoxItemChangedEvent += FilterCollections_CheckBoxItemChangedEvent;
            Sort = new RelayCommand(SortMethod, x => observableCollection.Count > 0);
        }

        
        #region RemapFiltersState
        private void RemapChecks(IEnumerable<Car> carsList, CheckBoxItemType? ignoreType = null)
        {
            FilterCollections.CheckBoxItemChangedEvent -= FilterCollections_CheckBoxItemChangedEvent;
            var types = CheckBoxItemTypes;
            if (ignoreType.HasValue)
                types.Remove(ignoreType.Value);
            foreach (var checkBoxItemType in types)
            {
                foreach (var specification in FilterCollections.AllSpecifications.Where(s => s.CheckBoxItemType == checkBoxItemType))
                {
                    if (!carsList.Any(c => c.Specifications.Any(sp => sp?.SpecificationType == specification.CheckBoxItemType.ToString() && sp.ID == specification.ID)))
                    {
                        specification.IsEnabled = false;
                        specification.IsChecked = false;
                    }
                    else if (!specification.IsEnabled)
                    {
                        specification.IsEnabled = true;
                    }
                }
            }
            FilterCollections.CheckBoxItemChangedEvent += FilterCollections_CheckBoxItemChangedEvent;
        }
        #endregion

        #region FiltersMethods
        public void ClearFilters(IEnumerable<Car> carsList)
        {
            this.carsList = carsList;
            FilterCollections.RemapCheckBoxLists();
            RemapChecks(carsList);
            CarsObservableCollection = new ObservableCollection<Car>(carsList);
        }

        private void FilterCollections_CheckBoxItemChangedEvent(object sender, CheckBoxItemChangedEventArg arg)
        {           
            List<Car> newCollection = new List<Car>(carsList);
            newCollection = RemapElementsInCollection2(newCollection, arg.SpecificationType);
            foreach (var item in CheckBoxItemTypes.Where(x => x != arg.SpecificationType))
            {
                newCollection = RemapElementsInCollection2(newCollection, item);
            }
            CarsObservableCollection = new ObservableCollection<Car>(newCollection);
            RemapChecks(newCollection, arg.SpecificationType);
        }

        public List<Car> RemapElementsInCollection2(List<Car> carCollection, CheckBoxItemType typeOfFiltrate)
        {
            var cheks = FilterCollections.AllSpecifications.Where(x => x.CheckBoxItemType == typeOfFiltrate && x.IsEnabled == true && x.IsChecked == true);
            if (cheks.Count() > 0)
            {
                var uncheks = FilterCollections.AllSpecifications.Where(x => x.CheckBoxItemType == typeOfFiltrate && x.IsEnabled == true && x.IsChecked == false);
                foreach (var item in uncheks)
                {
                    carCollection.RemoveAll(car =>
                    {
                        var carSpecification = car.Specifications.SingleOrDefault(x => x?.SpecificationType == typeOfFiltrate.ToString());
                        return carSpecification == null || carSpecification.ID == item.ID;
                    });
                }
            }
            return carCollection;
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
