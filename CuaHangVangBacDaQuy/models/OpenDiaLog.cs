using CuaHangVangBacDaQuy.viewmodels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CuaHangVangBacDaQuy.models
{
    public class OpenDiaLog:BaseViewModel
    {

        public bool _IsOpen = false;
        public bool IsOpen {
            get => _IsOpen; 
            set { 
               
                _IsOpen = value;
                OnPropertyChanged();
               
            } 
        }

    }
}
