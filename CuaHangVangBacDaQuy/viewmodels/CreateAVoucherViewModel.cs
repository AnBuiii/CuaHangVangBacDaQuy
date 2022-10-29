using CuaHangVangBacDaQuy.models;
using CuaHangVangBacDaQuy.views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace CuaHangVangBacDaQuy.viewmodels
{
    public class CreateAVoucherViewModel:BaseViewModel
    {
        #region Commands
        public ICommand LoadedCommand { get; set; }
        public ICommand AddProductCommand { get; set; }
        public ICommand AddCommand { get; set; }
        public ICommand AddCommand1 { get; set; }
        public ICommand EditCommand { get; set; }
        public ICommand SaveAddCommand { get; set; }
        #endregion
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
        #endregion
        private bool _IsOpenAddProductDialog;
        public bool IsOpenAddProductDialog
        {
            get { return _IsOpenAddProductDialog; }
            set { _IsOpenAddProductDialog = value; OnPropertyChanged(); }
        }

        private bool _IsOpenAddProductDialog1;
        public bool IsOpenAddProductDialog1
        {
            get { return _IsOpenAddProductDialog1; }
            set { _IsOpenAddProductDialog1 = value; OnPropertyChanged(); }
        }
        public CreateAVoucherViewModel()
        {
            IsOpenAddProductDialog = false;
            SanPhamList = new ObservableCollection<SanPham>(DataProvider.Ins.DB.SanPhams);
            LoaiSanPhamList = new ObservableCollection<LoaiSanPham>(DataProvider.Ins.DB.LoaiSanPhams);
            DonViList = new ObservableCollection<DonVi>(DataProvider.Ins.DB.DonVis);
            LoadedCommand = new RelayCommand<CreateAVoucherView>(p => true, p => Loaded(p));
            AddCommand = new RelayCommand<CreateAVoucherView>(p => true, p => IsOpenAddProductDialog = true);
            AddCommand1 = new RelayCommand<CreateAVoucherView>(p => true, p => IsOpenAddProductDialog1 = true);
            EditCommand = new RelayCommand<DataGridTemplateColumn>(p => true, p => IsOpenAddProductDialog = true);
            SaveAddCommand = new RelayCommand<CreateAVoucherView>(p => true, p => SaveAdd());
        }
        public void Loaded(CreateAVoucherView weddingHallUC)
        {

        }
        public void SaveAdd()
        {
            //lưu vào database
            //notify UI
            IsOpenAddProductDialog = false;
        }
    }
}
