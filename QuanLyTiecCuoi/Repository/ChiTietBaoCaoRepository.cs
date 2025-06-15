using QuanLyTiecCuoi.Data;
using QuanLyTiecCuoi.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyTiecCuoi.Repository
{
    public class ChiTietBaoCaoRepository
    {
        private readonly WeddingDbContext _context;

        public ChiTietBaoCaoRepository(WeddingDbContext context)
        {
            _context = context;
        }
        public List<CHITIETBAOCAO> GetChiTietBaoCao(int thang, int nam)
        {
            return _context.ChiTietBaoCaos
                .Where(b =>
                    (b.BAOCAOTHANG.Thang == thang && b.BAOCAOTHANG.Nam == nam )
                )
                .ToList();
        }
        public async Task AddRangeAsync(List<CHITIETBAOCAO> chiTietBaoCaoList)
        {
            _context.ChiTietBaoCaos.AddRange(chiTietBaoCaoList);
            await _context.SaveChangesAsync();
        }
        public async Task DeleteByBaoCaoIdAsync(int maBaoCao)
        {
            var chiTietList = _context.ChiTietBaoCaos
                .Where(c => c.MaBaoCao == maBaoCao)
                .ToList();

            _context.ChiTietBaoCaos.RemoveRange(chiTietList);
            await _context.SaveChangesAsync();
        }

    }
}
