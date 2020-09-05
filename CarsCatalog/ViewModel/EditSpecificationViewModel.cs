using CarCatalogDAL;
using CarCatalogDAL.Implementations;
using CarCatalogDAL.Models;
using CarsCatalog.Infrastructure;
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
    public class EditSpecificationViewModel : BaseViewModel
    {

        #region Commands
        public ICommand UpdateCommand { get; }
        public ICommand ExitCommand { get; }

        public ICommand SelectCollectionCommand { get; }

        public ICommand EditImage { get; }
        public ICommand AddCommand { get; }
        public ICommand RemoveCommand { get; }
        #endregion

        #region Properties
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
        #endregion

        #region Methods
        public EditSpecificationViewModel()
        {
            ExitCommand = new RelayCommand(ExitMethod);
            EditImage = new RelayCommand(AddImageMethod, x => SelectedItem != null);
            SelectCollectionCommand = new RelayCommand(SelectCollectionMethod);
            UpdateCommand = new RelayCommand(UpdateMethod, x => SelectedItem != null && SelectedItem.ID > 0);
            AddCommand = new RelayCommand(CreateNewSpecification, x => SelectedItem != null && SelectedItem.ID == 0);
            RemoveCommand = new RelayCommand(RemoveSpecification, x => SelectedItem != null && SelectedItem.ID > 0);
            Specifications = new ObservableCollection<ISpecification>();
        }

        private void SelectCollectionMethod(object o)
        {
            CurrentCollection = o as string;
            var list = new List<ISpecification>();
            switch (CurrentCollection)
            {
                case "BodyType":
                    SelectedItem = new BodyType();
                    list.AddRange(uof.BodyTypes.GetAll());
                    break;

                case "Manufacturer":
                    SelectedItem = new Manufacturer();
                    list.AddRange(uof.Manufacturers.GetAll());
                    break;

                case "GearBoxType":
                    SelectedItem = new GearBoxType();
                    list.AddRange(uof.GearBoxTypes.GetAll());
                    break;

                case "WheelDriveType":
                    SelectedItem = new WheelDriveType();
                    list.AddRange(uof.WheelDriveTypes.GetAll());
                    break;

                default:
                    break;
            }
            Specifications.Clear();
            list.ForEach(x => Specifications.Add(x));
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
            switch (CurrentCollection)
            {
                case "BodyType":
                    uof.BodyTypes.Update(SelectedItem as BodyType);
                    break;

                case "Manufacturer":
                    uof.Manufacturers.Update(SelectedItem as Manufacturer);
                    break;

                case "GearBoxType":
                    uof.GearBoxTypes.Update(SelectedItem as GearBoxType);
                    break;

                case "WheelDriveType":
                    uof.WheelDriveTypes.Update(SelectedItem as WheelDriveType);
                    break;

                default:
                    return;
            }
            RemapCollection();
        }

        private void ExitMethod(object o)
        {
            navigation.ClosePage();
        }

        private void CreateNewSpecification(object o)
        {
            switch (CurrentCollection)
            {
                case "BodyType":
                    uof.BodyTypes.Add(SelectedItem as BodyType);
                    break;

                case "Manufacturer":
                    uof.Manufacturers.Add(SelectedItem as Manufacturer);
                    break;

                case "GearBoxType":
                    uof.GearBoxTypes.Add(SelectedItem as GearBoxType);
                    break;

                case "WheelDriveType":
                    uof.WheelDriveTypes.Add(SelectedItem as WheelDriveType);
                    break;

                default:
                    return;
            }
            RemapCollection();
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
            RemapCollection();
        }
        #endregion
    }
}
