//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace CuaHangVangBacDaQuy.models
{
    using CuaHangVangBacDaQuy.viewmodels;
    using System;
    using System.Collections.Generic;

    public partial class ChiTietPhieuMua : BaseViewModel
    {
        public string MaChiTietPhieu { get; set; }

        private string _MaPhieu { get; set; }
        public string MaPhieu { get => _MaPhieu; set { _MaPhieu = value; OnPropertyChanged(); } }

        private string _MaSP { get; set; }
        public string MaSP { get => _MaSP; set { _MaSP = value; OnPropertyChanged(); } }

        private Nullable<int> _SoLuong { get; set; }
        public Nullable<int> SoLuong { get => _SoLuong; set { _SoLuong = value; OnPropertyChanged(); } }


        private PhieuMua _PhieuMua { get; set; }
        public virtual PhieuMua PhieuMua { get => _PhieuMua; set { _PhieuMua = value; OnPropertyChanged(); } }

        private SanPham _SanPham { get; set; }
        public virtual SanPham SanPham { get => _SanPham; set { _SanPham = value; OnPropertyChanged(); } }
    }
}
