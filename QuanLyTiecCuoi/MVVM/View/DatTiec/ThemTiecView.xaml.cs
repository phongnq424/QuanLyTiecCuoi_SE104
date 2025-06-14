using QuanLyTiecCuoi.MVVM.ViewModel;
using QuanLyTiecCuoi.MVVM.Model;
using System;
using System.Windows;
using System.Windows.Controls;
using QuanLyTiecCuoi.Data.Models;
using QuanLyTiecCuoi.MVVM.View.MainVindow;
using QuanLyTiecCuoi.MVVM.View.MonAn;
using QuanLyTiecCuoi.MVVM.View.DichVu;

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
            Loaded += ThemTiecView_Loaded;
            // Load các combobox (ca, sảnh)
            viewModel.LoadDanhSachCa();
            viewModel.LoadDanhSachSanh();
            ShiftComboBox.ItemsSource = viewModel.DanhSachCa;
            HallComboBox.ItemsSource = viewModel.DanhSachSanh;
            

        }
        private void ThemTiecView_Loaded(object sender, RoutedEventArgs e)
        {
            SelectedMonAnText.Text = string.Join(", ", viewModel.MonAnDaChon.Select(m => m.TenMon));
            SelectedDichVuText.Text = string.Join(", ", viewModel.DichVuDaChon.Select(d => d.TenDichVu));
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
            if (FilterDatePicker.SelectedDate.HasValue)
            {
                var selectedDate = FilterDatePicker.SelectedDate.Value;
                viewModel.TiecMoi.NgayDaiTiec = selectedDate;
                SelectedDateText.Text = selectedDate.ToString("dd/MM/yyyy");
                FilterDatePicker.Visibility = Visibility.Collapsed; // Ẩn sau khi chọn
            }
        }
        private void ShiftButton_Click(object sender, RoutedEventArgs e)
        {
            ShiftPopup.IsOpen = true;
        }

        private void ShiftComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ShiftComboBox.SelectedItem is CASANH ca)
            {
                viewModel.TiecMoi.MaCa = ca.MaCa;
                SelectedShiftText.Text = ca.TenCa;
                ShiftComboBox.DisplayMemberPath = "TenCa";
            }
        }

        private void HallButton_Click(object sender, RoutedEventArgs e)
        {
            HallPopup.IsOpen = true;
        }

        private void HallComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (HallComboBox.SelectedItem is SANH sanh)
            {
                viewModel.TiecMoi.MaSanh = sanh.MaSanh;
                SelectedHallText.Text = sanh.TenSanh;
                HallComboBox.DisplayMemberPath = "TenSanh";
            }
        }

        private void MonAnButton_Click(object sender, RoutedEventArgs e)
        {
            var chonMonAnPage = new ChonMonAn(viewModel.TiecMoi, viewModel.MonAnDaChon);
            var mainWindow = Application.Current.MainWindow as MainWindow;
            if (mainWindow != null)
            {
                mainWindow.MainFrame.Navigate(chonMonAnPage);
            }
        }


        private void DichVuButton_Click(object sender, RoutedEventArgs e)
        {
            var chonDichVuPage = new ChonDichVu(viewModel.TiecMoi, viewModel.DichVuDaChon);
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
