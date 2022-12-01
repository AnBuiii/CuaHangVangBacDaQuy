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
    public class SupplierViewModel: BaseViewModel
    {
        #region Params
        private ObservableCollection<NhaCungCap> _SuppliersList;
        public ObservableCollection<NhaCungCap> SuppliersList { get => _SuppliersList; set { _SuppliersList = value; OnPropertyChanged(); } }

        private List<string> _searchTypes;
        public List<string> SearchTypes { get { return _searchTypes; } set { _searchTypes = value; OnPropertyChanged(); } }
        private string _selectedSearchType;
        public string SelectedSearchType { get { return _selectedSearchType; } set { _selectedSearchType = value; OnPropertyChanged(); } }

        private string _contentSearch;
        public string ContentSearch
        {
            get { return _contentSearch; }
            set
            {
                _contentSearch = value;
                OnPropertyChanged();
                if (ContentSearch == null)
                    _contentSearch = "";
            }
        }
        private OpenDiaLog _IsOpenDiaLog;
        public OpenDiaLog IsOpenDiaLog
        {
            get { return _IsOpenDiaLog; }
            set { _IsOpenDiaLog = value; OnPropertyChanged(); }
        }

        private NhaCungCap _SelectedSupplier;
        public NhaCungCap SelectedSupplier
        {
            get => _SelectedSupplier;
            set
            {
                _SelectedSupplier = value;
                OnPropertyChanged();

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

        public ICommand EditCommand { get; set; }

        public ICommand AddCommand { get; set; }

        public ICommand DeleteSupplierCommand { get; set; }

        public ICommand SearchCommand { get; set; }
        #endregion
        public SupplierViewModel()
        {
            IsOpenDiaLog = new OpenDiaLog() { IsOpen = false };
            SearchTypes = new List<string> { "Mã nhà cung cấp", "Tên nhà cung cấp", "Địa chỉ", "Số điện thoại", };
            SelectedSearchType = SearchTypes[0];
            SearchCommand = new RelayCommand<DataGridTemplateColumn>(p => true, p => Search());
            SuppliersList = new ObservableCollection<NhaCungCap>(DataProvider.Ins.DB.NhaCungCaps);
            AddCommand = new RelayCommand<SupplierViewModel>((p) => true, p => ActionDiaLog("Add"));
            EditCommand = new RelayCommand<DataGridTemplateColumn>((p) => true, p => ActionDiaLog("Edit"));
            DeleteSupplierCommand = new RelayCommand<DataGridTemplateColumn>((p) => true, p => DeledteCustomer());
        }

        public void Search()
        {
            switch (SelectedSearchType)
            {
                case "Mã nhà cung cấp":
                    SuppliersList = new ObservableCollection<NhaCungCap>(
                        DataProvider.Ins.DB.NhaCungCaps.Where(
                            x => x.MaNCC.ToString().Contains(ContentSearch)));
                    break;
                case "Tên nhà cung cấp":
                    SuppliersList = new ObservableCollection<NhaCungCap>(
                         DataProvider.Ins.DB.NhaCungCaps.Where(
                             x => x.TenNCC.ToString().Contains(ContentSearch)));
                    break;
                case "Địa chỉ":
                    SuppliersList = new ObservableCollection<NhaCungCap>(
                         DataProvider.Ins.DB.NhaCungCaps.Where(
                             x => x.DiaChi.ToString().Contains(ContentSearch)));
                    break;
                case "Số điện thoại":
                    SuppliersList = new ObservableCollection<NhaCungCap>(
                         DataProvider.Ins.DB.NhaCungCaps.Where(
                             x => x.SoDT.ToString().Contains(ContentSearch)));
                    break;
                default:
                    break;
            }
        }

        private void ActionDiaLog(string caseDiaLog)
        {
            IsOpenDiaLog.IsOpen = true;
            switch (caseDiaLog)
            {
                case "Add":
                    AddNewSupplier();
                    break;

                case "Edit":
                    EditSupplier();
                    break;
            }
        }

        private void AddNewSupplier()
        {

           
            ContentAddSupplier = new AddOrEditSupplierViewModel("Thêm nhà cung cấp mới", ref _IsOpenDiaLog, ref _SuppliersList);
        }

        private void EditSupplier()
        {
            ContentAddSupplier = new AddOrEditSupplierViewModel("Chỉnh sửa thông tin nhà cung cấp", ref _IsOpenDiaLog, ref _SuppliersList, ref _SelectedSupplier);
         
        }

        private void DeledteCustomer()
        {
            var deletedSupplier = DataProvider.Ins.DB.NhaCungCaps.Where(c => c.MaNCC == SelectedSupplier.MaNCC).SingleOrDefault();
            if (DataProvider.Ins.DB.PhieuMuas.Where(s => s.NhaCungCap.MaNCC == deletedSupplier.MaNCC).Count() > 0)
            {
                MessageBox.Show("Nhà cung cấp " + deletedSupplier.TenNCC + " đã từng giao dịch, vui lòng xóa đơn mua hàng liên quan trước khi xóa nhà cung cấp!", "", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            if (MessageBox.Show("Bạn có chắc chắc muốn xóa nhà cung cấp " + deletedSupplier.TenNCC + " không?", "", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.No)
            {
                return;
            }
            DataProvider.Ins.DB.NhaCungCaps.Remove(deletedSupplier);
            DataProvider.Ins.DB.SaveChanges();
            SuppliersList.Remove(SelectedSupplier);

        }
    }
}
