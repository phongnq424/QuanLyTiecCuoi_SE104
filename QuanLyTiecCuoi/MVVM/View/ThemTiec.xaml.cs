using QuanLyTiecCuoi.Services;
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

namespace QuanLyTiecCuoi.MVVM.View
{
    /// <summary>
    /// Interaction logic for ThemTiec.xaml
    /// </summary>
    public partial class ThemTiec : Page
    {
        public ThemTiec()
        {
            InitializeComponent();
            IDatTiecService datTiecService = new DatTiecService();
           // this.DataContext = new ThemTiecViewModel(datTiecService);

            LoadShiftsFromDatabase();
            LoadHallsFromDatabase();
        }
        private void CalendarButton_Click(object sender, RoutedEventArgs e)
        {
            CalendarOverlay.Visibility = Visibility.Visible;
        }

        private void MyCalendar_SelectedDatesChanged(object sender, SelectionChangedEventArgs e)
        {
            if (MyCalendar.SelectedDate.HasValue)
            {
                DateTime selectedDate = MyCalendar.SelectedDate.Value;
                SelectedDateText.Text = selectedDate.ToString("dd/MM/yyyy");

                // Tự động đóng popup sau khi chọn ngày
                //   CalendarPopup.IsOpen = false;
                
                CalendarOverlay.Visibility = Visibility.Collapsed; // Đóng
            }
        }

        //CA
        private void LoadShiftsFromDatabase()
        {
            ShiftComboBox.Items.Clear();
            List<string> shiftsFromDb = new List<string> { "Món 1", "Món 2", "Món 3" }; // Replace with real DB call
            
            foreach (var shift in shiftsFromDb)
            {
                ShiftComboBox.Items.Add(new ComboBoxItem { Content = shift });
            }
        }

        private void ShiftButton_Click(object sender, RoutedEventArgs e)
        {
            ShiftPopup.IsOpen = true;
        }

        private void ShiftComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ShiftComboBox.SelectedItem is ComboBoxItem selectedItem)
            {
                SelectedShiftText.Text = selectedItem.Content.ToString();
                ShiftPopup.IsOpen = false;
            }
        }
        // SẢNH
        private void LoadHallsFromDatabase()
        {
            List<string> hallsFromDb = new List<string> { "Sảnh A", "Sảnh B", "Sảnh VIP", "Sảnh ngoài trời" }; // Thay bằng dữ liệu thực
            HallComboBox.Items.Clear();
            foreach (var hall in hallsFromDb)
            {
                HallComboBox.Items.Add(new ComboBoxItem { Content = hall });
            }
        }

        private void HallButton_Click(object sender, RoutedEventArgs e)
        {
            HallPopup.IsOpen = true;
        }

        private void HallComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (HallComboBox.SelectedItem is ComboBoxItem selectedItem)
            {
                SelectedHallText.Text =  selectedItem.Content.ToString();
                HallPopup.IsOpen = false;
            }
        }
        //FOOD:
        private bool monAnDaChon = false;

        private void MonAnButton_Click(object sender, RoutedEventArgs e)
        {
            // Mở cửa sổ chọn món ăn
            //ChonMonAnWindow chonMon = new ChonMonAnWindow();
            //chonMon.ShowDialog();

            //// Nếu người dùng đã chọn
            //if (!string.IsNullOrEmpty(chonMon.SelectedMonAn))
            //{
            //    // Cập nhật giao diện
            //    SelectedMonAnText.Text = chonMon.SelectedMonAn;
            //    monAnDaChon = true;

            //    // Đổi icon thành chỉnh sửa
            //    MonAnButtonIcon.Source = new BitmapImage(new Uri("edit_icon.png", UriKind.Relative));
            //}
        }

        private void DichVuButton_Click(object sender, RoutedEventArgs e)
        {
            // Mở cửa sổ chọn món ăn
            //ChonMonAnWindow chonMon = new ChonMonAnWindow();
            //chonMon.ShowDialog();

            //// Nếu người dùng đã chọn
            //if (!string.IsNullOrEmpty(chonMon.SelectedMonAn))
            //{
            //    // Cập nhật giao diện
            //    SelectedMonAnText.Text = chonMon.SelectedMonAn;
            //    monAnDaChon = true;

            //    // Đổi icon thành chỉnh sửa
            //    MonAnButtonIcon.Source = new BitmapImage(new Uri("edit_icon.png", UriKind.Relative));
            //}
        }
    }
}
