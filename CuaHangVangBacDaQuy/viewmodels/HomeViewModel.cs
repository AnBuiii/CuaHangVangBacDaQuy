using CuaHangVangBacDaQuy.models;
using CuaHangVangBacDaQuy.views;
using LiveCharts;
using LiveCharts.Defaults;
using LiveCharts.Wpf;
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
        private object _currentReportView;
        public object CurrentReportView
        {
            get => _currentReportView;
            set
            {
                _currentReportView = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<TonKho> _TonKhoList;
        public ObservableCollection<TonKho> TonKhoList { get => _TonKhoList; set { _TonKhoList = value; OnPropertyChanged(); } }

        private ObservableCollection<SanPham> _SanPhamList;
        public ObservableCollection<SanPham> SanPhamList { get => _SanPhamList; set { _SanPhamList = value; OnPropertyChanged(); } }

        private ObservableCollection<KhachHang> _KhachHangList;
        public ObservableCollection<KhachHang> KhachHangList { get => _KhachHangList; set { _KhachHangList = value; OnPropertyChanged(); } }

        private ObservableCollection<NhaCungCap> _NCCList;
        public ObservableCollection<NhaCungCap> NCCList { get => _NCCList; set { _NCCList = value; OnPropertyChanged(); } }

        private decimal _ImportVolume;
        public decimal ImportVolume { get => _ImportVolume; set { _ImportVolume = value; OnPropertyChanged(); } }

        private decimal _ExportVolume;
        public decimal ExportVolume { get => _ExportVolume; set { _ExportVolume = value; OnPropertyChanged(); } }

        private decimal _Inventory;
        public decimal Inventory { get => _Inventory; set { _Inventory = value; OnPropertyChanged(); } }

        private SeriesCollection _SeriesCollection;
        public SeriesCollection SeriesCollection
        {
            get => _SeriesCollection;
            set { _SeriesCollection = value; OnPropertyChanged(); }
        }

        private int _OutOfStockCount;
        public int OutOfStockCount
        {
            get => _OutOfStockCount; set { _OutOfStockCount = value; OnPropertyChanged(); }
        }

        private KhachHang _KhachHangTop;
        public KhachHang KhachHangTop
        {
            get => _KhachHangTop; set { _KhachHangTop = value; OnPropertyChanged(); }
        }

        private decimal _KhachHangValueTop;
        public decimal KhachHangValueTop
        {
            get => _KhachHangValueTop; set { _KhachHangValueTop = value; OnPropertyChanged(); }
        }

        private NhaCungCap _NCCTop;
        public NhaCungCap NCCTop
        {
            get => _NCCTop; 
            set
            {
                _NCCTop = value; OnPropertyChanged();
            }
        }
        private decimal _NCCValueTop;
        public decimal NCCValueTop
        {
            get => _NCCValueTop; set { _NCCValueTop = value; OnPropertyChanged(); }
        }




        public ICommand LoadHomeView { get; set; }
        public ICommand TonKhoCommand { get; set; }
        public ICommand DoanhThuCommand { get; set; }

        public HomeViewModel()
        {
            LoadHomeView = new RelayCommand<HomeView>((p) => true, (p) => LoadingHomeView(p, GetImportVolume()));
            TonKhoCommand = new RelayCommand<HomeView>((p) => true, (p) => CurrentReportView = new InventoryReport());
            DoanhThuCommand = new RelayCommand<HomeView>((p) => true, (p) => CurrentReportView = new RevenueReport());


            CurrentReportView = new InventoryReport();


        }

        private decimal GetImportVolume()
        {
            return ImportVolume;
        }


        private void LoadingHomeView(HomeView view, decimal importVolume)
        {
            TonKhoList = new ObservableCollection<TonKho>();
            SanPhamList = new ObservableCollection<SanPham>(DataProvider.Ins.DB.SanPhams);
            NCCList = new ObservableCollection<NhaCungCap>(DataProvider.Ins.DB.NhaCungCaps);
            KhachHangList = new ObservableCollection<KhachHang>(DataProvider.Ins.DB.KhachHangs);
            ImportVolume = 0;
            ExportVolume = 0;
            OutOfStockCount = 0;
            KhachHangTop = new KhachHang();
            KhachHangValueTop = 0;
            NCCTop = new NhaCungCap();
            NCCValueTop = 0;
            //foreach (var item in SanPhamList)
            //{
            //    var MuaList = DataProvider.Ins.DB.ChiTietPhieuMuas.Where(p => p.MaSP == item.MaSP);
            //    var BanList = DataProvider.Ins.DB.ChiTietPhieuBans.Where(p => p.MaSP == item.MaSP);

            //    int sumMua = 0;
            //    int sumBan = 0;

            //    decimal moneyMua = 0;
            //    decimal moneyBan = 0;

            //    if (MuaList != null && MuaList.Count() > 0)
            //    {
            //        sumMua = (int)MuaList.Sum(p => p.SoLuong);
            //        moneyMua = (decimal)((decimal)MuaList.Sum(p => p.SoLuong) * item.DonGia);
            //    }
            //    if (BanList != null && BanList.Count() > 0)
            //    {
            //        sumBan = (int)BanList.Sum(p => p.SoLuong);
            //        moneyBan = (decimal)((decimal)BanList.Sum(p => p.SoLuong) * item.DonGia);
            //    }
            //    if (sumMua == sumBan) OutOfStockCount++;

            //    ImportVolume += moneyMua;
            //    ExportVolume += moneyBan;

            //    TonKho tonkho = new TonKho
            //    {
            //        Stt = i,
            //        Count = sumMua - sumBan,
            //        SanPham = item
            //    };

            //    TonKhoList.Add(tonkho);
            //    i++;
            //}
            Inventory = ImportVolume - ExportVolume;

            foreach (KhachHang khachHang in KhachHangList)
            {
                decimal khachHangValue = 0;
                foreach (ChiTietPhieuBan ct in DataProvider.Ins.DB.ChiTietPhieuBans.Where(x => x.PhieuBan.KhachHang.MaKH == khachHang.MaKH))
                {
                    khachHangValue += (decimal)((decimal)ct.SoLuong * ct.SanPham.DonGia);
                }
                if (khachHangValue > KhachHangValueTop)
                {
                    KhachHangTop = khachHang;
                    KhachHangValueTop = khachHangValue;
                }
            }

            foreach (NhaCungCap ncc in NCCList)
            {
                decimal nccValue = 0;
                foreach (ChiTietPhieuMua ct in DataProvider.Ins.DB.ChiTietPhieuMuas.Where(x => x.PhieuMua.NhaCungCap.MaNCC == ncc.MaNCC))
                {
                    nccValue += (decimal)((decimal)ct.SoLuong * ct.SanPham.DonGia);
                }
                if (nccValue > NCCValueTop)
                {
                    NCCTop= ncc;
                    NCCValueTop = nccValue;
                }
            }

            SeriesCollection = new SeriesCollection();
            foreach (TonKho ton in TonKhoList)
            {
                SeriesCollection.Add(new PieSeries
                {
                    Title = ton.SanPham.TenSP,
                    //Values = new ChartValues<ObservableValue> { new ObservableValue(ton.Count) },
                    DataLabels = true
                }
                );

            }

        }





    }



}
