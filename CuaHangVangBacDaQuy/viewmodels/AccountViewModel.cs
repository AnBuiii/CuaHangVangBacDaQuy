using CuaHangVangBacDaQuy.models;
using CuaHangVangBacDaQuy.views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Security.Cryptography;
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
        public string TenDangNhap { get => _TenDangNhap; set { _TenDangNhap = value; OnPropertyChanged(); } }

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
        private string _Password;
        public string Password { get => _Password; set { _Password = value; OnPropertyChanged(); } }

        private string _ConfirmPassword;
        public string ConfirmPassword { get => _ConfirmPassword; set { _ConfirmPassword = value; OnPropertyChanged(); } }

        public ICommand LoadAccountView { get; set; }
        public ICommand AddCommand { get; set; }
        public ICommand EditCommand { get; set; }
        public ICommand ChangePasswordCommand { get; set; }
        public ICommand SavePasswordCommand { get; set; }

        private bool _IsOpenDialog;
        public bool IsOpenDialog { get => _IsOpenDialog; set { _IsOpenDialog = value; OnPropertyChanged(); } }
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

            EditCommand = new RelayCommand<object>((p) =>
            {
                return isItemSelected();

            }, (p) =>
            {
                var nguoidung = DataProvider.Ins.DB.NguoiDungs.Where(x => x.MaND == SelectedItem.MaND).SingleOrDefault();
                nguoidung.TenDangNhap = TenDangNhap;
                nguoidung.QuyenHan = SelectedQuyenHan;
                nguoidung.TenND = TenNguoiDung;
                DataProvider.Ins.DB.SaveChanges();
                NguoiDungList = new ObservableCollection<NguoiDung>(DataProvider.Ins.DB.NguoiDungs);
            });
            ChangePasswordCommand = new RelayCommand<object>((p) =>
            {
                return isItemSelected();

            }, (p) =>
            {
                IsOpenDialog = true;
                Password = "";
                ConfirmPassword = "";
            });
            SavePasswordCommand = new RelayCommand<object>((p) =>
            {
                if(Password == null || Password == "")
                {
                    return false;
                }
                if (Password != ConfirmPassword) return false;
                return true;

            }, (p) =>
            {
                var nguoidung = DataProvider.Ins.DB.NguoiDungs.Where(x => x.MaND == SelectedItem.MaND).SingleOrDefault();
                nguoidung.MatKhau = MD5Hash(Base64Encode(Password));
                DataProvider.Ins.DB.SaveChanges();
                NguoiDungList = new ObservableCollection<NguoiDung>(DataProvider.Ins.DB.NguoiDungs);
                IsOpenDialog = false;

            });

        }
        private bool isItemSelected()
        {
            if (string.IsNullOrEmpty(TenDangNhap) || SelectedItem == null)
                return false;

            var displayList = DataProvider.Ins.DB.NguoiDungs.Where(x => x.TenDangNhap == TenDangNhap);
            if (displayList == null || displayList.Count() == 0)
                return false;

            return true;
        }
        public static string Base64Encode(string plainText)
        {
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
            return System.Convert.ToBase64String(plainTextBytes);
        }



        public static string MD5Hash(string input)
        {
            StringBuilder hash = new StringBuilder();
            MD5CryptoServiceProvider md5provider = new MD5CryptoServiceProvider();
            byte[] bytes = md5provider.ComputeHash(new UTF8Encoding().GetBytes(input));

            for (int i = 0; i < bytes.Length; i++)
            {
                hash.Append(bytes[i].ToString("x2"));
            }
            return hash.ToString();
        }

    }
}
