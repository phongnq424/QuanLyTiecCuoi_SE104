using QuanLyTiecCuoi.Core;
using QuanLyTiecCuoi.Data.Models;
using QuanLyTiecCuoi.MVVM.View.TuyChinh;
using QuanLyTiecCuoi.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Xceed.Wpf.Toolkit.PropertyGrid.Attributes;

namespace QuanLyTiecCuoi.MVVM.ViewModel
{
    public class NhanVienViewModel : BaseViewModel
    {
        private readonly NhanVienService _nhanVienService;


        private String _MatKhauMoi = "";
        public  String MatKhauMoi
        {
            get => _MatKhauMoi; set {  _MatKhauMoi = value; OnPropertyChanged(); }  
        }

        private String _TenDangNhapMoi = "";
        public String TenDangNhapMoi
        {
            get => _TenDangNhapMoi; set { _TenDangNhapMoi = value; OnPropertyChanged(); }
        }

        private int _MaNhomCuaNguoiDungMoi;

        private List<String> _DanhSachTenNhom;
        public List<String> DanhSachTenNhom
        {
            get => _DanhSachTenNhom; set { _DanhSachTenNhom = value; OnPropertyChanged(); }
        }
        private String _NhomNguoiDungMoi = null;
        public String NhomNguoiDungMoi
        {
            get => _NhomNguoiDungMoi; set { _NhomNguoiDungMoi = value; OnPropertyChanged(); }
        }

        private ObservableCollection<NHOMVAPHANQUYEN> _DanhSachNhomNguoiDung;
        public ObservableCollection<NHOMVAPHANQUYEN> DanhSachNhomNguoiDung
        {
            get => _DanhSachNhomNguoiDung;
            set { _DanhSachNhomNguoiDung = value; OnPropertyChanged(); }
        }

        private ObservableCollection<NGUOIDUNG> _DanhSachNguoiDung;
        public ObservableCollection<NGUOIDUNG> DanhSachNguoiDung
        {
            get => _DanhSachNguoiDung; set { _DanhSachNguoiDung= value; OnPropertyChanged(); }
        }
        private NGUOIDUNG _NguoiDungDuocChon;
        public NGUOIDUNG NguoiDungDuocChon
        {
            get => _NguoiDungDuocChon; set { _NguoiDungDuocChon = value; OnPropertyChanged(); } 
        }

        private String _TenDangNhapDuocChinhSua;
        public String TenDangNhapDuocChinhSua
        {
            get => _TenDangNhapDuocChinhSua; set { _TenDangNhapDuocChinhSua = value; OnPropertyChanged(); }
        }


        private Visibility _ChinhSuaVisibility = Visibility.Hidden;
        public Visibility ChinhSuaVisibility
        {
            get => _ChinhSuaVisibility; set {  _ChinhSuaVisibility = value; OnPropertyChanged(); }
        }
        private String _NhomDuocChinhSua;
        public String NhomDuocChinhSua
        {
            get => _NhomDuocChinhSua;
            set { _NhomDuocChinhSua = value; OnPropertyChanged(); }
        }

        private NHOMNGUOIDUNG? NhomNguoiDungDuocChon;

        private String _TenNhomDuocChinhSua;
        public String TenNhomDuocChinhSua
        {
            get => _TenNhomDuocChinhSua; set { _TenNhomDuocChinhSua = value; OnPropertyChanged(); }    
        }

        private ObservableCollection<CHUCNANG> _DanhSachChucNang;
        public ObservableCollection<CHUCNANG> DanhSachChucNang
        {
            get => _DanhSachChucNang; set { _DanhSachChucNang = value; OnPropertyChanged(); }   
        }
        private List<String> _DanhSachTenChucNang;
        public List<String> DanhSachTenChucNang
        {
            get => _DanhSachTenChucNang; set { _DanhSachTenChucNang = value; OnPropertyChanged(); }
        }

        private String _TenNhomMoi = "";
        public String TenNhomMoi
        {
            get => _TenNhomMoi; set {  _TenNhomMoi = value; OnPropertyChanged(); }
        }

