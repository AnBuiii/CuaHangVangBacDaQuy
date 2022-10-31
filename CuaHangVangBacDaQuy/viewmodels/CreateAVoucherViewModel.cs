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
        private ObservableCollection<PhieuMua> _PhieuMuaList;
        public ObservableCollection<PhieuMua> PhieuMuaList { get => _PhieuMuaList; set { _PhieuMuaList = value; OnPropertyChanged(); } }
        private ObservableCollection<DonVi> _DonViList;
        public ObservableCollection<DonVi> DonViList { get => _DonViList; set { _DonViList = value; OnPropertyChanged(); } }
        private string _MaSanPham;
        public string MaSanPham { get => _MaSanPham; set { _MaSanPham = value; OnPropertyChanged(); } }
        private string _MaNhaCC;
        public string NhaCC { get => _MaNhaCC; set { _MaNhaCC = value; OnPropertyChanged(); } }
        private string _NgayLP;
        public string NgayLP { get => _NgayLP; set { _NgayLP = value; OnPropertyChanged(); } }
        private PhieuMua _SelectedPM;
        public PhieuMua SelectedPM { get => _SelectedPM; set { _SelectedPM = value; OnPropertyChanged(); } }
        private DonVi _SelectedDonVi;
        public DonVi SelectedDonVi { get => _SelectedDonVi; set { _SelectedDonVi = value; OnPropertyChanged(); } }
        private SanPham _SelectedItem;
        //public SanPham SelectedItem
        //{
        //    get => _SelectedItem;
        //    set
        //    {
        //        _SelectedItem = value;
        //        OnPropertyChanged();
        //        if (SelectedItem != null)
        //        {
        //            TenSanPham = SelectedItem.TenSP;
        //            DonGia = SelectedItem.DonGia;
        //            SelectedDonVi = SelectedItem.DonVi;

        //        }
        //    }
        //}
        private PhieuMua _SelectedIte;
        public SanPham SelectedItem;
        public PhieuMua SelectedIte
        {
            get => _SelectedIte;
            set
            {
                _SelectedIte = value;
                OnPropertyChanged();
                if (SelectedIte != null)
                {
                    MaSanPham=SelectedIte.MaPhieu;
                    NhaCC = SelectedIte.NhaCungCap.TenNCC;
                    NgayLP = SelectedIte.NgayLap.ToString();

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
            PhieuMuaList = new ObservableCollection<PhieuMua>(DataProvider.Ins.DB.PhieuMuas);
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
