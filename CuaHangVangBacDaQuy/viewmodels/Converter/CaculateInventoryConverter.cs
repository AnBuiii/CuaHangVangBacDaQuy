using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using CuaHangVangBacDaQuy.models;

namespace CuaHangVangBacDaQuy.viewmodels.Converter
{
    public class CaculateInventoryConverter : IValueConverter
    {
        //Convert Tính tồn kho sản phẩm
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {

            if(value != null)
            {
                var productCode = (string)value;

                return CaculateInventory(productCode);

                
            }
           return null;

        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return Binding.DoNothing;
        }

        public static int CaculateInventory(string productCode)
        {
            var MuaList = DataProvider.Ins.DB.ChiTietPhieuMuas.Where(p => p.MaSP == productCode);
            var BanList = DataProvider.Ins.DB.ChiTietPhieuBans.Where(p => p.MaSP == productCode);

            int sumMua = 0;
            int sumBan = 0;

            if (MuaList != null && MuaList.Count() > 0)
            {
                sumMua = (int)MuaList.Sum(p => p.SoLuong);
            }

            if (BanList != null && BanList.Count() > 0)
            {
                sumBan = (int)BanList.Sum(p => p.SoLuong);
            }
            return sumMua - sumBan;
        }
    }
}
