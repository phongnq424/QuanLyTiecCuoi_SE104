using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyTiecCuoi.MVVM.Model
{
    public class Sanh
    {
        public int MaSanh { get; set; }
        public string TenSanh { get; set; }
        public int MaLoaiSanh { get; set; }
        public int? SoLuongBanToiDa { get; set; }
        public string GhiChu { get; set; }
        public string HinhAnh { get; set; }
        public bool isDelelte { get; set; } = false;

        // Liên kết với LoaiSanh (navigation property - nếu cần hiển thị)
        public LoaiSanh LoaiSanh { get; set; }
    }
}
