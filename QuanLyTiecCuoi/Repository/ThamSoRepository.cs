using Microsoft.EntityFrameworkCore;
using QuanLyTiecCuoi.Data;
using QuanLyTiecCuoi.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyTiecCuoi.Repository
{
    public class ThamSoRepository
    {
        private readonly WeddingDbContext _context;

        public ThamSoRepository(WeddingDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Lay tham so
        /// </summary>
        /// <returns></returns>
        public async Task<THAMSO?> LayDanhSachThamSo()
        {
            return await _context.ThamSos.FirstOrDefaultAsync();
        }

        /// <summary>
        /// Cập nhật tham số
        /// </summary>
        /// <param name="thamSo"></param>
        /// <returns></returns>

        public async Task<THAMSO?> CapNhatThamSo(THAMSO thamSo)
        {
            var existing = await _context.ThamSos.FindAsync(thamSo.Id);
            if (existing != null)
            {
                _context.Entry(existing).CurrentValues.SetValues(thamSo);
                await _context.SaveChangesAsync();
                return existing;
            }
            return null;
        }
    }
}
