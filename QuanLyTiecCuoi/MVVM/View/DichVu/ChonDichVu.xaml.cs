using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
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

namespace QuanLyTiecCuoi.MVVM.View.DichVu
{
    /// <summary>
    /// Interaction logic for ChonDichVu.xaml
    /// </summary>
    public partial class ChonDichVu : Window
    {
        public ObservableCollection<DichVu> DanhSachDichVu { get; set; } = new ObservableCollection<DichVu>();
        public ObservableCollection<string> DichVuDaChon { get; set; } = new ObservableCollection<string>();
        public ChonDichVu()
        {
            InitializeComponent();
            DataContext = this;
            //this.maDatTiecHienTai = maDatTiec;
            LoadDichVuFromDatabase();
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
            if (sender is TextBox tb && string.IsNullOrWhiteSpace(tb.Text))
            {
                if (tb.Name == "txtSearchName")
                    tb.Text = "Tên dịch vụ";
                else if (tb.Name == "txtSearchPrice")
                    tb.Text = "Đơn giá";

                tb.Foreground = System.Windows.Media.Brushes.Gray;
            }
        }


        private void LoadDichVuFromDatabase()
        {
            string connectionString = "Server=LAPTOP-4M3UQQRL;Database=QuanLyTiecCuoi;Trusted_Connection=True;";
            string query = "SELECT MaDichVu, TenDichVu, DonGia, MoTa FROM DichVu";

            using (SqlConnection conn = new SqlConnection(connectionString))
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    DanhSachDichVu.Add(new DichVu   
                    {
                        MaDichVu = reader.GetInt32(0),
                        TenDichVu = reader.GetString(1),
                        DonGia = reader.GetDecimal(2),
                        MoTa = reader.GetString(1)
                    });
                }
            }

        }

        private void DichVu_Click(object sender, MouseButtonEventArgs e)
        {
            if (sender is Border border && border.DataContext is DichVu dichVu)
            {
                DichVuDaChon.Add(dichVu.TenDichVu);
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
            string insertQuery = "INSERT INTO CHITIETDVTIEC (MaCTDV, MaDatTiec, MaDichVu, SoLuong, DonGia) VALUES (@MaCTDV, @MaDatTiec, @MaDichVu, @SoLuong, @DonGia)";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                foreach (string tenDichVu in DichVuDaChon)
                {
                    var dv = DanhSachDichVu.FirstOrDefault(m => m.TenDichVu == tenDichVu);
                    if (dv != null)
                    {
                        using (SqlCommand cmd = new SqlCommand(insertQuery, conn))
                        {
                            cmd.Parameters.AddWithValue("@MaDatTiec", maDatTiecHienTai);
                            cmd.Parameters.AddWithValue("@MaMon", dv.MaDichVu);
                            cmd.Parameters.AddWithValue("@SoLuong", 1); // giả sử mỗi món 1 phần
                            cmd.Parameters.AddWithValue("@DonGia", dv.DonGia);
                            cmd.Parameters.AddWithValue("@GhiChu", DBNull.Value); // hoặc ghi chú nếu cần

                            cmd.ExecuteNonQuery();
                        }
                    }
                }
            }

            MessageBox.Show("Đã lưu thực đơn thành công!");
            this.Close(); // thoát form
        }
    }
    public class DichVu
    {
        public int MaDichVu { get; set; }
        public string TenDichVu { get; set; }
        public decimal DonGia { get; set; }

        public string MoTa {  get; set; }
    }
}
