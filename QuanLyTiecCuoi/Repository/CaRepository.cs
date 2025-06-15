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
            var query = _context.CaSanhs.AsQueryable();

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
            return _context.CaSanhs.ToList();
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

        public void DeleteCa(int maCa)
        {
            var ca = _context.CaSanhs.Find(maCa);
            if (ca != null)
            {
                _context.CaSanhs.Remove(ca);
                _context.SaveChanges();
            }
        }
    }

}
