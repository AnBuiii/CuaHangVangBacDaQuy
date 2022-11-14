using CuaHangVangBacDaQuy.models;
using CuaHangVangBacDaQuy.views.userControlDialog;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace CuaHangVangBacDaQuy.viewmodels.DialogContentViewModel
{
    public class AddOrEditAccountViewModel:BaseViewModel
    {
        private readonly ObservableCollection<NguoiDung> AccountsList;
        private ObservableCollection<QuyenHan> _JurisdictionsList;
        public ObservableCollection<QuyenHan> JurisdictionsList { get => _JurisdictionsList; set { _JurisdictionsList = value; OnPropertyChanged(); } }
        private QuyenHan _SelectedJurisdiction;
        public  QuyenHan SelectedJurisdiction { get => _SelectedJurisdiction; set { _SelectedJurisdiction = value; OnPropertyChanged(); } }

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

                    AccountName = EditedAccount.TenDangNhap;
                    AccountName1 = EditedAccount.TenND;
                    PasswordAccount = EditedAccount.MatKhau;
                    SelectedJurisdiction = EditedAccount.QuyenHan;


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

        private string _AccountName1;
        public string AccountName1
        {
            get => _AccountName1;
            set
            {
                _AccountName1 = value; OnPropertyChanged();
            }
        }


        private string _PasswordAccount;
        public string PasswordAccount { get => _PasswordAccount; set { _PasswordAccount = value; OnPropertyChanged(); } }

        public ICommand SaveCommand { get; set; }
        public ICommand CancelCommand { get; set; }




        public AddOrEditAccountViewModel()
        {


        }

        //constructor used for add new account
        public AddOrEditAccountViewModel(string titleView, ref OpenDiaLog isOpenDialog, ref ObservableCollection<NguoiDung> accountsList)
        {
            TitleView = titleView;
            openDiaLog = isOpenDialog;
            AccountsList = accountsList;
            JurisdictionsList = new ObservableCollection<QuyenHan>(DataProvider.Ins.DB.QuyenHans);
            CancelCommand = new RelayCommand<AddOrEditAccountUC>((p) => true, p => CheckCloseDiaLog());
            SaveCommand = new RelayCommand<AddOrEditAccountUC>((p) => CheckEmptyFieldDialog(), p => ActionAddAccount());



        }


        //constructor used for edit account
        public AddOrEditAccountViewModel(string tilteView, ref OpenDiaLog isOpenDialog, ref ObservableCollection<NguoiDung> suppliersList, ref NguoiDung editedAccount)
        {
            TitleView = tilteView;
            openDiaLog = isOpenDialog;
            AccountsList = suppliersList;
            JurisdictionsList = new ObservableCollection<QuyenHan>(DataProvider.Ins.DB.QuyenHans);
            EditedAccount = editedAccount;
            CancelCommand = new RelayCommand<AddOrEditAccountUC>((p) => true, p => CheckCloseDiaLog());
            SaveCommand = new RelayCommand<AddOrEditAccountUC>((p) => CheckEmptyFieldDialog(), p => ActionEditAccount());

        }


        bool CheckEmptyFieldDialog()
        {

            if (string.IsNullOrEmpty(AccountName) || string.IsNullOrEmpty(PasswordAccount))
            {
                return false;
            }
            return true;
        }

        private void ActionAddAccount()
        {

            var newAccount = new NguoiDung()
            {
                TenDangNhap = AccountName,
                TenND = AccountName1,
                //MatKhau = MD5Hash(Base64Encode(PasswordAccount)),
                MaQH = SelectedJurisdiction.MaQH,
                MatKhau = PasswordAccount

            };

            DataProvider.Ins.DB.NguoiDungs.Add(newAccount);
            DataProvider.Ins.DB.SaveChanges();
            AccountsList.Add(newAccount);
            openDiaLog.IsOpen = false;
        }

        private void ActionEditAccount()
        {
           
            openDiaLog.IsOpen = false;
            var account = DataProvider.Ins.DB.NguoiDungs.Where(x => x.MaND == EditedAccount.MaND).SingleOrDefault();
            account.TenDangNhap = AccountName;
            account.TenND = AccountName1;
            account.MatKhau = PasswordAccount;
            account.MaQH = SelectedJurisdiction.MaQH;
            DataProvider.Ins.DB.SaveChanges();
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

