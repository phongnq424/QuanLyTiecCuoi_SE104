using QuanLyTiecCuoi.Core;
using QuanLyTiecCuoi.Data;
using QuanLyTiecCuoi.Data.Models;
using QuanLyTiecCuoi.Services;
using QuanLyTiecCuoi.MVVM.View.HoaDon;
using QuanLyTiecCuoi.Repository;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using Microsoft.Extensions.DependencyInjection;
using System.Security.Permissions;
using System.Windows;
using System.Windows.Documents;
using Xceed.Wpf.Toolkit.PropertyGrid.Attributes;


namespace QuanLyTiecCuoi.MVVM.ViewModel
{
    public class HoaDonViewModel : BaseViewModel
    {
        private readonly HoaDonService _HoaDonService;
        private readonly IWindowService _WindowService;
        private ObservableCollection<HOADON> _DanhSachHoaDon;
        public ObservableCollection<HOADON> DanhSachHoaDon
        {
            get => _DanhSachHoaDon;
            set { _DanhSachHoaDon = value; OnPropertyChanged(); }
        }

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

        private string _LocMaDatTiec = "";
        public string LocMaDatTiec
        {
            get => _LocMaDatTiec;
            set { _LocMaDatTiec = value; OnPropertyChanged(); }
        }

        private HOADON _HoaDonDuocChon;
        public HOADON HoaDonDuocChon
        {
            get => _HoaDonDuocChon;
            set { _HoaDonDuocChon = value; OnPropertyChanged(); }
        }

        private ObservableCollection<CHITIETDVTIEC> _ChiTietDVTiecDuocChon;
        public ObservableCollection<CHITIETDVTIEC> ChiTietDVTiecDuocChon
        {
            get => _ChiTietDVTiecDuocChon;
            set { _ChiTietDVTiecDuocChon = value; OnPropertyChanged(); }
        }

        private DATTIEC? _TiecDuocChon;
        public DATTIEC? TiecDuocChon
        {
            get => _TiecDuocChon;
            set { _TiecDuocChon = value; OnPropertyChanged(); }
        }

        private ObservableCollection<CHITIETMENU> _MenuTiecDuocChon;
        public ObservableCollection<CHITIETMENU> MenuTiecDuocChon
        {
            get => _MenuTiecDuocChon;
            set { _MenuTiecDuocChon = value; OnPropertyChanged(); }
        }

        private ObservableCollection<DICHVU> _DanhSachDichVu;
        public ObservableCollection<DICHVU> DanhSachDichVu
        {
            get => _DanhSachDichVu;
            set { _DanhSachDichVu = value; OnPropertyChanged(); }
        }

        private List<String> _DanhSachTenDV;
        public List<String> DanhSachTenDV
        {
            get => _DanhSachTenDV;
            set { _DanhSachTenDV = value; OnPropertyChanged(); }
        }

        private String _ThongBaoPhatText = "";
        public String ThongBaoPhatText
        {
            get => _ThongBaoPhatText; set { _ThongBaoPhatText = value; OnPropertyChanged(); }
        }

        private THAMSO? _thamSo;
        private decimal _TienPhatThanhToanTre;

        private List<CHITIETDVTIEC> DanhSachDichVuTiecBiXoa;

        private bool _CoTheThayDoiSL;
        public bool CoTheThayDoiSL { get => _CoTheThayDoiSL; set { _CoTheThayDoiSL = value; OnPropertyChanged(); } }

        private bool _DangChonHoaDon = false;

        private Visibility _BtnLuuVisibility = Visibility.Hidden;
        public Visibility BtnLuuVisibility { get => _BtnLuuVisibility; set { _BtnLuuVisibility = value; OnPropertyChanged(); } }

        private Visibility _TextNgayThanhToan = Visibility.Hidden;
        public Visibility TextNgayThanhToan { get => _TextNgayThanhToan; set { _TextNgayThanhToan = value; OnPropertyChanged(); } }

