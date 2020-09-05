using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarCatalogDAL.Implementations
{
    public abstract class RepositoryBase
    {
        protected CarCatalogEntities db;

        public RepositoryBase(CarCatalogEntities db)
        {
            this.db = db;
        }
    }
}
