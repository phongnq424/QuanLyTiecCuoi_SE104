using QuanLyTiecCuoi.MVVM.Model;
using QuanLyTiecCuoi.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using QuanLyTiecCuoi.Repository;

namespace QuanLyTiecCuoi.Services
{
    public class ChiTietBaoCaoService
    {
        private readonly ChiTietBaoCaoRepository _chiTietBaoCaoRepo;

        public ChiTietBaoCaoService(ChiTietBaoCaoRepository ctbaoCaoRepo)
        {
            _chiTietBaoCaoRepo = ctbaoCaoRepo;
        }

        public List<ChiTietBaoCaoModel> GetChiTietBaoCaoByMonthYear(int thang, int nam)
        {
            var chiTietBaoCao = _chiTietBaoCaoRepo.GetChiTietBaoCao(thang, nam);
            var result = new List<ChiTietBaoCaoModel>();
            int index = 1;

            decimal tongDoanhThu = chiTietBaoCao.Sum(ct => ct.DoanhThu);
            int tongTiecCuoi = chiTietBaoCao.Sum(ct => ct.SoLuongTiecCuoi);

            foreach (var item in chiTietBaoCao)
            {
                var model = new ChiTietBaoCaoModel
                {
                    STT = index++,
                    NgayBaoCao = item.NgayBaoCao,
                    SoLuongTiecCuoi = item.SoLuongTiecCuoi,
                    DoanhThu = (double)item.DoanhThu,
                    DoanhThuFormatted = item.DoanhThu.ToString("#,0.##") + " VND",
                    TiLeDoanhThu = tongDoanhThu == 0 ? 0 : (double)(item.DoanhThu / tongDoanhThu),
                    TiLeTiecCuoi = tongTiecCuoi == 0 ? 0 : (double)item.SoLuongTiecCuoi / tongTiecCuoi
                };
                result.Add(model);
            }
            return result;
        }
    }
}
