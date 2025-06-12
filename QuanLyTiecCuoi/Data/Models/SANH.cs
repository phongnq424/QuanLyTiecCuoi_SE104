using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyTiecCuoi.Data.Models
{
    public class SANH
    {
        [Key]
        public int MaSanh { get; set; }
        [Required]
        [MaxLength(100)]
        public string TenSanh { get; set; }
        public int MaLoaiSanh { get; set; }
        public int SoLuongBanToiDa { get; set; }
        public string GhiChu { get; set; }

        [ForeignKey("MaLoaiSanh")]
        public virtual LOAISANH LoaiSanh { get; set; }
        public string HinhAnh { get; set; }
<<<<<<< HEAD
=======

>>>>>>> main
    }
}
