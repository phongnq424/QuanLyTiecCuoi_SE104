using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyTiecCuoi.Data.Models
{
    internal class CHITIETBAOCAO
    {
        [Key]
        public int MaChiTietBaoCao { get; set; }
        public int MaBaoCao { get; set; }
        [ForeignKey("MaBaoCao")]
        public BAOCAOTHANG BAOCAOTHANG { get; set; }
        public DateTime NgayBaoCao { get; set; }

        public int SoLuongTiecCuoi { get; set; }

        public decimal DoanhThu { get; set; }

        public decimal TyLe { get; set; }
    }
}
