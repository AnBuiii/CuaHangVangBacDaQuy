using CuaHangVangBacDaQuy.viewmodels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CuaHangVangBacDaQuy.models
{
    public class ProductAdded : BaseViewModel
    {
        public SanPham SanPham { get; set; }
        public int Stt { get; set; }

        private int _Amount;
        public int Amount { get => _Amount;
            set
            {
                _Amount = value;
                OnPropertyChanged();
                if (Amount != 0)
                {
                    IntoMoney = (decimal)(value * SanPham.DonGia);
                }
                else
                {   
                    IntoMoney = 0;
                }
                Console.WriteLine(Amount.ToString());
               
               
            }
        }

        private  decimal _IntoMoney;
        public decimal IntoMoney
        {
            get => _IntoMoney;
            set
            {
                _IntoMoney = value;
                OnPropertyChanged();
                           
            }
        }

       

    }
}
