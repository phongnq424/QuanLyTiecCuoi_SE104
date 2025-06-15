using QuanLyTiecCuoi.MVVM.Model;
using QuanLyTiecCuoi.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using QuanLyTiecCuoi.Repository;
using System.Printing;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Controls;

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
                    TiLeDoanhThu = tongDoanhThu == 0 ? 0 : (double)(item.DoanhThu / tongDoanhThu) * 100,
                    TiLeTiecCuoi = tongTiecCuoi == 0 ? 0 : (double)item.SoLuongTiecCuoi / tongTiecCuoi * 100
                };
                result.Add(model);
            }
            return result;
        }
        public void PrintBaoCao(List<ChiTietBaoCaoModel> data, int thang, int nam, double tongDoanhThu, int tongTiecCuoi)
        {
            FlowDocument doc = new FlowDocument
            {
                FontFamily = new System.Windows.Media.FontFamily("Cambria"),
                FontSize = 14,
                PagePadding = new Thickness(50)
            };

            // Tiêu đề chính
            Paragraph title = new Paragraph(new Run("BÁO CÁO CHI TIẾT THÁNG " + thang + " NĂM " + nam))
            {
                FontSize = 24,
                FontWeight = FontWeights.Bold,
                TextAlignment = TextAlignment.Center,
                Margin = new Thickness(0, 0, 0, 20)
            };
            doc.Blocks.Add(title);

            // Thông tin tổng quan
            Paragraph summary = new Paragraph
            {
                Margin = new Thickness(0, 0, 0, 20)
            };
            summary.Inlines.Add(new Run("Tổng doanh thu: ") { FontWeight = FontWeights.Bold });
            summary.Inlines.Add(new Run(string.Format("{0:N0} VND", tongDoanhThu)));
            summary.Inlines.Add(new LineBreak());
            summary.Inlines.Add(new Run("Tổng số tiệc cưới: ") { FontWeight = FontWeights.Bold });
            summary.Inlines.Add(new Run(tongTiecCuoi.ToString()));
            doc.Blocks.Add(summary);

            // Tạo bảng
            Table table = new Table
            {
                CellSpacing = 0,
                BorderThickness = new Thickness(1),
                BorderBrush = System.Windows.Media.Brushes.Black
            };

            // Cấu hình số cột
            int columns = 6;
            for (int i = 0; i < columns; i++)
            {
                var column = new TableColumn();
                table.Columns.Add(column);
            }

            // Header
            TableRowGroup headerGroup = new TableRowGroup();
            TableRow headerRow = new TableRow { FontWeight = FontWeights.Bold, Background = System.Windows.Media.Brushes.LightGray };
            headerRow.Cells.Add(new TableCell(new Paragraph(new Run("STT"))) { TextAlignment = TextAlignment.Center, Padding = new Thickness(4) });
            headerRow.Cells.Add(new TableCell(new Paragraph(new Run("Ngày"))) { TextAlignment = TextAlignment.Center, Padding = new Thickness(4) });
            headerRow.Cells.Add(new TableCell(new Paragraph(new Run("Số lượng TC"))) { TextAlignment = TextAlignment.Center, Padding = new Thickness(4) });
            headerRow.Cells.Add(new TableCell(new Paragraph(new Run("Doanh thu"))) { TextAlignment = TextAlignment.Center, Padding = new Thickness(4) });
            headerRow.Cells.Add(new TableCell(new Paragraph(new Run("Tỉ lệ doanh thu"))) { TextAlignment = TextAlignment.Center, Padding = new Thickness(4) });
            headerRow.Cells.Add(new TableCell(new Paragraph(new Run("Tỉ lệ tiệc cưới"))) { TextAlignment = TextAlignment.Center, Padding = new Thickness(4) });
            headerGroup.Rows.Add(headerRow);
            table.RowGroups.Add(headerGroup);

            // Dữ liệu
            TableRowGroup bodyGroup = new TableRowGroup();
            foreach (var item in data)
            {
                TableRow row = new TableRow();
                row.Cells.Add(new TableCell(new Paragraph(new Run(item.STT.ToString()))) { Padding = new Thickness(4), TextAlignment = TextAlignment.Center });
                row.Cells.Add(new TableCell(new Paragraph(new Run(item.NgayBaoCao.ToString("dd/MM/yyyy")))) { Padding = new Thickness(4), TextAlignment = TextAlignment.Center });
                row.Cells.Add(new TableCell(new Paragraph(new Run(item.SoLuongTiecCuoi.ToString()))) { Padding = new Thickness(4), TextAlignment = TextAlignment.Center });
                row.Cells.Add(new TableCell(new Paragraph(new Run(item.DoanhThuFormatted))) { Padding = new Thickness(4), TextAlignment = TextAlignment.Right });
                row.Cells.Add(new TableCell(new Paragraph(new Run((item.TiLeDoanhThu).ToString("0.##") + "%"))) { Padding = new Thickness(4), TextAlignment = TextAlignment.Right });
                row.Cells.Add(new TableCell(new Paragraph(new Run((item.TiLeTiecCuoi).ToString("0.##") + "%"))) { Padding = new Thickness(4), TextAlignment = TextAlignment.Right });
                bodyGroup.Rows.Add(row);
            }
            table.RowGroups.Add(bodyGroup);

            doc.Blocks.Add(table);

            // In
            PrintDialog printDialog = new PrintDialog();
            if (printDialog.ShowDialog() == true)
            {
                doc.PageHeight = printDialog.PrintableAreaHeight;
                doc.PageWidth = printDialog.PrintableAreaWidth;
                doc.ColumnWidth = printDialog.PrintableAreaWidth; // fix vỡ layout

                printDialog.PrintDocument(((IDocumentPaginatorSource)doc).DocumentPaginator, "In Báo Cáo Chi Tiết");
            }
        }

    }
}
