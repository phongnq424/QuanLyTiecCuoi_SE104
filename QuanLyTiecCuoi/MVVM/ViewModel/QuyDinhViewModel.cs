using QuanLyTiecCuoi.Core;
using QuanLyTiecCuoi.Data.Models;
using QuanLyTiecCuoi.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using static System.Net.Mime.MediaTypeNames;

namespace QuanLyTiecCuoi.MVVM.ViewModel
{
    public class QuyDinhViewModel : BaseViewModel
    {
        private readonly ThamSoService _thamSoService;

        private bool _ApDungQDPhat;
        public bool ApDungQDPhat
        {
            get => _ApDungQDPhat; set { _ApDungQDPhat = value; OnPropertyChanged(); }    
        }

        private String _TyLePhatText;
        public String TyLePhatText
        {
            get => _TyLePhatText; set { _TyLePhatText = value; OnPropertyChanged(); }
        }

        private Visibility _BtnSaveVisability = Visibility.Hidden;
        public Visibility BtnSaveVisability
        {
            get => _BtnSaveVisability; set { _BtnSaveVisability = value; OnPropertyChanged(); }
        }

        private String _Error;
        public String Error
        {
            get => _Error; set { _Error = value; OnPropertyChanged(); }
        }

        private THAMSO? _ThamSoHienTai;
        private decimal _TyLePhatMoi;
        public decimal TyLePhatMoi
        {
            get => _TyLePhatMoi; set { _TyLePhatMoi = value; OnPropertyChanged(); }
        }

        private decimal _PhanTramDatCoc;
        public decimal PhanTramDatCoc
        {
            get => _PhanTramDatCoc; set { _PhanTramDatCoc = value; OnPropertyChanged(); }
        }

        private String _PhanTramDatCocText;
        public String PhanTramDatCocText
        {
            get => _PhanTramDatCocText; set { _PhanTramDatCocText = value; OnPropertyChanged(); }
        }

        private String _SoNgayTreToiDaText;
        public string SoNgayTreToiDaText
        { get => _SoNgayTreToiDaText; set { _SoNgayTreToiDaText = value; OnPropertyChanged(); } }

        private int SoNgayTreToiDa;
        private bool CoTheLuuPhanTram = true;
        private bool CoTheLuuTyLePhat = true;
        private bool CoTheLuuSoNgayThanhToan = true;


        #region Command
        public ICommand FirstLoadCommand { get; set; }  
        public ICommand ThayDoiTyLePhatCommand { get; set; }
        public ICommand ThayDoiApDungQDPhatCommand { get; set; }
        public ICommand ThayDoiTyLeCocCommand { get; set; }
        public ICommand LuuThayDoiCommand { get; set;}
        public ICommand ThayDoiNgayTreToiDaCocCommand { get; set;}
        #endregion

