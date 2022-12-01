using CuaHangVangBacDaQuy.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace CuaHangVangBacDaQuy.viewmodels.Converter
{
    public class PriceConverter: IValueConverter
    {
        //giá trị nhập trước đó
        private int PastValue = 0;

        // Convert số lượng nhập từ model ra view
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {       
            if(value == null || (int)value == 0) return 0;
            PastValue = (int)value;
            return (int)value;
        }

        //Convert số lượng nhập hàng từ view vào model
        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
           
            if (value == null|| value.ToString() == "") return 0;
            string str = value as string;
            str = str.Replace(",", "");

            if (CheckField.CheckNumber(str))
            {
                
                return value;
            }
          
            return PastValue;
                   
        }


    }
}
