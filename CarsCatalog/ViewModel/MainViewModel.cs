using CarsCatalog.Infrastructure;
using CarsCatalog.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

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

        private class NavigationItem
        {
            public object Sender { get; set; }
            public ModuleUserControl Control { get; set; }
        }

        private List<NavigationItem> navigationList = new List<NavigationItem>();
        private ModuleUserControl currentControl;
        public ModuleUserControl CurrentControl
        {
            get => currentControl; 
            set 
            {
                currentControl = value;
                Notify();
            }
        }
        private readonly IApplicationNavigation applicationNavigation;

        public MainViewModel()
        {
            IsWaitWisible = false;
            IsMainEnable = true;
            applicationNavigation = DependencyResolver.Resolve<IApplicationNavigation>();
          
            applicationNavigation.WindowWaitEventHandler += (s, e) =>
            {
                ExecuteOperationInSyncThread(() =>
                {
                    IsWaitWisible = e.IsAwait;
                    IsMainEnable = !e.IsAwait;
                });
            };

            applicationNavigation.ChangeCurrentWindowEventHandler += (s, e) =>
            {
                ExecuteOperationInSyncThread(() =>
                {
                    if (CurrentControl != null)
                        navigationList.Add(new NavigationItem { Sender = s, Control = CurrentControl });
                    CurrentControl = e.NewUserControl;
                });
            };

            applicationNavigation.ClosePageEvent += (s, e) =>
            {
                var lastNavItem = navigationList.LastOrDefault();
                if (lastNavItem != null)
                {
                    CurrentControl = lastNavItem.Control;
                    (CurrentControl.DataContext as BaseViewModel)?.Remap();
                }
                else
                {
                    Remap();
                }
            };

            applicationNavigation.SetAwaiter(true);

            Task.Factory.StartNew(async () =>
            {
                await Task.Delay(TimeSpan.FromSeconds(10)).ConfigureAwait(false);
                ExecuteOperationInSyncThread(() =>
                {
                    Remap();
                });
            });
        }

        private void ExecuteOperationInSyncThread(Action action)
        {
            Application.Current.Dispatcher.BeginInvoke(action);
        }

        public override void Remap()
        {
            var uc = new CarCatalogUC();
            applicationNavigation.OpenNewWindow(uc);
            applicationNavigation.SetAwaiter(false);
            navigationList.Clear();
        }
    }
}
