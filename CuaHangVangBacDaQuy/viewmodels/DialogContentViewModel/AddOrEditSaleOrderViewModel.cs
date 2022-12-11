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
    public class AddOrEditSaleOrderViewModel : BaseViewModel
    {
        #region SaleOrderProperties

        private NguoiDung _Staff;
        public NguoiDung Staff
        {
            get => _Staff;
            set
            {
                _Staff = value;
                OnPropertyChanged();
            }
        }

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
        #endregion

        #region SaleOrderViewProperties
        public string code;
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
        private readonly OpenDiaLog OpenThisDiaLog;
        private string _TitleView;
        public string TitleView { get => _TitleView; set { _TitleView = value; OnPropertyChanged(); } }
        #endregion

        #region CalculatingProperties
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
                    foreach (ChiTietPhieuBan phieuBan in SelectedSaleOrder.ChiTietPhieuBans)
                    {
                        SelectedProductList.Add(new ChiTietPhieuBan()
                        {
                            MaChiTietPhieu = phieuBan.MaChiTietPhieu,
                            MaPhieu = phieuBan.MaPhieu,
                            MaSP = phieuBan.MaSP,
                            PhieuBan = phieuBan.PhieuBan,
                            SanPham = phieuBan.SanPham,
                            SoLuong = phieuBan.SoLuong
                        });
                    }
                    TotalMoney = SelectedProductList.Sum(p => p.SoLuong * p.SanPham.DonGia * (1 + p.SanPham.LoaiSanPham.LoiNhuan));
                    Staff = SelectedSaleOrder.NguoiDung;

                }
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

        private ObservableCollection<KhachHang> _SelectedCustomersList;
        public ObservableCollection<KhachHang> SelectedCustomersList
        {
            get => _SelectedCustomersList;
            set { _SelectedCustomersList = value; OnPropertyChanged(); }
        }

        private SanPham _SelectedProductItem;
        public SanPham SelectedProductItem
        {
            get => _SelectedProductItem;
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
                            MaPhieu = (SelectedSaleOrder != null) ? SelectedSaleOrder.MaPhieu : "",
                            MaSP = SelectedProductItem.MaSP,
                            SanPham = SelectedProductItem,
                            SoLuong = 1
                        };
                        SelectedProductList.Add(productAdded);
                        CaculateTotalMoney();
                    }
                }
            }
        }

        private ChiTietPhieuBan _SelectedProductDataGrid;
        public ChiTietPhieuBan SelectedProductDataGrid
        {
            get => _SelectedProductDataGrid;
            set
            {
                _SelectedProductDataGrid = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region Command
        public ICommand RemoveSelectedSupplierCommand { get; set; }
        public ICommand SaveCommand { get; set; }
        public ICommand CancelCommand { get; set; }
        public ICommand RemoveSelectedProductCommand { get; set; }
        public ICommand CaculateTotalMoneyCommand { get; set; }
        #endregion

        #region Constructors
        public AddOrEditSaleOrderViewModel()
        {

            SelectedCustomersList = new ObservableCollection<KhachHang>();
            SelectedProductList = new ObservableCollection<ChiTietPhieuBan>();

        }
        public AddOrEditSaleOrderViewModel(string titleView, ref OpenDiaLog openDiaLog, ref ObservableCollection<PhieuBan> saleOrdersList)
        {
            SelectedCustomersList = new ObservableCollection<KhachHang>();
            SelectedProductList = new ObservableCollection<ChiTietPhieuBan>();

            TitleView = titleView;
            OpenThisDiaLog = openDiaLog;
            SaleOrdersList = saleOrdersList;
            Staff = NguoiDung.Logged;

            SaveCommand = new RelayCommand<AddOrEditSaleOrderUC>((p) => true, p => AddNewSaleOrder());
            CancelCommand = new RelayCommand<AddOrEditSaleOrderUC>((p) => true, p => CheckCloseDiaLog());
            RemoveSelectedProductCommand = new RelayCommand<DataGridTemplateColumn>(p => true, p => RemoveSelectedProduct());
            CaculateTotalMoneyCommand = new RelayCommand<DataGridTemplateColumn>(p => true, p => CaculateTotalMoney());
        }

        public AddOrEditSaleOrderViewModel(string titleView, ref OpenDiaLog openDiaLog, ref ObservableCollection<PhieuBan> saleOrdersList, ref PhieuBan selectedSaleOrder)
        {
            SelectedCustomersList = new ObservableCollection<KhachHang>();
            SelectedProductList = new ObservableCollection<ChiTietPhieuBan>();

            TitleView = titleView;
            OpenThisDiaLog = openDiaLog;
            SaleOrdersList = saleOrdersList;
            SelectedSaleOrder = selectedSaleOrder;


            SaveCommand = new RelayCommand<AddOrEditSaleOrderUC>((p) => true, p => EditSaleOrder());
            CancelCommand = new RelayCommand<AddOrEditSaleOrderUC>((p) => true, p => CheckCloseDiaLog());
            RemoveSelectedProductCommand = new RelayCommand<DataGridTemplateColumn>(p => true, p => RemoveSelectedProduct());
            CaculateTotalMoneyCommand = new RelayCommand<DataGridTemplateColumn>(p => true, p => CaculateTotalMoney());
        }
        #endregion

        #region Funtion for creating or editing the SaleOrder
        void RemoveSelectedProduct()
        {
            if (SelectedProductDataGrid != null)
            {
                SelectedProductList.Remove(SelectedProductDataGrid);
                CaculateTotalMoney();
            }
        }

        void CaculateTotalMoney()
        {
            TotalMoney = SelectedProductList.Sum(p => p.SoLuong * p.SanPham.DonGia * (1 + p.SanPham.LoaiSanPham.LoiNhuan));
        }

        bool CheckProductStock()
        {
            foreach (var product in SelectedProductList)
            {
                if (CaculateInventoryConverter.CaculateInventory(product.MaSP) < product.SoLuong)
                {
                    if (product.SanPham != null)
                    {
                        MessageBox.Show("Sản phẩm " + product.SanPham.TenSP + " không đủ hàng!", "", MessageBoxButton.OK, MessageBoxImage.Warning);
                    }

                    return false;
                }
            }
            return true;
        }

        bool CheckProductStockEdit()
        {
            foreach (var product in SelectedProductList)
            {
                int count = CaculateInventoryConverter.CaculateInventory(product.MaSP);
                if (DataProvider.Ins.DB.ChiTietPhieuBans.Where(p => p.MaSP == product.MaSP && p.MaPhieu == SelectedSaleOrder.MaPhieu).SingleOrDefault() != null)
                {
                    count += (int)DataProvider.Ins.DB.ChiTietPhieuBans.Where(p => p.MaSP == product.MaSP && p.MaPhieu == SelectedSaleOrder.MaPhieu).SingleOrDefault().SoLuong;
                }

                if (count < product.SoLuong)
                {
                    if (product.SanPham != null)
                    {
                        MessageBox.Show("Sản phẩm " + product.SanPham.TenSP + " không đủ hàng!", "", MessageBoxButton.OK, MessageBoxImage.Warning);
                    }
                    return false;
                }
            }
            return true;
        }

        public bool CheckValidOrder()
        {
            if (SelectedCustomersList == null || SelectedCustomersList.Count == 0)
            {
                if (OpenThisDiaLog != null) MessageBox.Show("Vui lòng chọn khách hàng!", "", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }
            if (SelectedProductList == null || SelectedProductList.Count == 0)
            {
                if (OpenThisDiaLog != null) MessageBox.Show("Vui lòng chọn sản phẩm xuất kho!", "", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }


            foreach (var product in SelectedProductList)
            {
                if (product.SoLuong <= 0)
                {
                    if (OpenThisDiaLog != null) MessageBox.Show("Số lượng sản phẩm phải lớn hơn 0");
                    return false;
                }
                if (CaculateInventoryConverter.CaculateInventory(product.MaSP) < product.SoLuong)
                {
                    if (product.SanPham != null)
                    {
                         if (OpenThisDiaLog != null) MessageBox.Show("Sản phẩm " + product.SanPham.TenSP + " không đủ hàng!", "", MessageBoxButton.OK, MessageBoxImage.Warning);
                    }

                    return false;
                }
            }
            return true;
        }

        public void AddNewSaleOrder()
        {
            if (!CheckValidOrder()) return;

            //if (!CheckValidFieldInDialog()) return;
            //if (!CheckProductStock()) return;

            if (string.IsNullOrEmpty(code))
            {
                code = Guid.NewGuid().ToString();
            }

            PhieuBan newSaleOrder = new PhieuBan()
            {
                MaPhieu = code,
                NgayLap = DateTime.Now,
                MaKH = SelectedCustomer.MaKH,
                MaNV = Staff.MaND
            };
            DataProvider.Ins.DB.PhieuBans.Add(newSaleOrder);

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

            if (OpenThisDiaLog != null)
            {
                MessageBox.Show("Phiếu bán hàng được thêm thành công. Mã phiếu: " + newSaleOrder.MaPhieu);
                SaleOrdersList.Add(newSaleOrder);
                OpenThisDiaLog.IsOpen = false;
            }
        }
        public void EditSaleOrder()
        {
            if (!CheckValidOrder()) return;

            var editedSaleOrder = DataProvider.Ins.DB.PhieuBans.Where(i => i.MaPhieu == SelectedSaleOrder.MaPhieu).SingleOrDefault();

            editedSaleOrder.MaKH = SelectedCustomersList.First().MaKH;

            foreach(ChiTietPhieuBan phieuBan in DataProvider.Ins.DB.ChiTietPhieuBans.Where(x => x.PhieuBan.MaPhieu == SelectedSaleOrder.MaPhieu))
            {
                DataProvider.Ins.DB.ChiTietPhieuBans.Remove(phieuBan);
            }

            foreach (var item in SelectedProductList)
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
            if (OpenThisDiaLog != null)
            {
                MessageBox.Show("Phiếu bán hàng được chỉnh sửa thành công. Mã phiếu: " + editedSaleOrder.MaPhieu);
                OpenThisDiaLog.IsOpen = false;
                SelectedSaleOrder.ChiTietPhieuBans = new ObservableCollection<ChiTietPhieuBan>(DataProvider.Ins.DB.ChiTietPhieuBans.Where(p => p.MaPhieu == SelectedSaleOrder.MaPhieu));
                SelectedSaleOrder.MaPhieu = SelectedSaleOrder.MaPhieu;
                SaleOrdersList = new ObservableCollection<PhieuBan>(DataProvider.Ins.DB.PhieuBans);
            }

        }

        private bool CheckValidFieldInDialog()
        {
            if (SelectedCustomersList == null || SelectedCustomersList.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn khách hàng!", "", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }
            if (SelectedProductList == null || SelectedProductList.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn sản phẩm xuất kho!", "", MessageBoxButton.OK, MessageBoxImage.Warning);
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
