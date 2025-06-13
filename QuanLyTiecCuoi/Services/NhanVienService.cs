using Microsoft.EntityFrameworkCore;
using QuanLyTiecCuoi.Data.Models;
using QuanLyTiecCuoi.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyTiecCuoi.Data.Services
{
    public class NhanVienService
    {
        private readonly NhanVienRepository _nhanvienRepository;

        public NhanVienService(NhanVienRepository NhanVienRepo)
        {
            _nhanvienRepository = NhanVienRepo;
        }

        public async Task<List<NGUOIDUNG>> GetNguoiDung()
        {
            return await _nhanvienRepository.GetAllNguoiDungAsync();
        }

        public async Task<List<NHOMNGUOIDUNG>> GetNhomNguoiDungKhongDayDu()
        {
            return await _nhanvienRepository.GetNhomNguoiDung();
        }

        public async Task<List<KeyValuePair<NHOMNGUOIDUNG, List<PHANQUYEN>>>> GetNhomNguoiDungVaPhanQuyen()
        {
            List<KeyValuePair<NHOMNGUOIDUNG, List<PHANQUYEN>>> res = new List<KeyValuePair<NHOMNGUOIDUNG, List<PHANQUYEN>>>();
            var DanhSachNhom = await _nhanvienRepository.GetNhomNguoiDung();
            foreach(var i in DanhSachNhom)
            {
                var DanhSachPhanQuyen = await _nhanvienRepository.LayPhanQuyenTheoNhomNguoiDung(i);
                res.Add(new KeyValuePair<NHOMNGUOIDUNG, List<PHANQUYEN>>(i, DanhSachPhanQuyen));
            }
            return res;
        }

        public async Task<NGUOIDUNG?> TaoNguoiDungMoi(NGUOIDUNG nd)
        {
            if (nd == null) return null;
            return await _nhanvienRepository.AddUserAsync(nd);
        }

        public async Task<NGUOIDUNG?> ChinhSuaNguoiDung(NGUOIDUNG nd)
        {
            if (nd == null) return null;
            return await _nhanvienRepository.UpdateUserAsync(nd);
        }

        public async Task<bool> XoaNguoiDung(NGUOIDUNG nd)
        {
            if (nd == null) return false;
            return await _nhanvienRepository.DeleteAsync(nd);
        }

        public async Task<NHOMNGUOIDUNG> TaoNhomNguoiDungMoi(NHOMNGUOIDUNG nhomNguoiDung)
        {
            return await _nhanvienRepository.AddNhomNguoiDungAsync(nhomNguoiDung.TenNhom);
        }

        public async Task<NGUOIDUNG?> LayNguoiDungTheoTenDN(String tendn)
        {
            return await _nhanvienRepository.GetByTenDangNhapAsync(tendn);
        }

        public async Task<PHANQUYEN?> XoaPhanQuyen(PHANQUYEN pq)
        {
            return await _nhanvienRepository.XoaChucNangKhoiNhomAsync(pq);
        }

        public async Task<NHOMNGUOIDUNG?> LayNhomNguoiDungTheoTen(String tenNhom)

        {
            return await _nhanvienRepository.LayNhomTheoTenNhom(tenNhom);
        }

        public async Task<List<CHUCNANG>> LayDanhSachChucNang()
        {
            return await _nhanvienRepository.LayTatCaChucNangAsync();
        }

        public async Task<PHANQUYEN?> LayPhanQuyenTheoTenChucNangVaNhom(String tencn, NHOMNGUOIDUNG nhom)
        {
            return await _nhanvienRepository.LayPhanQuyenTenCNNhom(tencn, nhom);
        }

        public async Task<PHANQUYEN> TaoPhanQuyen(PHANQUYEN pq)
        {
            return await _nhanvienRepository.TaoPhanQuyen(pq);
        }

        public async Task<NHOMNGUOIDUNG> ChinhSuaTenNhom(NHOMNGUOIDUNG nhom)
        {
            return await _nhanvienRepository.ChinhSuaNhom(nhom);
        }

        public async Task<NGUOIDUNG?> CoTonTaiNguoiDungThuocNhom(NHOMNGUOIDUNG nhom)
        {
            return await _nhanvienRepository.CoTonTaiNguoiDungThuocNhom(nhom);
        }

        public async Task<bool> XoaNhom(NHOMNGUOIDUNG nhom)
        {
            //nhom ko co nguoi dung nao
            return await _nhanvienRepository.XoaNhom(nhom);
        }
    }
}
