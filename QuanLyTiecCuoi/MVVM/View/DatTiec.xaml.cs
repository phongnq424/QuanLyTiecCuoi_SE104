using QuanLyTiecCuoi.MVVM.Model.Services;
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
    /// Interaction logic for DatTiec.xaml
    /// </summary>
    public partial class DatTiec : Page
    {
        private readonly string placeholderText = "Nhập tên tiệc cưới...";
        private bool isPlaceholderActive = true;
        public DatTiec()
        {
            InitializeComponent();

            IDatTiecService datTiecService = new DatTiecService();
            DataContext = new DatTiecViewModel(datTiecService);
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            NavigationService?.Navigate(new ThemTiec());
        }

        private void InHoaDon_Click(object sender, RoutedEventArgs e)
        {
            NavigationService?.Navigate(new ThemTiec());
        }

        private void TextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBox tb = sender as TextBox;
            if (tb != null && isPlaceholderActive)
            {
                tb.Text = "";
                tb.Foreground = new SolidColorBrush(Colors.Black);
                isPlaceholderActive = false;
            }
        }

        private void TextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            TextBox tb = sender as TextBox;
            if (tb != null && string.IsNullOrWhiteSpace(tb.Text))
            {
                tb.Text = placeholderText;
                tb.Foreground = new SolidColorBrush(Colors.Gray);
                isPlaceholderActive = true;
            }
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            // Nếu muốn xử lý khi TextBox thay đổi nội dung
            // Có thể thêm logic tìm kiếm, gợi ý, v.v...
        }

    }
}
