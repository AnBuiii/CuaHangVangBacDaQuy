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
    public class AddOrEditImportReceiptViewModel : BaseViewModel
    {
        #region
        // các biến cho view chính này

        //private ObservableCollection<PhieuMua> _PhieuMuaList;
        //public ObservableCollection<PhieuMua> PhieuMuaList
        //{
        //    get => _PhieuMuaList;
        //    set
        //    {
        //        _PhieuMuaList = value;
        //        OnPropertyChanged();
        //    }
        //}

        private ObservableCollection<PhieuMua> PhieuMuaList;

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
                    foreach (ChiTietPhieuMua phieuMua in SelectedImportReceipt.ChiTietPhieuMuas)
                    {
                        SelectedProductList.Add(new ChiTietPhieuMua()
                        {
                            MaChiTietPhieu = phieuMua.MaChiTietPhieu,
                            MaPhieu = phieuMua.MaPhieu,
                            MaSP = phieuMua.MaSP,
                            PhieuMua = phieuMua.PhieuMua,
                            SanPham = phieuMua.SanPham,
                            SoLuong = phieuMua.SoLuong
                        });
                    }
                    TotalMoney = SelectedProductList.Sum(p => p.SoLuong * p.SanPham.DonGia);
                    Staff = SelectedImportReceipt.NguoiDung;

                }
            }
        }





        private readonly OpenDiaLog OpenThisDiaLog;  //tham chiếu để tắt bật dialog có view này
        private string _TitleView;
        public string TitleView { get => _TitleView; set { _TitleView = value; OnPropertyChanged(); } }

        public ICommand SaveCommand { get; set; }
        public ICommand CancelCommand { get; set; }
        public ICommand PrintCommand { get; set; }
        public ICommand RemoveSelectedProductCommand { get; set; }
        public ICommand CaculateTotalMoneyCommand { get; set; }

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
            get => _SelectedProductItem;
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
                        CaculateTotalMoney();
                    }


                }


            }
        }



        private ChiTietPhieuMua _SelectedProductDataGrid;
        public ChiTietPhieuMua SelectedProductDataGrid
        {
            get => _SelectedProductDataGrid;
            set
            {
                _SelectedProductDataGrid = value;
                OnPropertyChanged();

            }
        }


        #endregion





        #endregion



        #region Constructor
        public AddOrEditImportReceiptViewModel()
        {
            SelectedSuppliersList = new ObservableCollection<NhaCungCap>();
            SelectedProductList = new ObservableCollection<ChiTietPhieuMua>();
        }
        public AddOrEditImportReceiptViewModel(string titleView, ref OpenDiaLog openDiaLog, ref ObservableCollection<PhieuMua> phieuMuaList)
        {

            SelectedSuppliersList = new ObservableCollection<NhaCungCap>();
            SelectedProductList = new ObservableCollection<ChiTietPhieuMua>();

            TitleView = titleView;
            OpenThisDiaLog = openDiaLog;
            PhieuMuaList = phieuMuaList;

            Staff = NguoiDung.Logged;
            SaveCommand = new RelayCommand<AddOrEditImportReceiptUC>((p) => true, p => AddNewImportReceipt());
            CancelCommand = new RelayCommand<AddOrEditImportReceiptUC>((p) => true, p => CheckCloseDiaLog());
            PrintCommand = new RelayCommand<AddOrEditImportReceiptUC>((p) => false, p => Print());
            RemoveSelectedSupplierCommand = new RelayCommand<AddOrEditImportReceiptViewModel>((p) => true, p => { SelectedSuppliersList.Clear(); });
            RemoveSelectedProductCommand = new RelayCommand<DataGridTemplateColumn>(p => true, p => RemoveSelectedProduct());
            CaculateTotalMoneyCommand = new RelayCommand<DataGridTemplateColumn>(p => true, p => CaculateTotalMoney());



        }
        public AddOrEditImportReceiptViewModel(string titleView, ref OpenDiaLog openDiaLog, ref ObservableCollection<PhieuMua> phieuMuaList, ref PhieuMua selectedImportReceipt)
        {

            SelectedSuppliersList = new ObservableCollection<NhaCungCap>();
            SelectedProductList = new ObservableCollection<ChiTietPhieuMua>();

            TitleView = titleView;
            OpenThisDiaLog = openDiaLog;
            PhieuMuaList = phieuMuaList;
            SelectedImportReceipt = selectedImportReceipt;


            SaveCommand = new RelayCommand<AddOrEditImportReceiptUC>((p) => true, p => EditImportReceipt());
            CancelCommand = new RelayCommand<AddOrEditImportReceiptUC>((p) => true, p => CheckCloseDiaLog());
            PrintCommand = new RelayCommand<AddOrEditImportReceiptUC>((p) => true, p => Print());
            RemoveSelectedSupplierCommand = new RelayCommand<AddOrEditImportReceiptViewModel>((p) => true, p => { SelectedSuppliersList.Clear(); });
            RemoveSelectedProductCommand = new RelayCommand<DataGridTemplateColumn>(p => true, p => RemoveSelectedProduct());
            CaculateTotalMoneyCommand = new RelayCommand<DataGridTemplateColumn>(p => true, p => CaculateTotalMoney());
        }
        #endregion

        #region Funtion for creating or editing the Receipt

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
            exRange.Range["C2:E2"].Value = "PHIẾU MUA HÀNG";


            exRange.Range["B4:C4"].Font.Size = 12;
            exRange.Range["B4:B4"].Value = "Ngày lập:";
            exRange.Range["C4:E4"].MergeCells = true;
            exRange.Range["C4:E4"].Value = SelectedImportReceipt.NgayLap.Value.Day + "/" + SelectedImportReceipt.NgayLap.Value.Month + "/" + SelectedImportReceipt.NgayLap.Value.Year;

            exRange.Range["B5:C5"].Font.Size = 12;
            exRange.Range["B5:B5"].Value = "Nhà cung cấp:";
            exRange.Range["C5:E5"].MergeCells = true;
            exRange.Range["C5:E5"].Value = SelectedImportReceipt.NhaCungCap.TenNCC;

            exRange.Range["A6:F6"].Font.Bold = true;
            exRange.Range["A6:F6"].HorizontalAlignment = COMExcel.XlHAlign.xlHAlignCenter;
            exRange.Range["C6:F6"].ColumnWidth = 12;
            exRange.Range["A6:A6"].Value = "STT";
            exRange.Range["B6:B6"].Value = "Sản phẩm";
            exRange.Range["C6:C6"].Value = "Đơn vị";
            exRange.Range["D6:D6"].Value = "Số lượng";
            exRange.Range["E6:E6"].Value = "Đơn giá mua";
            exRange.Range["F6:F6"].Value = "Thành tiền";
            for (row = 0; row < SelectedProductList.Count; row++)
            {
                exSheet.Cells[1][row + 7] = (row + 1).ToString();
                exSheet.Cells[2][row + 7] = SelectedProductList[row].SanPham.TenSP;
                exSheet.Cells[3][row + 7] = SelectedProductList[row].SanPham.DonVi.TenDV;
                exSheet.Cells[4][row + 7] = SelectedProductList[row].SoLuong;
                exSheet.Cells[5][row + 7] = SelectedProductList[row].SanPham.DonGia;
                exSheet.Cells[6][row + 7] = SelectedProductList[row].SanPham.DonGia * SelectedProductList[row].SoLuong; 
            }
            exRange.Cells[5][row + 8].Font.Bold = true;
            exSheet.Cells[5][row + 8] = "Tổng tiền";
            exSheet.Cells[6][row + 8] = TotalMoney;

            exSheet.Name = "Phiếu mua hàng";
            exApp.Visible = true;

        }
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
            TotalMoney = (TotalMoney == null) ? 0 : SelectedProductList.Sum(p => p.SoLuong * p.SanPham.DonGia);
        }

        public string code;
        public void AddNewImportReceipt()
        {
            if (!CheckValidOrder()) return;
            if (string.IsNullOrEmpty(code))
            {
                code = Guid.NewGuid().ToString();
            }

            PhieuMua newImportReceipt = new PhieuMua()
            {
                MaPhieu = code,
                NgayLap = DateTime.Now,
                MaNCC = SelectedSupplier.MaNCC,
                MaNV = Staff.MaND

            };

            DataProvider.Ins.DB.PhieuMuas.Add(newImportReceipt);

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

            if (OpenThisDiaLog != null)
            {
                MessageBox.Show("Phiếu mua hàng được thêm thành công. Mã phiếu: " + newImportReceipt.MaPhieu);
                PhieuMuaList.Add(newImportReceipt);
                PhieuMuaList = new ObservableCollection<PhieuMua>(DataProvider.Ins.DB.PhieuMuas);
                OpenThisDiaLog.IsOpen = false;
            }



        }

        public bool CheckValidOrder()
        {
            if (SelectedSuppliersList == null || SelectedSuppliersList.Count == 0)
            {
                if (OpenThisDiaLog != null) MessageBox.Show("Vui lòng chọn nhà cung cấp!", "", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }
            if (SelectedProductList == null || SelectedProductList.Count == 0)
            {
                if (OpenThisDiaLog != null) MessageBox.Show("Vui lòng chọn sản phẩm nhập kho!", "", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }
            foreach(ChiTietPhieuMua ct in SelectedProductList)
            {
                if(ct.SoLuong <= 0)
                {
                    if (OpenThisDiaLog != null) MessageBox.Show("Số lượng phải lớn hơn 0", "", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return false;
                }
            }
            if (SelectedImportReceipt != null)
                foreach (var oldChitiet in DataProvider.Ins.DB.ChiTietPhieuMuas.Where(x => x.MaPhieu == SelectedImportReceipt.MaPhieu))
                {
                    int count = CaculateInventoryConverter.CaculateInventory(oldChitiet.MaSP);
                    count -= (int)DataProvider.Ins.DB.ChiTietPhieuMuas.Where(p => p.MaSP == oldChitiet.MaSP && p.MaPhieu == SelectedImportReceipt.MaPhieu).SingleOrDefault().SoLuong;

                    foreach (var newChitiet in SelectedProductList)
                    {
                        if (newChitiet.MaSP == oldChitiet.MaSP)
                        {
                            count += (int)newChitiet.SoLuong;
                        }

                    }
                    if (count < 0)
                    {
                        if (OpenThisDiaLog != null) MessageBox.Show("Sản phẩm " + oldChitiet.SanPham.TenSP + " số lượng không đúng");
                        return false;
                    }
                }
            return true;
        }

        bool CheckInventory()
        {
            foreach (var oldChitiet in DataProvider.Ins.DB.ChiTietPhieuMuas.Where(x => x.MaPhieu == SelectedImportReceipt.MaPhieu))
            {
                int count = CaculateInventoryConverter.CaculateInventory(oldChitiet.MaSP);
                count -= (int)DataProvider.Ins.DB.ChiTietPhieuMuas.Where(p => p.MaSP == oldChitiet.MaSP && p.MaPhieu == SelectedImportReceipt.MaPhieu).SingleOrDefault().SoLuong;

                foreach (var newChitiet in SelectedProductList)
                {
                    if (newChitiet.MaSP == oldChitiet.MaSP)
                    {
                        count += (int)newChitiet.SoLuong;
                    }

                }

                if (count < 0)
                {
                    MessageBox.Show("Sản phẩm " + oldChitiet.SanPham.TenSP);
                    return false;
                }
            }
            return true;
        }

        private void EditImportReceipt()
        {
            if (!CheckValidOrder()) return;

            var editedImportReceipt = DataProvider.Ins.DB.PhieuMuas.Where(i => i.MaPhieu == SelectedImportReceipt.MaPhieu).SingleOrDefault();

            editedImportReceipt.MaNCC = SelectedSuppliersList.First().MaNCC;
            foreach (ChiTietPhieuMua phieuMua in DataProvider.Ins.DB.ChiTietPhieuMuas.Where(x => x.PhieuMua.MaPhieu == SelectedImportReceipt.MaPhieu))
            {
                DataProvider.Ins.DB.ChiTietPhieuMuas.Remove(phieuMua);
            }

            foreach (var item in SelectedProductList)
            {
                ChiTietPhieuMua newDetailSaleOrder = new ChiTietPhieuMua()
                {
                    MaChiTietPhieu = Guid.NewGuid().ToString(),
                    MaPhieu = SelectedImportReceipt.MaPhieu,
                    MaSP = item.MaSP,
                    SoLuong = item.SoLuong,
                };

                DataProvider.Ins.DB.ChiTietPhieuMuas.Add(newDetailSaleOrder);

            }

            DataProvider.Ins.DB.SaveChanges();

            if (OpenThisDiaLog != null)
            {
                MessageBox.Show("Phiếu mua hàng được chỉnh sửa thành công. Mã phiếu: " + editedImportReceipt.MaPhieu);
                SelectedImportReceipt.ChiTietPhieuMuas = new ObservableCollection<ChiTietPhieuMua>(DataProvider.Ins.DB.ChiTietPhieuMuas.Where(p => p.MaPhieu == SelectedImportReceipt.MaPhieu));
                SelectedImportReceipt.MaPhieu = SelectedImportReceipt.MaPhieu;
                PhieuMuaList = new ObservableCollection<PhieuMua>(DataProvider.Ins.DB.PhieuMuas);
                OpenThisDiaLog.IsOpen = false;
            }

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
