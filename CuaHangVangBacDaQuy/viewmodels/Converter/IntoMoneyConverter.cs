using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace CuaHangVangBacDaQuy.viewmodels.Converter
{
    public class IntoMoneyConverter : IMultiValueConverter
    {

        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            //tìm textbox số lượng trong datagrid
            var dataGrid = values[0] as DataGrid;
            var textboxes = dataGrid.FindAllVisualDescendants()
                        .Where(elt => elt.Name == "txbSoLuong")
    .                   OfType<TextBox>() ;
            var textBox = textboxes.First();

            //đơn giá 
            var donGia = (decimal)values[1];
            
            if (textBox != null)
            {
                MessageBox.Show(donGia.ToString());
                return  donGia.ToString();
            }
           
           return "0";
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            return (object[])Binding.DoNothing;
        }
    }
}
