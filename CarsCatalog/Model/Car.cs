using CarsCatalog.DataAccessLayer.DAL_Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Media;

namespace CarsCatalog.Model
{
    public class Car : INotifyPropertyChanged
    {        
        public event PropertyChangedEventHandler PropertyChanged;
        private void Notify([CallerMemberName] string property = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }

        public Car() : this(0, "Unknown", null, 0, 20, null, null, null, null, null)
        {

        }

        public Car(
            int id, 
            string model, 
            string image, 
            int power,
            int price,
            Specification body,
            Specification brand,
            Specification gear,
            Specification wheeldrive,
            PropertyChangedEventHandler handler)
        {
            Id = id;
            Model = model;
            Image = image;
            Power = power;
            Price = price;
            Brand = brand;
            BodyType = body;
            Gearbox = gear;
            WheelDrive = wheeldrive;
            PropertyChanged += handler;
        }

        public int Id { get; set; }

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

        private int power;
        public int Power
        {
            get => power;
            set
            {
                power = value;
                Notify();
            }
        }

        private int price;
        public int Price
        {
            get => price;
            set
            {
                price = value;
                Notify();
            }
        }        

        private Specification brand;
        public Specification Brand
        {
            get => brand;
            set
            {
                brand = value;
                Notify();
            }
        }       

        private Specification bodyType;
        public Specification BodyType
        {
            get => bodyType;
            set
            {
                bodyType = value;
                Notify();
            }
        }       

        private Specification gearbox;
        public Specification Gearbox
        {
            get => gearbox;
            set
            {
                gearbox = value;
                Notify();
            }
        }

        private Specification wheelDrive;
        public Specification WheelDrive
        {
            get => wheelDrive;
            set
            {
                wheelDrive = value;
                Notify();
            }
        }       
    }
}
