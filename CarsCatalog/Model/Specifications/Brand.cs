using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace CarsCatalog.Model.Specifications
{
    public class Brand : Specification
    {
        public Brand(
           int id,
           string name,
           string image,
           PropertyChangedEventHandler handler)
           : base(id, name, image, handler)
        { }
    }
}