        #region Command
        public ICommand FirstLoadCommand { get; set; }
        public ICommand ThemNguoiDungCommand { get; set; }
        public ICommand ChinhSuaNguoiDungCommand { get; set; }
        public ICommand ChonNhanVienCommand { get; set; }
        public ICommand XoaNguoiDungCommand { get; set; }
        public ICommand ChonNhomCommand {  get; set; }
        public ICommand XoaPhanQuyenCommand { get; set; }
        public ICommand ThemNhomCommand { get; set; }
        public ICommand ChinhSuaNhomCommand { get; set; }
        public ICommand XoaNhomCommand { get; set; }    
        public ICommand ChuyenTabNhanVienCommand { get; set; }    
        #endregion


        public NhanVienViewModel(NhanVienService nhanVienService)
        {
            _nhanVienService = nhanVienService;

            FirstLoadCommand = new RelayCommand<Page>((p) => { return true; }, async (p) =>
            {
                var res = await _nhanVienService.GetNguoiDung();
                if(res != null)
                {
                    DanhSachNguoiDung = new ObservableCollection<NGUOIDUNG>(res);
                }
                var nhom = await _nhanVienService.GetNhomNguoiDungVaPhanQuyen();
                if (nhom != null)
                {
                    DanhSachNhomNguoiDung = new ObservableCollection<NHOMVAPHANQUYEN>(
                        nhom.Select(pair => new NHOMVAPHANQUYEN
                        {
                            Nhom = pair.Key,
                            PhanQuyen = new ObservableCollection<PHANQUYEN>(pair.Value)
                        })
                    );
                    GanDanhSachTenNhom();
                }
                var chucnang = await _nhanVienService.LayDanhSachChucNang();
                if (chucnang != null) {
                    DanhSachChucNang = new ObservableCollection<CHUCNANG>(chucnang);
                    GanDanhSachTenChucNang();
                }
            });


            ThemNguoiDungCommand = new RelayCommand<String?>((p) => { return true; }, async (p) => {
                _MaNhomCuaNguoiDungMoi = LayMaNhom(p);
                var res = await DieuKienThem();
                if (!res) return;

                NGUOIDUNG nguoidung = new NGUOIDUNG()
                {
                    TenDangNhap = TenDangNhapMoi,
                    MatKhau = MatKhauMoi,
                    MaNhom = _MaNhomCuaNguoiDungMoi,
                };

                NGUOIDUNG? user = await _nhanVienService.TaoNguoiDungMoi(nguoidung);
                if (user == null)
                {
                    MessageBox.Show("Lỗi kết nối database");
                }
                else
                {
                    MatKhauMoi = "";
                    TenDangNhapMoi = "";
                    _MaNhomCuaNguoiDungMoi = 0;
                    await ReloadNguoiDung();
                }
            });

            ChonNhanVienCommand = new RelayCommand<NGUOIDUNG>((p) => { return true; }, (p) => {
                NguoiDungDuocChon = p;
                DatChinhSua();
            });

            XoaNguoiDungCommand = new RelayCommand<NGUOIDUNG>((p) => { return true; }, async (p) =>
            {
                if (p == null) return;
                var result = MessageBox.Show(
                    $"Bạn có chắc chắn muốn xóa {p.TenDangNhap} không?",
                    "Xác nhận",
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Question
                );

                if (result == MessageBoxResult.Yes)
                {
                    var res = await _nhanVienService.XoaNguoiDung(p);
                    if(!res)
                    {
                        MessageBox.Show("Có lỗi khi xóa người dùng");
                        return;
                    }

                    await ReloadNguoiDung();
                }

            });

            ChinhSuaNguoiDungCommand = new RelayCommand<String>((p) => { return true; }, async (p) => {
                if (NguoiDungDuocChon.NHOMNGUOIDUNG.TenNhom == NhomDuocChinhSua)
                {
                    MessageBox.Show("Không có thông tin thay đổi");
                    return;
                }
                NguoiDungDuocChon.MaNhom = LayMaNhom(NhomDuocChinhSua);

                var user = await _nhanVienService.ChinhSuaNguoiDung(NguoiDungDuocChon);
                if(user == null)
                {
                    MessageBox.Show("Có lỗi khi xóa người dùng");
                    return;
                }

                await ReloadNguoiDung();
                ReloadChinhSua();

            });


            XoaPhanQuyenCommand = new RelayCommand<PHANQUYEN>((p) => { return true; }, async (p) => {
                if (p == null) return;
                PHANQUYEN? res = await _nhanVienService.XoaPhanQuyen(p);
                if (res == null)
                {
                    MessageBox.Show("Có lỗi xảy ra");
                    return;
                }
                XoaPhanQuyenUI(res);
            });

            ChonNhomCommand = new RelayCommand<NHOMVAPHANQUYEN?>((p) => { return true; }, (p) =>
            {
                if (p == null) return;
                NhomNguoiDungDuocChon = p.Nhom;
                TenNhomDuocChinhSua = NhomNguoiDungDuocChon.TenNhom;
            });

            ThemNhomCommand = new RelayCommand<String?>((p) => { return true; }, async (p) =>
            {
                if (p == null || p.Length == 0) return;
                var nhom = await _nhanVienService.LayNhomNguoiDungTheoTen(p);
                if (nhom != null)
                {
                    MessageBox.Show("Đã tồn tại nhóm người dùng có tên này");
                    return;
                }
                NHOMNGUOIDUNG nhomMoi = new NHOMNGUOIDUNG()
                {
                    TenNhom = p
                };
                AutoCloseMessageBox.ShowWaiting(2);
                var res = await _nhanVienService.TaoNhomNguoiDungMoi(nhomMoi);
                if(res == null)
                {
                    MessageBox.Show("Có lỗi xảy ra");
                    return;
                }
                await ReloadNhom();
                MessageBox.Show("Thêm nhóm người dùng thành công");
                TenNhomMoi = "";
                return;

            });

            ChinhSuaNhomCommand = new RelayCommand<String?>((p) => { return true; }, async (p) =>
            {
                if(NhomNguoiDungDuocChon == null)
                {
                    return;
                }
                if(p != null)
                {
                    await ThemPhanQuyen(p);
                }
                else
                {
                    DoiTenNhom();
                }
            });

            XoaNhomCommand = new RelayCommand<NHOMVAPHANQUYEN>((p) => { return true; }, async (p) =>
            {

                if (p == null) return;
                var user = await _nhanVienService.CoTonTaiNguoiDungThuocNhom(p.Nhom);
                if(user != null)
                {
                    MessageBox.Show("Không thể xóa nhóm vì có người dùng thuộc nhóm");
                    return;
                }
                var result = MessageBox.Show(
                    $"Bạn có chắc chắn muốn xóa nhóm {p.Nhom.TenNhom} không?",
                    "Xác nhận",                      
                    MessageBoxButton.YesNo,         
                    MessageBoxImage.Question         
                );

                if (result == MessageBoxResult.Yes)
                {
                    AutoCloseMessageBox.ShowWaiting(2);
                    var res = await _nhanVienService.XoaNhom(p.Nhom);
                    
                    if (res == true)
                    {
                        MessageBox.Show("Xóa nhóm thành công");
                        await ReloadNhom();
                    }
                    else
                    {
                        MessageBox.Show("Xóa nhóm không thành công.");
                    }
                    return;
                }
                
            });

        }

