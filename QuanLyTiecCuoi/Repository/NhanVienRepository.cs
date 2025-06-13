using Microsoft.EntityFrameworkCore;
using QuanLyTiecCuoi.Data;
using QuanLyTiecCuoi.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.Text;

namespace QuanLyTiecCuoi.Repository
{
    public class NhanVienRepository
    {
        private readonly WeddingDbContext _context;

        public NhanVienRepository(WeddingDbContext context)
        {
            _context = context;
        }
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
        public async Task<List<NGUOIDUNG>> GetAllNguoiDungAsync()
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
        public async Task<NGUOIDUNG> AddUserAsync(NGUOIDUNG nguoidung)
        {
            nguoidung.MatKhau = HashPassword(nguoidung.MatKhau);
            _context.NguoiDungs.Add(nguoidung);
            await _context.SaveChangesAsync();
            return nguoidung;
        }

        /// <summary>
        /// Cập nhật người dùng
        /// </summary>
        /// <param name="nguoiDung"></param>
        public async Task<NGUOIDUNG?> UpdateUserAsync(NGUOIDUNG nguoiDung)
        {
            var existing = await _context.NguoiDungs.FindAsync(nguoiDung.TenDangNhap);
            if (existing == null) return null;

            existing.MaNhom = nguoiDung.MaNhom;

            await _context.SaveChangesAsync();
            return existing;
        }

        /// <summary>
        /// Xóa người dùng
        /// </summary>
        /// <param></param>
        /// <returns></returns>
        public async Task<bool> DeleteAsync(NGUOIDUNG nguoiDung)
        {
            var existing = await _context.NguoiDungs.FindAsync(nguoiDung.TenDangNhap);
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
        /// Lấy nhóm theo tên nhóm
        /// </summary>
        /// <param name="tennhom"></param>
        /// <returns></returns>
        public async Task<NHOMNGUOIDUNG?> LayNhomTheoTenNhom(String tennhom)
        {
            return await _context.NhomNguoiDungs
                                 .FirstOrDefaultAsync(n => n.TenNhom == tennhom);
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
        /// Xóa phân quyền cho 1 nhóm
        /// </summary>
        /// <param name="pq"></param>
        /// <returns></returns>
        public async Task<PHANQUYEN?> XoaChucNangKhoiNhomAsync(PHANQUYEN pq)
        {
            if (pq == null)
                return null;

            _context.PhanQuyens.Remove(pq);
            await _context.SaveChangesAsync();
            return pq;
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
        /// <summary>
        /// Lấy danh sách phân quyền của nhóm
        /// </summary>
        /// <param name="nhom"></param>
        /// <returns></returns>
        public async Task<List<PHANQUYEN>> LayPhanQuyenTheoNhomNguoiDung(NHOMNGUOIDUNG nhom)
        {
            return await _context.PhanQuyens.Include(cn => cn.CHUCNANG).Where(pq => pq.MaNhom == nhom.MaNhom).ToListAsync();
        }

        /// <summary>
        /// Lấy phân quyền theo tên chức năng + nhóm
        /// </summary>
        /// <param name="tencn"></param>
        /// <param name="nhom"></param>
        /// <returns></returns>
        public async Task<PHANQUYEN?> LayPhanQuyenTenCNNhom(string tencn, NHOMNGUOIDUNG nhom)
        {
            return await _context.PhanQuyens
                .FirstOrDefaultAsync(pq => pq.CHUCNANG.TenChucNang == tencn && pq.MaNhom == nhom.MaNhom);
        }
        /// <summary>
        /// tao phan quyen
        /// </summary>
        /// <param name="pq"></param>
        /// <returns></returns>
        public async Task<PHANQUYEN> TaoPhanQuyen(PHANQUYEN pq)
        {
            _context.PhanQuyens.Add(pq);
            await _context.SaveChangesAsync();
            return pq;
        }

        /// <summary>
        /// chinh sua ten nhom
        /// </summary>
        /// <param name="nhom"></param>
        /// <returns></returns>
        public async Task<NHOMNGUOIDUNG> ChinhSuaNhom(NHOMNGUOIDUNG nhom)
        {
            var existing = await _context.NhomNguoiDungs.FindAsync(nhom.MaNhom);
            if (existing == null) return null;
            existing.TenNhom = nhom.TenNhom;

            await _context.SaveChangesAsync();
            return existing;
        }
        /// <summary>
        /// kiem tra truoc khi xoa nhom
        /// </summary>
        /// <param name="nhom"></param>
        /// <returns></returns>
        public async Task<NGUOIDUNG?> CoTonTaiNguoiDungThuocNhom(NHOMNGUOIDUNG nhom)
        {
            return await _context.NguoiDungs.FirstOrDefaultAsync(nd => nd.MaNhom == nhom.MaNhom);
        }
        /// <summary>
        /// Xoa nhom
        /// </summary>
        /// <param name="nhom"></param>
        /// <returns></returns>
        public async Task<bool> XoaNhom(NHOMNGUOIDUNG nhom)
        {
            var existing = await _context.NhomNguoiDungs.FindAsync(nhom.MaNhom);
            if (existing == null) return false;

            _context.NhomNguoiDungs.Remove(existing);
            await _context.SaveChangesAsync();
            return true;
        }

        private string HashPassword(string password)
        {
            using var sha256 = SHA256.Create();
            var bytes = Encoding.UTF8.GetBytes(password);
            var hash = sha256.ComputeHash(bytes);
            return Convert.ToBase64String(hash);
        }
    }
}
