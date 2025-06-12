using Microsoft.EntityFrameworkCore;
using QuanLyTiecCuoi.Data.Models;
using QuanLyTiecCuoi.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyTiecCuoi.Data.Services
{
    internal class NhanVienService
    {
        private readonly NhanVienRepository _nhanvienRepository;

        public NhanVienService(NhanVienRepository NhanVienRepo)
        {
            _nhanvienRepository = NhanVienRepo;
        }
    }
}
