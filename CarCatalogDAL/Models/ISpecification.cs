using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarCatalogDAL.Models
{
    public interface ISpecification
    {
        int ID { get; set; }
        string Name { get; set; }
        string Image { get; set; }
    }
}