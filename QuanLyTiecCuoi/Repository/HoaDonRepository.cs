using QuanLyTiecCuoi.Data;
using QuanLyTiecCuoi.Data.Models;
using Microsoft.EntityFrameworkCore;  // Cần để dùng các async EF Core
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuanLyTiecCuoi.Repository
{
    public class HoaDonRepository
    {
        private readonly WeddingDbContext _context;

        public HoaDonRepository(WeddingDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Lấy tất cả hóa đơn.
        /// </summary>
        public async Task<List<HOADON>> GetAllAsync()
        {
            return await _context.HoaDons.ToListAsync();
        }

        /// <summary>
        /// Lấy hóa đơn theo mã.
        /// </summary>
        public async Task<List<HOADON>> GetByIdAsync(string id)
        {
            return await _context.HoaDons
                .Where(h => h.MaHoaDon.ToString().Contains(id))
                .ToListAsync();
        }

        public async Task<List<HOADON>> GetByMaDatTiecAsync(String maDatTiec)
        {
            return await _context.HoaDons
                .Where(h => h.MaDatTiec.ToString().Contains(maDatTiec))
                .ToListAsync();
        }

        /// <summary>
        /// Lọc hóa đơn theo khoảng đơn giá.
        /// </summary>
        public async Task<List<HOADON>> GetByTongTienRangeAsync(decimal min, decimal max)
        {
            return await _context.HoaDons
                                 .Where(h => h.TongTienHD >= min && h.TongTienHD <= max)
                                 .ToListAsync();
        }

        /// <summary>
        /// Lọc hóa đơn theo ngày thanh toán.
        /// </summary>
        public async Task<List<HOADON>> GetByNgayThanhToanAsync(DateTime ngayThanhToan)
        {
            return await _context.HoaDons
                                 .Where(h => h.NgayThanhToan.HasValue && h.NgayThanhToan.Value.Date == ngayThanhToan.Date)
                                 .ToListAsync();
        }

        /// <summary>
        /// Lấy danh sách hóa đơn theo trạng thái thanh toán.
        /// </summary>
        public async Task<List<HOADON>> GetByTrangThaiThanhToanAsync(bool daThanhToan)
        {
            return daThanhToan
                ? await _context.HoaDons.Where(h => h.NgayThanhToan != null).ToListAsync()
                : await _context.HoaDons.Where(h => h.NgayThanhToan == null).ToListAsync();
        }

        /// <summary>
        /// Thêm mới một hóa đơn.
        /// </summary>
        public async Task<HOADON> AddAsync(HOADON hoaDon)
        {
            _context.HoaDons.Add(hoaDon);
            await _context.SaveChangesAsync();
            return hoaDon;
        }

        /// <summary>
        /// Cập nhật một hóa đơn.
        /// </summary>
        public async Task<HOADON?> UpdateAsync(HOADON hoaDon)
        {
            var existing = await _context.HoaDons.FindAsync(hoaDon.MaHoaDon);
            if (existing != null)
            {
                _context.Entry(existing).CurrentValues.SetValues(hoaDon);
                await _context.SaveChangesAsync();
                return existing;
            }
            return null;
        }

        /// <summary>
        /// Xóa một hóa đơn.
        /// </summary>
        public async Task<HOADON?> DeleteAsync(int maHoaDon)
        {
            var existing = await _context.HoaDons.FindAsync(maHoaDon);
            if (existing != null)
            {
                _context.HoaDons.Remove(existing);
                await _context.SaveChangesAsync();
                return existing;
            }
            return null;
        }


        /// <summary>
        /// Lấy thông tin chi tiết dịch vụ tiệc của hóa đơn đc chọn
        /// </summary>
        public async Task<List<CHITIETDVTIEC>> GetCTDV(int maDatTiec)
        {
            return await _context.ChiTietDVTiecs.Include(ct => ct.DichVu).Where(h => h.MaDatTiec == maDatTiec).ToListAsync();
        }

        /// <summary>
        /// Lấy thông tin đặt tiệc của hóa đơn đc chọn
        /// </summary>
        public async Task<DATTIEC?> GetDatTiec(int maDatTiec)
        {
            return await _context.DatTiecs.FirstOrDefaultAsync(h => h.MaDatTiec == maDatTiec);
        }

        /// <summary>
        /// Lấy thông tin Menu của hóa đơn đc chọn
        /// </summary>
        public async Task<List<CHITIETMENU>> GetMenu(int maDatTiec)
        {
            return await _context.ChiTietMenus.Where(h => h.MaDatTiec == maDatTiec).ToListAsync();
        }

        /// <summary>
        /// Lấy danh sach dich vu
        /// </summary>
        public async Task<List<DICHVU>> GetDichVu()
        {
            return await _context.DichVus.ToListAsync();
        }

        /// <summary>
        /// Lưu CTDV
        /// </summary>
        public async Task<CHITIETDVTIEC> AddCTDV(CHITIETDVTIEC ctdv)
        {
            _context.ChiTietDVTiecs.Add(ctdv);
            await _context.SaveChangesAsync();
            return ctdv;
        }

        public async Task<CHITIETDVTIEC?> DeleteCTDV(int maCTDV)
        {
            var existing = await _context.ChiTietDVTiecs.FindAsync(maCTDV);
            if (existing != null)
            {
                _context.ChiTietDVTiecs.Remove(existing);
                await _context.SaveChangesAsync();
                return existing;
            }
            return null;
        }

        public async Task<HOADON?> ThanhToan(HOADON hoaDon)
        {
            hoaDon.NgayThanhToan = DateTime.Now;
            hoaDon.TienPhaiThanhToan = 0;
            var existing = await _context.HoaDons.FindAsync(hoaDon.MaHoaDon);
            if (existing != null)
            {
                _context.Entry(existing).CurrentValues.SetValues(hoaDon);
                await _context.SaveChangesAsync();
                return existing;
            }
            return null;
        }

        public async Task<List<HOADON>> GetByNgayThanhToanRangeAsync(DateTime from, DateTime to)
        {
            return await _context.HoaDons
                .Where(h => h.NgayThanhToan >= from && h.NgayThanhToan < to)
                .ToListAsync();
        }
        public async Task<List<HOADON>> GetByThangNamAsync(int thang, int nam)
        {
            return await _context.HoaDons
                .Where(h => h.NgayThanhToan.HasValue &&
                            h.NgayThanhToan.Value.Month == thang &&
                            h.NgayThanhToan.Value.Year == nam)
                .ToListAsync();
        }


    }
}
