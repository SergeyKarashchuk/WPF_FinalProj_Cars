using CarsCatalog.Infrastructure;
using CarsCatalog.Model;
using CarsCatalog.Model.DataProviders;
using CarsCatalog.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows;
using CarsCatalog.ViewModel.Filter;
using Themes;
using CarsCatalog.ViewModel.StyleAndLanguage;
using CarCatalogDAL;
using CarCatalogDAL.Implementations;

namespace CarsCatalog.ViewModel
{
    public class MainViewModel : BaseViewModel
    {
        private bool isWaitWisible;
        public bool IsWaitWisible
        {
            get => isWaitWisible;
            set
            {
                isWaitWisible = value;
                Notify();
            }
        }

        private bool isMainEnable;
        public bool IsMainEnable
        {
            get => isMainEnable;
            set
            {
                isMainEnable = value;
                Notify();
            }
        }

        public MainViewModel()
        {
            IsWaitWisible = false;
            IsMainEnable = true;
            //ApplicationAwaiter.WindowWaitEventHandler += (s, e) =>
            //{
            //    Application.Current.Dispatcher.Invoke(() =>
            //    {
            //        IsWaitWisible = e.IsAwait;
            //        IsMainEnable = !e.IsAwait;
            //    });
            //};
        }
    }
}
