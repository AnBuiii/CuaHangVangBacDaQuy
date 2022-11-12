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

        private AddOrEditCustomerViewModel _ContentAddOrEditCustomer;
        public AddOrEditCustomerViewModel ContentAddOrEditCustomer
        {
            get => _ContentAddOrEditCustomer;
            set
            {
                _ContentAddOrEditCustomer = value;
                OnPropertyChanged();
               
            }
        }

        
        public ICommand EditCommand { get; set; }
       
        public ICommand AddCommand { get; set; }

        public ICommand DeleteCustomerCommand { get; set; }
        

        
        #endregion

        public CustomerViewModel()
        {

            IsOpenDiaLog = new OpenDiaLog() { IsOpen = false };
            CustomerList = new ObservableCollection<KhachHang>(DataProvider.Ins.DB.KhachHangs);         
            AddCommand = new RelayCommand<CustomerView>((p) => true, p =>  ActionDiaLog("Add"));
            EditCommand = new RelayCommand<DataGridTemplateColumn>((p)=>true, p=> ActionDiaLog("Edit"));
            DeleteCustomerCommand = new RelayCommand<DataGridTemplateColumn>((p) => true, p => DeledteCustomer());

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
            ContentAddOrEditCustomer = new AddOrEditCustomerViewModel("Thêm khách hàng", ref _IsOpenDiaLog, ref _CustomerList);
        }

        private void EditCustomer()
        {
            ContentAddOrEditCustomer = new AddOrEditCustomerViewModel("Sửa thông tin khách hàng", ref _IsOpenDiaLog, ref _CustomerList, ref _SelectedCustomer);
          
        }

        private void DeledteCustomer()
        {
            var deletedCustomer = DataProvider.Ins.DB.KhachHangs.Where(c => c.MaKH == SelectedCustomer.MaKH).SingleOrDefault();
            if(DataProvider.Ins.DB.PhieuBans.Where(s => s.MaKH == deletedCustomer.MaKH).Count() > 0){
                MessageBox.Show("Khách hàng " + deletedCustomer.TenKH + " đã từng giao dịch, vui lòng xóa đơn bán hàng trước khi xóa thông tin nhà cung cấp!", "", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            if(MessageBox.Show("Bạn có chắc chắc muốn xóa khách hàng " + deletedCustomer.TenKH + " không?", "", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.No)
            {
                return;
            }
            DataProvider.Ins.DB.KhachHangs.Remove(deletedCustomer);
            DataProvider.Ins.DB.SaveChanges();
            CustomerList.Remove(SelectedCustomer);


        }
        void loadCustomer(CustomerView view)
        {
            
        }

    }
}
