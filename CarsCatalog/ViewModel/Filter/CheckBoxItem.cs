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
        Brand,
        BodyType,
        Gearbox,
        WheelDrive
    }

    public class CheckBoxItem : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private void Notify([CallerMemberName]string property = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }

        public CheckBoxItem(string name, CheckBoxItemType type, PropertyChangedEventHandler handler)
        {
            CheckBoxItemType = type;
            Name = name;
            IsChecked = false;
            IsEnabled = true;
            PropertyChanged += handler;
        }

        public CheckBoxItemType CheckBoxItemType { get; }

        private bool isChecked;
        public bool IsChecked
        {
            get => isChecked;
            set
            {
                isChecked = value;
                if (isEnabled)
                    Notify();
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
