using Microsoft.EntityFrameworkCore;
using QuanLyTiecCuoi.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace QuanLyTiecCuoi.Data.Services
{
    internal class HoaDonService
    {
        public HoaDonService() { }
        private static HoaDonService _ins;

        public static HoaDonService Ins
        {
            get
            {
                if (_ins == null)
                {
                    _ins = new HoaDonService();
                }
                return _ins;
            }
            private set { _ins = value; }
        }

        public async Task<List<HOADON>> GetAll()
        {
            try
            {
                using (var context = new WeddingDbContext())
                {
                    var danhsach = (from c in context.HoaDons
                                    select new HOADON
                                    {
                                        MaHoaDon = c.MaHoaDon,
                                        MaDatTiec = c.MaDatTiec,
                                        NgayThanhToan = c.NgayThanhToan,
                                        TongTienBan = c.TongTienBan,
                                        TienPhaiThanhToan = c.TienPhaiThanhToan,
                                        TongTienDV = c.TongTienDV,
                                        TongTienHD = c.TongTienHD,
                                    }).ToListAsync();
                    return await danhsach;
                }


            }
            catch (Exception ex)
            {
                return null;
            }
        }

    }
}
