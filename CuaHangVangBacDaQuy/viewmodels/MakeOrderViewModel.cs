using CuaHangVangBacDaQuy.models;
using CuaHangVangBacDaQuy.viewmodels.DialogContentViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;


namespace CuaHangVangBacDaQuy.viewmodels
{
    public class MakeOrderViewModel : BaseViewModel
    {
        #region


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
                    SelectedSupplier = SelectedPurchaseOder.NhaCungCap;
                    SelectedProductList = new ObservableCollection<ProductAdded>();
                    foreach (var detail in SelectedPurchaseOder.ChiTietPhieuMuas)
                    {
                        SelectedProductList.Add(new ProductAdded() { Stt = SelectedProductList.Count, SanPham = detail.SanPham, Amount = (int)detail.SoLuong, IntoMoney = (decimal)detail.SanPham.DonGia, });
                    }
                    CaculateTotalMoney();
                }
            }
        }

        private bool _IsOpenDetailDialog;
        public bool IsOpenDetailDialog
        {
            get { return _IsOpenDetailDialog; }
            set { _IsOpenDetailDialog = value; OnPropertyChanged(); }
        }

        public ICommand AddPurchaseOrderCommand { get; set; }
        #endregion 

        string caseButtonSaveDialog { get; set; }

       //Các biến cho việc chọn nhà nhà cung cấp

        #region 

        private ObservableCollection<NhaCungCap> _SelectedSuppliersList;
        public ObservableCollection<NhaCungCap> SelectedSuppliersList
        {
            get => _SelectedSuppliersList;
            set { _SelectedSuppliersList = value; OnPropertyChanged(); }
        }


        private NhaCungCap _SelectedSupplier;
        public NhaCungCap SelectedSupplier
        {
            get
            {
                return _SelectedSupplier;
            }
            set
            {
                _SelectedSupplier = value;
                OnPropertyChanged();


                if (value != null && !SelectedSuppliersList.Contains(value))
                {
                    SelectedSuppliersList?.Clear();
                    SelectedSuppliersList.Add(value);

                }


            }
        }

        private AddSupplierViewModel _ContentAddSupplier;
        public AddSupplierViewModel ContentAddSupplier
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
        public ICommand EditSelectedSupplierCommand { get; set; }

        #endregion


        //Các biến cho việc chọn sản phẩm
        #region 

        private ObservableCollection<ProductAdded> _SelectedProductList;
        public ObservableCollection<ProductAdded> SelectedProductList
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
                        ProductAdded productAdded = new ProductAdded()
                        {
                            SanPham = SelectedProductItem,
                            Amount = 0,
                            IntoMoney = 0,

                        };


                        SelectedProductList.Add(productAdded);
                        SelectedProductItem = null;


                    }


                }


            }
        }



        private ProductAdded _SelectedProductDataGrid;
        public ProductAdded SelectedProductDataGrid
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


        private AddProductViewModel _ContentAddProduct;
        public AddProductViewModel ContentAddProduct
        {
            get => _ContentAddProduct;
            set
            {
                _ContentAddProduct = value;
                OnPropertyChanged();

            }
        }



        public ICommand AddProductCommand { get; set; }
        public ICommand RemoveSelectedProductCommand { get; set; }


        #endregion




        private decimal _TotalMoney;
        public decimal TotalMoney
        {
            get => _TotalMoney;

            set
            {

                _TotalMoney = value;
                OnPropertyChanged();
            }
        }

        public ICommand CaculateTotalMoneyCommand { get; set; }

        #endregion

        public MakeOrderViewModel()
        {

            AddPurchaseOrderCommand = new RelayCommand<MakeOrderViewModel>((p) => true, p => { IsOpenDetailDialog = true; });
            PhieuMuaList = new ObservableCollection<PhieuMua>(DataProvider.Ins.DB.PhieuMuas);


            SelectedSuppliersList = new ObservableCollection<NhaCungCap>();
            IsOpenAddSupplierDialog = new OpenDiaLog() { IsOpen = false };           
            AddSupplierCommand = new RelayCommand<MakeOrderViewModel>((p) => true, p => { OpenDialogAddSupplier(); });
            RemoveSelectedSupplierCommand = new RelayCommand<MakeOrderViewModel>((p) => true, p => { SelectedSuppliersList.Clear(); });


            SelectedProductList = new ObservableCollection<ProductAdded>();
            IsOpenAddProductDialog = new OpenDiaLog() { IsOpen = false };
            AddProductCommand = new RelayCommand<MakeOrderViewModel>(p => true, p => { OpenDialogAddProduct(); });
            RemoveSelectedProductCommand = new RelayCommand<DataGridTemplateColumn>(p => true, p => RemoveSelectedProduct());
            EditSelectedSupplierCommand = new RelayCommand<MakeOrderViewModel>((p) => true, p =>{caseButtonSaveDialog = "Edit";});


            CaculateTotalMoneyCommand = new RelayCommand<DataGridTemplateColumn>(p => true, p => CaculateTotalMoney());
           
        }



        public void OpenDialogAddSupplier()
        {

            ContentAddSupplier = new AddSupplierViewModel("Thêm nhà cung cấp mới", ref _IsOpenAddSupplierDialog, ref _SelectedSuppliersList);
            IsOpenAddSupplierDialog.IsOpen = true;
        }

        public void actionEditSupplier()
        {

        }



        public void OpenDialogAddProduct()
        {

            ContentAddProduct = new AddProductViewModel("Thêm sản phẩm mới", ref _IsOpenAddProductDialog, ref _SelectedProductList);
            IsOpenAddProductDialog.IsOpen = true;
        }

        void CaculateTotalMoney()
        {

            TotalMoney = SelectedProductList.Sum(p => p.IntoMoney);
        }

        void RemoveSelectedProduct()
        {
            if (SelectedProductDataGrid != null)
            {
                SelectedProductList.Remove(SelectedProductDataGrid);
                CaculateTotalMoney();
            }

        }
      

    }

}
