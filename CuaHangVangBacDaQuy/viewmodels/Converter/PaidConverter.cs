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
    public class PaidConverter:IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string code = value as string;
            decimal? total = 0;
            foreach (ChiTietPhieuBan products in DataProvider.Ins.DB.ChiTietPhieuBans.Where(p => p.PhieuBan.MaPhieu == code))
            {
                total += products.SoLuong * products.SanPham.DonGia * (1 + products.SanPham.LoaiSanPham.LoiNhuan);
            }
            total = (int)total;
            total -= DataProvider.Ins.DB.PhieuBans.Where(p => p.MaPhieu == code).FirstOrDefault().ThanhToan;
            
            if (DataProvider.Ins.DB.PhieuBans.Where(p => p.MaPhieu == code).FirstOrDefault().ThanhToan == 0) return "Chưa thanh toán";
            else if (total > 0) return "Còn nợ";
            return "Đã thanh toán";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return Binding.DoNothing;
        }
    }
}
