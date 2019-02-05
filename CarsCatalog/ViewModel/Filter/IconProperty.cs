using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace CarsCatalog.ViewModel.Filter
{
    public class IconProperty : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private void Notify([CallerMemberName]string property = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }

        public IconProperty()
        {
            IsExpanded = false;
        }

        private bool isExpanded;
        public bool IsExpanded
        {
            get => isExpanded;
            set
            {
                isExpanded = value;
                ChangeOpacity(value);
                Notify();
            }
        }

        private double opacity;
        public double Opacity
        {
            get => opacity;
            set
            {
                opacity = value;
                Notify();
            }
        }
        private void ChangeOpacity(bool value)
        {
            Opacity = (value == true ? 0.5 : 0);
        }
    }
}
