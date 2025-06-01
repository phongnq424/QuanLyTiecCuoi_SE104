using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyTiecCuoi.Data.Models
{
    public class LOAISANH
    {
        [Key]
        public int MaLoaiSanh { get; set; }
        [Required]
        [MaxLength(100)]
        public string TenLoaiSanh { get; set; }
       
        public double DonGiaBanToiThieu { get; set; }

    }
}
