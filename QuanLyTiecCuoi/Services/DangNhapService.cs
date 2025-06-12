using QuanLyTiecCuoi.Data.Models;
using QuanLyTiecCuoi.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyTiecCuoi.Services
{
    public class DangNhapService
    {
        private readonly NhanVienRepository _nhanvienRepository;

        public DangNhapService(NhanVienRepository NhanVienRepo)
        {
            _nhanvienRepository = NhanVienRepo;
        }

        public async Task<NGUOIDUNG?> Login(string UserName, string Password)
        {
            return await _nhanvienRepository.LoginAsync(UserName, Password);
        }

        public async Task<List<CHUCNANG>> LayChucNangNguoiDung(NGUOIDUNG nd)
        {
            return await _nhanvienRepository.GetChucNangByTenDangNhapAsync(nd);
        }

        public async Task<NGUOIDUNG> TaoNguoiDung(NGUOIDUNG nd)
        {
            return await _nhanvienRepository.AddAsync(nd);
        }
    }
}
