using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyTiecCuoi.MVVM.Model
{
    public class DatTiecServices
    {
        public int MaDatTiec { get; set; }
        public string TenCoDau { get; set; }
        public string TenChuRe { get; set; }
        public string SDT { get; set; }
        public SqlMoney TienDatCoc { get; set; }
        public int MaSanh { get; set; }
        public int MaCa { get; set; }

        public DateTime NgayDatTiec { get; set; }
        public int SoLuongBan { get; set; }
        public int SoBanDuTru { get; set; }
        public TimeSpan Gio { get; set; }
    }
}
