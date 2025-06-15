using QuanLyTiecCuoi.Data.Models;
using QuanLyTiecCuoi.MVVM.ViewModel.DichVu;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;


namespace QuanLyTiecCuoi.MVVM.View.DichVu
{
    public partial class ChonDichVu : Page
    {
        private readonly ChonDichVuViewModel _viewModel;

        public ChonDichVu(DATTIEC datTiec, ObservableCollection<DICHVU> dichVuDaChon)
        {
            InitializeComponent();
            _viewModel = new ChonDichVuViewModel(datTiec, dichVuDaChon);
            this.DataContext = _viewModel;
        }


        private void DichVu_Click(object sender, MouseButtonEventArgs e)
        {
            if (sender is Border border && border.DataContext is DICHVU dichVu)
            {
                _viewModel.ChonDichVu(dichVu);
            }
        }

        private void OkButton_Click(object sender, RoutedEventArgs e)
        {
            //_viewModel.LuuChiTietDichVu();
            if (this.NavigationService != null && this.NavigationService.CanGoBack)
            {
                this.NavigationService.GoBack();
            }
        }

        private void SearchBox_GotFocus(object sender, RoutedEventArgs e)
        {
            if (sender is TextBox tb && (tb.Text == "Tên dịch vụ" || tb.Text == "Đơn giá"))
            {
                tb.Text = "";
                tb.Foreground = System.Windows.Media.Brushes.Black;
            }
        }

        private void SearchBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (sender is TextBox tb)
            {
                if (string.IsNullOrWhiteSpace(tb.Text))
                {
                    if (tb.Name == "txtSearchName")
                    {
                        tb.Text = "Tên dịch vụ";
                        tb.Foreground = System.Windows.Media.Brushes.Gray;
                        _viewModel.TuKhoaTen = "";
                    }
                    else if (tb.Name == "txtSearchPrice")
                    {
                        tb.Text = "Đơn giá";
                        tb.Foreground = System.Windows.Media.Brushes.Gray;
                        _viewModel.TuKhoaGia = "";
                    }
                }
                else
                {
                    if (tb.Name == "txtSearchName")
                        _viewModel.TuKhoaTen = tb.Text;
                    else if (tb.Name == "txtSearchPrice")
                        _viewModel.TuKhoaGia = tb.Text;
                }
            }
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            if (this.NavigationService != null && this.NavigationService.CanGoBack)
            {
                this.NavigationService.GoBack();
            }
        }
    }
}