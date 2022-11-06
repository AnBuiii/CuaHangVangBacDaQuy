using CuaHangVangBacDaQuy.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace CuaHangVangBacDaQuy.viewmodels.Converter
{
    public class DecimalConverter: IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {

           // if (value == null) return "";
            //return (int)value;
            if (value == null||(decimal)value == 0) return "";
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
