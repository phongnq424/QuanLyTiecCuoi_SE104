using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyTiecCuoi.Data.Models
{
    public class THAMSO
    {
        [Key]
        public int Id { get; set; }
        public decimal TyLePhatThanhToanTreTheoNgay { get; set; }

        public bool ApDungQDPhatThanhToanTre { get; set; }
        public decimal PhanTramDatCoc { get; set; }

    }
}
