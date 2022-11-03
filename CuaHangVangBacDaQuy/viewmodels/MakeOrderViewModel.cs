using CuaHangVangBacDaQuy.models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;


namespace CuaHangVangBacDaQuy.viewmodels
{
    public class MakeOrderViewModel: BaseViewModel
    {
        #region


        string caseButtonSaveDialog { get; set; }

        private ObservableCollection<NhaCungCap> _SuppliersList;
        public ObservableCollection<NhaCungCap> SuppliersList
        {
            get => _SuppliersList;
            set { _SuppliersList = value; OnPropertyChanged(); }
        }

        private bool _IsOpenAddSupplierDialog;
        public bool IsOpenAddSupplierDialog
        {
            get { return _IsOpenAddSupplierDialog; }
            set { _IsOpenAddSupplierDialog = value; OnPropertyChanged(); }
        }

        
        private string _TitleDiaLog;
        public string TitleDiaLog
        {
            get { return _TitleDiaLog; }
            set { _TitleDiaLog = value; OnPropertyChanged(); }
        }



        private string _SupplierName;
        public string SupplierName
        {
            get => _SupplierName;
            set
            {
                _SupplierName = value; OnPropertyChanged();
            }
        }



        private string _SupplierAddress;
        public string SupplierAddress
        {
            get => _SupplierAddress;
            set
            {
                _SupplierAddress = value; OnPropertyChanged();
            }
            
        }


        private string _SupplierPhoneNumber;
        public string SupplierPhoneNumber { get => _SupplierPhoneNumber; set { _SupplierPhoneNumber = value; OnPropertyChanged(); } }


        private string _SupplierSelectedName;
        public string SupplierSelectedName
        {
            get => _SupplierSelectedName;
            set
            {
                _SupplierSelectedName = value; OnPropertyChanged();
            }
        }



        private string _SupplierSelectedAddress;
        public string SupplierSelectedAddress
        {
            get => _SupplierSelectedAddress;
            set
            {
                _SupplierSelectedAddress = value; OnPropertyChanged();
            }

        }


        private string _SupplierSelectedPhoneNumber;
        public string SupplierSelectedPhoneNumber { get => _SupplierSelectedPhoneNumber; set { _SupplierSelectedPhoneNumber = value; OnPropertyChanged(); } }



        private NhaCungCap _SelectedSupplier;
        public NhaCungCap SelectedSupplier
        {
            get
            {
                return _SelectedSupplier;
            } 
            set
            {

                
               
                if (SelectedSupplier != null )
                {

                   
                        SupplierSelectedName = SelectedSupplier.TenNCC;
                        SupplierSelectedAddress = SelectedSupplier.DiaChi;
                        SupplierSelectedPhoneNumber = SelectedSupplier.SoDT;
                    VisibilitySelectedSupplier = "Visible";




                }
                _SelectedSupplier = value;
                OnPropertyChanged();



            }
        }

        private string _VisibilitySelectedSupplier = "Collapsed";
        public string VisibilitySelectedSupplier 
        {
            get => _VisibilitySelectedSupplier;
            set
            {
              
                _VisibilitySelectedSupplier = value;
                OnPropertyChanged();

            }
        }

        public ICommand AddSupplierCommand { get; set; }
        public ICommand RemoveSelectedSupplierCommand { get; set; }
        public ICommand EditSelectedSupplierCommand { get; set; }
        public ICommand SaveAddCommand { get; set; }
      
       

        

        #endregion

        public MakeOrderViewModel()
        {

           

            SuppliersList = new ObservableCollection<NhaCungCap>(DataProvider.Ins.DB.NhaCungCaps);

            RemoveSelectedSupplierCommand = new RelayCommand<MakeOrderViewModel>((p) => true, p => {  VisibilitySelectedSupplier = "Collapsed";  });

            EditSelectedSupplierCommand = new RelayCommand<MakeOrderViewModel>((p) => true, p => {

                
                TitleDiaLog = "Chỉnh sửa thông tin nhà cung cấp";
                SupplierName = SupplierSelectedName;
                SupplierAddress = SupplierSelectedAddress;
                SupplierPhoneNumber = SupplierSelectedPhoneNumber;

                caseButtonSaveDialog = "Edit";
                IsOpenAddSupplierDialog = true;
                

            });
            

            AddSupplierCommand = new RelayCommand<MakeOrderViewModel>((p) => true, p => { TitleDiaLog = "Thêm nhà cung cấp"; ClearFieldDialog(); IsOpenAddSupplierDialog = true; caseButtonSaveDialog = "Add"; });          
            SaveAddCommand = new RelayCommand<MakeOrderViewModel>((p) => checkData(), p => {


                switch (caseButtonSaveDialog)
                {

                    case "Edit":
                        {
                            actionEditSupplier(); break;
                        }

                    case "Add":
                        {
                            actionAddSupplier(); break;
                        }                
                }
              
            
            });

        }


        
        public void actionAddSupplier()
        {

            var newSupplier = new NhaCungCap()
            {
                TenNCC = SupplierName,
                DiaChi = SupplierAddress,
                SoDT = SupplierPhoneNumber,

            };
            DataProvider.Ins.DB.NhaCungCaps.Add(newSupplier);
            DataProvider.Ins.DB.SaveChanges();
            SelectedSupplier = newSupplier;
            IsOpenAddSupplierDialog = false;

        }

        public void actionEditSupplier()
        {

        }

        void ClearFieldDialog()
        {
            SupplierName = "";
            SupplierAddress = "";
            SupplierPhoneNumber = "";
        }

        bool checkData()
        {
            if (string.IsNullOrEmpty(SupplierName) || string.IsNullOrEmpty(SupplierAddress) || string.IsNullOrEmpty(SupplierPhoneNumber))
            {
                return false;
            }
            return true;
        }


    }
}
