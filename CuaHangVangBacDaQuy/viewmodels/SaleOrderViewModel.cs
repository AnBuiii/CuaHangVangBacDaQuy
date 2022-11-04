using CuaHangVangBacDaQuy.models;
using CuaHangVangBacDaQuy.views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace CuaHangVangBacDaQuy.viewmodels
{
    public class SaleOrderViewModel : BaseViewModel
    {
        #region Commands
        public ICommand LoadedCommand { get; set; }
        public ICommand AddProductCommand { get; set; }
        public ICommand AddCommand { get; set; }
        public ICommand EditCommand { get; set; }
        public ICommand SaveAddCommand { get; set; }
        public ICommand SelectCustomerNextCommand { get; set; }
        #endregion
        #region Params
        private ObservableCollection<PhieuBan> _PhieuBanList;
        public ObservableCollection<PhieuBan> PhieuBanList { get => _PhieuBanList; set { _PhieuBanList = value; OnPropertyChanged(); } }
        private List<TonKho> _TonKhoList;
        public List<TonKho> TonKhoList { get => _TonKhoList; set { _TonKhoList = value; OnPropertyChanged(); } }
        private ObservableCollection<KhachHang> _CustomerList;
        public ObservableCollection<KhachHang> CustomerList { get => _CustomerList; set { _CustomerList = value; OnPropertyChanged(); } }

        private PhieuBan _SelectedItem;
        public PhieuBan SelectedItem
        {
            get => _SelectedItem;
            set
            {
                _SelectedItem = value;
                OnPropertyChanged();
                if(SelectedItem != null)
                {
                    SelectedCustomer = SelectedItem.KhachHang;
                    ChiTietPhieuBans = SelectedItem.ChiTietPhieuBans.ToList();
                    InitTonKhoList();
                }
            }
        }

        private KhachHang _SelectedCustomer;
        public KhachHang SelectedCustomer
        {
            get => _SelectedCustomer;
            
            set
            {
                _SelectedCustomer = value;
                OnPropertyChanged();   
            }
        }
        private List<ChiTietPhieuBan> _chiTietPhieuBans;
        public List<ChiTietPhieuBan> ChiTietPhieuBans
        {
            get => _chiTietPhieuBans;
            set
            {
                _chiTietPhieuBans = value;
                OnPropertyChanged();
            }
        }
        private ChiTietPhieuBan _SelectedChiTietPhieuBans;
        public ChiTietPhieuBan SelectedChiTietPhieuBans
        {
            get => _SelectedChiTietPhieuBans;
            set
            {
                _SelectedChiTietPhieuBans = value;
                OnPropertyChanged();
            }
        }




        private string _MaSanPham;
        public string MaSanPham { get => _MaSanPham; set { _MaSanPham = value; OnPropertyChanged(); } }
        private string _MaNhaCC;
        public string NhaCC { get => _MaNhaCC; set { _MaNhaCC = value; OnPropertyChanged(); } }
        private string _NgayLP;
        public string NgayLP { get => _NgayLP; set { _NgayLP = value; OnPropertyChanged(); } }

        
        #endregion
        private bool _IsOpenSelectProductDialog;
        public bool IsOpenSelectProductDialog
        {
            get { return _IsOpenSelectProductDialog; }
            set { _IsOpenSelectProductDialog = value; OnPropertyChanged(); }
        }
        private bool _IsOpenSelectCustomerDialog;
        public bool IsOpenSelectCustomerDialog
        {
            get { return _IsOpenSelectCustomerDialog; }
            set { _IsOpenSelectCustomerDialog = value; OnPropertyChanged(); }
        }


        public SaleOrderViewModel()
        {
            IsOpenSelectProductDialog = false;
            IsOpenSelectCustomerDialog = false;
            
            PhieuBanList = new ObservableCollection<PhieuBan>(DataProvider.Ins.DB.PhieuBans);
            CustomerList = new ObservableCollection<KhachHang>(DataProvider.Ins.DB.KhachHangs);
            AddCommand = new RelayCommand<MakeOderView>(p => true, p => Add());
            EditCommand = new RelayCommand<DataGridTemplateColumn>(p => true, p => Edit());
            SaveAddCommand = new RelayCommand<MakeOderView>(p => true, p => SaveAdd());
            SelectCustomerNextCommand = new RelayCommand<MakeOderView>(
                p =>
                {
                    if(SelectedItem != null)
                    {
                        if (SelectedItem.KhachHang == null) return false;
                    }
                    
                    return true;

                },
                p =>
                {
                    IsOpenSelectCustomerDialog = false;
                    IsOpenSelectProductDialog = true;
                }
            );
        }
        public void SaveAdd()
        {
            //lưu vào database
            //notify UI

        }
        private void Add()
        {
            //SelectedItem = new PhieuBan();
            //IsOpenSelectProductDialog = true;
            //if(SelectedChiTietPhieuBans != null)
            //{
            //    SelectedChiTietPhieuBans.SoLuong = 3;
            //}
            DataProvider.Ins.DB.SaveChanges();
            InitTonKhoList();
        }
        private void Edit()
        {
            IsOpenSelectCustomerDialog = true;
            SelectedCustomer = SelectedItem.KhachHang;
            IsOpenSelectCustomerDialog = true;

        }

        private void InitTonKhoList()
        {
            TonKhoList = new List<TonKho>();
            int i = 1;
            foreach (var item in DataProvider.Ins.DB.SanPhams)
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
