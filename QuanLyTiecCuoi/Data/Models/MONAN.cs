using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyTiecCuoi.Data.Models
{
    public class MONAN
    {
        [Key]
        public int MaMon { get; set; }

        [Required]
        [MaxLength(100)]
        public string TenMon { get; set; }
        public decimal DonGia { get; set; }
        [MaxLength(255)]
        public string HinhAnh { get; set; }
        public bool isDelelte { get; set; }
    }
}
