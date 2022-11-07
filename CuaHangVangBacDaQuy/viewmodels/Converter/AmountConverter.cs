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

        // Convert số lượng nhập từ model ra view
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {       
            if(value == null || (int)value == 0) return "";
            return value;
        }

        //Convert số lượng nhập hàng từ view vào model
        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            
            if (value == null) return 0;

            if (CheckField.checkNumber((string)value))
            {
                if (System.Convert.ToInt64(value) <= 2147483647) // giới hạn int trong sql
                {
                    return value;
                }
                else
                {
                    MessageBox.Show("Giá trị nhập quá lớn!", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            
            return 0;
                   
        }


    }
}
