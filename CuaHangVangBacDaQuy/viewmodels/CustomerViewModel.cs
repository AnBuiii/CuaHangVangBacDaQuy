using CuaHangVangBacDaQuy.models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using CuaHangVangBacDaQuy.views;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CuaHangVangBacDaQuy.viewmodels
{
    public class CustomerViewModel : BaseViewModel
    {

        #region Params
        private ObservableCollection<KhachHang> _CustomerList;
        public ObservableCollection<KhachHang> CustomerList
        {
            get  => _CustomerList; 
            set { _CustomerList = value; OnPropertyChanged(); }
        }

        private bool _IsOpenAddCustomerDialog;
        public bool IsOpenAddCustomerDialog
        {
            get { return _IsOpenAddCustomerDialog; }
            set { _IsOpenAddCustomerDialog = value; OnPropertyChanged(); }
        }

        

        private AddCustomerViewModel _addCustomerViewModel;
        public AddCustomerViewModel addCustomerViewModel
        {
            get { return _addCustomerViewModel; }
            set { _addCustomerViewModel = value; OnPropertyChanged(); }
        }

        public ICommand LoadCustomerView { get; set; }
        public ICommand AddCommand { get; set; }
        public ICommand EditCommand { get; set; }
        #endregion

        public CustomerViewModel()
        {   
            IsOpenAddCustomerDialog=false;
            CustomerList = new ObservableCollection<KhachHang>(DataProvider.Ins.DB.KhachHangs);
            AddCommand = new RelayCommand<CustomerView>((p) => true, p => { actionAddCustomer(); });
            EditCommand = new RelayCommand<CustomerView>((p) => true, p => IsOpenAddCustomerDialog = true);
            
           // LoadCustomerView = new RelayCommand<CustomerView>((p) => true, (p) => loadCustomer(p));
        }


        public void actionAddCustomer()
        {   
            addCustomerViewModel = new AddCustomerViewModel();
            IsOpenAddCustomerDialog = true;
        }
        void loadCustomer(CustomerView view)
        {
            
        }
    }
}
