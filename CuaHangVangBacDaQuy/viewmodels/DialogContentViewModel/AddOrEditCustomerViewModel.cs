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

        public int customerCode;

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

        bool ValidCustomerCheck()
        {

            if (EditedCustomer == null)
            {
                if (DataProvider.Ins.DB.KhachHangs.Where(x => x.TenKH == CustomerName).Count() > 0) return false;
                if (DataProvider.Ins.DB.KhachHangs.Where(x => x.SoDT == PhoneNumber).Count() > 0) return false;
            }
            else
            {
                if (CustomerName != EditedCustomer.TenKH)
                {
                    if (DataProvider.Ins.DB.KhachHangs.Where(x => x.TenKH == CustomerName).Count() > 0) return false;
                }
                if (PhoneNumber != EditedCustomer.SoDT)
                {

                    if (DataProvider.Ins.DB.KhachHangs.Where(x => x.SoDT == PhoneNumber).Count() > 0) return false;
                }


            }
            return true;

            //return ((EditedProduct == null && DataProvider.Ins.DB.SanPhams.Where(x => x.TenSP == ProductName).Count() == 0) || ()) && ProductPrice > 0;

        }


        bool CheckEmptyFieldDialog()
        {
            if (string.IsNullOrWhiteSpace(CustomerName) || string.IsNullOrWhiteSpace(Gender) || string.IsNullOrWhiteSpace(Address) || string.IsNullOrWhiteSpace(PhoneNumber))
            {
                return false;
            }
            return true;
        }

        public void ActionAddCustomer()
        {
            if (!CheckEmptyFieldDialog()) return;
            if (!CheckValidPhoneNumber() || !ValidCustomerCheck()) return;

            var newCus = new KhachHang()
            {
                TenKH = CustomerName,
                GioiTinh = Gender,
                DiaChi = Address,
                SoDT = PhoneNumber,
                NgayDK = DateTime.Now

            };
           
            DataProvider.Ins.DB.KhachHangs.Add(newCus);
            DataProvider.Ins.DB.SaveChanges();
            if(openDiaLog != null)
            {
                CustomerList.Add(newCus);
                openDiaLog.IsOpen = false;
            }
            customerCode = DataProvider.Ins.DB.KhachHangs.Where(x => x.TenKH == CustomerName).FirstOrDefault().MaKH;




        }

        public void ActionEditCustomer()
        {
            if (!CheckEmptyFieldDialog()) return;
            if (!CheckValidPhoneNumber() || !ValidCustomerCheck()) return;

            if (openDiaLog != null)
            {

                openDiaLog.IsOpen = false;
            }
            var customer = DataProvider.Ins.DB.KhachHangs.Where(x => x.MaKH == EditedCustomer.MaKH).SingleOrDefault();          
            customer.TenKH = CustomerName;
            customer.GioiTinh = Gender;
            customer.DiaChi = Address;
            customer.SoDT = PhoneNumber;
            DataProvider.Ins.DB.SaveChanges();
        }


        bool CheckValidPhoneNumber()
        {
            if (!CheckField.CheckPhone(PhoneNumber))
            {
                if (CustomerList != null)
                {
                    MessageBox.Show("Vui lòng nhập đúng định dạng số điện thoại!", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);

                }
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
