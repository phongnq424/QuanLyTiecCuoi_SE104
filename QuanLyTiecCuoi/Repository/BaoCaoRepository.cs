using QuanLyTiecCuoi.Data;
using QuanLyTiecCuoi.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace QuanLyTiecCuoi.Repository
{
    public class BaoCaoRepository
    {
        private readonly WeddingDbContext _context;

        public BaoCaoRepository(WeddingDbContext context)
        {
            _context = context;
        }

        public List<BAOCAOTHANG> GetAll()
        {
            return _context.BaoCaoThangs
                .OrderBy(b => b.Nam)
                .ThenBy(b => b.Thang)
                .ToList();
        }
        public bool CoHoaDonTrongThangNam(int thang, int nam)
        {
            return _context.HoaDons.Any(hd =>
                hd.NgayThanhToan.HasValue &&
                hd.NgayThanhToan.Value.Month == thang &&
                hd.NgayThanhToan.Value.Year == nam);
        }

        public List<BAOCAOTHANG> GetByFilters(
    DateTime? tuNgay = null,
    DateTime? denNgay = null,
    decimal? minDoanhThu = null,
    decimal? maxDoanhThu = null,
    int? minTiecCuoi = null,
    int? maxTiecCuoi = null)
        {
            var query = _context.BaoCaoThangs.AsQueryable();

            // Lấy giá trị mốc thời gian nếu cần
            if (!tuNgay.HasValue)
            {
                var first = query.OrderBy(b => b.Nam).ThenBy(b => b.Thang).FirstOrDefault();
                if (first != null)
                    tuNgay = new DateTime(first.Nam, first.Thang, 1);
            }

            if (!denNgay.HasValue)
            {
                var last = query.OrderByDescending(b => b.Nam).ThenByDescending(b => b.Thang).FirstOrDefault();
                if (last != null)
                    denNgay = new DateTime(last.Nam, last.Thang, 1).AddMonths(1).AddDays(-1); // cuối tháng
            }

            if (tuNgay.HasValue && denNgay.HasValue)
            {
                int fromYear = tuNgay.Value.Year;
                int fromMonth = tuNgay.Value.Month;
                int toYear = denNgay.Value.Year;
                int toMonth = denNgay.Value.Month;

                query = query.Where(b =>
                    (b.Nam > fromYear || (b.Nam == fromYear && b.Thang >= fromMonth)) &&
                    (b.Nam < toYear || (b.Nam == toYear && b.Thang <= toMonth))
                );
            }

            if (minDoanhThu.HasValue)
                query = query.Where(b => b.TongDoanhThu >= minDoanhThu.Value);

            if (maxDoanhThu.HasValue)
                query = query.Where(b => b.TongDoanhThu <= maxDoanhThu.Value);

            if (minTiecCuoi.HasValue)
                query = query.Where(b => b.TongTiecCuoi >= minTiecCuoi.Value);

            if (maxTiecCuoi.HasValue)
                query = query.Where(b => b.TongTiecCuoi <= maxTiecCuoi.Value);

            return query.OrderBy(b => b.Nam).ThenBy(b => b.Thang).ToList();
        }



        public void Add(BAOCAOTHANG baoCaoThang)
        {
            _context.BaoCaoThangs.Add(baoCaoThang);
            _context.SaveChanges();
        }

        public void Update(BAOCAOTHANG baoCaoThang)
        {
            _context.BaoCaoThangs.Update(baoCaoThang);
            _context.SaveChanges();
        }

        public void Delete(BAOCAOTHANG baoCaoThang)
        {
            _context.BaoCaoThangs.Remove(baoCaoThang);
            _context.SaveChanges();
        }

        public BAOCAOTHANG GetByMonthYear(int month, int year)
        {
            return _context.BaoCaoThangs
                .FirstOrDefault(b => b.Thang == month && b.Nam == year);
        }
    }
}
