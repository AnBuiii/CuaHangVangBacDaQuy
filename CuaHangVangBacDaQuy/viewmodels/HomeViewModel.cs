using CuaHangVangBacDaQuy.models;
using CuaHangVangBacDaQuy.views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace CuaHangVangBacDaQuy.viewmodels
{
    public class HomeViewModel : BaseViewModel
    {
        private ObservableCollection<TonKho> _TonKhoList;
        public ObservableCollection<TonKho> TonKhoList { get => _TonKhoList; set { _TonKhoList = value; OnPropertyChanged(); } }

        private DateTime _StartTotalImportDay;
        public DateTime StartTotalImportDay { get => _StartTotalImportDay; set { _StartTotalImportDay = value; OnPropertyChanged(); } }

        private DateTime _EndTotalImportDay;
        public DateTime EndTotalImportDay { get => _EndTotalImportDay; set { _EndTotalImportDay = value; OnPropertyChanged(); } }

        private DateTime _StartProductImportDay;
        public DateTime StartProductImportDay { get => _StartProductImportDay; set { _StartProductImportDay = value; OnPropertyChanged(); } }

        private DateTime _EndProductmportDay;
        public DateTime EndProductmportDay { get => _EndProductmportDay; set { _EndProductmportDay = value; OnPropertyChanged(); } }

        private long _ImportVolume;
        public long ImportVolume { get => _ImportVolume; set { _ImportVolume = value; OnPropertyChanged(); } }

        private long _ExportVolume;
        public long ExportVolume { get => _ExportVolume; set { _ExportVolume = value; OnPropertyChanged(); } }

        private long _Inventory;
        public long Inventory { get => _Inventory; set { _Inventory = value; OnPropertyChanged(); } }

        public ICommand LoadHomeView { get; set; }

        public ICommand FilterTotalImportCommand { get; set; }
        public ICommand FilterProductImportCommand { get; set; }

        public HomeViewModel()
        {
            LoadHomeView = new RelayCommand<HomeView>((p) => true, (p) => LoadingHomeView(p));
            FilterTotalImportCommand = new RelayCommand<HomeView>(p => true, p => InventoryManagementByDay());
        }

        private void LoadingHomeView(HomeView view)
        {
            TonKhoList = new ObservableCollection<TonKho>();

            var SanPhamList = DataProvider.Ins.DB.SanPhams;
            ImportVolume = 0;
            ExportVolume = 0;
            int i = 1;
            foreach (var item in SanPhamList)
            {
                var MuaList = DataProvider.Ins.DB.ChiTietPhieuMuas.Where(p => p.MaSP == item.MaSP);
                var BanList = DataProvider.Ins.DB.ChiTietPhieuBans.Where(p => p.MaSP == item.MaSP);

                int sumMua = 0;
                int sumBan = 0;

                if (MuaList != null && MuaList.Count() > 0)
                {
                    sumMua = (int)MuaList.Sum(p => p.SoLuong);
                }
                if (BanList != null && BanList.Count() > 0)
                {
                    sumBan = (int)BanList.Sum(p => p.SoLuong);
                }

                ImportVolume += sumMua;
                ExportVolume += sumBan;

                TonKho tonkho = new TonKho();
                tonkho.Stt = i;
                tonkho.Count = sumMua - sumBan;
                tonkho.SanPham = item;

                TonKhoList.Add(tonkho);
                i++;
            }
            Inventory = ImportVolume - ExportVolume;
        }
        private void InventoryManagementByDay()
        {
            var SanPhamList = DataProvider.Ins.DB.SanPhams;
            ImportVolume = 0;
            ExportVolume = 0;
            foreach (var item in SanPhamList)
            {
                var MuaList = DataProvider.Ins.DB.ChiTietPhieuMuas.Where(p => p.MaSP == item.MaSP && p.PhieuMua.NgayLap >= StartTotalImportDay && p.PhieuMua.NgayLap <= EndTotalImportDay);
                var BanList = DataProvider.Ins.DB.ChiTietPhieuBans.Where(p => p.MaSP == item.MaSP && p.PhieuBan.NgayLap >= StartTotalImportDay && p.PhieuBan.NgayLap <= EndTotalImportDay);

                int sumMua = 0;
                int sumBan = 0;

                if (MuaList != null && MuaList.Count() > 0)
                {
                    sumMua = (int)MuaList.Sum(p => p.SoLuong);
                    foreach(var x in MuaList)
                    {
                        MessageBox.Show(x.PhieuMua.NgayLap.ToString() + "  " + EndTotalImportDay.ToString());
                    }
                   
                }
                if (BanList != null && BanList.Count() > 0)
                {
                    sumBan = (int)BanList.Sum(p => p.SoLuong);
                }

                ImportVolume += sumMua;
                ExportVolume += sumBan;                              
            }
            Inventory = ImportVolume - ExportVolume;
        }


    }
    


}
