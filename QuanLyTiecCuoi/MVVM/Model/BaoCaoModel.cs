using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyTiecCuoi.MVVM.Model
{
    public class BaoCaoModel
    {
        public int Thang { get; set; }

        public int Nam { get; set; }
        public decimal TongDoanhThu { get; set; }
        public int TongTiecCuoi { get; set; }
        public List<ChiTietBaoCaoModel> ChiTietBaoCao { get; set; }

        public BaoCaoModel()
        {
            ChiTietBaoCao = new List<ChiTietBaoCaoModel>();
        }

    }
}
