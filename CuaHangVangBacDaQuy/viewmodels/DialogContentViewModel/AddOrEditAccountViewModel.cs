using CuaHangVangBacDaQuy.models;
using CuaHangVangBacDaQuy.views.userControlDialog;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows.Input;
using MessageBox = System.Windows.MessageBox;

namespace CuaHangVangBacDaQuy.viewmodels.DialogContentViewModel
{
    public class AddOrEditAccountViewModel : BaseViewModel
    {
        private readonly ObservableCollection<NguoiDung> AccountsList;
        private ObservableCollection<QuyenHan> _PermissionsList;
        public ObservableCollection<QuyenHan> PermissionsList { get => _PermissionsList; set { _PermissionsList = value; OnPropertyChanged(); } }
        private QuyenHan _SelectedPermission;
        public QuyenHan SelectedPermission { get => _SelectedPermission; set { _SelectedPermission = value; OnPropertyChanged(); } }

        private NguoiDung _EditedAccount;
        public NguoiDung EditedAccount
        {
            get => _EditedAccount;

            set
            {
                _EditedAccount = value;
                OnPropertyChanged();

                if (EditedAccount != null)
                {

                    AccountUsername = EditedAccount.TenDangNhap;
                    AccountName = EditedAccount.TenND;
                    PasswordAccount = "";
                    SelectedPermission = EditedAccount.QuyenHan;


                }
            }
        }

        private readonly OpenDiaLog openDiaLog;



        private string _TitleView;
        public string TitleView { get => _TitleView; set { _TitleView = value; OnPropertyChanged(); } }


        private string _AccountName;
        public string AccountName
        {
            get => _AccountName;
            set
            {
                _AccountName = value; OnPropertyChanged();
            }
        }

        private string _AccountUsername;
        public string AccountUsername
        {
            get => _AccountUsername;
            set
            {
                _AccountUsername = value; OnPropertyChanged();
            }
        }

        public int accountCode;


        private string _PasswordAccount;
        public string PasswordAccount
        {
            get => _PasswordAccount;
            set
            {
                _PasswordAccount = value;
                OnPropertyChanged();
            }
        }


        public ICommand SaveCommand { get; set; }
        public ICommand CancelCommand { get; set; }
        public ICommand ChangePasswordCommand { get; set; }
        public ICommand PasswordClickCommand { get; set; }




        public AddOrEditAccountViewModel()
        {
            PermissionsList = new ObservableCollection<QuyenHan>(DataProvider.Ins.DB.QuyenHans);

        }

        //constructor used for add new account
        public AddOrEditAccountViewModel(string titleView, ref OpenDiaLog isOpenDialog, ref ObservableCollection<NguoiDung> accountsList)
        {
            TitleView = titleView;
            openDiaLog = isOpenDialog;
            AccountsList = accountsList;

            PermissionsList = new ObservableCollection<QuyenHan>(DataProvider.Ins.DB.QuyenHans);

            CancelCommand = new RelayCommand<AddOrEditAccountUC>((p) => true, p => CheckCloseDiaLog());
            SaveCommand = new RelayCommand<AddOrEditAccountUC>((p) => true, p => ActionAddAccount());
            ChangePasswordCommand = new RelayCommand<PasswordBox>((p) => true, (p) => { PasswordAccount = p.Password; });

        }


        //constructor used for edit account
        public AddOrEditAccountViewModel(string tilteView, ref OpenDiaLog isOpenDialog, ref ObservableCollection<NguoiDung> suppliersList, ref NguoiDung editedAccount)
        {
            TitleView = tilteView;
            openDiaLog = isOpenDialog;
            AccountsList = suppliersList;
            EditedAccount = editedAccount;

            PermissionsList = new ObservableCollection<QuyenHan>(DataProvider.Ins.DB.QuyenHans);


            CancelCommand = new RelayCommand<AddOrEditAccountUC>((p) => true, p => CheckCloseDiaLog());
            SaveCommand = new RelayCommand<AddOrEditAccountUC>((p) => true, p => ActionEditAccount());
            ChangePasswordCommand = new RelayCommand<PasswordBox>((p) => true, (p) => { PasswordAccount = p.Password; });
        }


        bool ValidAccountCheck()
        {

            if (EditedAccount == null)
            {
                //if (DataProvider.Ins.DB.NguoiDungs.Where(x => x.TenND == AccountName).Count() > 0) return false;
                if (DataProvider.Ins.DB.NguoiDungs.Where(x => x.TenDangNhap == AccountUsername).Count() > 0) return false;
            }
            else
            {
                if (AccountName != EditedAccount.TenND)
                {
                    //if (DataProvider.Ins.DB.NguoiDungs.Where(x => x.TenND == AccountName).Count() > 0) return false;
                }
                if (AccountUsername != EditedAccount.TenDangNhap)
                {

                    if (DataProvider.Ins.DB.NguoiDungs.Where(x => x.TenDangNhap == AccountUsername).Count() > 0) return false;
                }


            }
            return true;

            //return ((EditedProduct == null && DataProvider.Ins.DB.SanPhams.Where(x => x.TenSP == ProductName).Count() == 0) || ()) && ProductPrice > 0;

        }


        bool CheckEmptyFieldDialog()
        {
            if (string.IsNullOrWhiteSpace(AccountName) || string.IsNullOrWhiteSpace(AccountUsername) || SelectedPermission == null || (EditedAccount == null && string.IsNullOrWhiteSpace(PasswordAccount)))
            {
                if(openDiaLog != null) MessageBox.Show("Các trường không được trống");
                return false;
            }
            return true;
        }

        public void ActionAddAccount()
        {
            if (!CheckEmptyFieldDialog()) return;
            if (!ValidAccountCheck()) return;
            var newAccount = new NguoiDung()
            {
                TenDangNhap = AccountUsername,
                TenND = AccountName,
                MatKhau = MD5Hash(Base64Encode(PasswordAccount)),
                MaQH = SelectedPermission.MaQH

            };

            DataProvider.Ins.DB.NguoiDungs.Add(newAccount);
            DataProvider.Ins.DB.SaveChanges();
            if (openDiaLog != null)
            {
                AccountsList.Add(newAccount);
                openDiaLog.IsOpen = false;
                MessageBox.Show("Người dùng được thêm thành công");
            }
            accountCode = DataProvider.Ins.DB.NguoiDungs.Where(x => x.TenDangNhap == AccountUsername).FirstOrDefault().MaND;
        }

        public void ActionEditAccount()
        {
            if (!CheckEmptyFieldDialog()) return;
            if (!ValidAccountCheck()) return;

            if (openDiaLog != null)
            {
                openDiaLog.IsOpen = false;
            }

            var account = DataProvider.Ins.DB.NguoiDungs.Where(x => x.MaND == EditedAccount.MaND).SingleOrDefault();
            account.TenDangNhap = AccountUsername;
            account.TenND = AccountName;
            if(PasswordAccount == "") account.MatKhau = EditedAccount.MatKhau;
            else account.MatKhau = MD5Hash(Base64Encode(PasswordAccount));
            account.MaQH = SelectedPermission.MaQH;
            DataProvider.Ins.DB.SaveChanges();
            if (openDiaLog != null)
            {
                openDiaLog.IsOpen = false;
                MessageBox.Show("Người dùng được sửa thành công");
            }
        }





        private void CheckCloseDiaLog()
        {

            if (MessageBox.Show("Những thay đổi của bạn sẽ không được lưu?", "",
                 MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
            {

                openDiaLog.IsOpen = false;

            }


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

