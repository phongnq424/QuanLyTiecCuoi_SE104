using QuanLyTiecCuoi.MVVM.ViewModel;
using QuanLyTiecCuoi.MVVM.Model;
using System;
using System.Windows;
using System.Windows.Controls;
using QuanLyTiecCuoi.Data.Models;

namespace QuanLyTiecCuoi.MVVM.View.DatTiec
{
    public partial class ThemTiecView : Page
    {
        private ThemTiecViewModel viewModel;

        public ThemTiecView()
        {
            InitializeComponent();
            viewModel = new ThemTiecViewModel();
            this.DataContext = viewModel;

            // Load các combobox (ca, sảnh)
            viewModel.LoadDanhSachCa();
            viewModel.LoadDanhSachSanh();
            ShiftComboBox.ItemsSource = viewModel.DanhSachCa;
            HallComboBox.ItemsSource = viewModel.DanhSachSanh;
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService?.GoBack();
        }

        private void CalendarButton_Click(object sender, RoutedEventArgs e)
        {
            CalendarOverlay.Visibility = Visibility.Visible;
        }

        private void MyCalendar_SelectedDatesChanged(object sender, SelectionChangedEventArgs e)
        {
            if (MyCalendar.SelectedDate.HasValue)
            {
                var selectedDate = MyCalendar.SelectedDate.Value;
                viewModel.TiecMoi.NgayDaiTiec = selectedDate;
                SelectedDateText.Text = selectedDate.ToString("dd/MM/yyyy");
                CalendarOverlay.Visibility = Visibility.Collapsed;
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
            viewModel.ChonMonAn();
            SelectedMonAnText.Text = string.Join(", ", viewModel.MonAnDaChon);
        }

        private void DichVuButton_Click(object sender, RoutedEventArgs e)
        {
            viewModel.ChonDichVu();
            SelectedDichVuText.Text = string.Join(", ", viewModel.DichVuDaChon);
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
