using CarCatalogDAL;
using CarCatalogDAL.Implementations;
using CarCatalogDAL.Models;
using CarsCatalog.Infrastructure;
using CarsCatalog.Model.DataProviders;
using CarsCatalog.ViewModel.StyleAndLanguage;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace CarsCatalog.ViewModel
{
    public class EditSpecificationViewModel : INotifyPropertyChanged
    {
        #region PropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;

        private void Notify([CallerMemberName]string property = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }
        #endregion

        #region Commands
        public ICommand UpdateCommand { get; }
        public ICommand ExitCommand { get; }

        public ICommand SelectCollectionCommand { get; }

        public ICommand EditImage { get; }
        public ICommand AddCommand { get; }
        public ICommand RemoveCommand { get; }
        #endregion

        #region Properties
        private UnitOfWork uof;

        private ObservableCollection<ISpecification> specifications;
        public ObservableCollection<ISpecification> Specifications
        {
            get => specifications;
            set
            {
                specifications = value;
                Notify();
            }
        }

        private ISpecification spec;
        public ISpecification SelectedItem
        {
            get => spec;
            set
            {
                spec = value;
                Notify();
            }
        }

        private string currentCollection;
        public string CurrentCollection
        {
            get => currentCollection;
            set
            {
                currentCollection = value;
                Notify();
            }
        }

        public StyleLangCollection StyleLanguage { get; }
        #endregion

        #region Methods
        public EditSpecificationViewModel()
        {
            StyleLanguage = StyleLangCollection.GetStyleLangCollection();

            uof = UnitOfWork.GetUnitOfWork();

            ExitCommand = new RelayCommand(ExitMethod);
            EditImage = new RelayCommand(AddImageMethod, x => SelectedItem != null);
            SelectCollectionCommand = new RelayCommand(SelectCollectionMethod);
            UpdateCommand = new RelayCommand(UpdateMethod);
            AddCommand = new RelayCommand(CreateNewSpecification);
            RemoveCommand = new RelayCommand(RemoveSpecification, x => SelectedItem != null);
        }       

        private void SelectCollectionMethod(object o)
        {
            CurrentCollection = o as string;
            var list = new List<ISpecification>();
            Specifications = new ObservableCollection<ISpecification>();
            switch (CurrentCollection)
            {
                case "BodyType":
                    list.AddRange(uof.BodyTypes.GetAll());
                    break;

                case "Manufacturer":
                    list.AddRange(uof.Manufacturers.GetAll());
                    break;

                case "GearBoxType":
                    list.AddRange(uof.GearBoxTypes.GetAll());
                    break;

                case "WheelDriveType":
                    list.AddRange(uof.WheelDriveTypes.GetAll());
                    break;

                default:
                    break;
            }

            Specifications = new ObservableCollection<ISpecification>(list);
        }

        private void AddImageMethod(object o)
        {
            string newImage = ImageCopyManager.CopyImageToFolder();
            if (newImage != null)
            {
                SelectedItem.Image = newImage;
            }
        }

        private void UpdateMethod(object o)
        {
            uof.SaveChanges();
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

        private void CreateNewSpecification(object o)
        {
            switch (CurrentCollection)
            {
                case "BodyType":
                    SelectedItem = new BodyType();
                    uof.BodyTypes.Add(SelectedItem as BodyType);
                    break;

                case "Manufacturer":
                    SelectedItem = new Manufacturer();
                    uof.Manufacturers.Add(SelectedItem as Manufacturer);
                    break;

                case "GearBoxType":
                    SelectedItem = new GearBoxType();
                    uof.GearBoxTypes.Add(SelectedItem as GearBoxType);
                    break;

                case "WheelDriveType":
                    SelectedItem = new WheelDriveType();
                    uof.WheelDriveTypes.Add(SelectedItem as WheelDriveType);
                    break;

                default:
                    return;
            }
            SelectCollectionMethod(CurrentCollection);
        }

        private void RemapCollection()
        {
            uof.SaveChanges();
            SelectCollectionMethod(CurrentCollection);
        }

        private void RemoveSpecification(object o)
        {
            switch (CurrentCollection)
            {
                case "BodyType":
                    uof.BodyTypes.Remove(SelectedItem as BodyType);
                    break;

                case "Manufacturer":
                    uof.Manufacturers.Remove(SelectedItem as Manufacturer);
                    break;

                case "GearBoxType":
                    uof.GearBoxTypes.Remove(SelectedItem as GearBoxType);
                    break;

                case "WheelDriveType":
                    uof.WheelDriveTypes.Remove(SelectedItem as WheelDriveType);
                    break;

                default:
                    return;
            }
            Specifications.Remove(SelectedItem);
            RemapCollection();
        }
        #endregion
    }
}
