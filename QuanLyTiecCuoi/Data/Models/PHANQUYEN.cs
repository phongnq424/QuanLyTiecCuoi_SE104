using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyTiecCuoi.Data.Models
{
    public class PHANQUYEN
    {
        public int MaNhom { get; set; }
        public int MaChucNang { get; set; }

        [ForeignKey("MaNhom")]
        public NHOMNGUOIDUNG NHOMNGUOIDUNG { get; set; }

        [ForeignKey("MaChucNang")]
        public CHUCNANG CHUCNANG { get; set; }
    }
}
