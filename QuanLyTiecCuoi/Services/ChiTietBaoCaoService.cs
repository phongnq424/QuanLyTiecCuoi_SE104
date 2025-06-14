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
                    TiLeDoanhThu = tongDoanhThu == 0 ? 0 : (double)(item.DoanhThu / tongDoanhThu),
                    TiLeTiecCuoi = tongTiecCuoi == 0 ? 0 : (double)item.SoLuongTiecCuoi / tongTiecCuoi
                };
                result.Add(model);
            }
            return result;
        }
        public void PrintBaoCao(List<ChiTietBaoCaoModel> data)
        {
            // Tạo FlowDocument để trình bày báo cáo
            FlowDocument doc = new FlowDocument();

            // Thiết lập font, margin,...
            doc.FontFamily = new System.Windows.Media.FontFamily("Cambria");
            doc.FontSize = 14;
            doc.PagePadding = new Thickness(50);

            // Tiêu đề
            Paragraph title = new Paragraph(new Run("Báo cáo chi tiết"));
            title.FontSize = 24;
            title.FontWeight = FontWeights.Bold;
            title.TextAlignment = TextAlignment.Center;
            doc.Blocks.Add(title);

            // Tạo bảng báo cáo
            Table table = new Table();
            table.CellSpacing = 5;
            doc.Blocks.Add(table);

            // Thêm 6 cột cho các trường: STT, Ngày, Số lượng, Doanh thu, Tỉ lệ doanh thu, Tỉ lệ tiệc cưới
            int columns = 6;
            for (int i = 0; i < columns; i++)
                table.Columns.Add(new TableColumn());

            // Header row
            TableRow headerRow = new TableRow();
            headerRow.Cells.Add(new TableCell(new Paragraph(new Run("STT"))) { FontWeight = FontWeights.Bold });
            headerRow.Cells.Add(new TableCell(new Paragraph(new Run("Ngày"))) { FontWeight = FontWeights.Bold });
            headerRow.Cells.Add(new TableCell(new Paragraph(new Run("Số lượng tiệc cưới"))) { FontWeight = FontWeights.Bold });
            headerRow.Cells.Add(new TableCell(new Paragraph(new Run("Doanh thu"))) { FontWeight = FontWeights.Bold });
            headerRow.Cells.Add(new TableCell(new Paragraph(new Run("Tỉ lệ doanh thu"))) { FontWeight = FontWeights.Bold });
            headerRow.Cells.Add(new TableCell(new Paragraph(new Run("Tỉ lệ tiệc cưới"))) { FontWeight = FontWeights.Bold });

            TableRowGroup trg = new TableRowGroup();
            trg.Rows.Add(headerRow);

            foreach (var item in data)
            {
                TableRow row = new TableRow();
                row.Cells.Add(new TableCell(new Paragraph(new Run(item.STT.ToString()))));
                row.Cells.Add(new TableCell(new Paragraph(new Run(item.NgayBaoCao.ToString("dd/MM/yyyy")))));
                row.Cells.Add(new TableCell(new Paragraph(new Run(item.SoLuongTiecCuoi.ToString()))));
                row.Cells.Add(new TableCell(new Paragraph(new Run(item.DoanhThuFormatted))));
                row.Cells.Add(new TableCell(new Paragraph(new Run((item.TiLeDoanhThu * 100).ToString("0.##") + "%"))));
                row.Cells.Add(new TableCell(new Paragraph(new Run((item.TiLeTiecCuoi * 100).ToString("0.##") + "%"))));
                trg.Rows.Add(row);
            }
            table.RowGroups.Add(trg);

            // Tạo đối tượng PrintDialog để chọn máy in
            PrintDialog printDialog = new PrintDialog();
            if (printDialog.ShowDialog() == true)
            {
                // Tự động scale document vừa trang in
                doc.PageHeight = printDialog.PrintableAreaHeight;
                doc.PageWidth = printDialog.PrintableAreaWidth;

                // In document
                printDialog.PrintDocument(((IDocumentPaginatorSource)doc).DocumentPaginator, "In Báo Cáo Chi Tiết");
            }
        }
    }
}
