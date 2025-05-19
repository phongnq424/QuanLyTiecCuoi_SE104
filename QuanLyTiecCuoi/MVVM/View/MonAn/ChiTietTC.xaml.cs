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
using System.Windows.Shapes;
using QuanLyTiecCuoi.MVVM.Model;

namespace QuanLyTiecCuoi.MVVM.View.MonAn
{
    /// <summary>
    /// Constructor dùng khi bạn muốn hiển thị chi tiết 1 món ăn.
    /// </summary>
    /// <param name="monAn">Đối tượng MonAn đã chọn</param>
    public partial class ChiTietTC : Window
    {
        public ChiTietTC()
        {
            InitializeComponent();
            
        }
        public ChiTietTC(MonAnModel monAn)
        {
            this.DataContext = monAn;
        }
        private void BtnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            // Chỉ bắt đầu drag khi là nút trái và không phải click lên control có xử lý riêng
            if (e.ButtonState == MouseButtonState.Pressed)
            {
                this.DragMove();
            }
        }
    }
}
