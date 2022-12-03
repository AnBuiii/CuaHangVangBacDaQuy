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
    public class TotalPriceOfASaleOrder : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {

            string receiptCode = value as string;
            List<ChiTietPhieuBan> detailReceiptList = new List<ChiTietPhieuBan>(DataProvider.Ins.DB.ChiTietPhieuBans.Where(p => p.MaPhieu == receiptCode));
            decimal? totalMoney = detailReceiptList.Sum(p => p.SoLuong * p.SanPham.DonGia * (1 + p.SanPham.LoaiSanPham.LoiNhuan));
            decimal result = (decimal)((totalMoney == null) ? 0 : totalMoney);
            return result.ToString("#,##0.");
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
