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
using QuanLyTiecCuoi.Data.Models;
using QuanLyTiecCuoi.MVVM.View.DatTiec;
using QuanLyTiecCuoi.MVVM.View.MainVindow;
using QuanLyTiecCuoi.MVVM.Model;

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

        private void btnCTSanh_Click(object sender, RoutedEventArgs e)
        {
            var SanhVM = App.AppHost.Services.GetRequiredService<SanhViewModel>();
            NavigationService.Navigate(new DSSanhView(SanhVM));
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            if (DataContext is SanhViewModel vm)
            {
                vm.RefreshDanhSachSanh(); 
            }
        }

        private void SanhItem_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            var border = sender as Border;
            if (border != null)
            {
                var sanh = border.Tag as Sanh;
                var viewModel = this.DataContext as SanhViewModel;

                if (sanh != null && viewModel != null && viewModel.SelectedDate.HasValue && viewModel.SelectedCaSanh != null)
                {
                    var ngay = viewModel.SelectedDate.Value;
                    var ca = viewModel.SelectedCaSanh.MaCa;

                    // Mở giao diện thêm tiệc mới 
                    var themView = new ThemTiecView(sanh, ngay, ca);
                    themView.viewModel.DanhSachChanged += () =>
                    {
                        var current = (App.Current.MainWindow as MainWindow)?.MainFrame.Content;
                        if (current is DatTiecView dtv)
                        {
                            (dtv.DataContext as DatTiecViewModel)?.LoadDanhSachDatTiec();
                        }
                    };
                    (App.Current.MainWindow as MainWindow)?.MainFrame.Navigate(themView);
                }
                else
                {
                    MessageBox.Show("Vui lòng chọn ngày và ca sảnh.");
                    return;
                }
            }
        }
    }
}
