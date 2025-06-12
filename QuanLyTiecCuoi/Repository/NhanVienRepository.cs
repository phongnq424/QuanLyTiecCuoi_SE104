using Microsoft.EntityFrameworkCore;
using QuanLyTiecCuoi.Data;
using QuanLyTiecCuoi.Data.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuanLyTiecCuoi.Repository
{
    public class NhanVienRepository
    {
        private readonly WeddingDbContext _context;

        public NhanVienRepository(WeddingDbContext context)
        {
            _context = context;
        }

        public async Task<NGUOIDUNG> LoginAsync(string username, string password)
        {
            return await _context.NguoiDungs
                .FirstOrDefaultAsync(x => x.TenDangNhap == username && x.MatKhau == password);
        }

        public async Task<List<CHUCNANG>> LayChucNangAsync(NGUOIDUNG nv)
        {
            return await (from pq in _context.PhanQuyens
                          where pq.MaNhom == nv.MaNhom
                          join cn in _context.ChucNangs on pq.MaChucNang equals cn.MaChucNang
                          select new CHUCNANG
                          {
                              MaChucNang = cn.MaChucNang,
                              TenChucNang = cn.TenChucNang,
                              TenManHinhDuocLoad = cn.TenManHinhDuocLoad
                          }).ToListAsync();
        }

        public async Task<List<CHUCNANG>> LayTatCaChucNangAsync()
        {
            return await _context.ChucNangs
                .Select(cn => new CHUCNANG
                {
                    MaChucNang = cn.MaChucNang,
                    TenChucNang = cn.TenChucNang,
                    TenManHinhDuocLoad = cn.TenManHinhDuocLoad
                })
                .ToListAsync();
        }
    }
}
