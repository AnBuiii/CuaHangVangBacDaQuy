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

        private bool _IsOpenAddCustomerDialog;
        public bool IsOpenAddCustomerDialog
        {
            get { return _IsOpenAddCustomerDialog; }
            set { _IsOpenAddCustomerDialog = value; OnPropertyChanged(); }
        }


        private string _FirstName;
        public string FirstName { get => _FirstName; set { _FirstName = value; OnPropertyChanged(); } }

        private string _LastName;
        public string LastName { get => _LastName; set { _LastName = value; OnPropertyChanged(); } }

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



        public ICommand SaveAddCommand { get; set; }
        public ICommand EditCommand { get; set; }
        public ICommand LoadCustomerView { get; set; }
        public ICommand AddCommand { get; set; }
        

        public ICommand LoadCustomerView { get; set; }
        #endregion

        public CustomerViewModel()
        {
            CustomerList = new ObservableCollection<KhachHang>(DataProvider.Ins.DB.KhachHangs);

            AddCommand = new RelayCommand<CustomerView>((p) => true, p => IsOpenAddCustomerDialog = true);


            SaveAddCommand = new RelayCommand<CustomerView>((p) =>
            {
               

                if (!checkData()) return false;
                var phone = DataProvider.Ins.DB.KhachHangs.Where(x => x.SoDT == PhoneNumber);

             
                     return true;
            }, p => { actionAddCustomer(); });

           EditCommand = new RelayCommand<CustomerView>((p) => true, p => IsOpenAddCustomerDialog = true);
            
            LoadCustomerView = new RelayCommand<CustomerView>((p) => true, (p) => loadCustomer(p));
        }


        public void actionAddCustomer()
        {

            var newCus = new KhachHang()
            {
                TenKH = FirstName + " " + LastName,
                GioiTinh = Gender,
                DiaChi = Address,
                SoDT = PhoneNumber,
                NgayDK = System.DateTime.Today

            };
            DataProvider.Ins.DB.KhachHangs.Add(newCus);
            DataProvider.Ins.DB.SaveChanges();
            CustomerList.Add(newCus);
            IsOpenAddCustomerDialog = false;

        }


        bool checkData()
        {
            if (string.IsNullOrEmpty(FirstName) || string.IsNullOrEmpty(LastName) || string.IsNullOrEmpty(LastName) || string.IsNullOrEmpty(Gender) || string.IsNullOrEmpty(Address) || string.IsNullOrEmpty(PhoneNumber))
            {
                return false;
            }
            return true;
        }

        void loadCustomer(CustomerView view)
        {
            
        }

    }
}
