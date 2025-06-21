using QuanLyTiecCuoi.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QuanLyTiecCuoi.Data;

namespace QuanLyTiecCuoi.Repository
{
    public class CaRepository
    {
        private readonly WeddingDbContext _context;

        public CaRepository(WeddingDbContext context)
        {
            _context = context;
        }

        public List<CASANH> GetFilteredCa(string tenCa, string gioBD, string gioKT)
        {
            var query = _context.CaSanhs
                .Where(c => !c.TinhTrang) // chỉ lấy ca chưa bị xóa
                .AsQueryable();

            if (!string.IsNullOrEmpty(tenCa))
                query = query.Where(c => c.TenCa.Contains(tenCa));

            if (TimeSpan.TryParse(gioBD, out TimeSpan parsedGioBD))
                query = query.Where(c => c.GioBatDau.TimeOfDay == parsedGioBD);

            if (TimeSpan.TryParse(gioKT, out TimeSpan parsedGioKT))
                query = query.Where(c => c.GioKetThuc.TimeOfDay == parsedGioKT);

            return query.ToList();
        }



        public void AddCa(CASANH ca)
        {
            _context.CaSanhs.Add(ca);
            _context.SaveChanges();
        }
        public List<CASANH> GetAllCa()
        {
            return _context.CaSanhs.Where(c => !c.TinhTrang).ToList();
        }

        public void UpdateCa(CASANH ca)
        {
            var existing = _context.CaSanhs.Find(ca.MaCa);
            if (existing != null)
            {
                existing.TenCa = ca.TenCa;
                existing.GioBatDau = ca.GioBatDau;
                existing.GioKetThuc = ca.GioKetThuc;
                _context.SaveChanges();
            }
        }

        public bool DeleteCa(int maCa, out string error)
        {
            error = null;
            var ca = _context.CaSanhs.Find(maCa);
            if (ca == null) return false;

            var now = DateTime.Now;

            // Kiểm tra có tiệc cưới nào trong tương lai dùng ca này không
            bool hasUpcomingEvent = _context.DatTiecs
                .Any(dt => dt.MaCa == maCa && dt.NgayDaiTiec >= now);

            if (hasUpcomingEvent)
            {
                error = "Không thể xóa ca vì có tiệc cưới đang sử dụng trong tương lai.";
                return false;
            }

            // Nếu chỉ trong quá khứ => xóa mềm
            ca.TinhTrang = true;
            _context.SaveChanges();
            return true;
        }

        public bool IsTimeConflict(CASANH caMoi)
        {
            return _context.CaSanhs.Any(c =>
                c.MaCa != caMoi.MaCa && // bỏ qua chính nó nếu đang cập nhật
                (
                    (caMoi.GioBatDau >= c.GioBatDau && caMoi.GioBatDau < c.GioKetThuc) || // bắt đầu trong khoảng ca khác
                    (caMoi.GioKetThuc > c.GioBatDau && caMoi.GioKetThuc <= c.GioKetThuc) || // kết thúc trong khoảng ca khác
                    (caMoi.GioBatDau <= c.GioBatDau && caMoi.GioKetThuc >= c.GioKetThuc) // bao trùm ca khác
                )
            );
        }

    }

}
