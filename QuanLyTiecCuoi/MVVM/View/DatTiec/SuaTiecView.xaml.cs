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
    public partial class SuaTiecView : Page
    {
        public SuaTiecViewModel viewModel { get; set; }

        public SuaTiecView(DATTIEC selectedTiec)
        {
            InitializeComponent();
            viewModel = new SuaTiecViewModel(selectedTiec);
            this.DataContext = viewModel;

            // Load các combobox (ca, sảnh)
            viewModel.LoadDanhSachCa();
            viewModel.LoadDanhSachSanh();

            // Gán dữ liệu cho ComboBox
            ShiftComboBox.ItemsSource = viewModel.DanhSachCa;
            HallComboBox.ItemsSource = viewModel.DanhSachSanh;

            FilterDatePicker.SelectedDate = viewModel.TiecMoi.NgayDaiTiec;

            ShiftComboBox.DisplayMemberPath = "TenCa";
            ShiftComboBox.SelectedValuePath = "MaCa";
            ShiftComboBox.SelectedValue = viewModel.TiecMoi.MaCa;

            HallComboBox.DisplayMemberPath = "TenSanh";
            HallComboBox.SelectedValuePath = "MaSanh";
            HallComboBox.SelectedValue = viewModel.TiecMoi.MaSanh;
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
            }
        }
        private void ShiftComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ShiftComboBox.SelectedItem is CASANH ca)
            {
                viewModel.TiecMoi.MaCa = ca.MaCa;
                ShiftComboBox.DisplayMemberPath = "TenCa";
            }
        }

        private void HallComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (HallComboBox.SelectedItem is SANH sanh)
            {
                viewModel.TiecMoi.MaSanh = sanh.MaSanh;
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
            if (viewModel.CapNhatTiec())
            {
                MessageBox.Show("Đã cập nhật tiệc cưới thành công!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                NavigationService?.GoBack();
            }
            else
            {
                MessageBox.Show("Cập nhật tiệc thất bại. Vui lòng kiểm tra dữ liệu.", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
