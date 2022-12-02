using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace CuaHangVangBacDaQuy.viewmodels.Converter
{
    public class IntoSaleMoneyConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values != null && values.Length > 0 && values[1] != null && values[0] != null)
            {
                int amount = (int)values[0];
                //MessageBox.Show(values[1].ToString());
                decimal unitPrice = 0;
                if (values[1] != null)
                {
                    decimal t;
                    if (decimal.TryParse(values[1].ToString(), out t))
                    {
                        unitPrice = (values[1] == null) ? 0 : decimal.Parse(values[1].ToString());
                    }
                }


                decimal intoMoney = amount * unitPrice * (  1);
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
