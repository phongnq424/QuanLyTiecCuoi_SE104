using QuanLyTiecCuoi.MVVM.Model;
using QuanLyTiecCuoi.Repository;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyTiecCuoi.Services
{
    public class SanhService
    {
        private readonly SanhRepository _sanhRepo;

        public SanhService(SanhRepository sanhRepo) 
        {
            _sanhRepo = sanhRepo;
        }

        public bool IsSanhDangDuocSuDung(int maSanh)
        {
            return _sanhRepo.IsUsedByAnyDatTiec(maSanh);
        }

        // Lấy tất cả các Sảnh
        public ObservableCollection<Sanh> GetAllSanh()
        {
            var sanhs = _sanhRepo.GetAll();

            return new ObservableCollection<Sanh>(
                sanhs.Select(s => new Sanh
                {
                    MaSanh = s.MaSanh,
                    TenSanh = s.TenSanh,
                    MaLoaiSanh = s.MaLoaiSanh,
                    SoLuongBanToiDa = s.SoLuongBanToiDa,
                    GhiChu = s.GhiChu,
                    HinhAnh = s.HinhAnh,
                    LoaiSanh = s.LoaiSanh != null ? new LoaiSanh
                    {
                        MaLoaiSanh = s.LoaiSanh.MaLoaiSanh,
                        TenLoaiSanh = s.LoaiSanh.TenLoaiSanh,
                        DonGiaBanToiThieu = s.LoaiSanh.DonGiaBanToiThieu
                    } : null
                })
            );
        }

        // Lấy Sảnh theo ID
        public Sanh GetSanhById(int maSanh)
        {
            var s = _sanhRepo.GetById(maSanh);
            if (s == null) return null;

            return new Sanh
            {
                MaSanh = s.MaSanh,
                TenSanh = s.TenSanh,
                MaLoaiSanh = s.MaLoaiSanh,
                SoLuongBanToiDa = s.SoLuongBanToiDa,
                GhiChu = s.GhiChu,
                HinhAnh = s.HinhAnh,
                LoaiSanh = s.LoaiSanh != null ? new LoaiSanh
                {
                    MaLoaiSanh = s.LoaiSanh.MaLoaiSanh,
                    TenLoaiSanh = s.LoaiSanh.TenLoaiSanh,
                    DonGiaBanToiThieu = s.LoaiSanh.DonGiaBanToiThieu
                } : null
            };
        }

        // Thêm Sảnh mới
        public void AddSanh(Sanh sanh)
        {
            var entity = new Data.Models.SANH
            {
                TenSanh = sanh.TenSanh,
                MaLoaiSanh = sanh.MaLoaiSanh,
                SoLuongBanToiDa = sanh.SoLuongBanToiDa ?? 0,
                GhiChu = sanh.GhiChu,
                HinhAnh = sanh.HinhAnh
            };
            _sanhRepo.AddSanh(entity);
        }

        // Chỉnh sửa Sảnh
        public void EditSanh(Sanh sanh)
        {
            var entity = _sanhRepo.GetById(sanh.MaSanh);
            if (entity != null)
            {
                entity.TenSanh = sanh.TenSanh;
                entity.MaLoaiSanh = sanh.MaLoaiSanh;
                entity.SoLuongBanToiDa = sanh.SoLuongBanToiDa ?? 0;
                entity.GhiChu = sanh.GhiChu;
                entity.HinhAnh = sanh.HinhAnh;
                _sanhRepo.UpdateSanh(entity);
            }
        }

        // Xóa một Sảnh
        public void DeleteSanh(Sanh sanh)
        {
            _sanhRepo.DeleteSanh(sanh.MaSanh);
        }
    }
}
