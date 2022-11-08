using CuaHangVangBacDaQuy.views;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace CuaHangVangBacDaQuy.viewmodels
{
    public class MainViewModel : BaseViewModel
    {

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
        #endregion

        public bool isLoaded = false;
        

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
                isLoaded = true;
                if (p == null)
                    return;
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
            HomeViewModel = new HomeViewModel();
            AccountViewModel = new AccountViewModel();
            ImportReceiptViewModel = new ImportReceiptViewModel();
            SaleOrderViewModel = new SaleOrderViewModel();
            ProductViewModel = new ProductViewModel();
            SupplierViewModel = new SupplierViewModel();
            CustomerViewModel = new CustomerViewModel();
            //viewmodel init
        }
    }
}
