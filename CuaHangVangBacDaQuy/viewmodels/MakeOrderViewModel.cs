using CuaHangVangBacDaQuy.models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
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


        private ObservableCollection<ProductAdded> _SelectedProductList;
        public ObservableCollection<ProductAdded> SelectedProductList
        {
            get => _SelectedProductList;
            set {
               
                _SelectedProductList = value; 
                OnPropertyChanged(); 
            }
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



        static int v = 0;
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
                    v++;
                    MessageBox.Show(v.ToString());
                }
               
               
                _SelectedSupplier = value;
                OnPropertyChanged();

            }
        }


        private decimal _TotalMoney;
        public decimal TotalMoney
        {
            get => _TotalMoney;

            set
            {

                _TotalMoney = value;
                OnPropertyChanged();
            }
        }

       
        private SanPham _SelectedProductItem;
        public SanPham SelectedProductItem
        {
            get
            {
                return _SelectedProductItem;
            }
            set
            {

                if (SelectedProductItem != null)
                {

                   
                   if(SelectedProductList.Where(x => x.SanPham.MaSP == SelectedProductItem.MaSP).Count() == 0)
                    {
                        ProductAdded productAdded = new ProductAdded()
                        {
                            SanPham = SelectedProductItem,
                            Amount = 0,
                            IntoMoney = 0,

                        };
                       
                        SelectedProductList.Add(productAdded);
                       
                    };
                   
                    
                }
                _SelectedProductItem = value;

                OnPropertyChanged();


            }
        }

        private ProductAdded _SelectedRemoveItem;
        public ProductAdded SelectedRemoveItem
        {
            get
            {
                return _SelectedRemoveItem;
            }
            set
            {


                _SelectedRemoveItem = value;
                OnPropertyChanged();

            }
        }




        private string _VisibilitySelectedSupplier = "Hidden";
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
        public ICommand RemoveSelectedProductCommand { get; set; }
        public ICommand EditSelectedSupplierCommand { get; set; }
        public ICommand SaveAddCommand { get; set; }
        public ICommand CaculateTotalMoneyCommand  { get; set; }




        #endregion

        public MakeOrderViewModel()
        {

           

            SuppliersList = new ObservableCollection<NhaCungCap>(DataProvider.Ins.DB.NhaCungCaps);

            RemoveSelectedSupplierCommand = new RelayCommand<MakeOrderViewModel>((p) => true, p => {  VisibilitySelectedSupplier = "Hidden";  });

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

            SelectedProductList = new ObservableCollection<ProductAdded>();

            CaculateTotalMoneyCommand = new RelayCommand<DataGridTemplateColumn>(p => true, p => caculateTotalMoney());
            RemoveSelectedProductCommand = new RelayCommand<DataGridTemplateColumn>(p => true, p => removeSelectedProduct());


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
        
        void caculateTotalMoney()
        {
            
            TotalMoney = SelectedProductList.Sum(p => p.IntoMoney);
        }

        void removeSelectedProduct()
        {

            if(SelectedRemoveItem != null)
            {
                SelectedProductList.Remove(SelectedRemoveItem);
            }

           // if(SelectedProductItem != null)
           // {
           //     ProductAdded product = SelectedProductList.Where(p => p.SanPham.MaSP == SelectedProductItem.MaSP).First();


           //     for (int i = 0; i < SelectedProductList.Count; i++)
           //     {
           //         if (product.SanPham.MaSP == SelectedProductList[i].SanPham.MaSP)
           //         {

           //             SelectedProductList.RemoveAt(i);
           //             //MessageBox.Show(SelectedProductList.Count.ToString());
           //             break;
           //         }
           //     }
           // }
           
           //  // SelectedProductList.Remove(product);
           
           //// MessageBox.Show(product.SanPham.TenSP);
        }

    }
}
