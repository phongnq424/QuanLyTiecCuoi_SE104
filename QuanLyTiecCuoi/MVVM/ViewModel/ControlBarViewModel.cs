using QuanLyTiecCuoi.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;


namespace QuanLyTiecCuoi.MVVM.ViewModel
{

    public class ControlBarViewModel : BaseViewModel
    {
        #region commands
        public ICommand CloseWindowCommand { get; set; }
        public ICommand MinimizeWindowCommand { get; set; }
        public ICommand MaximizeWindowCommand { get; set; }
        #endregion

        public ControlBarViewModel()
        {
            CloseWindowCommand = new RelayCommand<UserControl>((p) => { return true; }, (p) =>
            {
                FrameworkElement parent = GetParent(p);
                Window w = parent as Window;
                if (w != null)
                {
                    w.Owner?.Close(); //đóng wd owner (loginwd)
                    w.Close();
                }
            });
            MinimizeWindowCommand = new RelayCommand<UserControl>((p) => { return true; }, (p) =>
            {
                FrameworkElement parent = GetParent(p);
                Window w = parent as Window;
                if (w != null)
                    w.WindowState = WindowState.Minimized;
            });

            MaximizeWindowCommand = new RelayCommand<UserControl>((p) => { return true; }, (p) =>
            {
                FrameworkElement parent = GetParent(p);
                Window w = parent as Window;
                if (w != null)
                {
                    if (w.WindowState != WindowState.Maximized)
                    {
                        w.WindowState = WindowState.Maximized;
                    }
                    else
                    {
                        w.WindowState = WindowState.Normal;
                    }
                }
            });
        }

        FrameworkElement GetParent(UserControl p)
        {
            FrameworkElement parent = p;
            while (parent.Parent != null)
            {
                parent = parent.Parent as FrameworkElement;
            }
            return parent;
        }
    }
}

