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
using QuanLyTiecCuoi.Data.Models;
using QuanLyTiecCuoi.MVVM.Model;
using QuanLyTiecCuoi.MVVM.ViewModel.MonAn;

namespace QuanLyTiecCuoi.MVVM.View.MonAn
{
    /// <summary>
    /// Interaction logic for TuyChinhMonAn.xaml
    /// </summary>
    public partial class TuyChinhMonAn : Page
    {
        private TuyChinhMonAnViewModel _viewModel;
        

        public TuyChinhMonAn()
        {
            InitializeComponent();
            _viewModel = new TuyChinhMonAnViewModel();
            this.DataContext = _viewModel;

        }

        private void BtnXoa_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button btn && btn.DataContext is MONAN monAn)
            {
                _viewModel.XoaMonAn(monAn);
            }
        }

        private void BtnChiTiet_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button btn && btn.DataContext is MONAN monAn)
            {
                var window = new ChiTietTC(monAn);
                window.ShowDialog();
            }
        }

        private void BtnChinhSua_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button btn && btn.DataContext is MONAN monAn)
            {
                var window = new SuaMonAn(monAn); // truyền dữ liệu nếu cần
                window.ShowDialog();
            }
        }

        private void BtnThemMonAn_Click(object sender, RoutedEventArgs e)
        {
            var window = new ThemMonAn();
            window.ShowDialog();
        }

        private void SearchBox_GotFocus(object sender, RoutedEventArgs e)
        {
            if (sender is TextBox tb && (tb.Text == "Tên món ăn" || tb.Text == "Đơn giá"))
            {
                tb.Text = "";
            }
        }

        private void SearchBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (sender is TextBox tb && string.IsNullOrWhiteSpace(tb.Text))
            {
                if (tb.Name == "txtSearchName") tb.Text = "Tên món ăn";
                if (tb.Name == "txtSearchPrice") tb.Text = "Đơn giá";
            }
        }
    }



}

