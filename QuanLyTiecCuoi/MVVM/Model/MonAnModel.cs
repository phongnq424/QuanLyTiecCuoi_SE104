using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyTiecCuoi.MVVM.Model
{
    public class MonAnModel
    {
        int maMon;
        string tenMon;
        SqlMoney donGia;
        string imagePath;

        public MonAnModel() { }
        public int MaMon { get { return maMon; } set { maMon = value; } }
        public string TenMon { get { return tenMon; } set { tenMon = value; } }
        public SqlMoney DonGia { get { return donGia; } set { donGia = value; } }
        public string ImagePath { get { return imagePath; } set { imagePath = value; } }
    }
}
