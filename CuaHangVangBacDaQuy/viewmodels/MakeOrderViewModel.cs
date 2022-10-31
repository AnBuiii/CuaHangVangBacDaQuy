using CuaHangVangBacDaQuy.models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CuaHangVangBacDaQuy.viewmodels
{
    public class MakeOrderViewModel: BaseViewModel
    {
        #region Params
        private ObservableCollection<KhachHang> _CustomerList;
        public ObservableCollection<KhachHang> CustomerList
        {
            get => _CustomerList;
            set { _CustomerList = value; OnPropertyChanged(); }
        }

        private bool _IsOpenAddCustomerDialog;
        public bool IsOpenAddCustomerDialog
        {
            get { return _IsOpenAddCustomerDialog; }
            set { _IsOpenAddCustomerDialog = value; OnPropertyChanged(); }
        }
        #endregion

    }
}
