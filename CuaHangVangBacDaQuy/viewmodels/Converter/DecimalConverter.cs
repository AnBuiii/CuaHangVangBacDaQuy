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
        private decimal PastValue = 0;
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {          
            if ( value == null||(decimal)value == 0) return "";
            PastValue = (decimal)value;
            string str = PastValue.ToString("#,##0.");
            return str;
        }

        //Convert số đơn giá từ view vào model

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
           
            if (value == null||value.ToString().Count() == 0) return 0;
            
            string str = value as string;

          
            str = str.Replace(",", "");
            //MessageBox.Show(str);
            if (CheckField.CheckNumber(str))
            {
                
               // MessageBox.Show(str);
                if (System.Convert.ToDecimal(str) < 922337203685477) // giới hạn money trong sql
                {
                     return decimal.Parse(str);
                }
                else
                {
                    MessageBox.Show("Giá trị nhập quá lớn!", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);

                }
            }
            
           return PastValue;

        }
    }
}