        public QuyDinhViewModel(ThamSoService thamSoService)
        {
            _thamSoService = thamSoService;
            FirstLoadCommand = new RelayCommand<Page>((p) => { return true; }, async (p) =>
            {
                _ThamSoHienTai = await _thamSoService.LayThamSo();
                if (_ThamSoHienTai != null)
                {
                    ApDungQDPhat = _ThamSoHienTai.ApDungQDPhatThanhToanTre;
                    TyLePhatText = _ThamSoHienTai.TyLePhatThanhToanTreTheoNgay.ToString("0.0000");
                    TyLePhatMoi = _ThamSoHienTai.TyLePhatThanhToanTreTheoNgay;
                    PhanTramDatCoc = _ThamSoHienTai.PhanTramDatCoc;
                    PhanTramDatCocText = PhanTramDatCoc.ToString("0.0000");
                    SoNgayTreToiDa = _ThamSoHienTai.SLNgayThanhToanTreToiDa;
                    SoNgayTreToiDaText = SoNgayTreToiDa.ToString();

                    Error = "";
                    BtnSaveVisability = Visibility.Hidden;
                }
            });

            ThayDoiTyLePhatCommand = new RelayCommand<String?>((p) => { return true; },  (p) =>
            {
                if (_ThamSoHienTai == null) return;
                if (p != null && p.Length != 0)
                {
                    TyLePhatText = p;
                    bool hopLe = decimal.TryParse(TyLePhatText, out _);
                    if (hopLe)
                    {
                        TyLePhatMoi = decimal.Parse(TyLePhatText);
                        BtnSaveVisability = Visibility.Visible;
                        CoTheLuuTyLePhat = true;
                        Error = "";
                    }
                    else
                    { 
                        Error = "Phải nhập vào số thập phân";
                        BtnSaveVisability = Visibility.Visible;
                        CoTheLuuTyLePhat = false;
                        return;
                    }
                }
                if (ApDungQDPhat == _ThamSoHienTai.ApDungQDPhatThanhToanTre && TyLePhatText == _ThamSoHienTai.TyLePhatThanhToanTreTheoNgay.ToString("0.0000")
                    && PhanTramDatCocText == _ThamSoHienTai.PhanTramDatCoc.ToString("0.0000") && SoNgayTreToiDaText == _ThamSoHienTai.SLNgayThanhToanTreToiDa.ToString())
                {
                    return;
                }

                BtnSaveVisability = Visibility.Visible;

            });

            ThayDoiApDungQDPhatCommand = new RelayCommand<object>((p) => { return true; }, (p) => 
            {
                if (ApDungQDPhat == _ThamSoHienTai.ApDungQDPhatThanhToanTre && TyLePhatText == _ThamSoHienTai.TyLePhatThanhToanTreTheoNgay.ToString("0.0000")
                    && PhanTramDatCocText == _ThamSoHienTai.PhanTramDatCoc.ToString("0.0000") && SoNgayTreToiDaText == _ThamSoHienTai.SLNgayThanhToanTreToiDa.ToString())
                {
                    return;
                }

                BtnSaveVisability = Visibility.Visible;
            });

            ThayDoiTyLeCocCommand = new RelayCommand<String?>((p) => { return true; }, (p) =>
            {
                if (_ThamSoHienTai == null) return;
                if (p != null && p.Length != 0)
                {
                    PhanTramDatCocText = p;
                    bool hopLe = decimal.TryParse(PhanTramDatCocText, out _);
                    if (hopLe)
                    {
                        PhanTramDatCoc = decimal.Parse(PhanTramDatCocText);
                        BtnSaveVisability = Visibility.Visible;
                        CoTheLuuPhanTram = true;
                        Error = "";
                    }
                    else
                    {
                        CoTheLuuPhanTram = false;
                        Error = "Phải nhập vào số thập phân";
                        BtnSaveVisability = Visibility.Visible;
                        return;
                    }
                }
                if (ApDungQDPhat == _ThamSoHienTai.ApDungQDPhatThanhToanTre && TyLePhatText == _ThamSoHienTai.TyLePhatThanhToanTreTheoNgay.ToString("0.0000")
                    && PhanTramDatCocText == _ThamSoHienTai.PhanTramDatCoc.ToString("0.0000") && SoNgayTreToiDaText == _ThamSoHienTai.SLNgayThanhToanTreToiDa.ToString())
                {
                    return;
                }

                BtnSaveVisability = Visibility.Visible;

            });

            ThayDoiNgayTreToiDaCocCommand = new RelayCommand<String?>((p) => { return true; }, (p) =>
            {
                if (_ThamSoHienTai == null) return;
                if (p != null && p.Length != 0)
                {
                    SoNgayTreToiDaText = p;
                    int res = -1;
                    bool hopLe = int.TryParse(SoNgayTreToiDaText, out res);
                    if (hopLe && res >= 0)
                    {
                        SoNgayTreToiDa = int.Parse(SoNgayTreToiDaText);
                        BtnSaveVisability = Visibility.Visible;
                        CoTheLuuSoNgayThanhToan = true;
                        Error = "";
                    }
                    else
                    {
                        CoTheLuuSoNgayThanhToan = false;
                        Error = "Phải nhập vào số dương";
                        BtnSaveVisability = Visibility.Visible;
                        return;
                    }
                }
                if (ApDungQDPhat == _ThamSoHienTai.ApDungQDPhatThanhToanTre && TyLePhatText == _ThamSoHienTai.TyLePhatThanhToanTreTheoNgay.ToString("0.0000")
                    && PhanTramDatCocText == _ThamSoHienTai.PhanTramDatCoc.ToString("0.0000") && SoNgayTreToiDaText == _ThamSoHienTai.SLNgayThanhToanTreToiDa.ToString())
                {
                    return;
                }

                BtnSaveVisability = Visibility.Visible;

            });



            LuuThayDoiCommand = new RelayCommand<Page>((p) => { return true; }, async (p) =>
            {
                if(_ThamSoHienTai == null)
                {
                    MessageBox.Show("lỗi tham số null");
                    return;
                }
                if (ApDungQDPhat == _ThamSoHienTai.ApDungQDPhatThanhToanTre && TyLePhatText == _ThamSoHienTai.TyLePhatThanhToanTreTheoNgay.ToString("0.0000")
                    && PhanTramDatCocText == _ThamSoHienTai.PhanTramDatCoc.ToString("0.0000") && SoNgayTreToiDaText == _ThamSoHienTai.SLNgayThanhToanTreToiDa.ToString())
                {
                    Error = "Không có thông tin thay đổi.";
                    return;
                }

                if(CoTheLuuSoNgayThanhToan && CoTheLuuTyLePhat && CoTheLuuPhanTram)
                {

                    var thamsomoi = new THAMSO()
                    {
                        Id = _ThamSoHienTai.Id,
                        TyLePhatThanhToanTreTheoNgay = TyLePhatMoi,
                        ApDungQDPhatThanhToanTre = ApDungQDPhat,
                        PhanTramDatCoc = PhanTramDatCoc,
                        SLNgayThanhToanTreToiDa = SoNgayTreToiDa,
                    };


                    var res = await _thamSoService.CapNhatThamSo(thamsomoi);
                    if(res == null)
                    {
                        Error = "Có lỗi xảy ra";
                        return;
                    }

                    Error = "Thay đổi thành công";
                    BtnSaveVisability = Visibility.Hidden;
                    return;
                }
                else
                {
                    MessageBox.Show("Thay đổi không hợp lệ");

                }
            });
        }

    }
}
