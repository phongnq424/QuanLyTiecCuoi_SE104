using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyTiecCuoi.Data.Models
{
    internal class CHITIETDVTIEC
    {
        [Key]
        public int MaCTDV { get; set; }
        //giá dịch vụ tại thời điểm đặt tiệc
        public int MaDatTiec { get; set; }
        public int MaDichVu { get; set; }
        public int SoLuong { get; set; }

        [ForeignKey("MaDatTiec")]
        public virtual DATTIEC DatTiec { get; set; }

        [ForeignKey("MaDichVu")]
        public virtual DICHVU DichVu { get; set; }
    }
}
