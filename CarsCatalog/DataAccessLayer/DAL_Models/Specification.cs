using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace CarsCatalog.DataAccessLayer.DAL_Models
{
    public abstract class Specification : INotifyPropertyChanged
    {
        private int id;
        public int Id
        {
            get => id;
            set
            {
                id = value;
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

        private string image;
        public string Image
        {
            get => image;
            set
            {
                image = value;
                Notify();
            }
        }

        private void Notify([CallerMemberName] string property="")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }      
        public event PropertyChangedEventHandler PropertyChanged;

        public Specification()
        {
            Name = "Unknown";
        }
    }
}
