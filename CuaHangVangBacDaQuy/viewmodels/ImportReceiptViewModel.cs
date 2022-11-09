using CuaHangVangBacDaQuy.models;
using CuaHangVangBacDaQuy.viewmodels.DialogContentViewModel;
using CuaHangVangBacDaQuy.views.userControlDialog;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CuaHangVangBacDaQuy.viewmodels
{
    public class ImportReceiptViewModel : BaseViewModel
    {

        //Các biến cho thao tác trên view này
        #region các biến cho phiếu mua hàng 


        private ObservableCollection<PhieuMua> _PurchaseOrdersList;
        public ObservableCollection<PhieuMua> PurchaseOrdersList
        {
            get => _PurchaseOrdersList;
            set
            {
                _PurchaseOrdersList = value;
                OnPropertyChanged();
            }
        }

        private PhieuMua _SelectedPurchaseOder;
        public PhieuMua SelectedPurchaseOder
        {
            get => _SelectedPurchaseOder;
            set
            {
                _SelectedPurchaseOder = value;
                OnPropertyChanged();
                if (SelectedPurchaseOder != null)
                {
                    //SelectedSupplier = SelectedPurchaseOder.NhaCungCap;
                    //SelectedProductList = new ObservableCollection<ProductAdded>();
                    //foreach (var detail in SelectedPurchaseOder.ChiTietPhieuMuas)
                    //{
                    //    SelectedProductList.Add(new ProductAdded() { Stt = SelectedProductList.Count, SanPham = detail.SanPham, Amount = (int)detail.SoLuong, IntoMoney = (decimal)detail.SanPham.DonGia, });
                    //}
                    //CaculateTotalMoney();
                }
            }
        }


        private AddOrEditImportReceiptUC _addOrEditImportReceiptUC;
        public AddOrEditImportReceiptUC addOrEditImportReceiptUC { get => _addOrEditImportReceiptUC; set { _addOrEditImportReceiptUC = value; OnPropertyChanged(); } }

        private AddOrEditImportReceiptViewModel ContentAddOrEditImportReceipt;
       
        private OpenDiaLog _IsOpenMakeReceiptDialog;
        public OpenDiaLog IsOpenMakeReceiptDialog
        {
            get { return _IsOpenMakeReceiptDialog; }
            set { _IsOpenMakeReceiptDialog = value; OnPropertyChanged(); }
        }

        public ICommand AddImportReceiptCommand { get; set; }
        public ICommand EditCommand { get; set; }
        #endregion


        private object MainDataTemplate;
        public ImportReceiptViewModel(ref object mainDataTemplate)
        {   
            MainDataTemplate = mainDataTemplate;
        }
        public ImportReceiptViewModel()
        {

            
           IsOpenMakeReceiptDialog = new OpenDiaLog() { IsOpen = false };
            PurchaseOrdersList = new ObservableCollection<PhieuMua>(DataProvider.Ins.DB.PhieuMuas);
            AddImportReceiptCommand = new RelayCommand<MakeOrderViewModel>((p) => true, p => ActionDiaLog("Add"));
            EditCommand = new RelayCommand<MakeOrderViewModel>((p) => true, p => ActionDiaLog("Edit"));

        }

        private void ActionDiaLog(string caseDiaLog)
        {
            IsOpenMakeReceiptDialog.IsOpen = true;
            switch (caseDiaLog)
            {
                case "Add":
                    AddNewImportReceipt();
                    break;

                case "Edit":
                    EditImportReceipt();
                    break;
            }
        }

        private void AddNewImportReceipt()
        {
            MainDataTemplate = new AddOrEditImportReceiptViewModel("Đơn nhập hàng mới", ref _IsOpenMakeReceiptDialog, ref _PurchaseOrdersList);


            ContentAddOrEditImportReceipt = new AddOrEditImportReceiptViewModel("Đơn nhập hàng mới", ref _IsOpenMakeReceiptDialog, ref _PurchaseOrdersList);
            addOrEditImportReceiptUC = new AddOrEditImportReceiptUC
            {
                DataContext = ContentAddOrEditImportReceipt
            };

            //ContentAddOrEditImportReceipt = 
        }

        private void EditImportReceipt()
        {
            //ContentAddSupplier = new AddOrEditSupplierViewModel("Chỉnh sửa thông tin nhà cung cấp", ref _IsOpenDiaLog, ref _SuppliersList, ref _SelectedSupplier);

        }
    }
}
