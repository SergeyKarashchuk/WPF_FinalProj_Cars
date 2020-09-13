using CarCatalogDAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarCatalogDAL
{
    public class BodyType : CarSpecification
    {
        public override string SpecificationType => nameof(BodyType);
    }

    public class Manufacturer : CarSpecification
    {
        public override string SpecificationType => nameof(Manufacturer);
    }

    public class GearBoxType : CarSpecification
    {
        public override string SpecificationType => nameof(GearBoxType);
    }

    public class WheelDriveType : CarSpecification
    {
        public override string SpecificationType => nameof(WheelDriveType);
    }
}

