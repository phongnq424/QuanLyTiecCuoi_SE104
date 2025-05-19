using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Windows;
using System.Collections;

namespace QuanLyTiecCuoi.Services
{

    public class DatTiecService : IDatTiecService
    {
        private readonly string connectionString = "Data Source=KHANHVY; Initial Catalog = QuanLyTiecCuoi; Integrated Security = True;";
        //public List<DatTiecModel> GetAll()
        //{
        //    var list = new List<DatTiecModel>();
        //    string query = "SELECT TenCoDau, TenChuRe, SDT, TienDatCoc, MaCa, MaSanh, NgayDatTiec, SoLuongBan, SoBanDuTru  FROM DATTIEC";

        //    try
        //    {
        //        using (SqlConnection conn = new SqlConnection(connectionString))
        //        {
        //            conn.Open();
        //            SqlCommand cmd = new SqlCommand(query, conn);
        //            SqlDataReader reader = cmd.ExecuteReader();

        //            while (reader.Read())
        //            {
        //                list.Add(new DatTiecModel
        //                {
        //                    TenCoDau = reader.GetString(0),
        //                    TenChuRe = reader.GetString(1),
        //                    SDT = reader.GetString(2),
        //                    TienDatCoc = reader.GetSqlMoney(3),
        //                    MaCa = reader.GetInt32(4),
        //                    MaSanh = reader.GetInt32(5),
        //                    NgayDatTiec = reader.GetDateTime(6),
        //                    SoLuongBan = reader.GetInt32(7),
        //                    SoBanDuTru = reader.GetInt32(8),
        //                });
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show("Lỗi khi truy xuất dữ liệu: " + ex.Message);
        //    }


        //    return list;
        //}

        //public void Add(DatTiecModel tiec)
        //{
        //    int newMaDatTiec = 0;

        //    using (SqlConnection connection = new SqlConnection(connectionString))
        //    {
        //        connection.Open();

        //        // Truy vấn mã đặt tiệc mới nhất trong cơ sở dữ liệu
        //        string query = "SELECT TOP 1 MaDatTiec FROM DatTiec ORDER BY MaDatTiec DESC";

        //        using (SqlCommand command = new SqlCommand(query, connection))
        //        {
        //            var result = command.ExecuteScalar();
        //            if (result != null)
        //                newMaDatTiec = int.Parse(result.ToString()) + 1;
        //            else
        //            { }
        //        }
        //    }
        //    using (SqlConnection conn = new SqlConnection(connectionString))
        //    {
        //        string query = @"INSERT INTO DATTIEC 
        //            (MaDatTiec, TenCoDau, TenChuRe, SDT, TienDatCoc, SoLuongBan, SoBanDuTru, NgayDatTiec, MaCa, MaSanh) 
        //            VALUES (@MaDatTiec, @TenCoDau, @TenChuRe, @SDT, @TienDatCoc, @SoLuongBan, @SoBanDuTru, @NgayDatTiec, @MaCa, @MaSanh)";
        //        MessageBox.Show("Hii");

        //        SqlCommand cmd = new SqlCommand(query, conn);
        //        cmd.Parameters.AddWithValue("@MaDatTiec", newMaDatTiec);
        //        cmd.Parameters.AddWithValue("@TenCoDau", tiec.TenCoDau);
        //        cmd.Parameters.AddWithValue("@TenChuRe", tiec.TenChuRe);
        //        cmd.Parameters.AddWithValue("@SDT", tiec.SDT);
        //        cmd.Parameters.AddWithValue("@TienDatCoc", tiec.TienDatCoc);
        //        cmd.Parameters.AddWithValue("@SoLuongBan", tiec.SoLuongBan);
        //        cmd.Parameters.AddWithValue("@SoBanDuTru", tiec.SoBanDuTru);
        //        cmd.Parameters.AddWithValue("@NgayDatTiec", tiec.NgayDatTiec);
        //        cmd.Parameters.AddWithValue("@MaCa", tiec.MaCa);
        //        cmd.Parameters.AddWithValue("@MaSanh", tiec.MaSanh);

        //        conn.Open();
        //        cmd.ExecuteNonQuery();
        //    }
        //}
    }
}