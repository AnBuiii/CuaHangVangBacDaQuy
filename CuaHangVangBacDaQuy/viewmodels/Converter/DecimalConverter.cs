using CuaHangVangBacDaQuy.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace CuaHangVangBacDaQuy.viewmodels.Converter
{
    public class DecimalConverter: IValueConverter
    {

        //Convert số đơn giá từ model ra view
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {          
            if ( value == null||(decimal)value == 0) return "";
            return value;
        }

        //Convert số đơn giá từ view vào model

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
           
            if (value == null) return 0;
            //MessageBox.Show(value.GetType().ToString());

            if (CheckField.checkNumber((string)value))
            {
                if (System.Convert.ToInt64(value) < 922337203685477) // giới hạn money trong sql
                {
                     return value;
                }
                else
                {
                    MessageBox.Show("Giá trị nhập quá lớn!", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);

                }
            }
           return null;

        }
    }
}
