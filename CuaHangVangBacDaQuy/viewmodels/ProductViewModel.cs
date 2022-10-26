using CuaHangVangBacDaQuy.models;
using CuaHangVangBacDaQuy.views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
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
        private String _AddButtonText { get; set; }
        public String AddButtonText { get => _AddButtonText; set { _AddButtonText = value; OnPropertyChanged(); } }

        #endregion

        #region Commands
        public ICommand LoadedCommand { get; set; }
        public ICommand AddProductCommand { get; set; }
        public ICommand AddCommand { get; set; }
        public ICommand EditCommand { get; set; }
        public ICommand DeleteCommand { get; set; }
        public ICommand SaveAddCommand { get; set; }
        #endregion


        private bool _IsOpenProductDialog;
        public bool IsOpenProductDialog
        {
            get { return _IsOpenProductDialog; }
            set { _IsOpenProductDialog = value; OnPropertyChanged(); }
        }
        private bool _isEditing { get; set; }
        public bool isEditing
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
            SanPhamList = new ObservableCollection<SanPham>(DataProvider.Ins.DB.SanPhams);
            LoaiSanPhamList = new ObservableCollection<LoaiSanPham>(DataProvider.Ins.DB.LoaiSanPhams);
            DonViList = new ObservableCollection<DonVi>(DataProvider.Ins.DB.DonVis);
            LoadedCommand = new RelayCommand<ProductView>(p => true, p => Loaded(p));
            AddCommand = new RelayCommand<ProductView>(p => true, p => AddProduct());
            EditCommand = new RelayCommand<DataGridTemplateColumn>(p => true, p => EditProduct());
            EditCommand = new RelayCommand<DataGridTemplateColumn>(p => true, p => EditProduct());
            SaveAddCommand = new RelayCommand<ProductView>(p => true, p => SaveAdd());
        }
        public void Loaded(ProductView weddingHallUC)
        {

        }
        public void AddProduct()
        {
            isEditing = false;
            IsOpenProductDialog = true;
            SelectedItem = new SanPham();
        }
        public void EditProduct()
        {
            isEditing = true;
            IsOpenProductDialog = true;
        }
        public void DeleteProduct()
        {

        }
        public void SaveAdd()
        {
            //lưu vào database
            //notify UI
            if (!isEditing)
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
                isEditing = false;

            }
        }
    }
}