        private void XoaPhanQuyenUI(PHANQUYEN? res)
        {
            if (res == null) return;
            foreach(var i in DanhSachNhomNguoiDung)
            {
                if(i.Nhom.MaNhom == res.MaNhom)
                {
                    i.PhanQuyen.Remove(res);
                    OnPropertyChanged(nameof(DanhSachNhomNguoiDung));
                    return;
                }
            }
        }

        private async void DoiTenNhom()
        {
            if (NhomNguoiDungDuocChon == null) return;
            if(TenNhomDuocChinhSua == null || TenNhomDuocChinhSua.Length == 0)
            {
                MessageBox.Show("Tên nhóm không hợp lệ");
                return;
            }
            if(NhomNguoiDungDuocChon.TenNhom == TenNhomDuocChinhSua)
            {
                MessageBox.Show("Tên nhóm không thay đổi");
                return;
            }

            NhomNguoiDungDuocChon.TenNhom = TenNhomDuocChinhSua;
            var nhom = await _nhanVienService.ChinhSuaTenNhom(NhomNguoiDungDuocChon);
            if(nhom == null)
            {
                MessageBox.Show("Có lỗi xảy ra");
                return;
            }
            await ReloadNhom();
            MessageBox.Show("Đổi tên nhóm thành công.");
            return;
        }

