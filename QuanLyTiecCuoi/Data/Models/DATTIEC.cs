using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;

namespace QuanLyTiecCuoi.Data.Models
{
    public class DATTIEC
    {
        [Key]
        public int MaDatTiec { get; set; }

        [Required]
        [MaxLength(100)]
        public string TenCoDau { get; set; }

        [Required]
        [MaxLength(100)]
        public string TenChuRe { get; set; }

        [Required]
        [MaxLength(10)]
        public string SDT { get; set; }

        public int MaSanh { get; set; }
        public int MaCa { get; set; }

        public decimal TienDatCoc { get; set; }
        public int SoLuongBan { get; set; }
        public int SoBanDuTru { get; set; }

        public DateTime NgayDaiTiec { get; set; }
        public TimeSpan Gio { get; set; }

        [ForeignKey("MaSanh")]
        public virtual SANH Sanh { get; set; }

        [ForeignKey("MaCa")]
        public virtual CASANH CaSanh { get; set; }
    }
}
