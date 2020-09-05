using CarsCatalog.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarsCatalog.Infrastructure
{
    public delegate void WindowWaitEvent(object sender, AwaiterEventArg arg);
    public class AwaiterEventArg : EventArgs
    {
        public bool IsAwait { get; }
        public AwaiterEventArg(bool isAwait)
        {
            IsAwait = isAwait;
        }
    }

    public delegate void ChangeCurrentWindowEvent(object sender, ChangeCurrentWindowEventArg arg);
    public class ChangeCurrentWindowEventArg : EventArgs
    {
        public ChangeCurrentWindowEventArg(ModuleUserControl control)
        {
            NewUserControl = control;
        }

        public ModuleUserControl NewUserControl { get; }
    }
    public delegate void ClosePageEvent(object sender, EventArgs arg);

    public interface IApplicationNavigation
    {
        event WindowWaitEvent WindowWaitEventHandler;
        event ChangeCurrentWindowEvent ChangeCurrentWindowEventHandler;
        event ClosePageEvent ClosePageEvent;
        void OpenNewWindow(ModuleUserControl page);
        void SetAwaiter(bool isAwait);
        void ClosePage();
    }

    public class ApplicationNavigation : IApplicationNavigation
    {
        public event WindowWaitEvent WindowWaitEventHandler;
        public event ChangeCurrentWindowEvent ChangeCurrentWindowEventHandler;
        public event ClosePageEvent ClosePageEvent;

        public void SetAwaiter(bool isAwait)
        {
            WindowWaitEventHandler?.Invoke(this, new AwaiterEventArg(isAwait));
        }

        public void OpenNewWindow(ModuleUserControl page)
        {
            ChangeCurrentWindowEventHandler?.Invoke(this, new ChangeCurrentWindowEventArg(page));
        }

        public void ClosePage()
        {
            ClosePageEvent?.Invoke(this, new EventArgs());
        }
    }
}