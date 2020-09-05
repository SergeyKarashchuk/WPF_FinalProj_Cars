using CarCatalogDAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarCatalogDAL
{
    public class Car : BaseNotifyEntity
    {
        public int ID { get; set; }

        private string model;
        public string Model
        {
            get => model;
            set
            {
                model = value;
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

        private int? power;
        public int? Power
        {
            get => power;
            set
            {
                power = value;
                Notify();
            }
        }

        private decimal? price;
        public decimal? Price
        {
            get => price;
            set
            {
                price = value;
                Notify();
            }
        }

        private ISpecification bodyType;
        public ISpecification BodyType
        {
            get => bodyType;
            set
            {
                bodyType = value;
                Notify();
            }
        }

        private ISpecification manufacturer;
        public ISpecification Manufacturer
        {
            get => manufacturer;
            set
            {
                manufacturer = value;
                Notify();
            }
        }

        private ISpecification gearBoxType;
        public ISpecification GearBoxType
        {
            get => gearBoxType;
            set
            {
                gearBoxType = value;
                Notify();
            }
        }

        private ISpecification wheelDriveType;
        public ISpecification WheelDriveType
        {
            get => wheelDriveType;
            set
            {
                wheelDriveType = value;
                Notify();
            }
        }
    }
}
