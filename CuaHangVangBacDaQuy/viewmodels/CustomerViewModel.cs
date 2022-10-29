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
        private ObservableCollection<KhachHang> CustomerList
        {
            get  => _CustomerList; 
            set { _CustomerList = value; OnPropertyChanged(); }
        }

        public ICommand LoadCustomerView { get; set; }
        #endregion

        public CustomerViewModel()
        {
            CustomerList = new ObservableCollection<KhachHang>(DataProvider.Ins.DB.KhachHangs);
            
            LoadCustomerView = new RelayCommand<CustomerView>((p) => true, (p) => loadCustomer(p));
        }


        void loadCustomer(CustomerView view)
        {
            
        }
    }
}
