using Microsoft.EntityFrameworkCore;
using QuanLyTiecCuoi.Data.Models;
using QuanLyTiecCuoi.Repository;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace QuanLyTiecCuoi.Services
{
    public class NhanVienService
    {
        private readonly NhanVienRepository _nhanvienRepository;

        public NhanVienService(NhanVienRepository NhanVienRepo)
        {
            _nhanvienRepository = NhanVienRepo;
        }
    }
}
