using CuaHangVangBacDaQuy.models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CuaHangVangBacDaQuy.viewmodels
{
    public class ImportReceiptViewModel:BaseViewModel
    {

        //Các biến cho thao tác trên view này
        #region các biến cho phiếu mua hàng 


        private ObservableCollection<PhieuMua> _PhieuMuaList;
        public ObservableCollection<PhieuMua> PhieuMuaList
        {
            get => _PhieuMuaList;
            set
            {

                _PhieuMuaList = value;
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

        private OpenDiaLog _IsOpenMakeOderDialog;
        public OpenDiaLog IsOpenMakeOderDialog
        {
            get { return _IsOpenMakeOderDialog; }
            set { _IsOpenMakeOderDialog = value; OnPropertyChanged(); }
        }

        public ICommand AddPurchaseOrderCommand { get; set; }
        #endregion 

        public ImportReceiptViewModel()
        {
            AddPurchaseOrderCommand = new RelayCommand<MakeOrderViewModel>((p) => true, p => { IsOpenMakeOderDialog.IsOpen = true; });
            PhieuMuaList = new ObservableCollection<PhieuMua>(DataProvider.Ins.DB.PhieuMuas);


        }
    }
}
