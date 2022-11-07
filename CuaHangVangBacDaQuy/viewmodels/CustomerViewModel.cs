using CuaHangVangBacDaQuy.models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using CuaHangVangBacDaQuy.views;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;
using System.Windows.Controls;
using CuaHangVangBacDaQuy.viewmodels.DialogContentViewModel;

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


        private OpenDiaLog _IsOpenDiaLog;
        public OpenDiaLog IsOpenDiaLog
        {
            get { return _IsOpenDiaLog; }
            set { _IsOpenDiaLog = value; OnPropertyChanged(); } 
        }
        
        private KhachHang _SelectedCustomer;
        public KhachHang SelectedCustomer
        {
            get => _SelectedCustomer;
            set
            {
                  _SelectedCustomer = value;
                OnPropertyChanged();
              
                
            }
        }

        private AddCustomerViewModel _ContentAddCustomer;
        public AddCustomerViewModel ContentAddCustomer
        {
            get => _ContentAddCustomer;
            set
            {
                _ContentAddCustomer = value;
                OnPropertyChanged();
               
            }
        }

        
        public ICommand EditCommand { get; set; }
       
        public ICommand AddCommand { get; set; }
        

        
        #endregion

        public CustomerViewModel()
        {

            IsOpenDiaLog = new OpenDiaLog() { IsOpen = false };
            CustomerList = new ObservableCollection<KhachHang>(DataProvider.Ins.DB.KhachHangs);         
            AddCommand = new RelayCommand<CustomerView>((p) => true, p =>  ActionDiaLog("Add"));
            EditCommand = new RelayCommand<DataGridTemplateColumn>((p)=>true, p=> ActionDiaLog("Edit"));
            
        }


        private void ActionDiaLog(string caseDiaLog)
        {
            IsOpenDiaLog.IsOpen = true;
            switch (caseDiaLog)
            {
                case "Add":
                    addNewCustomer();
                    break;

                case "Edit":
                    EditCustomer();
                    break;
            }
        }

        private void addNewCustomer()
        {
            ContentAddCustomer = new AddCustomerViewModel("Thêm khách hàng", ref _IsOpenDiaLog, ref _CustomerList);
        }

        private void EditCustomer()
        {
            ContentAddCustomer = new AddCustomerViewModel("Sửa thông tin khách hàng", ref _IsOpenDiaLog, ref _CustomerList, ref _SelectedCustomer);
          
        }
        void loadCustomer(CustomerView view)
        {
            
        }

    }
}
