using CuaHangVangBacDaQuy.models;
using CuaHangVangBacDaQuy.viewmodels.DialogContentViewModel;
using CuaHangVangBacDaQuy.views;
using CuaHangVangBacDaQuy.views.userControlDialog;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media.Animation;
using MessageBox = System.Windows.MessageBox;

namespace CuaHangVangBacDaQuy.viewmodels
{
    public class SaleOrderViewModel : BaseViewModel
    {
        //Các biến cho thao tác trên view này
        #region các biến cho phiếu bán hàng

        private decimal? _RemainMoney;
        public decimal? RemainMoney
        {
            get => _RemainMoney;
            set { _RemainMoney = value; OnPropertyChanged(); }
        }
        private string _PaidMoney;
        public string PaidMoney
        {
            get => _PaidMoney;
            set { _PaidMoney = value; OnPropertyChanged();
                CalculateMoney();
            }
        }
        private int _TotalMoney;
        public int TotalMoney
        {
            get => _TotalMoney;
            set { _TotalMoney = value; OnPropertyChanged(); }
        }

        private ObservableCollection<PhieuBan> _SaleOrderssList;
        public ObservableCollection<PhieuBan> SaleOrdersList
        {
            get => _SaleOrderssList;
            set
            {
                _SaleOrderssList = value;
                OnPropertyChanged();
            }
        }

        private PhieuBan _SelectedSaleOrder;
        public PhieuBan SelectedSaleOrder
        {
            get => _SelectedSaleOrder;
            set
            {
                _SelectedSaleOrder = value;
                OnPropertyChanged();
               
            }
        }


        private AddOrEditSaleOrderUC _addOrEditSaleOrderUC;
        public AddOrEditSaleOrderUC addOrEditSaleOrderUC { get => _addOrEditSaleOrderUC; set { _addOrEditSaleOrderUC = value; OnPropertyChanged(); } }

        

        private AddOrEditSaleOrderViewModel ContentAddOrEditSaleOrder;

        private OpenDiaLog _IsOpenMakeSaleOrderDialog;
        public OpenDiaLog IsOpenMakeSaleOrderDialog
        {
            get { return _IsOpenMakeSaleOrderDialog; }
            set { _IsOpenMakeSaleOrderDialog = value; OnPropertyChanged(); }
        }

        private OpenDiaLog _IsOpenPayDialog;
        public OpenDiaLog IsOpenPayDialog
        {
            get => _IsOpenPayDialog;
            set { _IsOpenPayDialog = value; OnPropertyChanged(); }
        }

        private string _contentSearch;
        public string ContentSearch
        {
            get { return _contentSearch; }
            set
            {
                _contentSearch = value;
                OnPropertyChanged();
                if (ContentSearch == null) ContentSearch = "";
            }
        }
        private string _selectedSearchType;
        public string SelectedSearchType { get { return _selectedSearchType; } set { _selectedSearchType = value; OnPropertyChanged(); } }

        private List<string> _searchTypes;
        public List<string> SearchTypes { get { return _searchTypes; } set { _searchTypes = value; OnPropertyChanged(); } }


        public ICommand AddImportReceiptCommand { get; set; }
        public ICommand EditCommand { get; set; }
        public ICommand DeleteCommand { get; set; }
        public ICommand PaidCommand { get; set; }
        public ICommand SearchCommand { get; set; }
        public ICommand CancelCommand { get; set; }
        public ICommand SaveCommand { get; set; }
        #endregion

        public SaleOrderViewModel()
        {


            IsOpenMakeSaleOrderDialog = new OpenDiaLog() { IsOpen = false };
            IsOpenPayDialog = new OpenDiaLog() { IsOpen = false };
            SaleOrdersList = new ObservableCollection<PhieuBan>(DataProvider.Ins.DB.PhieuBans);
            SearchTypes = new List<string> { "Mã phiếu", "Khách hàng", };
            SelectedSearchType = SearchTypes[1];
            AddImportReceiptCommand = new RelayCommand<SaleOrderView>((p) => true, p => ActionDiaLog("Add"));
            EditCommand = new RelayCommand<SaleOrderView>((p) => true, p => ActionDiaLog("Edit"));
            PaidCommand = new RelayCommand<SaleOrderView>((p) => true, p => PaidSaleOrder());
            CancelCommand = new RelayCommand<SaleOrderView>((p) => true, p => IsOpenPayDialog.IsOpen = false);
            DeleteCommand = new RelayCommand<SaleOrderView>((p) => true, p => DeleteSaleOrder());
            SearchCommand = new RelayCommand<DataGridTemplateColumn>(p => true, p => Search());
            SaveCommand = new RelayCommand<SaleOrderView>((p) => true, p => Pay());

        }






