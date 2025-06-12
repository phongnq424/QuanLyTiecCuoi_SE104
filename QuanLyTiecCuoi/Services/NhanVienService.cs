<<<<<<< HEAD
﻿using Microsoft.EntityFrameworkCore;
using QuanLyTiecCuoi.Data.Models;
=======
﻿using QuanLyTiecCuoi.Data.Models;
>>>>>>> main
using QuanLyTiecCuoi.Repository;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace QuanLyTiecCuoi.Services
{
    public class NhanVienService
    {
<<<<<<< HEAD
        private readonly NhanVienRepository _nhanvienRepository;

        public NhanVienService(NhanVienRepository NhanVienRepo)
        {
            _nhanvienRepository = NhanVienRepo;
=======
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
>>>>>>> main
        }
    }
}
