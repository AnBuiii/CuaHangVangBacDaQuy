using CuaHangVangBacDaQuy.models;
using CuaHangVangBacDaQuy.views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

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

        public InventoryReportViewModel()
        {

            LoadView = new RelayCommand<InventoryReport>((p) => true, (p) => LoadTonKhoList());
            SelectedTime = DateTime.Now;
            LoadTonKhoList();

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