        private void ActionDiaLog(string caseDiaLog)
        {
            IsOpenMakeSaleOrderDialog.IsOpen = true;
            switch (caseDiaLog)
            {
                case "Add":
                    AddNewSaleOrder();
                    break;

                case "Edit":
                    EditSaleOrder();
                    break;
            }
        }

        private void Pay()
        {
            if(!int.TryParse(PaidMoney, out int a))
            {
                MessageBox.Show("Số tiền thanh toán không hợp lệ");
                return;
            }
            if(a <= 0)
            {
                MessageBox.Show("Số tiền thanh toán phải lớn hơn 0");
                return; 
            }
            if(a > RemainMoney)
            {
                MessageBox.Show("Số tiền thanh toán vượt quá số tiền nợ");
                return;

            }
            SelectedSaleOrder.ThanhToan += a;
            MessageBox.Show("Thanh toán thành công");
            DataProvider.Ins.DB.SaveChanges();
            IsOpenPayDialog.IsOpen = false;
            SelectedSaleOrder.MaPhieu = SelectedSaleOrder.MaPhieu;

        }

        private void CalculateMoney()
        {
            if(int.TryParse(PaidMoney, out int a))
            {
                TotalMoney = (int)(RemainMoney - a);
            } else TotalMoney = 0;
        }
        private void PaidSaleOrder()
        {
            
            IsOpenPayDialog.IsOpen = true;
            TotalMoney = 0;
            RemainMoney = 0;
            foreach(ChiTietPhieuBan products in SelectedSaleOrder.ChiTietPhieuBans)
            {
                RemainMoney += products.SoLuong * products.SanPham.DonGia * (1 + products.SanPham.LoaiSanPham.LoiNhuan);
            }
            RemainMoney = (int) RemainMoney - SelectedSaleOrder.ThanhToan;
            PaidMoney = "0";


        }
        private void AddNewSaleOrder()
        {
            ContentAddOrEditSaleOrder = new AddOrEditSaleOrderViewModel("Phiếu bán hàng mới", ref _IsOpenMakeSaleOrderDialog, ref _SaleOrderssList);

            addOrEditSaleOrderUC = new AddOrEditSaleOrderUC { 
                DataContext = ContentAddOrEditSaleOrder
            };
        }

        private void EditSaleOrder()
        {
            ContentAddOrEditSaleOrder = new AddOrEditSaleOrderViewModel("Chỉnh sửa phiếu bán hàng", ref _IsOpenMakeSaleOrderDialog, ref _SaleOrderssList, ref _SelectedSaleOrder);
            addOrEditSaleOrderUC = new AddOrEditSaleOrderUC
            {
                DataContext = ContentAddOrEditSaleOrder
            };
        }
        public bool isTest;
        public void DeleteSaleOrder()
        {
            if (!isTest)
            {
                if (MessageBox.Show("Bạn có chắc chắc muốn xóa phiếu mua " + SelectedSaleOrder.MaPhieu + " không?", "", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.No)
                {
                    return;
                }
            }
            
            ObservableCollection<ChiTietPhieuBan> deleteChiTietPhieuBans = new ObservableCollection<ChiTietPhieuBan>(DataProvider.Ins.DB.ChiTietPhieuBans.Where(x => x.MaPhieu == SelectedSaleOrder.MaPhieu));
            foreach (ChiTietPhieuBan ctphieu in deleteChiTietPhieuBans)
            {
                DataProvider.Ins.DB.ChiTietPhieuBans.Remove(ctphieu);
                DataProvider.Ins.DB.SaveChanges();
            }
            DataProvider.Ins.DB.PhieuBans.Remove(SelectedSaleOrder);
            DataProvider.Ins.DB.SaveChanges();
            SaleOrdersList.Remove(SelectedSaleOrder);
        }
        public void Search()
        {
            switch (SelectedSearchType)
            {
                case "Mã phiếu":
                    SaleOrdersList = new ObservableCollection<PhieuBan>(
                        DataProvider.Ins.DB.PhieuBans.Where(
                            x => x.MaPhieu.ToString().Contains(ContentSearch)));
                    break;
                case "Khách hàng":
                    SaleOrdersList = new ObservableCollection<PhieuBan>(
                         DataProvider.Ins.DB.PhieuBans.Where(
                             x => x.KhachHang.TenKH.ToString().Contains(ContentSearch)));
                    break;
                default:
                    break;
            }
        }
    }
}
