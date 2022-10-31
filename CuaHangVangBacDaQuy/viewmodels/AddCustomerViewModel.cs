using CuaHangVangBacDaQuy.models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;

namespace CuaHangVangBacDaQuy.viewmodels
{
    public class AddCustomerViewModel:BaseViewModel
    {



        private string _FirstName;
        public string FirstName { get => _FirstName; set { _FirstName = value; OnPropertyChanged(); } }

        private string _LastName;
        public string LastName { get => _LastName; set { _LastName = value; OnPropertyChanged(); } }

        private string _Gender;
        public string Gender {
            get => _Gender; 
            set { 
                _Gender = value; OnPropertyChanged();
            } 
        }


        private string _Address;
        public string Address { get => _Address; set { _Address = value; OnPropertyChanged(); } }

        private string _PhoneNumber;
        public string PhoneNumber { get => _PhoneNumber; set { _PhoneNumber = value; OnPropertyChanged(); } }

      

        public ICommand AddCommand { get; set; }
        public ICommand EditCommand { get; set; }

        public AddCustomerViewModel() {


        }



        public AddCustomerViewModel(ObservableCollection<KhachHang> listCustomer, bool isOpenDialog)
        {

            AddCommand = new RelayCommand<AddCustomerViewModel>((p) =>
            {


                if (!checkData()) return false;
                var phone = DataProvider.Ins.DB.KhachHangs.Where(x => x.SoDT == PhoneNumber);
               
                if (phone != null || phone.Count() > 0) return false;
              
                return true;
            },

            (p) =>
            {


                addCustomer(listCustomer);


            });


        }


        bool checkData()
        {
            if(string.IsNullOrEmpty(FirstName)|| string.IsNullOrEmpty(LastName)|| string.IsNullOrEmpty(LastName)|| string.IsNullOrEmpty(Gender)|| string.IsNullOrEmpty(Address) || string.IsNullOrEmpty(PhoneNumber))
            {
                return false;
            }
            return true;
        }

        private void addCustomer(ObservableCollection<KhachHang> listCus)
        {
           
                var newCus = new KhachHang() {
                TenKH = FirstName + " " + LastName,
                GioiTinh = Gender,
                DiaChi = Address,
                SoDT = PhoneNumber,
                NgayDK = System.DateTime.Today

            };
            DataProvider.Ins.DB.KhachHangs.Add(newCus);
            DataProvider.Ins.DB.SaveChanges();
            listCus.Add(newCus);
          
            
            
        
        }
      
    }
}
