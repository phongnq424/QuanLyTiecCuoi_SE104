using QuanLyTiecCuoi.Data.Models;
using QuanLyTiecCuoi.MVVM.View.MainVindow;
using QuanLyTiecCuoi.MVVM.ViewModel.MonAn;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace QuanLyTiecCuoi.MVVM.View.MonAn
{
    public partial class ChonMonAn : Page
    {
        private readonly ChonMonAnViewModel _viewModel;

        public ChonMonAn(DATTIEC datTiec, ObservableCollection<MONAN> monAnDaChon)
        {
            InitializeComponent();
            _viewModel = new ChonMonAnViewModel(datTiec, monAnDaChon);
            this.DataContext = _viewModel;
        }

        private void MonAn_Click(object sender, MouseButtonEventArgs e)
        {
            if (sender is Border border && border.DataContext is MONAN monAn)
            {
                _viewModel.ChonMonAn(monAn);
            }
        }
        private void Ok_Click(object sender, RoutedEventArgs e)
        {
            //_viewModel.LuuChiTietMenu();
            if (this.NavigationService != null && this.NavigationService.CanGoBack)
            {
                this.NavigationService.GoBack();
            }
            else
            {
                // Cách 2: fallback khi NavigationService null (dùng MainFrame của MainWindow)
                var mainWindow = Application.Current.MainWindow as MainWindow;
                if (mainWindow != null && mainWindow.MainFrame.CanGoBack)
                {
                    mainWindow.MainFrame.GoBack();
                }
            }
        }


        private void SearchBox_GotFocus(object sender, RoutedEventArgs e)
        {
            if (sender is TextBox tb && (tb.Text == "Tên món ăn" || tb.Text == "Đơn giá"))
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
                        tb.Text = "Tên món ăn";
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