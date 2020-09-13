using CarCatalogDAL.Abstractions;
using CarCatalogDAL.Implementations;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace CarsCatalog.ViewModel.Filter
{    
    public class FilterCollections : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public event CheckBoxItemChangedEventHandler CheckBoxItemChangedEvent;
        private void Notify([CallerMemberName]string property="")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }

        #region Fields

        private IUnitOfWork uof;
        private List<CheckBoxItem> brandList;
        public List<CheckBoxItem> ManufacturerChecks
        {
            get => brandList;
            set
            {
                brandList = value;
                Notify();
            }
        }

        private List<CheckBoxItem> bodyTypesList;
        public List<CheckBoxItem> BodyTypesChecks
        {
            get => bodyTypesList;
            set
            {
                bodyTypesList = value;
                Notify();
            }
        }

        private List<CheckBoxItem> gearboxList;
        public List<CheckBoxItem> GearBoxTypesChecks
        {
            get => gearboxList;
            set
            {
                gearboxList = value;
                Notify();
            }
        }

        private List<CheckBoxItem> wheeldriveList;
        public List<CheckBoxItem> WheelDriveChecks
        {
            get => wheeldriveList;
            set
            {
                wheeldriveList = value;
                Notify();
            }
        }

        public List<CheckBoxItem> AllSpecifications 
        {
            get
            {
                return WheelDriveChecks.Union(GearBoxTypesChecks.Union(BodyTypesChecks.Union(ManufacturerChecks))).ToList();
            }
        }


        #endregion

        #region Methods

        public FilterCollections()
        {
            uof = DependencyResolver.Resolve<IUnitOfWork>();
            FillCheckBoxLists();
        } 
        
        public void RemapCheckBoxLists()
        {
            FillCheckBoxLists();
        }


        private void FillCheckBoxLists()
        {
            ManufacturerChecks = new List<CheckBoxItem>(uof.Manufacturers.GetAll()
                .Select(x => new CheckBoxItem(x.ID, x.Name, CheckBoxItemType.Manufacturer)));
            ManufacturerChecks.ForEach(x => x.CheckBoxItemChangedEvent += CheckHandler);

            BodyTypesChecks = new List<CheckBoxItem>(uof.BodyTypes.GetAll()
                .Select(x => new CheckBoxItem(x.ID, x.Name, CheckBoxItemType.BodyType)));
            BodyTypesChecks.ForEach(x => x.CheckBoxItemChangedEvent += CheckHandler);

            GearBoxTypesChecks = new List<CheckBoxItem>(uof.GearBoxTypes.GetAll()
                .Select(x => new CheckBoxItem(x.ID, x.Name, CheckBoxItemType.GearBoxType)));
            GearBoxTypesChecks.ForEach(x => x.CheckBoxItemChangedEvent += CheckHandler);

            WheelDriveChecks = new List<CheckBoxItem>(uof.WheelDriveTypes.GetAll()
                .Select(x => new CheckBoxItem(x.ID, x.Name, CheckBoxItemType.WheelDriveType)));
            WheelDriveChecks.ForEach(x => x.CheckBoxItemChangedEvent += CheckHandler);
        }

        private void CheckHandler(object sender, CheckBoxItemChangedEventArg arg)
        {
            CheckBoxItemChangedEvent?.Invoke(sender, arg);
        }       
        #endregion
    }
}
