using CuaHangVangBacDaQuy.models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace CuaHangVangBacDaQuy.viewmodels.Converter
{
    public class TotalPriceOfAReceipt : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string receiptCode = value as string;
            List<ChiTietPhieuMua> detailReceiptList = new List<ChiTietPhieuMua>(DataProvider.Ins.DB.ChiTietPhieuMuas.Where(p=>p.MaPhieu == receiptCode));
            decimal? totalMoney = detailReceiptList.Sum(p => p.SoLuong * p.SanPham.DonGia);
            decimal result = (decimal)((totalMoney == null) ? 0 : totalMoney);
            return  result.ToString("#,##0.");
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return Binding.DoNothing;
        }
    }
}
