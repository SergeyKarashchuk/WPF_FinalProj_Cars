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

        private void Notify([CallerMemberName]string property="")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }

        #region Fields

        private UnitOfWork uof;
        private PropertyChangedEventHandler handler;
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

        #endregion

        #region Methods

        public FilterCollections(PropertyChangedEventHandler handler)
        {
            uof = UnitOfWork.GetUnitOfWork();
            this.handler = handler;
            FillCheckBoxLists(handler);
        } 
        
        public void RemapCheckBoxLists()
        {
            FillCheckBoxLists(this.handler);
        }


        private void FillCheckBoxLists(PropertyChangedEventHandler handler)
        {
            ManufacturerChecks = new List<CheckBoxItem>(uof.Manufacturers.GetAll()
                .Select(x => new CheckBoxItem(x.Name, CheckBoxItemType.Brand, handler)));

            BodyTypesChecks = new List<CheckBoxItem>(uof.BodyTypes.GetAll()
                .Select(x => new CheckBoxItem(x.Name, CheckBoxItemType.BodyType ,handler)));

            GearBoxTypesChecks = new List<CheckBoxItem>(uof.GearBoxTypes.GetAll()
                .Select(x => new CheckBoxItem(x.Name,CheckBoxItemType.Gearbox, handler)));

            WheelDriveChecks = new List<CheckBoxItem>(uof.WheelDriveTypes.GetAll()
                .Select(x => new CheckBoxItem(x.Name, CheckBoxItemType.WheelDrive, handler)));
        }

       
        #endregion


    }
}