        private void UpdateTenNhom(NHOMNGUOIDUNG nhom)
        {
            foreach (var item in DanhSachNhomNguoiDung)
            {
                if (item.Nhom.MaNhom == nhom.MaNhom)
                {
                    item.Nhom = new NHOMNGUOIDUNG
                    {
                        MaNhom = nhom.MaNhom,
                        TenNhom = nhom.TenNhom
                    };

                    OnPropertyChanged(nameof(DanhSachNhomNguoiDung));
                    return;
                }
            }

        }

        private async Task ThemPhanQuyen(string tencn)
        {
            if (NhomNguoiDungDuocChon == null) return;
            var phanquyen = await _nhanVienService.LayPhanQuyenTheoTenChucNangVaNhom(tencn, NhomNguoiDungDuocChon);
            if(phanquyen != null)
            {
                MessageBox.Show("Phân quyền đã tồn tại");
                return;
            }
            var maCN = LayMaCN(tencn);
            if (maCN == 0)
            {
                MessageBox.Show("Có lỗi xảy ra");
                return;
            }
            PHANQUYEN pq = new PHANQUYEN()
            {
                MaNhom = NhomNguoiDungDuocChon.MaNhom,
                MaChucNang = maCN,
            };
            var res = await _nhanVienService.TaoPhanQuyen(pq);
            if(res == null)
            {
                MessageBox.Show("Có lỗi xảy ra");
                return;
            }
            ThemPhanQuyenUI(res);

        }

        private void ThemPhanQuyenUI(PHANQUYEN res)
        {
            foreach (var i in DanhSachNhomNguoiDung)
            {
                if (i.Nhom.MaNhom == res.MaNhom)
                {
                    i.PhanQuyen.Add(res);
                    OnPropertyChanged(nameof(DanhSachNhomNguoiDung));
                    return;
                }
            }
        }

        private int LayMaCN(string tencn)
        {
            foreach(var i in DanhSachChucNang)
            {
                if(i.TenChucNang.Equals(tencn))
                    { return i.MaChucNang; }
            }
            return 0;
        }

        private void GanDanhSachTenChucNang()
        {
            DanhSachTenChucNang = new List<string>();
            foreach(var i in DanhSachChucNang)
            {
                DanhSachTenChucNang.Add(i.TenChucNang);
            }
        }

        private void ReloadChinhSua()
        {
            NhomDuocChinhSua = "";
            TenDangNhapDuocChinhSua = "";
        }

        private void DatChinhSua()
        {
            if(NguoiDungDuocChon == null) return;
            ChinhSuaVisibility = Visibility.Visible;

            TenDangNhapDuocChinhSua = NguoiDungDuocChon.TenDangNhap;
            NhomDuocChinhSua = NguoiDungDuocChon.NHOMNGUOIDUNG.TenNhom;
        }

        private async Task ReloadNguoiDung()
        {
            var res = await _nhanVienService.GetNguoiDung();
            if (res != null)
            {
                DanhSachNguoiDung = new ObservableCollection<NGUOIDUNG>(res);
            }
        }

