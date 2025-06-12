using QuanLyTiecCuoi.Data.Models;
using QuanLyTiecCuoi.Data;
using System;
using Microsoft.EntityFrameworkCore;
using QuanLyTiecCuoi.Data;
using QuanLyTiecCuoi.Data.Models;
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

        public async Task<List<HOADON>> GetAllAsync()
        {
            return await _context.HoaDons.ToListAsync();
        }
    }

}
