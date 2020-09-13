using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace CarCatalogDAL.Models
{
    public abstract class CarSpecification :  ISpecification
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected void Notify([CallerMemberName] string property = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }

        public int ID { get; set; }

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

        public abstract string SpecificationType { get; }

        public override bool Equals(object obj)
        {
            if (obj == null || obj.GetType() != this.GetType() || !(obj is CarSpecification carSpecification))
                return false;
            return carSpecification.ID == this.ID;
        }
    }
}
