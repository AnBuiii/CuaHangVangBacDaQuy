using CuaHangVangBacDaQuy.models;
using CuaHangVangBacDaQuy.views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using COMExcel = Microsoft.Office.Interop.Excel;

namespace CuaHangVangBacDaQuy.viewmodels.DialogContentViewModel
{
    internal class InventoryReportViewModel : BaseViewModel
    {
        private ObservableCollection<TonKho> _TonKhoList;
        public ObservableCollection<TonKho> TonKhoList { get => _TonKhoList; set { _TonKhoList = value; OnPropertyChanged(); } }

        private DateTime _SelectedTime;
        public DateTime SelectedTime
        {
            get => _SelectedTime;
            set { _SelectedTime = value; OnPropertyChanged(); LoadTonKhoList(); }
        }

        public ICommand LoadView { get; set; }
        public ICommand PrintCommand { get; set; }

        public InventoryReportViewModel()
        {

            LoadView = new RelayCommand<InventoryReport>((p) => true, (p) => LoadTonKhoList());
            PrintCommand = new RelayCommand<Window>((p) => { return true; }, (p) => { Print(); });
            SelectedTime = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);

            LoadTonKhoList();

        }
        private void Print()
        {
            COMExcel.Application exApp = new COMExcel.Application();
            COMExcel.Workbook exBook; //Trong 1 chương trình Excel có nhiều Workbook
            COMExcel.Worksheet exSheet; //Trong 1 Workbook có nhiều Worksheet
            COMExcel.Range exRange;

            int row = 0;
            exBook = exApp.Workbooks.Add(COMExcel.XlWBATemplate.xlWBATWorksheet);
            exSheet = exBook.Worksheets[1];
            // Định dạng chung
            exRange = exSheet.Cells[1, 1];
            exRange.Range["A1:Z300"].Font.Name = "Times new roman"; //Font chữ
            exRange.Range["C2:E2"].Font.Size = 16;
            exRange.Range["C2:E2"].Font.Bold = true;
            exRange.Range["C2:E2"].Font.ColorIndex = 3;
            exRange.Range["C2:E2"].MergeCells = true;
            exRange.Range["C2:E2"].HorizontalAlignment = COMExcel.XlHAlign.xlHAlignCenter;
            exRange.Range["C2:E2"].Value = "Báo cáo tồn kho";


            exRange.Range["B4:C6"].Font.Size = 12;
            exRange.Range["B4:B4"].Value = "Tháng:";
            exRange.Range["C4:E4"].MergeCells = true;
            exRange.Range["C4:E4"].Value = SelectedTime.Month + "/" + SelectedTime.Year;

            exRange.Range["A6:F6"].Font.Bold = true;
            exRange.Range["A6:F6"].HorizontalAlignment = COMExcel.XlHAlign.xlHAlignCenter;
            exRange.Range["C6:F6"].ColumnWidth = 12;
            exRange.Range["A6:A6"].Value = "STT";
            exRange.Range["B6:B6"].Value = "Sản phẩm";
            exRange.Range["C6:C6"].Value = "Tồn đầu";
            exRange.Range["D6:D6"].Value = "Số lượng mua";
            exRange.Range["E6:E6"].Value = "Số lượng bán";
            exRange.Range["F6:F6"].Value = "Tồn cuối";
            for (row = 0; row < TonKhoList.Count; row++)
            {
                exSheet.Cells[1][row + 7] = (row + 1).ToString();
                exSheet.Cells[2][row + 7] = TonKhoList[row].SanPham.TenSP;
                exSheet.Cells[3][row + 7] = TonKhoList[row].TonDau;
                exSheet.Cells[4][row + 7] = TonKhoList[row].Mua;
                exSheet.Cells[5][row + 7] = TonKhoList[row].Ban;
                exSheet.Cells[6][row + 7] = TonKhoList[row].TonCuoi;
            }
            exSheet.Name = "Báo cáo tồn kho";
            exApp.Visible = true;

        }

        private void LoadTonKhoList()
        {
            TonKhoList = new ObservableCollection<TonKho>();
            foreach(SanPham sanpham in DataProvider.Ins.DB.SanPhams)
            {
                int tonDau = 0;
                foreach(ChiTietPhieuMua phieuMua in DataProvider.Ins.DB.ChiTietPhieuMuas.Where(p=> p.SanPham.MaSP == sanpham.MaSP && p.PhieuMua.NgayLap.Value < SelectedTime))
                {
                    tonDau += (int)phieuMua.SoLuong;
                }
                foreach (ChiTietPhieuBan phieuBan in DataProvider.Ins.DB.ChiTietPhieuBans.Where(p => p.SanPham.MaSP == sanpham.MaSP && p.PhieuBan.NgayLap.Value < SelectedTime))
                {
                    tonDau -= (int)phieuBan.SoLuong;
                }

                int  mua = 0;
                foreach (ChiTietPhieuMua phieuMua in DataProvider.Ins.DB.ChiTietPhieuMuas.Where(p => p.SanPham.MaSP == sanpham.MaSP && SelectedTime.Month == p.PhieuMua.NgayLap.Value.Month && SelectedTime.Year == p.PhieuMua.NgayLap.Value.Year))
                {
                    mua += (int)phieuMua.SoLuong;
                }

                int ban = 0;
                foreach (ChiTietPhieuBan phieuBan in DataProvider.Ins.DB.ChiTietPhieuBans.Where(p => p.SanPham.MaSP == sanpham.MaSP && SelectedTime.Month == p.PhieuBan.NgayLap.Value.Month && SelectedTime.Year == p.PhieuBan.NgayLap.Value.Year))
                {
                    ban += (int)phieuBan.SoLuong;
                }
                TonKho tonKho = new TonKho()
                {
                    SanPham = sanpham,
                    TonDau = tonDau,
                    Mua = mua,
                    Ban = ban,
                    TonCuoi = tonDau + mua - ban
                };
                TonKhoList.Add(tonKho);
            }
        }

    }
}
