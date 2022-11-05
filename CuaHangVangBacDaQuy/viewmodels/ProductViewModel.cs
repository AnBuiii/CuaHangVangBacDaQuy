using CuaHangVangBacDaQuy.models;
using CuaHangVangBacDaQuy.views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Markup;



namespace CuaHangVangBacDaQuy.viewmodels
{
    public class ProductViewModel : BaseViewModel
    {
        #region Params
        private ObservableCollection<SanPham> _SanPhamList;
        public ObservableCollection<SanPham> SanPhamList { get => _SanPhamList; set { _SanPhamList = value; OnPropertyChanged(); } }
        private ObservableCollection<LoaiSanPham> _LoaiSanPhamList;
        public ObservableCollection<LoaiSanPham> LoaiSanPhamList { get => _LoaiSanPhamList; set { _LoaiSanPhamList = value; OnPropertyChanged(); } }
        private ObservableCollection<DonVi> _DonViList;
        public ObservableCollection<DonVi> DonViList { get => _DonViList; set { _DonViList = value; OnPropertyChanged(); } }
        private string _TenSanPham;
        public string TenSanPham { get => _TenSanPham; set { _TenSanPham = value; OnPropertyChanged(); } }
        private decimal? _DonGia;
        public decimal? DonGia { get => _DonGia; set { _DonGia = value; OnPropertyChanged(); } }
        private LoaiSanPham _SelectedLoaiSP;
        public LoaiSanPham SelectedLoaiSP { get => _SelectedLoaiSP; set { _SelectedLoaiSP = value; OnPropertyChanged(); } }
        private DonVi _SelectedDonVi;
        public DonVi SelectedDonVi { get => _SelectedDonVi; set { _SelectedDonVi = value; OnPropertyChanged(); } }
        private SanPham _SelectedItem;
        public SanPham SelectedItem
        {
            get => _SelectedItem;
            set
            {
                _SelectedItem = value;
                OnPropertyChanged();
                if (SelectedItem != null)
                {
                    TenSanPham = SelectedItem.TenSP;
                    DonGia = SelectedItem.DonGia;
                    SelectedLoaiSP = SelectedItem.LoaiSanPham;
                    SelectedDonVi = SelectedItem.DonVi;

                }

               
            }
        }
        private string _AddButtonText { get; set; }
        public string AddButtonText { get => _AddButtonText; set { _AddButtonText = value; OnPropertyChanged(); } }

