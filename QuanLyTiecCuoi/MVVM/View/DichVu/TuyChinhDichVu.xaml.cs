using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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
using QuanLyTiecCuoi.MVVM.Model;
using QuanLyTiecCuoi.MVVM.View.DichVu;

namespace QuanLyTiecCuoi.MVVM.View.DichVu
{
    /// <summary>
    /// Interaction logic for TuyChinhDichVu.xaml
    /// </summary>
    public partial class TuyChinhDichVu : Page
    {
        public ObservableCollection<DichVuModel> DichVuList { get; set; }
        // View có filter
        private ICollectionView _dichVuView;
        public TuyChinhDichVu()
        {
            InitializeComponent();
            // Khởi tạo dữ liệu (thay bằng gọi DB hoặc service của bạn)
            this.Loaded += TuyChinhDichVu_Loaded;
        }
        private void TuyChinhDichVu_Loaded(object sender, RoutedEventArgs e)
        {
            this.Loaded -= TuyChinhDichVu_Loaded;

            // Ở đây chắc chắn tất cả control đã được khởi tạo từ XAML
            DichVuList = new ObservableCollection<DichVuModel>(LoadDichVuDemo());
            _dichVuView = CollectionViewSource.GetDefaultView(DichVuList);
            _dichVuView.Filter = FilterDichVu;
            //itemsControlDichVu.ItemsSource = _dichVuView;

            txtSearchName.TextChanged += SearchBox_TextChanged;
            txtSearchPrice.TextChanged += SearchBox_TextChanged;
        }
        private IEnumerable<DichVuModel> LoadDichVuDemo()
        {
            return new[]
            {
               new DichVuModel { TenDichVu="Âm thanh", DonGia=1500000 },
              new DichVuModel { TenDichVu="Ban nhạc", DonGia=3500000 },
                new DichVuModel { TenDichVu="Trang trí cổng", DonGia=4000000 },
            };
        }
        // Khi người dùng gõ/chỉnh sửa trong 2 ô tìm kiếm
        private void SearchBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            _dichVuView.Refresh();
        }
        // Hàm filter chính
        private bool FilterDichVu(object item)
        {
            if (item is DichVuModel m)
            {
                var keyName = txtSearchName.Text.Trim();
                var keyPrice = txtSearchPrice.Text.Trim();

                bool matchName = string.IsNullOrEmpty(keyName)
                    || m.TenDichVu.IndexOf(keyName, StringComparison.InvariantCultureIgnoreCase) >= 0;

                bool matchPrice = string.IsNullOrEmpty(keyPrice)
                    || m.DonGia.ToString().Contains(keyPrice);

                return matchName && matchPrice;
            }
            return false;
        }

        // --- Các sự kiện của nút ---

        // Chi tiết
        private void BtnChiTiet_Click(object sender, RoutedEventArgs e)
        {
            if ((sender as Button)?.DataContext is DichVuModel m)
            {
                var win = new ChiTietTC(m);
                win.Show();
            }
        }

        // Chỉnh sửa
        private void BtnChinhSua_Click(object sender, RoutedEventArgs e)
        {
            if ((sender as Button)?.DataContext is DichVuModel m)
            {
                var win = new SuaDichVu(m);
                win.Show();
            }
        }

        // Thêm món ăn
        private void BtnThemDichVu_Click(object sender, RoutedEventArgs e)
        {
            var win = new ThemDichVu();
            // Giả sử dialog trả về NewMonAn khi OK
            if (win.ShowDialog() == true && win.NewDichVu != null)
            {
                DichVuList.Add(win.NewDichVu);
                _dichVuView.Refresh();
            }
        }

        // Xóa với MessageBox xác nhận
        private void BtnXoa_Click(object sender, RoutedEventArgs e)
        {
            if ((sender as Button)?.DataContext is DichVuModel m)
            {
                var res = MessageBox.Show(
                    "Bạn có chắc muốn xóa dịch vụ này?",
                    "Xác nhận xóa",
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Question);

                if (res == MessageBoxResult.Yes)
                    DichVuList.Remove(m);
            }
        }

        // Xử lý placeholder (đã có trong XAML)
        private void SearchBox_GotFocus(object sender, RoutedEventArgs e)
        {
            if (sender is TextBox tb &&
                (tb.Text == "Tên dịch vụ" || tb.Text == "Đơn giá"))
            {
                tb.Text = "";
                tb.Foreground = Brushes.Black;
                tb.FontStyle = FontStyles.Normal;
            }
        }

        private void SearchBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (sender is TextBox tb && string.IsNullOrWhiteSpace(tb.Text))
            {
                tb.FontStyle = FontStyles.Italic;
                tb.Foreground = new SolidColorBrush(Color.FromRgb(0x88, 0x88, 0x88));
                if (tb.Name == "txtSearchName")
                    tb.Text = "Tên dịch vụ";
                else if (tb.Name == "txtSearchPrice")
                    tb.Text = "Đơn giá";
            }
        }
    }
}

