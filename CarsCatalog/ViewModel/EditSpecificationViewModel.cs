using CarsCatalog.DataAccessLayer;
using CarsCatalog.DataAccessLayer.DAL_Models;
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

        private ObservableCollection<Specification> specifications;
        public ObservableCollection<Specification> Specifications
        {
            get => specifications;
            set
            {
                specifications = value;
                Notify();
            }
        }

        private Specification spec;
        public Specification SelectedItem
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

            switch (CurrentCollection)
            {
                case "BodyType":
                    Specifications = new ObservableCollection<Specification>(uof.BodyTypes.GetAll());
                    break;

                case "Brand":
                    Specifications = new ObservableCollection<Specification>(uof.Brands.GetAll());
                    break;

                case "Gearbox":
                    Specifications = new ObservableCollection<Specification>(uof.Gearboxes.GetAll());
                    break;

                case "WheelDrive":
                    Specifications = new ObservableCollection<Specification>(uof.WheelDrives.GetAll());
                    break;

                default:
                    break;
            }
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

                case "Brand":
                    SelectedItem = new Brand();
                    uof.Brands.Add(SelectedItem as Brand);

                    break;

                case "Gearbox":
                    SelectedItem = new Gearbox();
                    uof.Gearboxes.Add(SelectedItem as Gearbox);
                    break;

                case "WheelDrive":
                    SelectedItem = new WheelDrive();
                    uof.WheelDrives.Add(SelectedItem as WheelDrive);
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

                case "Brand":
                    uof.Brands.Remove(SelectedItem as Brand);
                    break;

                case "Gearbox":
                    uof.Gearboxes.Remove(SelectedItem as Gearbox);
                    break;

                case "WheelDrive":
                    uof.WheelDrives.Remove(SelectedItem as WheelDrive);
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
