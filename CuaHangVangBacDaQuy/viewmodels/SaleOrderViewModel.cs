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
using System.Windows.Controls;
using System.Windows.Input;

namespace CuaHangVangBacDaQuy.viewmodels
{
    public class SaleOrderViewModel : BaseViewModel
    {
        //Các biến cho thao tác trên view này
        #region các biến cho phiếu bán hàng


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

        public ICommand AddImportReceiptCommand { get; set; }
        public ICommand EditCommand { get; set; }
        #endregion

        public SaleOrderViewModel()
        {


            IsOpenMakeSaleOrderDialog = new OpenDiaLog() { IsOpen = false };
            SaleOrdersList = new ObservableCollection<PhieuBan>(DataProvider.Ins.DB.PhieuBans);
            AddImportReceiptCommand = new RelayCommand<SaleOrderView>((p) => true, p => ActionDiaLog("Add"));
            EditCommand = new RelayCommand<SaleOrderView>((p) => true, p => ActionDiaLog("Edit"));

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
    }
}
