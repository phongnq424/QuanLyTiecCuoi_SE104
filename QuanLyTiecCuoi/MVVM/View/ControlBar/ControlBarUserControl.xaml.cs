using Microsoft.Extensions.DependencyInjection;
using QuanLyTiecCuoi.MVVM.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace QuanLyTiecCuoi.MVVM.View.ControlBar
{
    /// <summary>
    /// Interaction logic for ControlBarUserControl.xaml
    /// </summary>
    public partial class ControlBarUserControl : UserControl
    {
        public ControlBarUserControl()
        {
            InitializeComponent();
            if (App.AppHost?.Services != null)
            {
                var vm = App.AppHost.Services.GetRequiredService<ControlBarViewModel>();
                DataContext = vm;
            }
            else
            {

            }
        }
    }
}