        private async Task ReloadNhom()
        {
            var nhom = await _nhanVienService.GetNhomNguoiDungVaPhanQuyen();
            if (nhom != null)
            {
                DanhSachNhomNguoiDung = new ObservableCollection<NHOMVAPHANQUYEN>(
                    nhom.Select(pair => new NHOMVAPHANQUYEN
                    {
                        Nhom = pair.Key,
                        PhanQuyen = new ObservableCollection<PHANQUYEN>(pair.Value)
                    })
                );
                GanDanhSachTenNhom();
            }
        }

        private int LayMaNhom(string? tenNhom)
        {
            if (tenNhom == null) return 0;
            foreach(var i in DanhSachNhomNguoiDung)
            {
                if (i.Nhom.TenNhom.Equals(tenNhom))
                    return i.Nhom.MaNhom;
            }
            return 0;
        }

        private async Task<bool> DieuKienThem()
        {
            if(!DieuKienTenDangNhap(TenDangNhapMoi))
            {
                return false;
            }
            if(!DieuKienMatKhau(MatKhauMoi))
            {
                return false;
            }
            if(_MaNhomCuaNguoiDungMoi == 0)
            {
                MessageBox.Show("Sai mã nhóm");
                return false;
            }
            var user = await _nhanVienService.LayNguoiDungTheoTenDN(TenDangNhapMoi);

            if (user != null)
            {
                MessageBox.Show("Tên đăng nhập đã tồn tại");
                return false;
            }
            return true;
        }

        private bool DieuKienMatKhau(String MatKhau)
        {
            if (string.IsNullOrWhiteSpace(MatKhau))
            {
                MessageBox.Show("Mật khẩu không được để trống.");
                return false;
            }

            if (MatKhau.Length < 6)
            {
                MessageBox.Show("Mật khẩu phải có ít nhất 6 ký tự.");
                return false;
            }

            if (!Regex.IsMatch(MatKhau, @"[A-Z]"))
            {
                MessageBox.Show("Mật khẩu phải chứa ít nhất một chữ hoa.");
                return false;
            }

            if (!Regex.IsMatch(MatKhau, @"[a-z]"))
            {
                MessageBox.Show("Mật khẩu phải chứa ít nhất một chữ thường.");
                return false;
            }

            if (!Regex.IsMatch(MatKhau, @"\d"))
            {
                MessageBox.Show("Mật khẩu phải chứa ít nhất một số.");
                return false;
            }

            return true;
        }

        private bool DieuKienTenDangNhap(String TenDangNhap)
        {
            if (string.IsNullOrWhiteSpace(TenDangNhap))
            {
                MessageBox.Show("Tên đăng nhập không được để trống.");
                return false;
            }

            if (TenDangNhap.Length < 5 || TenDangNhap.Length > 20)
            {
                MessageBox.Show("Tên đăng nhập phải có độ dài từ 5 đến 20 ký tự.");
                return false;
            }

            if (!Regex.IsMatch(TenDangNhap, @"^[a-zA-Z0-9]+$"))
            {
                MessageBox.Show("Tên đăng nhập chỉ được chứa chữ cái và số, không có ký tự đặc biệt.");
                return false;
            }

            return true;
        }

        private void GanDanhSachTenNhom()
        {
            List<String> dstennhom = new List<string>();
            foreach(var i in DanhSachNhomNguoiDung)
            {
                dstennhom.Add(i.Nhom.TenNhom);
            }
            DanhSachTenNhom = dstennhom;
            OnPropertyChanged(nameof(DanhSachTenNhom));
        }
    }

    public class NHOMVAPHANQUYEN : INotifyPropertyChanged
    {
        private NHOMNGUOIDUNG _nhom;
        public NHOMNGUOIDUNG Nhom
        {
            get => _nhom;
            set
            {
                _nhom = value;
                OnPropertyChanged(nameof(Nhom));
            }
        }

        private ObservableCollection<PHANQUYEN> _phanQuyen;
        public ObservableCollection<PHANQUYEN> PhanQuyen
        {
            get => _phanQuyen;
            set
            {
                _phanQuyen = value;
                OnPropertyChanged(nameof(PhanQuyen));
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
