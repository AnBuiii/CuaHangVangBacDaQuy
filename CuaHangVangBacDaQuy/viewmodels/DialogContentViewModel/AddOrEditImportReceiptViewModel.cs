using CuaHangVangBacDaQuy.models;
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
    public class AddOrEditImportReceiptViewModel : BaseViewModel
    {
        #region
        // các biến cho view chính này

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

        private PhieuMua _SelectedImportReceipt;
        public PhieuMua SelectedImportReceipt
        {
            get => _SelectedImportReceipt;
            set
            {
                _SelectedImportReceipt = value;
                OnPropertyChanged();
                if (SelectedImportReceipt != null)
                {
                    SelectedSupplier = SelectedImportReceipt.NhaCungCap;
                    SelectedProductList = new ObservableCollection<ChiTietPhieuMua>(SelectedImportReceipt.ChiTietPhieuMuas);
                    TotalMoney = SelectedProductList.Sum(p => p.SoLuong * p.SanPham.DonGia);

                }
            }
        }

        private ObservableCollection<ChiTietPhieuMua> InsertProductsList;
        private ObservableCollection<ChiTietPhieuMua> DeletedProductsList;



        private readonly OpenDiaLog OpenThisDiaLog;  //tham chiếu để tắt bật dialog có view này
        private string _TitleView;
        public string TitleView { get => _TitleView; set { _TitleView = value; OnPropertyChanged(); } }

        public ICommand SaveCommand { get; set; }
        public ICommand CancelCommand { get; set; }




        public ICommand RemoveSelectedProductCommand { get; set; }
        public ICommand CaculateTotalMoneyCommand { get; set; } // dùng để tính lại tổng tiền hóa đơn khi nhập số lượng sản phẩm

        //Các biến cho việc chọn nhà cung cấp

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
        public ICommand RemoveSelectedSupplierCommand { get; set; }

        #endregion


        //Các biến cho việc chọn sản phẩm

        #region 

        private ObservableCollection<ChiTietPhieuMua> _SelectedProductList;
        public ObservableCollection<ChiTietPhieuMua> SelectedProductList
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
                        ChiTietPhieuMua productAdded = new ChiTietPhieuMua()
                        {
                            MaSP = SelectedProductItem.MaSP,
                            SanPham = SelectedProductItem,
                            SoLuong = 1
                        };

                        SelectedProductList.Add(productAdded);
                        // nếu là thêm sản phẩm vào phiếu đang chỉnh sửa
                        if (SelectedImportReceipt != null && DataProvider.Ins.DB.ChiTietPhieuMuas.Where(p => p.MaPhieu == SelectedImportReceipt.MaPhieu && p.MaSP == SelectedProductItem.MaSP).Count() == 0)
                        {
                            InsertProductsList.Add(productAdded);
                        }
                        SelectedProductItem = null;

                    }


                }


            }
        }



        private ChiTietPhieuMua _SelectedProductDataGrid;
        public ChiTietPhieuMua SelectedProductDataGrid
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


        #endregion





        #endregion




        public AddOrEditImportReceiptViewModel()
        {


        }
        //constructor cho việc tạo phiếu mua hàng mới 
        public AddOrEditImportReceiptViewModel(string titleView, ref OpenDiaLog openDiaLog, ref ObservableCollection<PhieuMua> phieuMuaList)
        {

            SelectedSuppliersList = new ObservableCollection<NhaCungCap>();
            SelectedProductList = new ObservableCollection<ChiTietPhieuMua>();

            TitleView = titleView;
            OpenThisDiaLog = openDiaLog;
            PhieuMuaList = phieuMuaList;


            SaveCommand = new RelayCommand<AddOrEditImportReceiptUC>((p) => true, p => AddNewImportReceipt());
            CancelCommand = new RelayCommand<AddOrEditImportReceiptUC>((p) => true, p => CheckCloseDiaLog());
            RemoveSelectedSupplierCommand = new RelayCommand<AddOrEditImportReceiptViewModel>((p) => true, p => { SelectedSuppliersList.Clear(); });
            RemoveSelectedProductCommand = new RelayCommand<DataGridTemplateColumn>(p => true, p => RemoveSelectedProduct("UNSAVED"));
            CaculateTotalMoneyCommand = new RelayCommand<DataGridTemplateColumn>(p => true, p => CaculateTotalMoney());



        }
        // constructor cho việc chỉnh sửa phiếu mua hàng

        public AddOrEditImportReceiptViewModel(string titleView, ref OpenDiaLog openDiaLog, ref ObservableCollection<PhieuMua> phieuMuaList, ref PhieuMua selectedImportReceipt)
        {
            InsertProductsList = new ObservableCollection<ChiTietPhieuMua>();
            DeletedProductsList = new ObservableCollection<ChiTietPhieuMua>();
            SelectedSuppliersList = new ObservableCollection<NhaCungCap>();
            SelectedProductList = new ObservableCollection<ChiTietPhieuMua>();
            TitleView = titleView;
            OpenThisDiaLog = openDiaLog;
            PhieuMuaList = phieuMuaList;
            SelectedImportReceipt = selectedImportReceipt;


            SaveCommand = new RelayCommand<AddOrEditImportReceiptUC>((p) => true, p => EditImportReceipt());
            CancelCommand = new RelayCommand<AddOrEditImportReceiptUC>((p) => true, p => CheckCloseDiaLog());
            RemoveSelectedSupplierCommand = new RelayCommand<AddOrEditImportReceiptViewModel>((p) => true, p => { SelectedSuppliersList.Clear(); });
            RemoveSelectedProductCommand = new RelayCommand<DataGridTemplateColumn>(p => true, p => RemoveSelectedProduct("SAVED"));
            CaculateTotalMoneyCommand = new RelayCommand<DataGridTemplateColumn>(p => true, p => CaculateTotalMoney());



        }

        #region Funtion for creating or editing the Receipt
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


                        ChiTietPhieuMua deletedProduct = SelectedProductDataGrid;

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

        private void AddNewImportReceipt()
        {
            if (!CheckValidFieldInDialog()) return;

            PhieuMua newImportReceipt = new PhieuMua()
            {
                MaPhieu = Guid.NewGuid().ToString(),
                NgayLap = DateTime.Now,
                MaNCC = SelectedSupplier.MaNCC,

            };

            DataProvider.Ins.DB.PhieuMuas.Add(newImportReceipt);



            //Chi tiet phieu
            foreach (var item in SelectedProductList)
            {
                ChiTietPhieuMua newDetailImportReceitpt = new ChiTietPhieuMua()
                {
                    MaChiTietPhieu = Guid.NewGuid().ToString(),
                    MaPhieu = newImportReceipt.MaPhieu,
                    MaSP = item.MaSP,
                    SoLuong = item.SoLuong,
                };

                DataProvider.Ins.DB.ChiTietPhieuMuas.Add(newDetailImportReceitpt);

            }
            DataProvider.Ins.DB.SaveChanges();
            PhieuMuaList.Add(newImportReceipt);
            PhieuMuaList = new ObservableCollection<PhieuMua>(DataProvider.Ins.DB.PhieuMuas);
            OpenThisDiaLog.IsOpen = false;


        }
        private void EditImportReceipt()
        {
            if (!CheckValidFieldInDialog()) return;
            OpenThisDiaLog.IsOpen = false;
            var editedImportReceipt = DataProvider.Ins.DB.PhieuMuas.Where(i => i.MaPhieu == SelectedImportReceipt.MaPhieu).SingleOrDefault();
            editedImportReceipt.MaNCC = SelectedSuppliersList.First().MaNCC;


            foreach (var item in DeletedProductsList)
            {
                var selectedProduct = DataProvider.Ins.DB.ChiTietPhieuMuas.Where(p => p.MaPhieu == item.MaPhieu && p.MaSP == item.MaSP).SingleOrDefault();
                DataProvider.Ins.DB.ChiTietPhieuMuas.Remove(selectedProduct);

            }
            DataProvider.Ins.DB.SaveChanges();

            foreach (var item in InsertProductsList)
            {

                ChiTietPhieuMua newDetailImportReceitpt = new ChiTietPhieuMua()
                {
                    MaChiTietPhieu = Guid.NewGuid().ToString(),
                    MaPhieu = SelectedImportReceipt.MaPhieu,
                    MaSP = item.MaSP,
                    SoLuong = item.SoLuong,
                };

                DataProvider.Ins.DB.ChiTietPhieuMuas.Add(newDetailImportReceitpt);



            }


            DataProvider.Ins.DB.SaveChanges();
            SelectedImportReceipt.ChiTietPhieuMuas = new ObservableCollection<ChiTietPhieuMua>(DataProvider.Ins.DB.ChiTietPhieuMuas.Where(p => p.MaPhieu == SelectedImportReceipt.MaPhieu));
            editedImportReceipt.MaPhieu = editedImportReceipt.MaPhieu;
            PhieuMuaList = new ObservableCollection<PhieuMua>(DataProvider.Ins.DB.PhieuMuas);

        }

        private bool CheckValidFieldInDialog()
        {
            if (SelectedSuppliersList == null || SelectedSuppliersList.Count == 0)
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
