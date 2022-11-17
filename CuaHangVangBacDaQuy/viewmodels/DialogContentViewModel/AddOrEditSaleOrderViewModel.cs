using CuaHangVangBacDaQuy.models;
using CuaHangVangBacDaQuy.viewmodels.Converter;
using CuaHangVangBacDaQuy.views.userControlDialog;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace CuaHangVangBacDaQuy.viewmodels.DialogContentViewModel
{
    public class AddOrEditSaleOrderViewModel:BaseViewModel
    {
        #region
        // các biến cho view chính này

        private ObservableCollection<PhieuBan> _SaleOrdersList;
        public ObservableCollection<PhieuBan> SaleOrdersList
        {
            get => _SaleOrdersList;
            set
            {

                _SaleOrdersList = value;
                OnPropertyChanged();
            }
        }


        private decimal? _TotalMoney;
        public decimal? TotalMoney
        {
            get => _TotalMoney;

            set
            {
                _TotalMoney = value;
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
                if (SelectedSaleOrder != null)
                {
                    SelectedCustomer = SelectedSaleOrder.KhachHang;
                    SelectedProductList = new ObservableCollection<ChiTietPhieuBan>(SelectedSaleOrder.ChiTietPhieuBans);
                    TotalMoney = SelectedProductList.Sum(p => p.SoLuong * p.SanPham.DonGia);

                }
            }
        }

        private ObservableCollection<ChiTietPhieuBan> InsertProductsList;
        private ObservableCollection<ChiTietPhieuBan> DeletedProductsList;



        private readonly OpenDiaLog OpenThisDiaLog;  //tham chiếu để tắt bật dialog có view này
        private string _TitleView;
        public string TitleView { get => _TitleView; set { _TitleView = value; OnPropertyChanged(); } }

        public ICommand SaveCommand { get; set; }
        public ICommand CancelCommand { get; set; }




        public ICommand RemoveSelectedProductCommand { get; set; }
        public ICommand CaculateTotalMoneyCommand { get; set; } // dùng để tính lại tổng tiền hóa đơn khi nhập số lượng sản phẩm

        //Các biến cho việc chọn nhà cung cấp

        #region 

        private ObservableCollection<KhachHang> _SelectedCustomersList;
        public ObservableCollection<KhachHang> SelectedCustomersList
        {
            get => _SelectedCustomersList;
            set { _SelectedCustomersList = value; OnPropertyChanged(); }
        }


        private KhachHang _SelectedCustomer;
        public KhachHang SelectedCustomer
        {
            get
            {
                return _SelectedCustomer;
            }
            set
            {
                _SelectedCustomer = value;
                OnPropertyChanged();


                if (value != null && !SelectedCustomersList.Contains(value))
                {
                    SelectedCustomersList?.Clear();
                    SelectedCustomersList.Add(value);

                }


            }
        }

        private AddOrEditSupplierViewModel _ContentAddSupplier;
        public AddOrEditSupplierViewModel ContentAddSupplier
        {
            get => _ContentAddSupplier;
            set
            {
                _ContentAddSupplier = value;
                OnPropertyChanged();

            }
        }



        private OpenDiaLog _IsOpenAddSupplierDialog;
        public OpenDiaLog IsOpenAddSupplierDialog
        {
            get { return _IsOpenAddSupplierDialog; }
            set { _IsOpenAddSupplierDialog = value; OnPropertyChanged(); }
        }

        public ICommand AddSupplierCommand { get; set; }
        public ICommand RemoveSelectedSupplierCommand { get; set; }

        #endregion


        //Các biến cho việc chọn sản phẩm

        #region 

        private ObservableCollection<ChiTietPhieuBan> _SelectedProductList;
        public ObservableCollection<ChiTietPhieuBan> SelectedProductList
        {
            get => _SelectedProductList;
            set
            {

                _SelectedProductList = value;
                OnPropertyChanged();
            }
        }

        private SanPham _SelectedProductItem;
        public SanPham SelectedProductItem
        {
            get
            {
                return _SelectedProductItem;
            }
            set
            {
                _SelectedProductItem = value;

                OnPropertyChanged();

                if (SelectedProductItem != null)
                {


                    if (SelectedProductList.Where(x => x.SanPham == SelectedProductItem).Count() == 0)
                    {
                        ChiTietPhieuBan productAdded = new ChiTietPhieuBan()
                        {
                            MaSP = SelectedProductItem.MaSP,
                            SanPham = SelectedProductItem,
                            SoLuong = 0
                        };

                        SelectedProductList.Add(productAdded);
                        // nếu là thêm sản phẩm vào phiếu đang chỉnh sửa
                        if (SelectedSaleOrder != null && DataProvider.Ins.DB.ChiTietPhieuBans.Where(p => p.MaPhieu == SelectedSaleOrder.MaPhieu && p.MaSP == SelectedProductItem.MaSP).Count() == 0)
                        {

                            InsertProductsList.Add(productAdded);
                        }
                        SelectedProductItem = null;

                    }


                }


            }
        }



        private ChiTietPhieuBan _SelectedProductDataGrid;
        public ChiTietPhieuBan SelectedProductDataGrid
        {
            get
            {
                return _SelectedProductDataGrid;
            }
            set
            {
                _SelectedProductDataGrid = value;
                OnPropertyChanged();

            }
        }


        private OpenDiaLog _IsOpenAddProductDialog;
        public OpenDiaLog IsOpenAddProductDialog
        {
            get { return _IsOpenAddProductDialog; }
            set { _IsOpenAddProductDialog = value; OnPropertyChanged(); }
        }


        private AddOrEditProductViewModel _ContentAddProduct;
        public AddOrEditProductViewModel ContentAddProduct
        {
            get => _ContentAddProduct;
            set
            {
                _ContentAddProduct = value;
                OnPropertyChanged();

            }
        }

        public ICommand AddProductCommand { get; set; }



        #endregion





        #endregion

        public AddOrEditSaleOrderViewModel()
        {


        }
        //constructor cho việc tạo phiếu bán hàng mới 
        public AddOrEditSaleOrderViewModel(string titleView, ref OpenDiaLog openDiaLog, ref ObservableCollection<PhieuBan> saleOrdersList)
        {

            SelectedCustomersList = new ObservableCollection<KhachHang>();
            SelectedProductList = new ObservableCollection<ChiTietPhieuBan>();

            TitleView = titleView;
            OpenThisDiaLog = openDiaLog;
            SaleOrdersList = saleOrdersList;


            SaveCommand = new RelayCommand<AddOrEditSaleOrderUC>((p) => true, p => AddNewImportReceipt());
            CancelCommand = new RelayCommand<AddOrEditImportReceiptUC>((p) => true, p => CheckCloseDiaLog());
            RemoveSelectedProductCommand = new RelayCommand<DataGridTemplateColumn>(p => true, p => RemoveSelectedProduct("UNSAVED"));
            CaculateTotalMoneyCommand = new RelayCommand<DataGridTemplateColumn>(p => true, p => CaculateTotalMoney());



        }
        // constructor cho việc chỉnh sửa phiếu bán hàng

        public AddOrEditSaleOrderViewModel(string titleView, ref OpenDiaLog openDiaLog, ref ObservableCollection<PhieuBan> saleOrdersList, ref PhieuBan selectedSaleOrder)
        {
            InsertProductsList = new ObservableCollection<ChiTietPhieuBan>();
            DeletedProductsList = new ObservableCollection<ChiTietPhieuBan>();
            SelectedCustomersList = new ObservableCollection<KhachHang>();
            SelectedProductList = new ObservableCollection<ChiTietPhieuBan>();
            TitleView = titleView;
            OpenThisDiaLog = openDiaLog;
            SaleOrdersList = saleOrdersList;
            SelectedSaleOrder = selectedSaleOrder;


            SaveCommand = new RelayCommand<AddOrEditSaleOrderUC>((p) => true, p => EditImportReceipt());
            CancelCommand = new RelayCommand<AddOrEditSaleOrderUC>((p) => true, p => CheckCloseDiaLog());
            RemoveSelectedProductCommand = new RelayCommand<DataGridTemplateColumn>(p => true, p => RemoveSelectedProduct("SAVED"));
            CaculateTotalMoneyCommand = new RelayCommand<DataGridTemplateColumn>(p => true, p => CaculateTotalMoney());



        }

        #region Funtion for creating or editing the SaleOrder
        void RemoveSelectedProduct(string caseRemove)
        {
            switch (caseRemove)
            {
                case "UNSAVED":
                    {
                        if (SelectedProductDataGrid != null)
                        {
                            SelectedProductList.Remove(SelectedProductDataGrid);
                            CaculateTotalMoney();
                        }
                        break;
                    }
                case "SAVED":
                    {


                        ChiTietPhieuBan deletedProduct = SelectedProductDataGrid;

                        if (InsertProductsList.Contains(deletedProduct))
                        {
                            InsertProductsList.Remove(deletedProduct);
                        }
                        else
                            DeletedProductsList.Add(deletedProduct);

                        SelectedProductList.Remove(SelectedProductDataGrid);

                        CaculateTotalMoney();

                        break;
                    }

            }


        }

        void CaculateTotalMoney()
        {
            //(bool)value ? parameter : Binding.DoNothin


            TotalMoney = (TotalMoney == null) ? 0 : SelectedProductList.Sum(p => p.SoLuong * p.SanPham.DonGia);

        }

        bool CheckProductStock()
        {
            foreach (var product in SelectedProductList)
            {
                if(CaculateInventoryConverter.CaculateInventory(product.MaSP) <= 0)
                {
                    MessageBox.Show("Sản phẩm " + product.SanPham.TenSP + " đã hết hàng!", "", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return false;
                }

                if (CaculateInventoryConverter.CaculateInventory(product.MaSP) < product.SoLuong)
                {
                    MessageBox.Show("Sản phẩm " + product.SanPham.TenSP + " không đủ hàng!", "", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return false;
                }
            }
            return true;
        }

        private void AddNewImportReceipt()
        {
            

            if (!CheckValidFieldInDialog()) return;
            if(!CheckProductStock()) return;

            PhieuBan newSaleOrder = new PhieuBan()
            {
                MaPhieu = Guid.NewGuid().ToString(),
                NgayLap = DateTime.Now,
                MaKH = SelectedCustomer.MaKH,

            };

            DataProvider.Ins.DB.PhieuBans.Add(newSaleOrder);



            //Chi tiet phieu
            foreach (var item in SelectedProductList)
            {
                ChiTietPhieuBan newDetailSaleOrder = new ChiTietPhieuBan()
                {
                    MaChiTietPhieu = Guid.NewGuid().ToString(),
                    MaPhieu = newSaleOrder.MaPhieu,
                    MaSP = item.MaSP,
                    SoLuong = item.SoLuong,
                };

                DataProvider.Ins.DB.ChiTietPhieuBans.Add(newDetailSaleOrder);

            }
            DataProvider.Ins.DB.SaveChanges();
            SaleOrdersList.Add(newSaleOrder);
            OpenThisDiaLog.IsOpen = false;

        }
        private void EditImportReceipt()
        {
            OpenThisDiaLog.IsOpen = false;
            var editedSaleOrder = DataProvider.Ins.DB.PhieuBans.Where(i => i.MaPhieu == SelectedSaleOrder.MaPhieu).SingleOrDefault();
            editedSaleOrder.MaKH = SelectedCustomersList.First().MaKH;


            foreach (var item in DeletedProductsList)
            {
                var selectedProduct = DataProvider.Ins.DB.ChiTietPhieuBans.Where(p => p.MaPhieu == item.MaPhieu && p.MaSP == item.MaSP).SingleOrDefault();
                DataProvider.Ins.DB.ChiTietPhieuBans.Remove(selectedProduct);

            }
         
            foreach (var item in InsertProductsList)
            {

                ChiTietPhieuBan newDetailSaleOrder = new ChiTietPhieuBan()
                {
                    MaChiTietPhieu = Guid.NewGuid().ToString(),
                    MaPhieu = SelectedSaleOrder.MaPhieu,
                    MaSP = item.MaSP,
                    SoLuong = item.SoLuong,
                };

                DataProvider.Ins.DB.ChiTietPhieuBans.Add(newDetailSaleOrder);

            }


            DataProvider.Ins.DB.SaveChanges();
            SelectedSaleOrder.ChiTietPhieuBans = new ObservableCollection<ChiTietPhieuBan>(DataProvider.Ins.DB.ChiTietPhieuBans.Where(p => p.MaPhieu == SelectedSaleOrder.MaPhieu));
            editedSaleOrder.MaPhieu = editedSaleOrder.MaPhieu;

        }

        private bool CheckValidFieldInDialog()
        {
            if (SelectedCustomersList == null || SelectedCustomersList.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn nhà cung cấp!", "", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }
            if (SelectedProductList == null || SelectedProductList.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn sản phẩm nhập kho!", "", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }
            return true;
        }

        private void CheckCloseDiaLog()
        {

            if (MessageBox.Show("Những thay đổi của bạn sẽ không được lưu?", "",
                 MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
            {

                OpenThisDiaLog.IsOpen = false;

            }


        }
        #endregion
    }
}
