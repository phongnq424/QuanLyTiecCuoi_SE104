using QuanLyTiecCuoi.Data.Models;
using QuanLyTiecCuoi.Repository;
using System.Collections.Generic;

namespace QuanLyTiecCuoi.Services
{
    public class ChiTietDichVuService
    {
        private readonly ChiTietDichVuRepository _repository;

        private readonly DichVuRepository _dichVuRepository;

        public ChiTietDichVuService(ChiTietDichVuRepository repository, DichVuRepository dichVuRepository)
        {
            _repository = repository;
            _dichVuRepository = dichVuRepository;
        }
        public List<DICHVU> LayDanhSachDichVuTheoDatTiec(int maDatTiec)
        {
            var chiTietDichVus = _repository.GetByMaDatTiec(maDatTiec);
            var danhSachDichVu = new List<DICHVU>();

            foreach (var ct in chiTietDichVus)
            {
                var dv = _dichVuRepository.GetById(ct.MaDichVu);
                if (dv != null)
                {
                    dv.SoLuong = ct.SoLuong;   // ✅ Gán lại số lượng từ CHITIETDVTIEC
                    dv.DonGia = ct.DonGia;     // ✅ Gán lại đơn giá nếu khác đơn giá gốc
                    danhSachDichVu.Add(dv);
                }
            }

            return danhSachDichVu;
        }

        public void ThemChiTiet(CHITIETDVTIEC chiTiet)
        {
            _repository.Add(chiTiet);
        }

        public void ThemNhieuChiTiet(List<CHITIETDVTIEC> danhSach)
        {
            _repository.AddRange(danhSach);
        }

        public List<CHITIETDVTIEC> LayTheoMaDatTiec(int maDatTiec)
        {
            return _repository.GetByMaDatTiec(maDatTiec);
        }

        public void XoaTheoMaDatTiec(int maDatTiec)
        {
            _repository.DeleteByMaDatTiec(maDatTiec);
        }
        public void CapNhatChiTietDichVu(int maDatTiec, List<CHITIETDVTIEC> danhSachMoi)
        {
            _repository.CapNhatChiTietDichVu(maDatTiec, danhSachMoi);
        }
    }
}
