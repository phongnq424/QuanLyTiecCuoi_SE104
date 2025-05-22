using QuanLyTiecCuoi.Core;
using QuanLyTiecCuoi.Data.Models;
using QuanLyTiecCuoi.Data.Services;
using QuanLyTiecCuoi.MVVM.View.HoaDon;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace QuanLyTiecCuoi.MVVM.ViewModel
{
    internal class HoaDonViewModel : BaseViewModel
    {
        private ObservableCollection<HOADON> _DanhSachHoaDon;
        public ObservableCollection<HOADON> DanhSachHoaDon
        {
            get => _DanhSachHoaDon;
            set { _DanhSachHoaDon = value; OnPropertyChanged(); }
        }

        private ObservableCollection<HOADON> DanhSachHoaDonDayDu;

        private bool _UseDateFilter = false;
        public bool UseDateFilter
        {
            get => _UseDateFilter;
            set { _UseDateFilter = value; OnPropertyChanged(); }
        }

        private DateTime _LocNgayThanhToan = DateTime.Today;
        public DateTime LocNgayThanhToan
        {
            get => _LocNgayThanhToan;
            set { _LocNgayThanhToan = value; OnPropertyChanged(); }
        }

        private string _LocMaDatTiec;
        public string LocMaDatTiec
        {
            get => _LocMaDatTiec; set { _LocMaDatTiec = value; OnPropertyChanged(); }
        }

        private HOADON _HoaDonDuocChon;
        public HOADON HoaDonDuocChon
        {
            get => _HoaDonDuocChon;
            set { _HoaDonDuocChon = value; OnPropertyChanged(); }
        }

        #region Command
        public ICommand FirstLoadCommand {  get; set; }
        public ICommand SetDateFilterCommand { get; set; }
        public ICommand LocHoaDonCommand { get; set; }
        public ICommand ChonHoaDonCommand { get; set; }
        public ICommand LuuThayDoiCommand { get; set; }
        public ICommand InHoaDonCommand { get; set; }
        public ICommand ThanhToanCommand { get; set; }

        #endregion

        public HoaDonViewModel()
        {
            FirstLoadCommand = new RelayCommand<Page>((p) => { return true; }, async (p) =>
            {
                List<HOADON> res = await HoaDonService.Ins.GetAll();
                if (res != null)
                {
                    DanhSachHoaDon = new ObservableCollection<HOADON>(res);
                    DanhSachHoaDonDayDu = DanhSachHoaDon;
                }
            });

            SetDateFilterCommand = new RelayCommand<DatePicker>((p) => { return true; }, async (p) =>
            {
                p.Visibility = UseDateFilter ? System.Windows.Visibility.Visible : System.Windows.Visibility.Hidden;
            });

            LocHoaDonCommand = new RelayCommand<DatePicker>((p) => { return true; }, async (p) =>
            {
                if (DanhSachHoaDonDayDu != null)
                    LocHoaDon();
            });

            ChonHoaDonCommand = new RelayCommand<Object>((p) => { return true; }, async (p) =>
            {
                if(HoaDonDuocChon != null)
                {
                    ChiTietHoaDonWindow wd = new ChiTietHoaDonWindow();
                    wd.ShowDialog();
                }
            });


        }

        private void LocHoaDon()
        {
            DanhSachHoaDon = new ObservableCollection<HOADON>();
            if (UseDateFilter && LocMaDatTiec.Length != 0)
            {
                foreach (HOADON hd in DanhSachHoaDonDayDu)
                {
                    if (hd.MaDatTiec.ToString().Contains(LocMaDatTiec) && hd.NgayThanhToan.Date == LocNgayThanhToan.Date)
                    {
                        DanhSachHoaDon.Add(hd);
                    }
                }
            }
            else if (UseDateFilter)
            {
                foreach (HOADON hd in DanhSachHoaDonDayDu)
                {
                    if (hd.NgayThanhToan.Date == LocNgayThanhToan.Date)
                    {
                        DanhSachHoaDon.Add(hd);
                    }
                }
            }
            else if (LocMaDatTiec.Length != 0)
            {
                foreach (HOADON hd in DanhSachHoaDonDayDu)
                {
                    if (hd.MaDatTiec.ToString().Contains(LocMaDatTiec))
                    {
                        DanhSachHoaDon.Add(hd);
                    }
                }

            }
        }
    }
}
