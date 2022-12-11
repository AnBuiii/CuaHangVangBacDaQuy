using CuaHangVangBacDaQuy.models;
using CuaHangVangBacDaQuy.viewmodels.DialogContentViewModel;
using CuaHangVangBacDaQuy.views;
using CuaHangVangBacDaQuy.views.userControlDialog;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace CuaHangVangBacDaQuy.viewmodels
{
    public class MainViewModel : BaseViewModel
    {



        private static MainViewModel _ins;
        public static MainViewModel Ins
        {
            get
            {
                if (_ins == null)
                    _ins = new MainViewModel();

                return _ins;
            }
            set
            {
                _ins = value;
            }
        }

        private object _dataTemplate;
        public object DataTemplate
        {
            get => _dataTemplate;
            set
            {
                _dataTemplate = value;
                OnPropertyChanged();
            }
        }
        #region ViewModelParams
        public HomeViewModel HomeViewModel { get; set; }
        public AccountViewModel AccountViewModel { get; set; }
        public ImportReceiptViewModel ImportReceiptViewModel { get; set; }
        public SaleOrderViewModel SaleOrderViewModel { get; set; }

        public CustomerViewModel CustomerViewModel { get; set; }
        public ProductViewModel ProductViewModel { get; set; }
        public SupplierViewModel SupplierViewModel { get; set; }

        public AddOrEditImportReceiptViewModel AddOrEditImportReceiptViewModel { get; set; }
        #endregion

        #region Command
        public ICommand LoadedWindowCommand { get; set; }
        public ICommand HomeViewCommand { get; set; }
        public ICommand AccountViewCommand { get; set; }
        public ICommand CloseWindowCommand { get; set; }
        public ICommand CustomerViewCommand { get; set; }
        public ICommand MakeOrderCommand { get; set; }
        public ICommand SaleOrderViewCommand { get; set; }
        public ICommand ProductViewCommand { get; set; }
        public ICommand SupplierViewCommand { get; set; }
        public ICommand ChangePasswordCommand { get; set; }
        public ICommand SavePasswordCommand { get; set; }
        public ICommand NewPasswordChangedCommand { get; set; }
        public ICommand ConfirmPasswordChangedCommand { get; set; }
        public ICommand LogOutCommand { get; set; }

        #endregion

        

        #region ChangePassword
        private OpenDiaLog _IsOpenChangePasswordDialog;
        public OpenDiaLog IsOpenChangePasswordDialog
        {
            get => _IsOpenChangePasswordDialog;
            set
            {
                _IsOpenChangePasswordDialog = value;
                OnPropertyChanged();
            }
        }
        private string _NewPassword;
        public string NewPassword
        {
            get => _NewPassword;
            set
            {
                _NewPassword = value;
                OnPropertyChanged();
            }
        }

        private string _ConfirmPassword;
        public string ConfirmPassword
        {
            get => _ConfirmPassword;
            set
            {
                _ConfirmPassword = value;
                OnPropertyChanged();
            }
        }
        #endregion



        public MainViewModel()
        {
            InitNavBar();

            _dataTemplate = HomeViewModel;

            CloseWindowCommand = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                Application.Current.Shutdown();
            });

            HomeViewCommand = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                DataTemplate = HomeViewModel;
            });
            MakeOrderCommand = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                DataTemplate = ImportReceiptViewModel;

            });
            SaleOrderViewCommand = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                DataTemplate = SaleOrderViewModel;
            });

            CustomerViewCommand = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                DataTemplate = CustomerViewModel;
            });

            ProductViewCommand = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                DataTemplate = ProductViewModel;
            });
            SupplierViewCommand = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                DataTemplate = SupplierViewModel;
            });

            AccountViewCommand = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                DataTemplate = AccountViewModel;
            });

            LoadedWindowCommand = new RelayCommand<Window>((p) => { return true; }, (p) =>
               {
                   
                   if (p == null)
                       return;
                   p.DataContext = Ins;
                   p.Hide();
                   LoginWindow loginWindow = new LoginWindow();
                   loginWindow.ShowDialog();

                   if (loginWindow.DataContext == null)
                       return;
                   var loginVM = loginWindow.DataContext as LoginViewModel;

                   if (loginVM.IsLogin)
                   {
                       p.Show();

                       //LoadTonKhoData();
                   }
                   else
                   {
                       p.Close();
                   }
               });
            ChangePasswordCommand = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                IsOpenChangePasswordDialog.IsOpen = true;
            });
            SavePasswordCommand = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                ChangePassword();
            });
            NewPasswordChangedCommand = new RelayCommand<PasswordBox>((p) => { return true; }, (p) =>
            {
                NewPassword = p.Password;
            });
            ConfirmPasswordChangedCommand = new RelayCommand<PasswordBox>((p) => { return true; }, (p) =>
            {
                ConfirmPassword = p.Password;
            });
            LogOutCommand = new RelayCommand<MainWindow>((p) => { return true; }, (p) =>
            {
                NguoiDung.Logged = null;

                p.Hide();
                LoginWindow loginWindow = new LoginWindow();
                loginWindow.ShowDialog();

                if (loginWindow.DataContext == null)
                    return;
                var loginVM = loginWindow.DataContext as LoginViewModel;

                if (loginVM.IsLogin)
                {
                    p.Show();

                    //LoadTonKhoData();
                }
                else
                {
                    p.Close();
                }
            });


        }
        public void InitNavBar()
        {
            IsOpenChangePasswordDialog = new OpenDiaLog { IsOpen = false };
            NewPassword = "";
            ConfirmPassword = "";
            HomeViewModel = new HomeViewModel();
            AccountViewModel = new AccountViewModel();
            ImportReceiptViewModel = new ImportReceiptViewModel();
            SaleOrderViewModel = new SaleOrderViewModel();
            ProductViewModel = new ProductViewModel();
            SupplierViewModel = new SupplierViewModel();
            CustomerViewModel = new CustomerViewModel();
            AddOrEditImportReceiptViewModel = new AddOrEditImportReceiptViewModel();
            //viewmodel init

        }
        public void ChangePassword()
        {
            if (string.IsNullOrWhiteSpace(NewPassword)|| string.IsNullOrWhiteSpace(ConfirmPassword))
            {
                MessageBox.Show("Các trường không được trống");
                return;
            }
            if(NewPassword != ConfirmPassword)
            {
                MessageBox.Show("Xác nhận mật khẩu không khớp");
                return;
            }
            var nguoidung = DataProvider.Ins.DB.NguoiDungs.Where(x => x.MaND == NguoiDung.Logged.MaND).SingleOrDefault();
            nguoidung.MatKhau = MD5Hash(Base64Encode(NewPassword));
            DataProvider.Ins.DB.SaveChanges();
            MessageBox.Show("Thay đổi mật khẩu thành công");
            IsOpenChangePasswordDialog.IsOpen = false;
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
