using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyTiecCuoi.Data.Models
{
    internal class NHOMNGUOIDUNG
    {
        [Key]
        public int MaNhom { get; set; }
        public string TenNhom { get; set; }
    }
}
