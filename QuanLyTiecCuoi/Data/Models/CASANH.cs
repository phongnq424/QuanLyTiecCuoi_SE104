using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyTiecCuoi.Data.Models
{
    public class CASANH
    {
        [Key]
        public int MaCa { get; set; }

        [Required]
        [MaxLength(60)]
        public string TenCa { get; set; }

        public DateTime GioBatDau { get; set; }

        public DateTime GioKetThuc { get; set; }
        public bool TinhTrang { get; set; }
    }
}
