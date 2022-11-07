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


        string caseButtonSaveDialog { get; set; }

       
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


                if (value != null && !SelectedSuppliersList.Contains(value) )
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
                    caculateTotalMoney();
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
        private bool _IsOpenDetailDialog;
        public bool IsOpenDetailDialog
        {
            get { return _IsOpenDetailDialog; }
            set { _IsOpenDetailDialog = value; OnPropertyChanged(); }
        }

        private bool _IsOpenProductDialog;
        public bool IsOpenProductDialog
        {
            get { return _IsOpenProductDialog; }
            set { _IsOpenProductDialog = value; OnPropertyChanged(); }
        }


        private string _NewProductName;
        public string NewProductName
        {
            get => _NewProductName;
            set
            {
                _NewProductName = value; OnPropertyChanged();
            }
        }



        private decimal? _NewProductPrice;
        public decimal? NewProductPrice
        {
            get => _NewProductPrice;
            set
            {
                _NewProductPrice = value; OnPropertyChanged();
            }

        }


        private ObservableCollection<LoaiSanPham> _ProductTypeList;
        public ObservableCollection<LoaiSanPham> ProductTypeList { get => _ProductTypeList; set { _ProductTypeList = value; OnPropertyChanged(); } }

        private LoaiSanPham _SelectedProductType;
        public LoaiSanPham SelectedProductType
        {
            get => _SelectedProductType;
            set
            {
                _SelectedProductType = value; OnPropertyChanged();
            }

        }


        private ObservableCollection<DonVi> _ProductUnitList;
        public ObservableCollection<DonVi> ProductUnitList { get => _ProductUnitList; set { _ProductUnitList = value; OnPropertyChanged(); } }

        private DonVi _SelectedProductUnit;
        public DonVi SelectedProductUnit
        {
            get => _SelectedProductUnit;
            set
            {
                _SelectedProductUnit = value; OnPropertyChanged();
            }

        }


        public ICommand AddPurchaseOrderCommand { get; set; }
        public ICommand AddSupplierCommand { get; set; }
       
        public ICommand RemoveSelectedSupplierCommand { get; set; }
        public ICommand RemoveSelectedProductCommand { get; set; }
        public ICommand EditSelectedSupplierCommand { get; set; }
        public ICommand SaveSupplierCommand { get; set; }
        public ICommand AddProductCommand { get; set; }
        public ICommand CancelAddSupplierDiaLogCommand { get; set; }
        public ICommand CaculateTotalMoneyCommand { get; set; }
        public ICommand SaveProductCommand { get; set; }
        public ICommand EditCommand { get; set; }




        #endregion

        public MakeOrderViewModel()
        {




            PhieuMuaList = new ObservableCollection<PhieuMua>(DataProvider.Ins.DB.PhieuMuas);
            SelectedSuppliersList = new ObservableCollection<NhaCungCap>();
            IsOpenAddSupplierDialog = new OpenDiaLog() { IsOpen = false };
            AddPurchaseOrderCommand = new RelayCommand<MakeOrderViewModel>((p) => true, p => { IsOpenDetailDialog = true; });



            AddSupplierCommand = new RelayCommand<MakeOrderViewModel>((p) => true, p => { actionAddSupplier(); });

            {
                switch (caseButtonSaveDialog)
                {

                    case "Edit":
                        {
                            actionEditSupplier(); break;
                        }

                    case "Add":
                        {
                            actionAddSupplier(); break;
                        }
                }
            };
        

            CancelAddSupplierDiaLogCommand = new RelayCommand<MakeOrderViewModel>(
               (p) => true, p => CheckCloseProductDiaLog()
           );

            SelectedProductList = new ObservableCollection<ProductAdded>();
            AddProductCommand = new RelayCommand<MakeOrderViewModel>(p => true, p => AddProduct());

            RemoveSelectedSupplierCommand = new RelayCommand<MakeOrderViewModel>((p) => true, p => { SelectedSuppliersList.Clear(); });

            EditSelectedSupplierCommand = new RelayCommand<MakeOrderViewModel>((p) => true, p =>
            {

                caseButtonSaveDialog = "Edit";
               // IsOpenAddSupplierDialog = true;

            });

            ProductTypeList = new ObservableCollection<LoaiSanPham>(DataProvider.Ins.DB.LoaiSanPhams);
            ProductUnitList = new ObservableCollection<DonVi>(DataProvider.Ins.DB.DonVis);
            SaveProductCommand = new RelayCommand<MakeOrderViewModel>(
                (p) =>
                {
                    if (CheckValidProductDiaLog()) return true;
                    return false;

                }

                 , p => SaveAddNewProduct()
            );
            CaculateTotalMoneyCommand = new RelayCommand<DataGridTemplateColumn>(p => true, p => caculateTotalMoney());
            RemoveSelectedProductCommand = new RelayCommand<DataGridTemplateColumn>(p => true, p => removeSelectedProduct());
            EditCommand = new RelayCommand<DataGridTemplateColumn>(p => true, p => Edit());


        }



        public void actionAddSupplier()
        {
            
            

            ContentAddSupplier = new AddSupplierViewModel("Thêm nhà cung cấp mới", ref _IsOpenAddSupplierDialog,  ref _SelectedSuppliersList);
            IsOpenAddSupplierDialog.IsOpen = true;
           

        }

        public void actionEditSupplier()
        {

        }

      
        void caculateTotalMoney()
        {

            TotalMoney = SelectedProductList.Sum(p => p.IntoMoney);
        }

        void removeSelectedProduct()
        {
            if (SelectedProductDataGrid != null)
            {
                SelectedProductList.Remove(SelectedProductDataGrid);
                caculateTotalMoney();
            }


        }
        private void Edit()
        {
            IsOpenDetailDialog = true;
        }


        private void CheckCloseProductDiaLog()
        {

            if (MessageBox.Show("Những thay đổi của bạn sẽ không được lưu?", "",
                 MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
            {
               // IsOpenAddSupplierDialog = false;
            }


        }

      

        void AddProduct()
        {
            ClearFieldAddProductDialog();
            SelectedProductItem = null;
            IsOpenProductDialog = true;

        }

        void ClearFieldAddProductDialog()
        {
            NewProductName = "";
            NewProductPrice = null;
            SelectedProductType = null;
            SelectedProductUnit = null;

        }

        bool CheckValidProductDiaLog()
        {
            if (string.IsNullOrWhiteSpace(NewProductName) || string.IsNullOrEmpty(NewProductPrice.ToString()) || NewProductPrice.ToString() == "0" || SelectedProductType == null || SelectedProductUnit == null) return false;
            return true;
        }
        public void SaveAddNewProduct()
        {

            var newProduct = new SanPham()
            {
                MaSP = Guid.NewGuid().ToString(),
                TenSP = NewProductName,
                DonGia = NewProductPrice,
                MaLoaiSP = SelectedProductType.MaLoaiSP,
                MaDV = SelectedProductUnit.MaDV
            };

            DataProvider.Ins.DB.SanPhams.Add(newProduct);
            DataProvider.Ins.DB.SaveChanges();
            SelectedProductList.Add(
                             new ProductAdded()
                             {
                                 SanPham = newProduct,
                                 Amount = 0,
                                 IntoMoney = 0

                             });

                //Sele = new ObservableCollection<SanPham>(DataProvider.Ins.DB.SanPhams);
                IsOpenProductDialog = false;


        }


    }
}
