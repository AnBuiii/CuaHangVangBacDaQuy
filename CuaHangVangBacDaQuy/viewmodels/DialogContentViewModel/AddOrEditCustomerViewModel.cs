using CuaHangVangBacDaQuy.models;
using CuaHangVangBacDaQuy.views.userControl;
using CuaHangVangBacDaQuy.views.userControlDialog;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace CuaHangVangBacDaQuy.viewmodels.DialogContentViewModel
{
    public class AddOrEditCustomerViewModel : BaseViewModel
    {
        private  ObservableCollection<KhachHang> CustomerList;

        private KhachHang _EditedCustomer;
        public  KhachHang EditedCustomer
        {
            get => _EditedCustomer;

            set {
                _EditedCustomer = value;
                OnPropertyChanged();

                if(EditedCustomer != null)
                {
                   
                    CustomerName = value.TenKH;
                    Gender = value.GioiTinh;
                    Address = value.DiaChi;
                    PhoneNumber = value.SoDT;
                    
                }
            }
        }

        private readonly OpenDiaLog openDiaLog;

        private string _TilteView;
        public string TilteView { get => _TilteView; set { _TilteView = value; OnPropertyChanged(); } }


        private string _CustomerName;
        public string CustomerName
        {
            get => _CustomerName;
            set
            {
                _CustomerName = value; OnPropertyChanged();
            }
        }


        private string _Gender;
        public string Gender
        {
            get => _Gender;
            set
            {
                _Gender = value; OnPropertyChanged();
            }
        }


        private string _Address;
        public string Address { get => _Address; set { _Address = value; OnPropertyChanged(); } }

        private string _PhoneNumber;
        public string PhoneNumber { get => _PhoneNumber; set { _PhoneNumber = value; OnPropertyChanged(); } }

        public ICommand SaveCommand { get; set; }
        public ICommand CancelCommand { get; set; }


        public AddOrEditCustomerViewModel()
        {

        }

        
        public AddOrEditCustomerViewModel(string tilteView, ref OpenDiaLog isOpenDialog, ref ObservableCollection<KhachHang> customersList)
        {
           

            TilteView = tilteView;
            openDiaLog = isOpenDialog;
            CustomerList = customersList;

            CancelCommand = new RelayCommand<AddOrEditCustomerUC>((p) => true, p => CheckCloseDiaLog());
            SaveCommand = new RelayCommand<AddOrEditCustomerUC>((p) => CheckEmptyFieldDialog(), p => ActionAddCustomer());
          

        }

        public AddOrEditCustomerViewModel(string tilteView, ref OpenDiaLog isOpenDialog, ref ObservableCollection<KhachHang> customersList, ref KhachHang editedCustomer)
        {
            TilteView = tilteView;
            openDiaLog = isOpenDialog;
            CustomerList = customersList;
            EditedCustomer = editedCustomer;
            CancelCommand = new RelayCommand<AddOrEditCustomerUC>((p) => true, p => CheckCloseDiaLog());
            SaveCommand = new RelayCommand<AddOrEditCustomerUC>((p) => CheckEmptyFieldDialog(), p => ActionEditCustomer());
           
            
        }

        


        bool CheckEmptyFieldDialog()
        {
            if (string.IsNullOrEmpty(CustomerName) || string.IsNullOrEmpty(Gender) || string.IsNullOrEmpty(Address) || string.IsNullOrEmpty(PhoneNumber))
            {
                return false;
            }
            return true;
        }

        public void ActionAddCustomer()
        {
            if (!CheckValidPhoneNumber() || !CheckExistPhoneNumer()) return;

            var newCus = new KhachHang()
            {
                TenKH = CustomerName,
                GioiTinh = Gender,
                DiaChi = Address,
                SoDT = PhoneNumber,
                NgayDK = System.DateTime.Now

            };
           
            DataProvider.Ins.DB.KhachHangs.Add(newCus);
            DataProvider.Ins.DB.SaveChanges();
            CustomerList.Add(newCus);
            openDiaLog.IsOpen= false;


        }

        public void ActionEditCustomer()
        {
            if (!CheckValidPhoneNumber()) return;
            openDiaLog.IsOpen = false;
            var customer = DataProvider.Ins.DB.KhachHangs.Where(x => x.MaKH == EditedCustomer.MaKH).SingleOrDefault();          
            customer.TenKH = CustomerName;
            customer.GioiTinh = Gender;
            customer.DiaChi = Address;
            DataProvider.Ins.DB.SaveChanges();
        }

        
        bool CheckValidPhoneNumber()
        {
            if (!CheckField.CheckPhone(PhoneNumber))
            {

                MessageBox.Show("Vui lòng nhập đúng định dạng số điện thoại!", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            return true;


        }

        bool CheckExistPhoneNumer()
        {

            if (CustomerList.Where(p => p.SoDT == PhoneNumber).Count() > 0)
            {
                MessageBox.Show("Số điện thoại đã tồn tại!", "", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            return true;
        }
        private void CheckCloseDiaLog()
        {
           
            if (MessageBox.Show("Những thay đổi của bạn sẽ không được lưu?", "",
                 MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
            {
               
                openDiaLog.IsOpen = false;
               
            }
         

        }
       
    }
}