        private Visibility _TextSoTienThanhToan = Visibility.Visible;
        public Visibility TextSoTienThanhToan { get => _TextSoTienThanhToan; set { _TextSoTienThanhToan = value; OnPropertyChanged(); } }
        private Visibility _BtnDaThanhToanVisibility = Visibility.Hidden;
        public Visibility BtnDaThanhToanVisibility { get => _BtnDaThanhToanVisibility; set { _BtnDaThanhToanVisibility = value; OnPropertyChanged(); } }

        private DateTime ngayToiDaThanhToan;

        #region Command
        public ICommand FirstLoadCommand { get; set; }
        public ICommand SetDateFilterCommand { get; set; }
        public ICommand LocHoaDonCommand { get; set; }
        public ICommand ChonHoaDonCommand { get; set; }
        public ICommand InHoaDonCommand { get; set; }
        public ICommand ThanhToanCommand { get; set; }
        public ICommand ThemDichVuCommand { get; set; }
        public ICommand DoiSoLuongCommand { get; set; }
        public ICommand XoaDichVuCommand { get; set; }
        public ICommand LuuThayDoiHoaDonCommand { get; set; }
        public ICommand ThayDoiSoLuongBanCommand { get; set; }

        #endregion

        public HoaDonViewModel(HoaDonService hoaDonService, IWindowService windowService)
        {
            _HoaDonService = hoaDonService;
            _WindowService = windowService;
            FirstLoadCommand = new RelayCommand<Page>((p) => { return true; }, async (p) =>
            {
                List<HOADON> res = await hoaDonService.GetAllHoaDonsAsync();
                if (res != null)
                {
                    DanhSachHoaDon = new ObservableCollection<HOADON>(res);
                }
            });

            SetDateFilterCommand = new RelayCommand<DatePicker>((p) => { return true; }, async (p) =>
            {
                p.Visibility = UseDateFilter ? System.Windows.Visibility.Visible : System.Windows.Visibility.Hidden;
                LocHoaDon();
            });

            LocHoaDonCommand = new RelayCommand<DatePicker>((p) => { return true; }, async (p) =>
            {
                if (DanhSachHoaDon != null)
                    LocHoaDon();
            });

            ChonHoaDonCommand = new RelayCommand<HOADON>((p) => { return true; }, async (p) =>
            {
                if (_DangChonHoaDon == true) return;
                _DangChonHoaDon = true;

                try
                {
                    if (p != null)
                    {
                        HoaDonDuocChon = p;
                        _TienPhatThanhToanTre = 0;
                        TiecDuocChon = await hoaDonService.GetDatTiec(p.MaDatTiec);
                        if (TiecDuocChon == null)
                        {
                            MessageBox.Show("Có lỗi khi load hóa đơn");
                            return;
                        }

                        _thamSo = await _HoaDonService.LayThamSo();

                        PHANQUYEN();

                        ChiTietDVTiecDuocChon = new ObservableCollection<CHITIETDVTIEC>(await hoaDonService.GetCTDVT(TiecDuocChon.MaDatTiec));
                        MenuTiecDuocChon = new ObservableCollection<CHITIETMENU>(await hoaDonService.GetMenu(TiecDuocChon.MaDatTiec));
                        DanhSachDichVu = new ObservableCollection<DICHVU>(await hoaDonService.GetDV());
                        GanDanhSachTenDV();
                        KiemTraPhatThanhToanTre();
                        DatThanhToanChoHoaDonDuocChon();
                        _WindowService.ShowChiTietHoaDon(this);
                    }
                }
                finally
                {
                    _DangChonHoaDon = false;
                }
            });

            ThemDichVuCommand = new RelayCommand<String?>((p) => { return true; }, async (p) =>
            {
                if (HoaDonDuocChon.NgayThanhToan != null || TiecDuocChon == null || TiecDuocChon.NgayDaiTiec > DateTime.Now.Date) return;
                if (p != null)
                {
                    foreach (var i in ChiTietDVTiecDuocChon)
                    {
                        if (i.DichVu.TenDichVu.Equals(p))
                        {
                            MessageBox.Show("Dịch vụ đã thêm trước đó");
                            return;
                        }
                    }
                    foreach (var i in DanhSachDichVu)
                    {
                        if (i.TenDichVu.Equals(p))
                        {
                            var chiTiet = new CHITIETDVTIEC
                            {
                                MaDatTiec = HoaDonDuocChon.MaDatTiec,
                                MaDichVu = i.MaDichVu,
                                DonGia = i.DonGia,
                                SoLuong = 1,
                                DichVu = i
                            };
                            ChiTietDVTiecDuocChon.Add(chiTiet);
                            OnPropertyChanged(nameof(ChiTietDVTiecDuocChon));
                            TinhThanhTienDVHoaDon();
                            return;
                        }
                    }
                }
            });

            DoiSoLuongCommand = new RelayCommand<CHITIETDVTIEC>((p) => { return true; }, async (p) =>
            {
                if (HoaDonDuocChon.NgayThanhToan != null) return;
                if (p != null)
                {
                    var danhSachMoi = new ObservableCollection<CHITIETDVTIEC>(ChiTietDVTiecDuocChon);
                    ChiTietDVTiecDuocChon = danhSachMoi;
                    OnPropertyChanged(nameof(ChiTietDVTiecDuocChon));
                    TinhThanhTienDVHoaDon();
                }
            });

            XoaDichVuCommand = new RelayCommand<CHITIETDVTIEC>((p) => { return true; }, async (p) =>
            {
                if (HoaDonDuocChon.NgayThanhToan != null) return;
                if (p != null)
                {
                    if (DanhSachDichVuTiecBiXoa == null) DanhSachDichVuTiecBiXoa = new List<CHITIETDVTIEC>();
                    DanhSachDichVuTiecBiXoa.Add(p);
                    var danhSachMoi = new ObservableCollection<CHITIETDVTIEC>(ChiTietDVTiecDuocChon);
                    danhSachMoi.Remove(p);
                    ChiTietDVTiecDuocChon = danhSachMoi;
                    OnPropertyChanged(nameof(ChiTietDVTiecDuocChon));
                    TinhThanhTienDVHoaDon();
                }
            });

            LuuThayDoiHoaDonCommand = new RelayCommand<object>((p) => { return true; }, async (p) =>
            {
                if (HoaDonDuocChon.NgayThanhToan != null)
                {
                    MessageBox.Show("Hóa đơn đã thanh toán");
                    return;
                }
                if (DanhSachDichVuTiecBiXoa != null)
                {
                    bool res = await hoaDonService.XoaCTDVT(DanhSachDichVuTiecBiXoa);
                    if (!res)
                    {
                        MessageBox.Show("Có lỗi xảy ra");
                        return;
                    }
                }

                bool result = await hoaDonService.AddCTDVT(ChiTietDVTiecDuocChon.ToList());
                if (!result)
                {
                    MessageBox.Show("Có lỗi xảy ra");
                    return;
                }
                HoaDonDuocChon.TienPhat = _TienPhatThanhToanTre;
                var HoaDonMoi = await hoaDonService.UpdateHoaDonAsync(HoaDonDuocChon);
                if (HoaDonMoi != null)
                {
                    HoaDonDuocChon = HoaDonMoi;
                    await Reload();
                    BtnLuuVisibility = Visibility.Hidden;

                }
                else
                {
                    MessageBox.Show("Có lỗi xảy ra");
                }
            });

            ThanhToanCommand = new RelayCommand<Window>((p) => { return true; }, async (p) =>
            {
                if(TiecDuocChon != null && TiecDuocChon.NgayDaiTiec.Date >= DateTime.Now.Date)
                {
                    MessageBox.Show("Hóa đơn chỉ được xác nhận thanh toán từ ngày đãi tiệc");
                    return;
                }
                if (BtnLuuVisibility == Visibility.Visible)
                {
                    MessageBox.Show("Chưa lưu thay đổi");
                    return;
                }
                HoaDonDuocChon.TienPhat = _TienPhatThanhToanTre;
                HoaDonDuocChon.TienPhaiThanhToan = HoaDonDuocChon.TongTienHD - HoaDonDuocChon.DATTIEC.TienDatCoc + _TienPhatThanhToanTre;
                var hd = await hoaDonService.ThanhToan(HoaDonDuocChon);
                if (hd != null)
                {
                    HoaDonDuocChon = hd;
                    OnPropertyChanged(nameof(HoaDonDuocChon));
                    MessageBox.Show("Xác nhận thanh toán thành công");
                    await Reload();
                    DatThanhToanChoHoaDonDuocChon();
                    return;
                }

            });

            InHoaDonCommand = new RelayCommand<Window>((p) => { return true; }, (p) =>
            {
                if (BtnLuuVisibility == Visibility.Visible)
                {
                    MessageBox.Show("Chưa lưu thay đổi");
                    return;
                }
                if (BtnDaThanhToanVisibility == Visibility.Visible)
                {
                    MessageBox.Show("Hóa đơn chưa được xác nhận thanh toán");
                    return;
                }
                InHoaDon();
            });

            ThayDoiSoLuongBanCommand = new RelayCommand<String?>((p) => { return true; }, (p) =>
            {
                if(p == null ||  p.Length == 0 || TiecDuocChon == null)
                {
                    return;
                }

                int soluong = 0;
                if(int.TryParse(p, out soluong))
                {
                    if(TiecDuocChon.SoLuongBan > soluong || soluong > TiecDuocChon.SoLuongBan + TiecDuocChon.SoBanDuTru)
                    {
                        MessageBox.Show($"Số bàn thực tế sử dụng trong khoản {TiecDuocChon.SoLuongBan} đến {TiecDuocChon.SoBanDuTru + TiecDuocChon.SoLuongBan}");
                        return;
                    }
                    HoaDonDuocChon.SoLuongBan = soluong;
                    TinhThanhTienDVHoaDon();
                }
                else
                {
                    MessageBox.Show("Nhập vào số");
                }
            });

        }

