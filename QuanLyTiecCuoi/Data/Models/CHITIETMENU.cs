using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyTiecCuoi.Data.Models
{
    public class CHITIETMENU
    {
        [Key]
        public int MaCTMN { get; set; }

        public int MaDatTiec { get; set; }
        public int MaMon { get; set; }
        public int SoLuong { get; set; }

        [MaxLength]
        public string GhiChu { get; set; }

        [ForeignKey("MaDatTiec")]
        public virtual DATTIEC DatTiec { get; set; }

        [ForeignKey("MaMon")]
        public virtual MONAN MonAn { get; set; }
    }
}
