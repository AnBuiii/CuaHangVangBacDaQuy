﻿using CuaHangVangBacDaQuy.models;
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
    public class AddOrEditSupplierViewModel:BaseViewModel
    {
        private readonly ObservableCollection<NhaCungCap> SuppliersList;
        
        private NhaCungCap _EditedSupplier;
        public NhaCungCap EditedSupplier
        {
            get => _EditedSupplier;

            set
            {
                _EditedSupplier = value;
                OnPropertyChanged();

                if (EditedSupplier != null)
                {
                  
                        SupplierName = EditedSupplier.TenNCC;
                        SupplierAddress = EditedSupplier.DiaChi;
                        SupplierPhoneNumber = EditedSupplier.SoDT;
                                         

                }
            }
        }

        private readonly OpenDiaLog openDiaLog;

        private string _TitleView;
        public string TitleView { get => _TitleView; set { _TitleView = value; OnPropertyChanged(); } }


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

        public ICommand SaveCommand { get; set; }
        public ICommand CancelCommand { get; set; }


        public AddOrEditSupplierViewModel()
        {

        }

        //constructor used for add new supplier 
        public AddOrEditSupplierViewModel(string tilteView, ref OpenDiaLog isOpenDialog, ref ObservableCollection<NhaCungCap> suppliersList)
        {


            TitleView = tilteView;
            openDiaLog = isOpenDialog;
            SuppliersList = suppliersList;

            CancelCommand = new RelayCommand<AddOrEditSupplierUC>((p) => true, p => CheckCloseDiaLog());
            SaveCommand = new RelayCommand<AddOrEditSupplierUC>((p) => checkEmptyFieldDialog(), p => ActionAddSupplier());


        }

        //constructor used for edit supplier
        public AddOrEditSupplierViewModel(string tilteView, ref OpenDiaLog isOpenDialog, ref ObservableCollection<NhaCungCap> suppliersList, ref NhaCungCap editedSupplier)
        {
            TitleView = tilteView;
            openDiaLog = isOpenDialog;
            SuppliersList = suppliersList;
            EditedSupplier = editedSupplier;
            CancelCommand = new RelayCommand<AddOrEditSupplierUC>((p) => true, p => CheckCloseDiaLog());
            SaveCommand = new RelayCommand<AddOrEditSupplierUC>((p) => checkEmptyFieldDialog(), p => ActionEditCustomer());
        }



        //constructor used for add new supplier in make pushchase oder view

        //public AddSupplierViewModel(string tilteView, ref OpenDiaLog isOpenDialog, ref ObservableCollection<NhaCungCap> suppliersList, ref ObservableCollection<NhaCungCap> selectedSuppliersList)
        //{
        //    TitleView = tilteView;
        //    openDiaLog = isOpenDialog;
        //    SuppliersList = suppliersList;
        //    SelectedSuppliersList = selectedSuppliersList;
        //    CancelCommand = new RelayCommand<AddSupplierUC>((p) => true, p => CheckCloseDiaLog());
        //    SaveCommand = new RelayCommand<AddSupplierUC>((p) => checkEmptyFieldDialog(), p => ActionAddSupplier());
        //}


       


        bool checkEmptyFieldDialog()
        {

            if (string.IsNullOrEmpty(SupplierName) || string.IsNullOrEmpty(SupplierAddress) || string.IsNullOrEmpty(SupplierPhoneNumber))
            {
                return false;
            }
            return true;
        }

        private void ActionAddSupplier()
        {
            if (!CheckValidPhoneNumber() || !CheckExistPhoneNumer()) return;

            var newSup = new NhaCungCap()
            {
                TenNCC = SupplierName,
                DiaChi = SupplierAddress,
                SoDT = SupplierPhoneNumber,               

            };

            DataProvider.Ins.DB.NhaCungCaps.Add(newSup);
            DataProvider.Ins.DB.SaveChanges();           
            SuppliersList.Add(newSup);                     
            openDiaLog.IsOpen = false;
        }

        private void ActionEditCustomer()
        {
            if (!CheckValidPhoneNumber()) return;
            openDiaLog.IsOpen = false;
            var supplier = DataProvider.Ins.DB.NhaCungCaps.Where(x => x.MaNCC == EditedSupplier.MaNCC).SingleOrDefault();
            supplier.TenNCC = SupplierName;
            supplier.DiaChi = SupplierAddress;
            supplier.SoDT = SupplierPhoneNumber;
            DataProvider.Ins.DB.SaveChanges();
        }


        bool CheckValidPhoneNumber()
        {
            if (!CheckField.CheckPhone(SupplierPhoneNumber))
            {

                MessageBox.Show("Vui lòng nhập đúng định dạng số điện thoại!", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            return true;


        }

        bool CheckExistPhoneNumer()
        {

            if (SuppliersList.Where(p => p.SoDT == SupplierPhoneNumber).Count() > 0)
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