        private void PHANQUYEN()
        {
            if (MainWindowViewModel._DanhSachChucNang.Any(cn => cn.TenChucNang == "Hóa đơn"))
                BtnLuuVisibility = Visibility.Hidden;
            else
                return;
        }

        private void KiemTraPhatThanhToanTre()
        {
            if (_thamSo == null || TiecDuocChon == null || HoaDonDuocChon == null)
                return;
            if (HoaDonDuocChon.NgayThanhToan != null && HoaDonDuocChon.TienPhat != 0)
            {
                ThongBaoPhatText = $"Hóa đơn bị phạt {HoaDonDuocChon.TienPhat.ToString("N0")} VND vì thanh toán trễ.";
                return;
            }

            if (_thamSo.ApDungQDPhatThanhToanTre && !HoaDonDuocChon.NgayThanhToan.HasValue)
            {
                var ngayDaiTiec = TiecDuocChon.NgayDaiTiec.Date;

                ngayToiDaThanhToan = ngayDaiTiec.AddDays(_thamSo.SLNgayThanhToanTreToiDa);
                if (DateTime.Now.Date > ngayToiDaThanhToan)
                {
                    int soNgayTre = (DateTime.Now.Date - ngayToiDaThanhToan).Days;

                    if (HoaDonDuocChon.NgayThanhToan != null) return;
                    decimal TongTienTatCaDichVu = ChiTietDVTiecDuocChon?.Sum(i => i.ThanhTien) ?? 0;
                    HoaDonDuocChon.TongTienDV = TongTienTatCaDichVu;
                    HoaDonDuocChon.TongTienBan = HoaDonDuocChon.DonGiaBan * HoaDonDuocChon.SoLuongBan;
                    HoaDonDuocChon.TongTienHD = HoaDonDuocChon.TongTienDV + HoaDonDuocChon.TongTienBan;
                    _TienPhatThanhToanTre = soNgayTre * _thamSo.TyLePhatThanhToanTreTheoNgay * HoaDonDuocChon.TongTienHD;
                    HoaDonDuocChon.TienPhat = _TienPhatThanhToanTre;
                    HoaDonDuocChon.TienPhaiThanhToan = HoaDonDuocChon.TongTienHD - HoaDonDuocChon.DATTIEC.TienDatCoc + _TienPhatThanhToanTre;
                    OnPropertyChanged(nameof(HoaDonDuocChon));

                    ThongBaoPhatText = $"Hóa đơn bị phạt {_TienPhatThanhToanTre.ToString("N0")} VND vì thanh toán trễ {soNgayTre} ngày với mức phạt theo ngày {_thamSo.TyLePhatThanhToanTreTheoNgay.ToString("P4")} kể từ sau ngày đặt tiệc {_thamSo.SLNgayThanhToanTreToiDa} ngày ({ngayToiDaThanhToan:dd/MM/yyyy})";
                    return;
                }
                else
                {
                    _TienPhatThanhToanTre = 0;
                    ThongBaoPhatText = "";
                }
            }
            else
            {
                _TienPhatThanhToanTre = 0;
                ThongBaoPhatText = "";
            }
        }


