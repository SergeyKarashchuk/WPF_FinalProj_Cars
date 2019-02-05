using CarsCatalog.DataAccessLayer.DAL_Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarsCatalog.DataAccessLayer
{
    public interface IRepository<T>
    {
        void Add(T obj);
        void Update(T obj);
        void Remove(T obj);
        T Get(int id);
        BindingList<T> GetAll();
    }
}
