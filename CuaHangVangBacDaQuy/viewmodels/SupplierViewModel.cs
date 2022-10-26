using CuaHangVangBacDaQuy.models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CuaHangVangBacDaQuy.viewmodels
{
    public class SupplierViewModel: BaseViewModel
    {
        #region Params
        private ObservableCollection<NhaCungCap> _NhaCungCapList;
        public ObservableCollection<NhaCungCap> NhaCungCapList { get => _NhaCungCapList; set { _NhaCungCapList = value; OnPropertyChanged(); } }
        #endregion
        #region Command
        #endregion
        public SupplierViewModel()
        {
            NhaCungCapList = new ObservableCollection<NhaCungCap>(DataProvider.Ins.DB.NhaCungCaps);
        }
    }
}