        private void InHoaDon()
        {
            FlowDocument document = TaoDocument();
            PrintDialog printDialog = new PrintDialog();
            if (printDialog.ShowDialog() == true)
            {
                printDialog.PrintDocument(((IDocumentPaginatorSource)document).DocumentPaginator, "Hóa đơn bán hàng");
            }
        }

        private FlowDocument TaoDocument()
        {
            FlowDocument doc = new FlowDocument
            {
                FontFamily = new System.Windows.Media.FontFamily("Tahoma"),
                FontSize = 12,
                PagePadding = new Thickness(20),
                PageWidth = 800,
                ColumnWidth = double.PositiveInfinity
            };

            Paragraph title = new Paragraph(new Run("HÓA ĐƠN THANH TOÁN"))
            {
                FontSize = 18,
                FontWeight = FontWeights.Bold,
                TextAlignment = TextAlignment.Center,
                Margin = new Thickness(0, 0, 0, 20)
            };
            doc.Blocks.Add(new Paragraph(new Run("SereniteWedding"))
            {
                TextAlignment = TextAlignment.Center
            });
            doc.Blocks.Add(new Paragraph(new Run("Khu phố 6, P.Linh Trung, Tp.Thủ Đức, Tp.HCM\n----------------------"))
            {
                TextAlignment = TextAlignment.Center
            });

            // Nội dung mẫu
            Paragraph content = new Paragraph();
            content.Inlines.Add(new Bold(new Run("Ngày đãi tiệc: ")));
            content.Inlines.Add(new Run(TiecDuocChon?.NgayDaiTiec.ToString("dd/MM/yyyy")));
            content.Inlines.Add(new LineBreak());
            content.Inlines.Add(new Bold(new Run("Ngày thanh toán: ")));
            content.Inlines.Add(new Run(HoaDonDuocChon.NgayThanhToan?.ToString("dd/MM/yyyy")));
            content.Inlines.Add(new LineBreak());
            content.Inlines.Add(new Bold(new Run("Tổng tiền thanh toán: ")));
            content.Inlines.Add(new Run($"{HoaDonDuocChon.TongTienHD:C0}"));
            content.Inlines.Add(new LineBreak());
            if(HoaDonDuocChon.TienPhat != 0)
            {
                content.Inlines.Add(new Bold(new Run($"Tiền phạt: ")));
                content.Inlines.Add(new Run($"{HoaDonDuocChon.TienPhat:C0}"));
            content.Inlines.Add(new LineBreak());
            }

            // Tạo bảng
            Table table = new Table();
            table.CellSpacing = 0;
            table.Columns.Add(new TableColumn { Width = new GridLength(250) }); 
            table.Columns.Add(new TableColumn { Width = new GridLength(100) }); 
            table.Columns.Add(new TableColumn { Width = new GridLength(120) }); 
            table.Columns.Add(new TableColumn { Width = new GridLength(130) }); 

            // Tạo phần thân bảng
            TableRowGroup group = new TableRowGroup();

            // Dòng tiêu đề
            group.Rows.Add(new TableRow());
            group.Rows[0].Cells.Add(new TableCell(new Paragraph(new Bold(new Run("Tên dịch vụ")))) { TextAlignment = TextAlignment.Center });
            group.Rows[0].Cells.Add(new TableCell(new Paragraph(new Bold(new Run("Số lượng")))) { TextAlignment = TextAlignment.Center });
            group.Rows[0].Cells.Add(new TableCell(new Paragraph(new Bold(new Run("Đơn giá")))) { TextAlignment = TextAlignment.Center });
            group.Rows[0].Cells.Add(new TableCell(new Paragraph(new Bold(new Run("Thành tiền")))) { TextAlignment = TextAlignment.Center });

            // Dòng dữ liệu từ danh sách
            foreach (var item in ChiTietDVTiecDuocChon)
            {
                TableRow row = new TableRow();
                row.Cells.Add(new TableCell(new Paragraph(new Run(item.DichVu.TenDichVu))) { TextAlignment = TextAlignment.Left });
                row.Cells.Add(new TableCell(new Paragraph(new Run(item.SoLuong.ToString()))) { TextAlignment = TextAlignment.Center });
                row.Cells.Add(new TableCell(new Paragraph(new Run($"{item.DonGia:N0}"))) { TextAlignment = TextAlignment.Right });
                row.Cells.Add(new TableCell(new Paragraph(new Run($"{item.ThanhTien:N0}"))) { TextAlignment = TextAlignment.Right });
                group.Rows.Add(row);
            }
            table.RowGroups.Add(group);
            Paragraph dv = new Paragraph();
            dv.Inlines.Add(new Bold(new Run("Tổng tiền dịch vụ: ")));
            dv.Inlines.Add(new Run($"{HoaDonDuocChon.TongTienDV:C0}"));

            Table table2 = new Table();
            table2.CellSpacing = 0;
            table2.Columns.Add(new TableColumn { Width = new GridLength(250) });
            table2.Columns.Add(new TableColumn { Width = new GridLength(100) });

            // Tạo phần thân bảng
            TableRowGroup group2 = new TableRowGroup();

            // Dòng tiêu đề
            group2.Rows.Add(new TableRow());
            group2.Rows[0].Cells.Add(new TableCell(new Paragraph(new Bold(new Run("Tên món")))) { TextAlignment = TextAlignment.Center });
            group2.Rows[0].Cells.Add(new TableCell(new Paragraph(new Bold(new Run("Đơn giá")))) { TextAlignment = TextAlignment.Center });

            // Dòng dữ liệu từ danh sách
            foreach (var item in MenuTiecDuocChon)
            {
                TableRow row = new TableRow();
                row.Cells.Add(new TableCell(new Paragraph(new Run(item.MonAn.TenMon))) { TextAlignment = TextAlignment.Left });
                row.Cells.Add(new TableCell(new Paragraph(new Run($"{item.MonAn.DonGia:N0}"))) { TextAlignment = TextAlignment.Right });
                group2.Rows.Add(row);
            }
            table2.RowGroups.Add(group2);
            Paragraph menu = new Paragraph();
            menu.Inlines.Add(new Bold(new Run("Tổng tiền ăn: ")));
            menu.Inlines.Add(new Run($"{HoaDonDuocChon.TongTienBan:C0}"));

            // Thêm vào bảng
            doc.Blocks.Add(title);
            doc.Blocks.Add(content);
            doc.Blocks.Add(new Paragraph(new Run("Chi tiết dịch vụ:")) { FontWeight = FontWeights.Bold, Margin = new Thickness(0, 20, 0, 5) });

            // Thêm bảng vào FlowDocument
            doc.Blocks.Add(table);
            doc.Blocks.Add(new Paragraph(new Run("Chi tiết menu mỗi bàn:")) { FontWeight = FontWeights.Bold, Margin = new Thickness(0, 20, 0, 5) });
            doc.Blocks.Add(table2);

            // Thêm các đoạn vào document

            return doc;
        }

