using System;
using System.Collections.Generic;

namespace QuanLyTiecCuoi.MVVM.Model
{
    public class BaoCaoModel
    {
        public int STT { get; set; }

        public int Thang { get; set; }
        public int Nam { get; set; }

        public decimal TongDoanhThu { get; set; }
        public int TongTiecCuoi { get; set; }

        public List<ChiTietBaoCaoModel> ChiTietBaoCao { get; set; }

        public string TongDoanhThuFormatted => TongDoanhThu.ToString("N0") + " VNĐ";

        public BaoCaoModel()
        {
            ChiTietBaoCao = new List<ChiTietBaoCaoModel>();
        }
    }
}
