using QuanLyTiecCuoi.Data.Models;
using QuanLyTiecCuoi.Data;

namespace QuanLyTiecCuoi.Repository
{
    public class DatTiecRepository
    {
        private readonly WeddingDbContext _context;

        public DatTiecRepository(WeddingDbContext context)
        {
            _context = context;
        }

        public List<DATTIEC> GetAllDatTiec()
        {
            return _context.DatTiecs.ToList();
        }

        public DATTIEC? GetDatTiecById(int id)
        {
            return _context.DatTiecs.FirstOrDefault(d => d.MaDatTiec == id);
        }
        public List<DATTIEC> GetDatTiecByCoDau(string name)
        {
            return _context.DatTiecs.Where(d => d.TenCoDau == name).ToList();
        }
        public List<DATTIEC> GetDatTiecByChuRe(string name)
        {
            return _context.DatTiecs.Where(d => d.TenChuRe == name).ToList();
        }

        public List<DATTIEC> GetDatTiecByPhone(string SDT)
        {
            return _context.DatTiecs.Where(d => d.SDT == SDT).ToList();
        }

        public void AddDatTiec(DATTIEC datTiec)
        {
            _context.DatTiecs.Add(datTiec);
            _context.SaveChanges();
        }

        public void UpdateDatTiec(DATTIEC datTiec)
        {
            _context.DatTiecs.Update(datTiec);
            _context.SaveChanges();
        }

        public void DeleteDatTiec(int id)
        {
            var datTiec = _context.DatTiecs.FirstOrDefault(d => d.MaDatTiec == id);
            if (datTiec != null)
            {
                _context.DatTiecs.Remove(datTiec);
                _context.SaveChanges();
            }
        }
        public List<CASANH> GetAllCaSanhs()
        {
            return _context.CaSanhs.ToList();
        }

        public List<SANH> GetAllSanhs()
        {
            return _context.Sanhs.ToList();
        }
    }
}
