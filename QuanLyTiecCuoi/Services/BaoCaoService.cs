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
        private readonly ChiTietBaoCaoRepository _chiTietBaoCaoRepo;
        private readonly HoaDonRepository _hoaDonRepository;

        public BaoCaoService( BaoCaoRepository baoCaoRepo, ChiTietBaoCaoRepository chiTietRepo, HoaDonRepository hoaDonRepository )
        {
            _baoCaoRepo = baoCaoRepo;
            _chiTietBaoCaoRepo = chiTietRepo;
            _hoaDonRepository = hoaDonRepository; 
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

        public async Task TaoBaoCaoAsync(int thang, int nam)
        {
            var hoaDons = await _hoaDonRepository.GetByThangNamAsync(thang, nam);

            if (hoaDons == null || !hoaDons.Any())
                throw new InvalidOperationException("Không có hóa đơn nào trong tháng/năm này.");

            // Tính tổng
            var tongDoanhThu = hoaDons.Sum(h => h.TongTienHD);
            var tongTiecCuoi = hoaDons.Count();

            // Kiểm tra báo cáo tháng đã tồn tại chưa
            var baoCaoHienTai = _baoCaoRepo.GetByThangNam(thang, nam); // bạn cần hàm này trong repo

            if (baoCaoHienTai != null)
            {
                // Nếu đã có, thì cập nhật lại
                baoCaoHienTai.TongDoanhThu = tongDoanhThu;
                baoCaoHienTai.TongTiecCuoi = tongTiecCuoi;

                _baoCaoRepo.Update(baoCaoHienTai);

                await _chiTietBaoCaoRepo.DeleteByBaoCaoIdAsync(baoCaoHienTai.MaBaoCao);
            }
            else
            {
                // Nếu chưa có thì tạo mới
                baoCaoHienTai = new BAOCAOTHANG
                {
                    Thang = thang,
                    Nam = nam,
                    TongDoanhThu = tongDoanhThu,
                    TongTiecCuoi = tongTiecCuoi
                };
                await _baoCaoRepo.AddAsync(baoCaoHienTai);
            }

            // Tạo lại chi tiết báo cáo
            var chiTietList = hoaDons
                .GroupBy(h => h.NgayThanhToan!.Value.Date)
                .Select(g => new CHITIETBAOCAO
                {
                    MaBaoCao = baoCaoHienTai.MaBaoCao,
                    NgayBaoCao = g.Key,
                    SoLuongTiecCuoi = g.Count(),
                    DoanhThu = g.Sum(x => x.TongTienHD),
                    TyLe = Math.Round((decimal)g.Sum(x => x.TongTienHD) / baoCaoHienTai.TongDoanhThu * 100, 2)
                })
                .ToList();

            await _chiTietBaoCaoRepo.AddRangeAsync(chiTietList);
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
