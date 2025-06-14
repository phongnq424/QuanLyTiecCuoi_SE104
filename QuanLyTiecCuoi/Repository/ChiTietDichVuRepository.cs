using QuanLyTiecCuoi.Data;
using QuanLyTiecCuoi.Data.Models;
using System.Collections.Generic;
using System.Linq;

namespace QuanLyTiecCuoi.Repository
{
    public class ChiTietDichVuRepository
    {
        private readonly WeddingDbContext _context;

        public ChiTietDichVuRepository(WeddingDbContext context)
        {
            _context = context;
        }

        public void Add(CHITIETDVTIEC entity)
        {
            _context.ChiTietDVTiecs.Add(entity);
            _context.SaveChanges();
        }

        public void AddRange(List<CHITIETDVTIEC> entities)
        {
            _context.ChiTietDVTiecs.AddRange(entities);
            _context.SaveChanges();
        }

        public List<CHITIETDVTIEC> GetByMaDatTiec(int maDatTiec)
        {
            return _context.ChiTietDVTiecs.Where(c => c.MaDatTiec == maDatTiec).ToList();
        }

        public void DeleteByMaDatTiec(int maDatTiec)
        {
            var items = _context.ChiTietDVTiecs.Where(x => x.MaDatTiec == maDatTiec).ToList();
            if (items.Any())
            {
                _context.ChiTietDVTiecs.RemoveRange(items);
                _context.SaveChanges();
            }
        }
    }
}