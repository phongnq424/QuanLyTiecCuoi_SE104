using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using System.Data.SqlClient;
using QuanLyTiecCuoi.MVVM.Model;

namespace QuanLyTiecCuoi.MVVM.View.MonAn
{
    public partial class ChonMonAn : Window
    {
        public ObservableCollection<MonAnModel> DanhSachMonAn { get; set; } = new ObservableCollection<MonAnModel>();
        public ObservableCollection<string> MonAnDaChon { get; set; } = new ObservableCollection<string>();
        


        public ChonMonAn()
        {
            InitializeComponent();
            DataContext = this;
            //this.maDatTiecHienTai = maDatTiec;
            LoadMonAnFromDatabase();


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
            if (sender is TextBox tb && string.IsNullOrWhiteSpace(tb.Text))
            {
                if (tb.Name == "txtSearchName")
                    tb.Text = "Tên món ăn";
                else if (tb.Name == "txtSearchPrice")
                    tb.Text = "Đơn giá";

                tb.Foreground = System.Windows.Media.Brushes.Gray;
            }
        }


        private void LoadMonAnFromDatabase()
        {
            //string connectionString = "Server=LAPTOP-4M3UQQRL;Database=QuanLyTiecCuoi;Trusted_Connection=True;";
            //string query = "SELECT MaMon, TenMon, DonGia FROM MonAn";

            //using (SqlConnection conn = new SqlConnection(connectionString))
            //using (SqlCommand cmd = new SqlCommand(query, conn))
            //{
            //    conn.Open();
            //    SqlDataReader reader = cmd.ExecuteReader();
            //    while (reader.Read())
            //    {
            //        DanhSachMonAn.Add(new MonAnModel
            //        {
            //            MaMon = reader.GetInt32(0),
            //            TenMon = reader.GetString(1),
            //            DonGia = reader.GetDecimal(2)
            //        });
            //    }
            //}
           
        }

        private void MonAn_Click(object sender, MouseButtonEventArgs e)
        {
            if (sender is Border border && border.DataContext is MonAnModel monAn)
            {
                MonAnDaChon.Add(monAn.TenMon);
            }
        }
        private void Minimize_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void Maximize_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = (this.WindowState == WindowState.Maximized) ? WindowState.Normal : WindowState.Maximized;
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        private int maDatTiecHienTai = 1; // hoặc truyền vào khi mở cửa sổ

        private void OkButton_Click(object sender, RoutedEventArgs e)
        {
            string connectionString = "Server=LAPTOP-4M3UQQRL;Database=QuanLyTiecCuoi;Trusted_Connection=True;";
            string insertQuery = "INSERT INTO CHITIETMENU (MaDatTiec, MaMon, SoLuong, DonGia, GhiChu) VALUES (@MaDatTiec, @MaMon, @SoLuong, @DonGia, @GhiChu)";

            //using (SqlConnection conn = new SqlConnection(connectionString))
            //{
            //    conn.Open();

            //    foreach (string tenMon in MonAnDaChon)
            //    {
            //        var mon = DanhSachMonAn.FirstOrDefault(m => m.TenMon == tenMon);
            //        if (mon != null)
            //        {
            //            using (SqlCommand cmd = new SqlCommand(insertQuery, conn))
            //            {
            //                cmd.Parameters.AddWithValue("@MaDatTiec", maDatTiecHienTai);
            //                cmd.Parameters.AddWithValue("@MaMon", mon.MaMon);
            //                cmd.Parameters.AddWithValue("@SoLuong", 1); // giả sử mỗi món 1 phần
            //                cmd.Parameters.AddWithValue("@DonGia", mon.DonGia);
            //                cmd.Parameters.AddWithValue("@GhiChu", DBNull.Value); // hoặc ghi chú nếu cần

            //                cmd.ExecuteNonQuery();
            //            }
            //        }
            //    }
            //}

            MessageBox.Show("Đã lưu thực đơn thành công!");
            this.Close(); // thoát form
        }

    }

    
}
