using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyTiecCuoi.MVVM.Model
{
    public class DichVuModel
    {
        int maDichVu;
        string tenDichVu;
        SqlMoney donGia;
        string moTa;
        string imagePath;
        public DichVuModel() { }
        
        public int MaDichVu { get { return maDichVu; } set { maDichVu = value; } }
        public string TenDichVu { get {return tenDichVu; } set { tenDichVu = value; } }
        public string MoTa { get { return moTa; } set { moTa = value; } }   
        public SqlMoney DonGia { get { return donGia; } set { donGia = value; } }
        public string ImagePath { get { return imagePath; } set { imagePath = value; } }
    }
}
