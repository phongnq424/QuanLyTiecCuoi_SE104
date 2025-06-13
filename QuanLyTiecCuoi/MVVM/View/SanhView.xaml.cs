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
using QuanLyTiecCuoi.MVVM.ViewModel;
using System.Windows.Shapes;
using QuanLyTiecCuoi.Services;
using Microsoft.Extensions.DependencyInjection;

namespace QuanLyTiecCuoi.MVVM.View
{
    /// <summary>
    /// Interaction logic for SanhView.xaml
    /// </summary>
    public partial class SanhView : Page
    {

        public SanhView(SanhViewModel vm)
        {
            InitializeComponent();
            DataContext = vm;
        }

        private void btnCTLoaiSanh_Click(object sender, RoutedEventArgs e)
        {
            var loaiSanhVM = App.AppHost.Services.GetRequiredService<LoaiSanhViewModel>();
            NavigationService.Navigate(new DSLoaiSanhView(loaiSanhVM));
        }

        private void btnCTSanh_Click(object sender, RoutedEventArgs e)
        {
            var SanhVM = App.AppHost.Services.GetRequiredService<SanhViewModel>();
            NavigationService.Navigate(new DSSanhView(SanhVM));
        }
    }
}
