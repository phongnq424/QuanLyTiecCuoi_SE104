using Microsoft.EntityFrameworkCore;
using QuanLyTiecCuoi.Data;
using QuanLyTiecCuoi.Data.Models;
<<<<<<< HEAD
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.Text;
=======
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
>>>>>>> main

namespace QuanLyTiecCuoi.Repository
{
    public class NhanVienRepository
    {
        private readonly WeddingDbContext _context;

        public NhanVienRepository(WeddingDbContext context)
        {
            _context = context;
        }
<<<<<<< HEAD
        /// <summary>
        /// Login
        /// </summary>
        /// <returns></returns>
        public async Task<NGUOIDUNG?> LoginAsync(string tenDangNhap, string matKhau)
        {
            var hashedPassword = HashPassword(matKhau);

            var user = await _context.NguoiDungs
                .Include(nd => nd.NHOMNGUOIDUNG)
                .FirstOrDefaultAsync(nd =>
                    nd.TenDangNhap == tenDangNhap &&
                    nd.MatKhau == hashedPassword);

            return user;
        }


        /// <summary>
        /// Lấy toàn bộ người dùng và nhóm quyền
        /// </summary>
        public async Task<List<NGUOIDUNG>> GetAllAsync()
        {
            return await _context.NguoiDungs
                .Include(nd => nd.NHOMNGUOIDUNG)
                .ToListAsync();
        }
        /// <summary>
        /// Lấy người dùng theo tên đăng nhập
        /// </summary>
        /// <param name="tenDangNhap"></param>
        public async Task<NGUOIDUNG?> GetByTenDangNhapAsync(string tenDangNhap)
        {
            return await _context.NguoiDungs
                .Include(nd => nd.NHOMNGUOIDUNG)
                .FirstOrDefaultAsync(nd => nd.TenDangNhap == tenDangNhap);
        }
        /// <summary>
        /// Thêm người dùng mới
        /// </summary>
        /// <param name="NGUOIDUNG"></param>
        /// <returns></returns>
        public async Task<NGUOIDUNG> AddAsync(NGUOIDUNG nguoidung)
        {
            nguoidung.MatKhau = HashPassword(nguoidung.MatKhau);
            _context.NguoiDungs.Add(nguoidung);
            await _context.SaveChangesAsync();
            return nguoidung;
        }

        /// <summary>
        /// Cập nhật người dùng
        /// </summary>
        /// <param name="NGUOIDUNG"></param>
        public async Task<bool> UpdateAsync(NGUOIDUNG NGUOIDUNG)
        {
            var existing = await _context.NguoiDungs.FindAsync(NGUOIDUNG.TenDangNhap);
            if (existing == null) return false;

            existing.MatKhau = NGUOIDUNG.MatKhau;
            existing.MaNhom = NGUOIDUNG.MaNhom;

            await _context.SaveChangesAsync();
            return true;
        }

        /// <summary>
        /// Xóa người dùng
        /// </summary>
        /// <param name="tenDangNhap"></param>
        /// <returns></returns>
        public async Task<bool> DeleteAsync(string tenDangNhap)
        {
            var existing = await _context.NguoiDungs.FindAsync(tenDangNhap);
            if (existing == null) return false;

            _context.NguoiDungs.Remove(existing);
            await _context.SaveChangesAsync();
            return true;
        }
        /// <summary>
        /// Lấy danh sách chức năng (permissions) theo nhóm người dùng
        /// </summary>
        /// <param name="tenDangNhap"></param>
        /// <returns></returns>
        public async Task<List<CHUCNANG>> GetChucNangByTenDangNhapAsync(NGUOIDUNG nd)
        {
            return await _context.ChucNangs
                .Where(cn => _context.PhanQuyens
                    .Any(pq => pq.MaNhom == nd.MaNhom && pq.MaChucNang == cn.MaChucNang))
                .ToListAsync();
        }

        /// <summary>
        /// lấy nhóm người dùng
        /// </summary>
        /// <returns></returns>
        public async Task<List<NHOMNGUOIDUNG>> GetNhomNguoiDung()
        {
            return await _context.NhomNguoiDungs.ToListAsync();
        }

        /// <summary>
        /// thêm nhóm người dùng
        /// </summary>
        /// <param name="tenNhom"></param>
        /// <returns></returns>
        public async Task<NHOMNGUOIDUNG> AddNhomNguoiDungAsync(string tenNhom)
        {
            var nhom = new NHOMNGUOIDUNG { TenNhom = tenNhom };
            _context.NhomNguoiDungs.Add(nhom);
            await _context.SaveChangesAsync();
            return nhom;
        }

        /// <summary>
        /// thêm chức năng cho nhóm
        /// </summary>
        /// <param name="maNhom"></param>
        /// <param name="maChucNang"></param>
        /// <returns></returns>
        public async Task<bool> ThemChucNangChoNhomAsync(int maNhom, int maChucNang)
        {
            // Kiểm tra đã tồn tại quyền này chưa
            bool exists = await _context.PhanQuyens
                .AnyAsync(pq => pq.MaNhom == maNhom && pq.MaChucNang == maChucNang);

            if (exists)
                return false; // Đã có rồi, không thêm lại

            var pq = new PHANQUYEN
            {
                MaNhom = maNhom,
                MaChucNang = maChucNang
            };

            _context.PhanQuyens.Add(pq);
            await _context.SaveChangesAsync();
            return true;
        }

        /// <summary>
        /// Xoa chức năng ra khỏi nhóm
        /// </summary>
        /// <param name="maNhom"></param>
        /// <param name="maChucNang"></param>
        /// <returns></returns>
        public async Task<bool> XoaChucNangKhoiNhomAsync(int maNhom, int maChucNang)
        {
            var pq = await _context.PhanQuyens
                .FirstOrDefaultAsync(p => p.MaNhom == maNhom && p.MaChucNang == maChucNang);

            if (pq == null)
                return false;

            _context.PhanQuyens.Remove(pq);
            await _context.SaveChangesAsync();
            return true;
        }

        /// <summary>
        /// lấy danh sách chức năng của nhóm
        /// </summary>
        /// <param name="maNhom"></param>
        /// <returns></returns>
        public async Task<List<CHUCNANG>> LayDanhSachChucNangTheoNhomAsync(int maNhom)
        {
            var phanQuyens = await _context.PhanQuyens
                .Include(pq => pq.CHUCNANG)
                .Where(pq => pq.MaNhom == maNhom)
                .ToListAsync();

            var danhSachChucNang = phanQuyens.Select(pq => pq.CHUCNANG!).ToList();
            return danhSachChucNang;
        }

        /// <summary>
        ///lấy toàn bộ chức năng
        /// </summary>
        /// <returns></returns>
        public async Task<List<CHUCNANG>> LayTatCaChucNangAsync()
        {
            return await _context.ChucNangs.ToListAsync();
        }



        private string HashPassword(string password)
        {
            using var sha256 = SHA256.Create();
            var bytes = Encoding.UTF8.GetBytes(password);
            var hash = sha256.ComputeHash(bytes);
            return Convert.ToBase64String(hash);
=======

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
>>>>>>> main
        }
    }
}
