using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using CuaHangVangBacDaQuy.models;
namespace CuaHangVangBacDaQuy.viewmodels.Converter
{
    public class AmountConverter : IValueConverter
    {


        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {

            //if (value == null) return "0";
            //return (int)value;
            
            if(value == null || (int)value == 0) return "";
            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            //  return Binding.DoNothing;
            //MessageBox.Show("cc");
            if (CheckField.checkNumber((string)value)) return value; 
            return 0;
           
        }


    }
}
