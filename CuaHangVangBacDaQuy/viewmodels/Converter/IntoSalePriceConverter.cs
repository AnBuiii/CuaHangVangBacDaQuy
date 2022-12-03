using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace CuaHangVangBacDaQuy.viewmodels.Converter
{
    public class IntoSalePriceConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values != null && values.Length > 0 && values[1] != null && values[0] != null)
            {



                decimal intoMoney = (decimal)values[0] * (1 + (decimal)values[1]);
                return intoMoney.ToString("#,##0.");
            }
            ;
            return "0";
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            return (object[])Binding.DoNothing;
        }
    }
}
