using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarCatalogDAL.Models
{
    public class CarSpecification : BaseNotifyEntity, ISpecification
    {
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
    }
}
