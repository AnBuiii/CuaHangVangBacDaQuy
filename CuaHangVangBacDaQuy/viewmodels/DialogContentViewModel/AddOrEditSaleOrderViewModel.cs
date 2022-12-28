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
using COMExcel = Microsoft.Office.Interop.Excel;


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

        private bool _IsPaid;
        public bool IsPaid { get => _IsPaid; set { _IsPaid = value; OnPropertyChanged(); } }
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
        public ICommand PrintCommand { get; set; }
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
            PrintCommand = new RelayCommand<AddOrEditSaleOrderUC>((p) => false, p => AddNewSaleOrder());
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
            if(SelectedSaleOrder.ThanhToan == 0) IsPaid = false;
            else IsPaid = true;

            SaveCommand = new RelayCommand<AddOrEditSaleOrderUC>((p) => true, p => EditSaleOrder());
            CancelCommand = new RelayCommand<AddOrEditSaleOrderUC>((p) => true, p => CheckCloseDiaLog());
            PrintCommand = new RelayCommand<AddOrEditSaleOrderUC>((p) => true, p => Print());
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

        void Print()
        {
            COMExcel.Application exApp = new COMExcel.Application();
            COMExcel.Workbook exBook; //Trong 1 chương trình Excel có nhiều Workbook
            COMExcel.Worksheet exSheet; //Trong 1 Workbook có nhiều Worksheet
            COMExcel.Range exRange;

            int row = 0;
            exBook = exApp.Workbooks.Add(COMExcel.XlWBATemplate.xlWBATWorksheet);
            exSheet = exBook.Worksheets[1];
            // Định dạng chung
            exRange = exSheet.Cells[1, 1];
            exRange.Range["A1:Z300"].Font.Name = "Times new roman"; //Font chữ
            exRange.Range["C2:E2"].Font.Size = 16;
            exRange.Range["C2:E2"].Font.Bold = true;
            exRange.Range["C2:E2"].Font.ColorIndex = 3;
            exRange.Range["C2:E2"].MergeCells = true;
            exRange.Range["C2:E2"].HorizontalAlignment = COMExcel.XlHAlign.xlHAlignCenter;
            exRange.Range["C2:E2"].Value = "PHIẾU BÁN HÀNG";


            exRange.Range["B4:C4"].Font.Size = 12;
            exRange.Range["B4:B4"].Value = "Ngày lập:";
            exRange.Range["C4:E4"].MergeCells = true;
            exRange.Range["C4:E4"].Value = SelectedSaleOrder.NgayLap.Value.Day + "/" + SelectedSaleOrder.NgayLap.Value.Month + "/" + SelectedSaleOrder.NgayLap.Value.Year;

            exRange.Range["B5:C5"].Font.Size = 12;
            exRange.Range["B5:B5"].Value = "Khách hàng:";
            exRange.Range["C5:E5"].MergeCells = true;
            exRange.Range["C5:E5"].Value = SelectedSaleOrder.KhachHang.TenKH;

            exRange.Range["A6:F6"].Font.Bold = true;
            exRange.Range["A6:F6"].HorizontalAlignment = COMExcel.XlHAlign.xlHAlignCenter;
            exRange.Range["C6:F6"].ColumnWidth = 12;
            exRange.Range["A6:A6"].Value = "STT";
            exRange.Range["B6:B6"].Value = "Sản phẩm";
            exRange.Range["C6:C6"].Value = "Đơn vị";
            exRange.Range["D6:D6"].Value = "Số lượng";
            exRange.Range["E6:E6"].Value = "Đơn giá bán";
            exRange.Range["F6:F6"].Value = "Thành tiền";
            for (row = 0; row < SelectedProductList.Count; row++)
            {
                exSheet.Cells[1][row + 7] = (row + 1).ToString();
                exSheet.Cells[2][row + 7] = SelectedProductList[row].SanPham.TenSP;
                exSheet.Cells[3][row + 7] = SelectedProductList[row].SanPham.DonVi.TenDV;
                exSheet.Cells[4][row + 7] = SelectedProductList[row].SoLuong;
                exSheet.Cells[5][row + 7] = (int)SelectedProductList[row].SanPham.DonGia * (1 + SelectedProductList[row].SanPham.LoaiSanPham.LoiNhuan);
                exSheet.Cells[6][row + 7] = (int)SelectedProductList[row].SanPham.DonGia * (1 + SelectedProductList[row].SanPham.LoaiSanPham.LoiNhuan) * SelectedProductList[row].SoLuong;
            }
            exRange.Cells[5][row + 8].Font.Bold = true;
            exSheet.Cells[5][row + 8] = "Tổng tiền";
            exSheet.Cells[6][row + 8] = (int)TotalMoney;

            exSheet.Name = "Phiếu bán hàng";
            exApp.Visible = true;

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
                MaNV = Staff.MaND,
                ThanhToan = IsPaid ? 1 : 0
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
            editedSaleOrder.ThanhToan = IsPaid ? 1 : 0;

            foreach (ChiTietPhieuBan phieuBan in DataProvider.Ins.DB.ChiTietPhieuBans.Where(x => x.PhieuBan.MaPhieu == SelectedSaleOrder.MaPhieu))
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
