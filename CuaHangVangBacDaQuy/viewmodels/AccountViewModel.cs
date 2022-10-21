using CuaHangVangBacDaQuy.models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CuaHangVangBacDaQuy.viewmodels
{
    public class AccountViewModel: BaseViewModel
    {
        private ObservableCollection<TonKho> _NguoiDungList;
        public ObservableCollection<TonKho> NguoiDungList { get => _NguoiDungList; set { _NguoiDungList = value; OnPropertyChanged(); } }
        public AccountViewModel()
        {
           
        }
    }
}
