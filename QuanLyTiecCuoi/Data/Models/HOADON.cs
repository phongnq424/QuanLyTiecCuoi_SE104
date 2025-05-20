using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyTiecCuoi.Data.Models
{
    public class HOADON
    {
        [Key]
        public int MaHoaDon { get; set; }
        public DateTime NgayThanhToan { get; set; }

        //bỏ đơn giá bàn
        public decimal DonGiaBan { get; set; }
        public decimal TongTienBan { get; set; }
        public int MaDatTiec { get; set; }
        public decimal TongTienDV { get; set; }
        public decimal TongTienHD { get; set; }
        public decimal TienPhaiThanhToan { get; set; }

        [ForeignKey("MaDatTiec")]
        public DATTIEC DATTIEC { get; set; }
    }
}
