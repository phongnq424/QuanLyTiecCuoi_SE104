using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyTiecCuoi.Data.Models
{
    public class DICHVU
    {
        [Key]
        public int MaDichVu { get; set; }

        [Required]
        [MaxLength(100)]
        public string TenDichVu { get; set; }
        public int SoLuong { get; set; }

        public decimal DonGia { get; set; }

        public string MoTa { get; set; }

        [MaxLength(255)]
        public string HinhAnh { get; set; }
        public bool TinhTrang { get; set; }
    }
}