        private async Task Reload()
        {
            List<HOADON> res = await _HoaDonService.GetAllHoaDonsAsync();
            if (res != null)
            {
                DanhSachHoaDon = new ObservableCollection<HOADON>(res);
            }
        }

        private void GanDanhSachTenDV()
        {
            DanhSachTenDV = new List<string>();
            if (DanhSachDichVu != null)
            {
                foreach (var i in DanhSachDichVu)
                {
                    DanhSachTenDV.Add(i.TenDichVu);
                }
            }
        }

        private void TinhThanhTienDVHoaDon()
        {
            if (HoaDonDuocChon.NgayThanhToan != null || TiecDuocChon != null) return;
            int soNgayTre = (DateTime.Now.Date - ngayToiDaThanhToan).Days;

            BtnLuuVisibility = Visibility.Visible;
            decimal TongTienTatCaDichVu = ChiTietDVTiecDuocChon?.Sum(i => i.ThanhTien) ?? 0;
            HoaDonDuocChon.TongTienDV = TongTienTatCaDichVu;
            HoaDonDuocChon.TongTienBan = HoaDonDuocChon.DonGiaBan * HoaDonDuocChon.SoLuongBan;
            HoaDonDuocChon.TongTienHD = HoaDonDuocChon.TongTienDV + HoaDonDuocChon.TongTienBan;
            if(_thamSo != null && _thamSo.ApDungQDPhatThanhToanTre)
            {
                ngayToiDaThanhToan = TiecDuocChon.NgayDaiTiec.AddDays(_thamSo.SLNgayThanhToanTreToiDa);
                if (DateTime.Now.Date > ngayToiDaThanhToan)
                {
                    _TienPhatThanhToanTre = soNgayTre * _thamSo.TyLePhatThanhToanTreTheoNgay * HoaDonDuocChon.TongTienHD;
                    HoaDonDuocChon.TienPhat = _TienPhatThanhToanTre;
                    ThongBaoPhatText = $"Hóa đơn bị phạt {_TienPhatThanhToanTre.ToString("N0")} VND vì thanh toán trễ {soNgayTre} ngày với mức phạt theo ngày {_thamSo.TyLePhatThanhToanTreTheoNgay.ToString("P4")} kể từ sau ngày đặt tiệc {_thamSo.SLNgayThanhToanTreToiDa} ngày ({ngayToiDaThanhToan:dd/MM/yyyy})";
                }
            }
            HoaDonDuocChon.TienPhaiThanhToan = HoaDonDuocChon.TongTienHD - HoaDonDuocChon.DATTIEC.TienDatCoc + _TienPhatThanhToanTre;
            OnPropertyChanged(nameof(HoaDonDuocChon));
        }

