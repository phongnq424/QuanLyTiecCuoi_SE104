using QuanLyTiecCuoi.Data;
using QuanLyTiecCuoi.Data.Models;
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
    public class LoaiSanhService
    {
        private readonly LoaiSanhRepository _loaiSanhRepo;

        public LoaiSanhService(LoaiSanhRepository loaiSanhRepo)
        {
            _loaiSanhRepo = loaiSanhRepo;
        }

        public bool IsLoaiSanhDangDuocSuDung(int maLoaiSanh)
        {
            return _loaiSanhRepo.IsUsedByAnySanh(maLoaiSanh);
        }

        // Lấy tất cả các LoaiSanh từ database
        public ObservableCollection<LoaiSanh> GetAllLoaiSanh()
        {
            var list = _loaiSanhRepo.GetAll();
            return new ObservableCollection<LoaiSanh>(
                list.Select(e => new LoaiSanh
                {
                    MaLoaiSanh = e.MaLoaiSanh,
                    TenLoaiSanh = e.TenLoaiSanh,
                    DonGiaBanToiThieu = e.DonGiaBanToiThieu
                })
            );
        }

        // Lấy Loại Sảnh theo ID
        public LoaiSanh GetLoaiSanhById(int maLoaiSanh)
        {
            var e = _loaiSanhRepo.GetById(maLoaiSanh);
            if (e == null) return null;

            return new LoaiSanh
            {
                MaLoaiSanh = e.MaLoaiSanh,
                TenLoaiSanh = e.TenLoaiSanh,
                DonGiaBanToiThieu = e.DonGiaBanToiThieu
            };
        }

        // Thêm mới Loại Sảnh 
        public void AddLoaiSanh(LoaiSanh loaiSanh)
        {
            var entity = new Data.Models.LOAISANH
            {
                TenLoaiSanh = loaiSanh.TenLoaiSanh,
                DonGiaBanToiThieu = loaiSanh.DonGiaBanToiThieu ?? 0
            };
            _loaiSanhRepo.AddLoaiSanh(entity);

        }

        // Chỉnh sửa Loại Sảnh
        public void EditLoaiSanh(LoaiSanh loaiSanh)
        {
            var entity = _loaiSanhRepo.GetById(loaiSanh.MaLoaiSanh);
            if (entity != null)
            {
                entity.TenLoaiSanh = loaiSanh.TenLoaiSanh;
                entity.DonGiaBanToiThieu = loaiSanh.DonGiaBanToiThieu ?? 0;
                _loaiSanhRepo.UpdateLoaiSanh(entity);
            }
        }

        // Xóa Loại Sảnh
        public void DeleteLoaiSanh(LoaiSanh loaiSanh)
        {
            _loaiSanhRepo.DeleteLoaiSanh(loaiSanh.MaLoaiSanh);
        }
    }
}
