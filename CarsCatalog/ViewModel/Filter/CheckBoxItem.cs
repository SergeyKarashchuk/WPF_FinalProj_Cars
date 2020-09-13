using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace CarsCatalog.ViewModel.Filter
{
    public enum CheckBoxItemType
    {
        Manufacturer,
        BodyType,
        GearBoxType,
        WheelDriveType,
    }

    public delegate void CheckBoxItemChangedEventHandler(object sender, CheckBoxItemChangedEventArg arg);

    public class CheckBoxItemChangedEventArg
    {
        public CheckBoxItemChangedEventArg(CheckBoxItemType specificationType, bool isChecked)
        {
            SpecificationType = specificationType;
            IsChecked = isChecked;
        }

        public CheckBoxItemType SpecificationType { get; }
        public bool IsChecked { get; }
    }

    public class CheckBoxItem : INotifyPropertyChanged
    {
        public event CheckBoxItemChangedEventHandler CheckBoxItemChangedEvent;

        public event PropertyChangedEventHandler PropertyChanged;
        private void Notify([CallerMemberName]string property = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }

        private void FileterItemChanged(bool isChecked)
        {
            CheckBoxItemChangedEvent?.Invoke(this, new CheckBoxItemChangedEventArg(CheckBoxItemType, isChecked));
        }

        public CheckBoxItem(int id, string name, CheckBoxItemType type)
        {
            ID = id;
            Name = name;
            CheckBoxItemType = type;
            IsChecked = false;
            IsEnabled = true;
        }
        public int ID { get; }
        public CheckBoxItemType CheckBoxItemType { get; }

        private bool isChecked;
        public bool IsChecked
        {
            get => isChecked;
            set
            {
                isChecked = value;
                if (isEnabled)
                {
                    Notify();
                    FileterItemChanged(value);
                }
            }
        }

        private bool isEnabled;
        public bool IsEnabled
        {
            get => isEnabled;
            set
            {
                isEnabled = value;
                Notify();
            }
        }

        private string name;
        public string Name
        {
            get => name;
            set
            {
                name = value;
                Notify();
            }
        }       
    }
}
