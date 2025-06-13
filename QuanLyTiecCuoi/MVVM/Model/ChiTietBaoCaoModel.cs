using QuanLyTiecCuoi.Data.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyTiecCuoi.MVVM.Model
{
    public class ChiTietBaoCaoModel
    {
        public int STT { get; set; }
        public DateTime NgayBaoCao { get; set; }

        public int SoLuongTiecCuoi { get; set; }
        public double DoanhThu { get; set; }
        public string DoanhThuFormatted { get; set; }
        public double TiLeDoanhThu { get; set; }
        public double TiLeTiecCuoi { get; set; }
    }
}
