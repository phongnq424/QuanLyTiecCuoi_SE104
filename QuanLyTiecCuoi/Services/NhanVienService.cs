using QuanLyTiecCuoi.Data.Models;
using QuanLyTiecCuoi.Repository;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace QuanLyTiecCuoi.Services
{
    public class NhanVienService
    {
        private readonly NhanVienRepository _repo;

        public NhanVienService(NhanVienRepository repo)
        {
            _repo = repo;
        }

        public async Task<NGUOIDUNG> LoginAsync(string username, string password)
        {
            return await _repo.LoginAsync(username, password);
        }

        public async Task<List<CHUCNANG>> LayChucNangAsync(NGUOIDUNG nv)
        {
            return await _repo.LayChucNangAsync(nv);
        }

        public async Task<List<CHUCNANG>> LayTatCaChucNangAsync()
        {
            return await _repo.LayTatCaChucNangAsync();
        }
    }
}
