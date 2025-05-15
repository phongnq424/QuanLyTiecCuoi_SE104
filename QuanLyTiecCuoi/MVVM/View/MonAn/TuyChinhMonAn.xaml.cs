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

namespace QuanLyTiecCuoi.MVVM.View.MonAn
{
    /// <summary>
    /// Interaction logic for TuyChinhMonAn.xaml
    /// </summary>
    public partial class TuyChinhMonAn : Page
    {
        // Collection gốc
        public ObservableCollection<MonAnModel> MonAnList { get; set; }

        // View có filter
        private ICollectionView _monAnView;
        public TuyChinhMonAn()
        {
            InitializeComponent();
            // Khởi tạo dữ liệu (thay bằng gọi DB hoặc service của bạn)
            this.Loaded += TuyChinhMonAn_Loaded;
        }
        private void TuyChinhMonAn_Loaded(object sender, RoutedEventArgs e)
        {
            this.Loaded -= TuyChinhMonAn_Loaded;

            // Ở đây chắc chắn tất cả control đã được khởi tạo từ XAML
            MonAnList = new ObservableCollection<MonAnModel>(LoadMonAnDemo());
            _monAnView = CollectionViewSource.GetDefaultView(MonAnList);
            _monAnView.Filter = FilterMonAn;
            itemsControlMonAn.ItemsSource = _monAnView;

            txtSearchName.TextChanged += SearchBox_TextChanged;
            txtSearchPrice.TextChanged += SearchBox_TextChanged;
        }
        private IEnumerable<MonAnModel> LoadMonAnDemo()
        {
            return new[]
            {
               new MonAnModel { TenMon="Gỏi bò", DonGia=150000 },
              new MonAnModel { TenMon="Lẩu Thái", DonGia=350000 },
                new MonAnModel { TenMon="Tráng miệng", DonGia=100000 },
            };
        }
        // Khi người dùng gõ/chỉnh sửa trong 2 ô tìm kiếm
        private void SearchBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            _monAnView.Refresh();
        }
        // Hàm filter chính
        private bool FilterMonAn(object item)
        {
            if (item is MonAnModel m)
            {
                var keyName = txtSearchName.Text.Trim();
                var keyPrice = txtSearchPrice.Text.Trim();

                bool matchName = string.IsNullOrEmpty(keyName)
                    || m.TenMon.IndexOf(keyName, StringComparison.InvariantCultureIgnoreCase) >= 0;

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
            if ((sender as Button)?.DataContext is MonAnModel m)
            {
                var win = new ChiTietTC(m);
                win.Show();
            }
        }

        // Chỉnh sửa
        private void BtnChinhSua_Click(object sender, RoutedEventArgs e)
        {
            if ((sender as Button)?.DataContext is MonAnModel m)
            {
                var win = new SuaMonAn(m);
                win.Show();
            }
        }

        // Thêm món ăn
        private void BtnThemMonAn_Click(object sender, RoutedEventArgs e)
        {
            var win = new ThemMonAn();
            // Giả sử dialog trả về NewMonAn khi OK
            if (win.ShowDialog() == true && win.NewMonAn != null)
            {
                MonAnList.Add(win.NewMonAn);
                _monAnView.Refresh();
            }
        }

        // Xóa với MessageBox xác nhận
        private void BtnXoa_Click(object sender, RoutedEventArgs e)
        {
            if ((sender as Button)?.DataContext is MonAnModel m)
            {
                var res = MessageBox.Show(
                    "Bạn có chắc muốn xóa món ăn này?",
                    "Xác nhận xóa",
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Question);

                if (res == MessageBoxResult.Yes)
                    MonAnList.Remove(m);
            }
        }

        // Xử lý placeholder (đã có trong XAML)
        private void SearchBox_GotFocus(object sender, RoutedEventArgs e)
        {
            if (sender is TextBox tb &&
                (tb.Text == "Tên món ăn" || tb.Text == "Đơn giá"))
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
                    tb.Text = "Tên món ăn";
                else if (tb.Name == "txtSearchPrice")
                    tb.Text = "Đơn giá";
            }
        }
    }

    
}