        #region SearchBar
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
                //if (ContentSearch == "")
                //    Load(false);
            }
        }
        #endregion

        #endregion

        #region Commands
        public ICommand LoadedCommand { get; set; }
        
        public ICommand AddCommand { get; set; }
        public ICommand EditCommand { get; set; }
        
        public ICommand CancelDiaLogCommand { get; set; }
        public ICommand DeleteCommand { get; set; }
        public ICommand SaveProductCommand { get; set; }
        public ICommand SearchCommand { get; set; }
        #endregion



        private bool _IsOpenProductDialog;
        public bool IsOpenProductDialog
        {
            get { return _IsOpenProductDialog; }
            set { _IsOpenProductDialog = value; OnPropertyChanged(); }
        }
        private bool _isEditing;
        public bool IsEditing
        {
            get { return _isEditing; }
            set
            {
                _isEditing = value; OnPropertyChanged(); 
                if (_isEditing)
                {
                    AddButtonText = "Save change";
                } else
                {
                    AddButtonText = "Save new Product";
                }
            }
        }

        public ProductViewModel()
        {
            IsOpenProductDialog = false;
            SearchTypes = new List<string> {"Mã sản phẩm","Tên sản phẩm", };
            SelectedSearchType = SearchTypes[1];
            SanPhamList = new ObservableCollection<SanPham>(DataProvider.Ins.DB.SanPhams);
            LoaiSanPhamList = new ObservableCollection<LoaiSanPham>(DataProvider.Ins.DB.LoaiSanPhams);
            DonViList = new ObservableCollection<DonVi>(DataProvider.Ins.DB.DonVis);
            LoadedCommand = new RelayCommand<ProductView>(p => true, p => Loaded(p));
            AddCommand = new RelayCommand<ProductView>(p => true, p => AddProduct());
            EditCommand = new RelayCommand<DataGridTemplateColumn>(p => true, p => EditProduct());
            
            SaveProductCommand = new RelayCommand<ProductView>(
                (p) =>
                    {
                        if (CheckValidProductDiaLog()) return true;
                        return false;
                        
                    }
                        
                 , p => SaveAdd()
            );
            CancelDiaLogCommand = new RelayCommand<ProductView>(
                (p) => true, p => CheckCloseProductDiaLog()
            ) ;
            DeleteCommand = new RelayCommand<DataGridTemplateColumn>(p => true, p => DeleteProduct());
            SearchCommand = new RelayCommand<DataGridTemplateColumn>(p => true, p => Search());
        }

        private void Search()
        {
            switch (SelectedSearchType)
            {
                case "Mã sản phẩm":
                    SanPhamList = new ObservableCollection<SanPham>(
                        DataProvider.Ins.DB.SanPhams.Where(
                            x => x.MaSP.ToString().Contains(ContentSearch)));
                    break;
                case "Tên sản phẩm":
                    SanPhamList = new ObservableCollection<SanPham>(
                         DataProvider.Ins.DB.SanPhams.Where(
                             x => x.TenSP.ToString().Contains(ContentSearch)));
                    break;
                default:
                    break;
            }
        }

        public void Loaded(ProductView weddingHallUC)
        {

        }
        public void AddProduct()
        {
            IsEditing = false;
            IsOpenProductDialog = true;
            SelectedItem = new SanPham();
        }
        public void EditProduct()
        {
            IsEditing = true;
            IsOpenProductDialog = true;
        }
        public void DeleteProduct()
        {
            DataProvider.Ins.DB.SanPhams.Attach(SelectedItem);
            DataProvider.Ins.DB.SanPhams.Remove(SelectedItem);
            DataProvider.Ins.DB.SaveChanges();
            SanPhamList = new ObservableCollection<SanPham>(DataProvider.Ins.DB.SanPhams);
        }
        public void SaveAdd()
        {
            if (!IsEditing)
            {
                var SanPham = new SanPham() { MaSP = Guid.NewGuid().ToString(), TenSP = TenSanPham, DonGia = DonGia, MaLoaiSP = SelectedLoaiSP.MaLoaiSP, MaDV = SelectedDonVi.MaDV };
                DataProvider.Ins.DB.SanPhams.Add(SanPham);
                DataProvider.Ins.DB.SaveChanges();
                SanPhamList = new ObservableCollection<SanPham>(DataProvider.Ins.DB.SanPhams);
                IsOpenProductDialog = false;
            }
            else
            {
                var SanPham = DataProvider.Ins.DB.SanPhams.Where(x => x.MaSP == SelectedItem.MaSP).SingleOrDefault();
                SanPham.TenSP = TenSanPham;
                SanPham.DonGia = DonGia;
                SanPham.MaLoaiSP = SelectedLoaiSP.MaLoaiSP;
                SanPham.MaDV = SelectedDonVi.MaDV;
                DataProvider.Ins.DB.SaveChanges();
                SanPhamList = new ObservableCollection<SanPham>(DataProvider.Ins.DB.SanPhams);
                IsOpenProductDialog = false;
                IsEditing = false;

            }
        }


        private void CheckCloseProductDiaLog()
        {

            if(MessageBox.Show("Những thay đổi của bạn sẽ không được lưu?", "",
                 MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
            {
                IsOpenProductDialog=false;
            }
            

        }
        private bool CheckValidProductDiaLog()
        {
            if(string.IsNullOrWhiteSpace(TenSanPham) || string.IsNullOrEmpty(DonGia.ToString()) || DonGia.ToString() == "0" || SelectedLoaiSP == null || SelectedDonVi == null) return false;
            return true;
        }
    }
}
