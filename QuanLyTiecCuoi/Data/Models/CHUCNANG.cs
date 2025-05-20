using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyTiecCuoi.Data.Models
{
    public class CHUCNANG
    {
        [Key]
        public int MaChucNang { get; set; }
        public string TenChucNang { get; set; }
        public string TenManHinhDuocLoad { get; set; }
    }
}
