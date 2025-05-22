using QuanLyTiecCuoi.MVVM.Model;
using QuanLyTiecCuoi.Repository;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace QuanLyTiecCuoi.Services
{
    public class BaoCaoService
    {
        private readonly BaoCaoRepository _baoCaoRepo;

        public BaoCaoService(BaoCaoRepository baoCaoRepo)
        {
            _baoCaoRepo = baoCaoRepo;
        }

        public List<BaoCaoModel> GetBaoCaoTuNgayDenNgay(DateTime tuNgay, DateTime denNgay)
        {
            var baoCaoThangs = _baoCaoRepo.GetBaoCaoThangs(tuNgay, denNgay);

            var result = new List<BaoCaoModel>();

            DateTime current = new DateTime(tuNgay.Year, tuNgay.Month, 1);

            while (current <= denNgay)
            {
                int thang = current.Month;
                int nam = current.Year;

                var baoCaoThang = baoCaoThangs.FirstOrDefault(b => b.Thang == thang && b.Nam == nam);

                var model = new BaoCaoModel
                {
                    Thang = thang,
                    Nam = nam,
                    TongDoanhThu = baoCaoThang?.TongDoanhThu ?? 0,
                    TongTiecCuoi = baoCaoThang?.TongTiecCuoi ?? 0,
                    ChiTietBaoCao = new List<ChiTietBaoCaoModel>()
                };

                // Giả sử có lấy thêm chi tiết từ repository tương tự

                // Tính toán các chỉ số tỉ lệ, format...

                result.Add(model);

                current = current.AddMonths(1);
            }

            return result;
        }
    }

}
