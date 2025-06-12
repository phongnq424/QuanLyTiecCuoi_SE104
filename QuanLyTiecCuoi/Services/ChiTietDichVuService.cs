using QuanLyTiecCuoi.Data.Models;
using QuanLyTiecCuoi.Repositories;
using System.Collections.Generic;

namespace QuanLyTiecCuoi.Services
{
    public class ChiTietDichVuService
    {
        private readonly ChiTietDichVuRepository _repository;

        public ChiTietDichVuService(ChiTietDichVuRepository repository)
        {
            _repository = repository;
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
    }
}
