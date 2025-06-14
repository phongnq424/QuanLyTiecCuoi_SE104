using QuanLyTiecCuoi.Data.Models;
using QuanLyTiecCuoi.MVVM.Model;
using QuanLyTiecCuoi.Repository;
using System;
using System.Collections.Generic;
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

        public List<BaoCaoModel> GetBaoCaoTheoBoLoc(
            DateTime? tuNgay = null,
            DateTime? denNgay = null,
            decimal? minDoanhThu = null,
            decimal? maxDoanhThu = null,
            int? minTiecCuoi = null,
            int? maxTiecCuoi = null)
        {
            var baoCaoThangs = _baoCaoRepo.GetByFilters(tuNgay, denNgay, minDoanhThu, maxDoanhThu, minTiecCuoi, maxTiecCuoi);

            var result = new List<BaoCaoModel>();

            foreach (var bc in baoCaoThangs)
            {
                result.Add(new BaoCaoModel
                {
                    Thang = bc.Thang,
                    Nam = bc.Nam,
                    TongDoanhThu = bc.TongDoanhThu,
                    TongTiecCuoi = bc.TongTiecCuoi,
                    ChiTietBaoCao = new List<ChiTietBaoCaoModel>() // Tuỳ bạn có muốn điền chi tiết không
                });
            }

            return result;
        }

        public void ThemBaoCao(BAOCAOTHANG baoCaoThang)
        {
            _baoCaoRepo.Add(baoCaoThang);
        }

        public void CapNhatBaoCao(BAOCAOTHANG baoCaoThang)
        {
            _baoCaoRepo.Update(baoCaoThang);
        }

        public void XoaBaoCao(BAOCAOTHANG baoCaoThang)
        {
            _baoCaoRepo.Delete(baoCaoThang);
        }

        public List<(int Thang, int Nam)> GetAvailableMonthsAndYears()
        {
            var data = _baoCaoRepo.GetAll();

            return data
                .Select(x => new { x.Thang, x.Nam })
                .Distinct()
                .OrderBy(x => x.Nam)
                .ThenBy(x => x.Thang)
                .Select(x => (x.Thang, x.Nam))
                .ToList();
        }
        public bool CoHoaDonTrongThangNam(int thang, int nam)
        {
            return _baoCaoRepo.CoHoaDonTrongThangNam(thang, nam);
        }




    }
}
