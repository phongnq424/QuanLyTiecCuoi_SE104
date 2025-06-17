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

        private void ClearFilter_Click(object sender, MouseButtonEventArgs e)
        {
            if (DataContext is SanhViewModel vm)
            {
                vm.SelectedDate = null;
                vm.SelectedCaSanh = null;
            }
        }

        private void btnCTSanh_Click(object sender, RoutedEventArgs e)
        {
            var vm = (MainWindowViewModel)Application.Current.MainWindow.DataContext;

            // Lọc danh sách các chức năng có tên màn hình là DSSanhView hoặc QLDSSanhView
            var chucNangs = vm.DanhSachChucNang
                              .Where(c => c.TenManHinhDuocLoad == "QLDSSanhView" || c.TenManHinhDuocLoad == "DSSanhView")
                              .ToList();

            // Ưu tiên QLDSSanhView nếu tồn tại, nếu không thì lấy DSSanhView
            var chon = chucNangs.FirstOrDefault(c => c.TenManHinhDuocLoad == "QLDSSanhView")
                    ?? chucNangs.FirstOrDefault(c => c.TenManHinhDuocLoad == "DSSanhView");

            if (chon != null)
                vm.DieuHuongCommand.Execute(chon);
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            if (DataContext is SanhViewModel vm)
            {
                vm.RefreshDanhSachSanh(); 
            }

            var mainvm = (MainWindowViewModel)Application.Current.MainWindow.DataContext;

            if (!(mainvm.DanhSachChucNang.Any(cn => cn.TenChucNang == "Sảnh" || mainvm.DanhSachChucNang.Any(cn => cn.TenChucNang == "Quản lý sảnh"))))
            {
                btnChiTietSanh.Visibility = Visibility.Collapsed;
                return;
            }
        }

        private void SanhItem_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            var vm = (MainWindowViewModel)Application.Current.MainWindow.DataContext;

            if (!vm.DanhSachChucNang.Any(cn => cn.TenChucNang == "Đặt tiệc"))
            {
                MessageBox.Show("Bạn không có chức năng đặt tiệc!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }  
            
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
                    var mainVM = (App.Current.MainWindow as MainWindow)?.DataContext as MainWindowViewModel;
                    if (mainVM != null)
                    {
                        foreach (var cn in mainVM.DanhSachChucNang)
                            cn.IsChecked = false;

                        var chucNangDatTiec = mainVM.DanhSachChucNang
                            .FirstOrDefault(c => c.TenChucNang == "Đặt tiệc");

                        if (chucNangDatTiec != null)
                        {
                            chucNangDatTiec.IsChecked = true;
                            mainVM.DangChon = chucNangDatTiec;
                        }
                    }
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
