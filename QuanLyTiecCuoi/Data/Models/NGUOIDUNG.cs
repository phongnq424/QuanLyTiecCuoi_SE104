using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyTiecCuoi.Data.Models
{
    internal class NGUOIDUNG
    {
        [Key]
        public int TenDangNhap { get; set; }
        public string MatKhau { get; set; }
        public int MaNhom { get; set; }

        [ForeignKey("MaNhom")]
        public NHOMNGUOIDUNG NHOMNGUOIDUNG { get; set; }
    }
}
