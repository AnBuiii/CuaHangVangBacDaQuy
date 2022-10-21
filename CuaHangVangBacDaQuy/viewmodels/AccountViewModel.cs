using CuaHangVangBacDaQuy.models;
using CuaHangVangBacDaQuy.views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;
using System.Windows.Input;

namespace CuaHangVangBacDaQuy.viewmodels
{
    public class AccountViewModel : BaseViewModel
    {
        private ObservableCollection<NguoiDung> _NguoiDungList;
        public ObservableCollection<NguoiDung> NguoiDungList { get => _NguoiDungList; set { _NguoiDungList = value; OnPropertyChanged(); } }

        private ObservableCollection<QuyenHan> _QuyenHanList;
        public ObservableCollection<QuyenHan> QuyenHanList { get => _QuyenHanList; set { _QuyenHanList = value; OnPropertyChanged(); } }

        private string _TenDangNhap;
        public string TenDangNhap { get =>_TenDangNhap; set { _TenDangNhap = value; OnPropertyChanged(); } }

        private QuyenHan _SelectedQuyenHan;
        public QuyenHan SelectedQuyenHan { get => _SelectedQuyenHan; set { _SelectedQuyenHan = value; OnPropertyChanged(); } }

        private string _TenNguoiDung;
        public string TenNguoiDung { get => _TenNguoiDung; set { _TenNguoiDung = value; OnPropertyChanged(); } }

        private NguoiDung _SelectedItem;
        public NguoiDung SelectedItem
        {
            get => _SelectedItem;
            set
            {
                _SelectedItem = value;
                OnPropertyChanged();
                if (SelectedItem != null)
                {
                    TenDangNhap = SelectedItem.TenDangNhap;
                    SelectedQuyenHan = SelectedItem.QuyenHan;
                    TenNguoiDung = SelectedItem.TenND;
                }
            }
        }
        public ICommand LoadAccountView { get; set; }
        public ICommand AddCommand { get; set; }
        public AccountViewModel()
        {
            NguoiDungList = new ObservableCollection<NguoiDung>(DataProvider.Ins.DB.NguoiDungs);
            QuyenHanList = new ObservableCollection<QuyenHan>(DataProvider.Ins.DB.QuyenHans);

            AddCommand = new RelayCommand<object>((p) =>
            {
                if (SelectedQuyenHan == null)
                    return false;
                return true;

            }, (p) =>
            {
                var NguoiDung = new NguoiDung { TenDangNhap = TenDangNhap, MaQH = SelectedQuyenHan.MaQH, };

                //DataProvider.Ins.DB.Objects.Add(Object);
                //DataProvider.Ins.DB.SaveChanges();

                //List.Add(Object);
            });
        }
       
    }
}
