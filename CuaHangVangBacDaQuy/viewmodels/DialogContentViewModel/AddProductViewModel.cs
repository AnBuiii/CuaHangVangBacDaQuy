using CuaHangVangBacDaQuy.models;
using CuaHangVangBacDaQuy.views.userControl;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace CuaHangVangBacDaQuy.viewmodels.DialogContentViewModel
{
    public class AddProductViewModel:BaseViewModel
    {
        private readonly ObservableCollection<SanPham> ProductList;
        private readonly ObservableCollection<ProductAdded> ProductAddedList;

        private ObservableCollection<LoaiSanPham> _TypeProductList;
        public ObservableCollection<LoaiSanPham> TypeProductList { get => _TypeProductList; set { _TypeProductList = value; OnPropertyChanged(); } }
        private ObservableCollection<DonVi> _UnitList;
        public ObservableCollection<DonVi> UnitList { get => _UnitList; set { _UnitList = value; OnPropertyChanged(); } }

        private LoaiSanPham _SelectedTypeProduct;
        public LoaiSanPham SelectedTypeProduct { get => _SelectedTypeProduct; set { _SelectedTypeProduct = value; OnPropertyChanged(); } }
        private DonVi _SelectedUnit;
        public DonVi SelectedUnit { get => _SelectedUnit; set { _SelectedUnit = value; OnPropertyChanged(); } }

        private SanPham _EditedProduct;
        public SanPham EditedProduct
        {
            get => _EditedProduct;

            set
            {
                _EditedProduct = value;
                OnPropertyChanged();

                if (EditedProduct != null)
                {
                    ProductName = value.TenSP;
                    ProductPrice = (decimal)value.DonGia;
                    SelectedTypeProduct = value.LoaiSanPham;
                    SelectedUnit = value.DonVi;
                }
            }
        }

        private readonly OpenDiaLog openDiaLog;

        private string _TilteView;
        public string TilteView { get => _TilteView; set { _TilteView = value; OnPropertyChanged(); } }


        private string _ProductName;
        public string ProductName
        {
            get => _ProductName;
            set
            {
                _ProductName = value; OnPropertyChanged();
            }
        }


        private decimal _ProductPrice = 0;
        public decimal ProductPrice
        {
            get => _ProductPrice;
            set
            {
                _ProductPrice = value; OnPropertyChanged();
            }
        }

        public ICommand SaveCommand { get; set; }
        public ICommand CancelCommand { get; set; }


        public AddProductViewModel()
        {

        }

        //constructor dùng cho thêm sản phẩm mới từ màn hình quản lý sản phẩm
        public AddProductViewModel(string tilteView, ref OpenDiaLog isOpenDialog, ref ObservableCollection<SanPham> productsList)
        {

            TypeProductList = new ObservableCollection<LoaiSanPham>(DataProvider.Ins.DB.LoaiSanPhams);
            UnitList = new ObservableCollection<DonVi>(DataProvider.Ins.DB.DonVis);
            TilteView = tilteView;
            openDiaLog = isOpenDialog;
            ProductList = productsList;
            CancelCommand = new RelayCommand<AddProductUC>((p) => true, p => CheckCloseDiaLog());
            SaveCommand = new RelayCommand<AddProductUC>((p) => CheckEmptyFieldDialog(), p => ActionAddProduct());



        }

        //constructor dùng cho chỉnh sủa sản phẩm mới từ màn hình quản lý sản phẩm
        public AddProductViewModel(string tilteView, ref OpenDiaLog isOpenDialog, ref ObservableCollection<SanPham> productsList, ref SanPham editedProduct)
        {
            TypeProductList = new ObservableCollection<LoaiSanPham>(DataProvider.Ins.DB.LoaiSanPhams);
            UnitList = new ObservableCollection<DonVi>(DataProvider.Ins.DB.DonVis);
            TilteView = tilteView;
            openDiaLog = isOpenDialog;
            ProductList = productsList;
            EditedProduct = editedProduct;
            CancelCommand = new RelayCommand<AddCustomerUC>((p) => true, p => CheckCloseDiaLog());
            SaveCommand = new RelayCommand<AddCustomerUC>((p) => CheckEmptyFieldDialog(), p => ActionEditProduct());


        }

        //constructor dùng cho thêm sản phẩm mới từ màn hình tạo đơn mua hàng mới(danh sách sản phẩm là list các sản phẩm được chọn để tạo đơn trong datagridview )
        public AddProductViewModel(string tilteView, ref OpenDiaLog isOpenDialog, ref ObservableCollection<ProductAdded> productsAddedList)
        {

            TypeProductList = new ObservableCollection<LoaiSanPham>(DataProvider.Ins.DB.LoaiSanPhams);
            UnitList = new ObservableCollection<DonVi>(DataProvider.Ins.DB.DonVis);
            TilteView = tilteView;
            openDiaLog = isOpenDialog;
            ProductAddedList = productsAddedList;
            CancelCommand = new RelayCommand<AddProductUC>((p) => true, p => CheckCloseDiaLog());
            SaveCommand = new RelayCommand<AddProductUC>((p) => CheckEmptyFieldDialog(), p => ActionAddProduct());

        }


        //constructor dùng cho chỉnh sủa sản phẩm mới từ màn hình tạo đơn mua hàng


        bool CheckEmptyFieldDialog()
        {
            if (string.IsNullOrWhiteSpace(ProductName) || string.IsNullOrEmpty(ProductPrice.ToString()) || ProductPrice.ToString() == "0" || SelectedTypeProduct == null || SelectedUnit == null) return false;
            return true;
        }

        private void ActionAddProduct()
        {
           
            var newProduct = new SanPham()
            {
                MaSP = Guid.NewGuid().ToString(),
                TenSP = ProductName,
                DonGia = ProductPrice,
                MaLoaiSP = SelectedTypeProduct.MaLoaiSP,
                MaDV = SelectedUnit.MaDV,
                
            };

            DataProvider.Ins.DB.SanPhams.Add(newProduct);
            DataProvider.Ins.DB.SaveChanges();

            //nếu constuctor được khởi tạo từ view quản lý sản phẩm
            if(ProductList != null)
            {
                ProductList.Add(newProduct);
            }

            //nếu constuctor được khởi tạo từ view tạo đơn mua hàng
            if(ProductAddedList != null)
            {

                ProductAdded productAdded = new ProductAdded(){
                    SanPham = newProduct,
                    Amount = 0,
                    IntoMoney = 0,

                };            
                ProductAddedList.Add(productAdded);
            }
            openDiaLog.IsOpen = false;


        }

        private void ActionEditProduct()
        {
           
            openDiaLog.IsOpen = false;
            var editedProduct = DataProvider.Ins.DB.SanPhams.Where(x => x.MaSP == EditedProduct.MaSP).SingleOrDefault();
            editedProduct.TenSP = ProductName;
            editedProduct.DonGia = ProductPrice;
            editedProduct.MaLoaiSP = SelectedTypeProduct.MaLoaiSP;
            editedProduct.MaDV = SelectedUnit.MaDV;
            DataProvider.Ins.DB.SaveChanges();
        }


       
        private void CheckCloseDiaLog()
        {

            if (MessageBox.Show("Những thay đổi của bạn sẽ không được lưu?", "",
                 MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
            {

                openDiaLog.IsOpen = false;

            }
            // IsOpenDialog = false;


        }
    }
}
