using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarsCatalog.DataAccessLayer.DAL_Models
{
    public class Car
    {        
        public int Id { get; set; }       
        public string Model { get; set; }     
        public string Image { get; set; }    
        public int Power { get; set; }
        public int Price { get; set; }
        public int? BrandId { get; set; }
        public int? BodyTypeId { get; set; }
        public int? GearboxId { get; set; }
        public int? WheelDriveId { get; set; }
    }
}
