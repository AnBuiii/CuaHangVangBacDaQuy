using CuaHangVangBacDaQuy.models;
using CuaHangVangBacDaQuy.views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CuaHangVangBacDaQuy.viewmodels
{
    public class HomeViewModel : BaseViewModel
    {
        private ObservableCollection<TonKho> _TonKhoList;
        public ObservableCollection<TonKho> TonKhoList { get => _TonKhoList; set { _TonKhoList = value; OnPropertyChanged(); } }

        public ICommand LoadHomeView { get; set; }
        public HomeViewModel()
        {
            LoadHomeView = new RelayCommand<HomeView>((p) => true, (p) => LoadingHomeView(p));
        }

        private void LoadingHomeView(HomeView view)
        {
            TonKhoList = new ObservableCollection<TonKho>();

            var SanPhamList = DataProvider.Ins.DB.SanPhams;

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

                TonKho tonkho = new TonKho();
                tonkho.Stt = i;
                tonkho.Count = sumMua - sumBan;
                tonkho.SanPham = item;

                TonKhoList.Add(tonkho);
                i++;
            }
        }
    }
    


}
