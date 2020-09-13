using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarCatalogDAL.Models
{
    public interface ISpecification : INotifyPropertyChanged
    {
        int ID { get; set; }
        string Name { get; set; }
        string Image { get; set; }
        string SpecificationType { get; }
    }
}