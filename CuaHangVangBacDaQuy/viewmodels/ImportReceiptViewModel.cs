using CuaHangVangBacDaQuy.models;
using CuaHangVangBacDaQuy.viewmodels.DialogContentViewModel;
using CuaHangVangBacDaQuy.views;
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


        private ObservableCollection<PhieuMua> _ImportReceiptsList;
        public ObservableCollection<PhieuMua> ImportReceiptsList
        {
            get => _ImportReceiptsList;
            set
            {
                _ImportReceiptsList = value;
                OnPropertyChanged();
            }
        }

        private PhieuMua _SelectedImportReceipt;
        public PhieuMua SelectedImportReceipt
        {
            get => _SelectedImportReceipt;
            set
            {
                _SelectedImportReceipt = value;
                OnPropertyChanged();
               
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

        public ImportReceiptViewModel()
        {

            
           IsOpenMakeReceiptDialog = new OpenDiaLog() { IsOpen = false };
            ImportReceiptsList = new ObservableCollection<PhieuMua>(DataProvider.Ins.DB.PhieuMuas);
            AddImportReceiptCommand = new RelayCommand<ImportReceiptView>((p) => true, p => ActionDiaLog("Add"));
            EditCommand = new RelayCommand<ImportReceiptView>((p) => true, p => ActionDiaLog("Edit"));

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
            


            ContentAddOrEditImportReceipt = new AddOrEditImportReceiptViewModel("Đơn nhập hàng mới", ref _IsOpenMakeReceiptDialog, ref _ImportReceiptsList);
            addOrEditImportReceiptUC = new AddOrEditImportReceiptUC
            {
                DataContext = ContentAddOrEditImportReceipt
            };

            //ContentAddOrEditImportReceipt = 
        }

        private void EditImportReceipt()
        {

            ContentAddOrEditImportReceipt = new AddOrEditImportReceiptViewModel("Đơn nhập hàng mới", ref _IsOpenMakeReceiptDialog, ref _ImportReceiptsList, ref _SelectedImportReceipt);
            addOrEditImportReceiptUC = new AddOrEditImportReceiptUC
            {
                DataContext = ContentAddOrEditImportReceipt
            };
            //ContentAddSupplier = new AddOrEditSupplierViewModel("Chỉnh sửa thông tin nhà cung cấp", ref _IsOpenDiaLog, ref _SuppliersList, ref _SelectedSupplier);

        }
    }
}
