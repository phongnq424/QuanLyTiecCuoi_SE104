using QuanLyTiecCuoi.MVVM.ViewModel;
using QuanLyTiecCuoi.MVVM.Model;
using System;
using System.Windows;
using System.Windows.Controls;
using QuanLyTiecCuoi.Data.Models;
using QuanLyTiecCuoi.MVVM.View.MainVindow;
using QuanLyTiecCuoi.MVVM.View.MonAn;
using QuanLyTiecCuoi.MVVM.View.DichVu;
using QuanLyTiecCuoi.MVVM.ViewModel;
using QuanLyTiecCuoi.Services;

namespace QuanLyTiecCuoi.MVVM.View.DatTiec
{
    public partial class ThemTiecView : Page
    {
        public ThemTiecViewModel viewModel { get; set; }

        public ThemTiecView()
        {
            InitializeComponent();
            viewModel = new ThemTiecViewModel();
            this.DataContext = viewModel;
            viewModel.LoadDanhSachCa();
            viewModel.LoadDanhSachSanh();
        }

        public ThemTiecView(Sanh sanh, DateTime ngay, int maCa)
        {
            InitializeComponent();
            viewModel = new ThemTiecViewModel(sanh, ngay, maCa);
            this.DataContext = viewModel;

            viewModel.LoadDanhSachCa();
            viewModel.LoadDanhSachSanh();

            ButtonBack.Visibility = Visibility.Collapsed;
            FilterDatePicker.IsEnabled = false;
            CaComboBox.IsEnabled = false;
            SanhComboBox.IsEnabled = false;
        }
        private void ca_changed(object sender, SelectionChangedEventArgs e)
        {
            if (viewModel != null)
            {
                viewModel.LoadDanhSachSanh();
            }
        }
        private void TextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            if (sender is TextBox tb)
            {
                tb.Clear();
            }
        }
        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService?.GoBack();
        }
        private void FilterDatePicker_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            viewModel.LoadDanhSachSanh();
            if (FilterDatePicker.SelectedDate.HasValue)
            {
                var selectedDate = FilterDatePicker.SelectedDate.Value;
                viewModel.TiecMoi.NgayDaiTiec = selectedDate;
            }
        }
        private void MonAnButton_Click(object sender, RoutedEventArgs e)
        {
            var chonMonAnPage = new ChonMonAn(this.DataContext as ThemTiecViewModel);

            var mainWindow = Application.Current.MainWindow as MainWindow;
            if (mainWindow != null)
            {
                mainWindow.MainFrame.Navigate(chonMonAnPage);
            }
        }
        private void DichVuButton_Click(object sender, RoutedEventArgs e)
        {
            var chonDichVuPage = new ChonDichVu(viewModel);
            var mainWindow = Application.Current.MainWindow as MainWindow;
            if (mainWindow != null)
            {
                mainWindow.MainFrame.Navigate(chonDichVuPage);
            }
        }


        private void LuuTiec(object sender, RoutedEventArgs e)
        {
            if (viewModel.ThemTiecMoi())
            {
                MessageBox.Show("Đã lưu tiệc cưới thành công!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                NavigationService?.GoBack();
            }
            else
            {
                MessageBox.Show("Lưu tiệc thất bại. Vui lòng kiểm tra dữ liệu.", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}