        private void DatThanhToanChoHoaDonDuocChon()
        {
            if (HoaDonDuocChon == null) return;
            if (HoaDonDuocChon.NgayThanhToan == null || TiecDuocChon == null || TiecDuocChon.NgayDaiTiec <= DateTime.Now.Date)
            {
                TextSoTienThanhToan = Visibility.Visible;
                BtnDaThanhToanVisibility = Visibility.Visible;
                TextNgayThanhToan = Visibility.Hidden;
                CoTheThayDoiSL = true;
                PHANQUYEN();
                OnPropertyChanged(nameof(CoTheThayDoiSL));
            }
            else
            {
                TextSoTienThanhToan = Visibility.Hidden;
                BtnDaThanhToanVisibility = Visibility.Hidden;
                TextNgayThanhToan = Visibility.Visible;
                BtnLuuVisibility = Visibility.Hidden;
                CoTheThayDoiSL = false;
                PHANQUYEN();
                OnPropertyChanged(nameof(CoTheThayDoiSL));
            }
        }

        private bool _isLocHoaDonRunning = false;
        private async void LocHoaDon()
        {
            if (_isLocHoaDonRunning) return;
            _isLocHoaDonRunning = true;

            try
            {
                if (LocMaDatTiec.Length != 0)
                {
                    var res = await _HoaDonService.GetHoaDonByMaDatTiecAsync(LocMaDatTiec);
                    if (res != null)
                        DanhSachHoaDon = new ObservableCollection<HOADON>(res);
                }
                else if (UseDateFilter)
                {
                    var res = await _HoaDonService.GetHoaDonsByNgayThanhToanAsync(LocNgayThanhToan);
                    if (res != null)
                        DanhSachHoaDon = new ObservableCollection<HOADON>(res);
                }
                else
                {
                    await Reload();
                }
            }
            finally
            {
                _isLocHoaDonRunning = false;
            }
        }
    }
}
