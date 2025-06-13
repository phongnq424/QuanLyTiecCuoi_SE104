using QuanLyTiecCuoi.Data.Models;
using QuanLyTiecCuoi.Data;

namespace QuanLyTiecCuoi.Repository
{ 
    public class BaoCaoRepository
    {
        private readonly WeddingDbContext _context;

        public BaoCaoRepository(WeddingDbContext context)
        {
            _context = context;
        }

        public List<BAOCAOTHANG> GetBaoCaoThangs(DateTime tuNgay, DateTime denNgay)
        {
            int fromYear = tuNgay.Year;
            int fromMonth = tuNgay.Month;
            int toYear = denNgay.Year;
            int toMonth = denNgay.Month;

            return _context.BaoCaoThangs
                .Where(b =>
                    (b.Nam > fromYear || (b.Nam == fromYear && b.Thang >= fromMonth)) &&
                    (b.Nam < toYear || (b.Nam == toYear && b.Thang <= toMonth))
                )
                .ToList();
        }

        public void AddBaoCaoThang(BAOCAOTHANG baoCaoThang)
        {
            _context.BaoCaoThangs.Add(baoCaoThang);
            _context.SaveChanges();
        }

        // Thêm các hàm sửa, xóa tương tự
    }
}
