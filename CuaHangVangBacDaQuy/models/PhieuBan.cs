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
    
    public partial class PhieuBan:BaseViewModel
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public PhieuBan()
        {
            this.ChiTietPhieuBans = new HashSet<ChiTietPhieuBan>();
        }

        private string _MaPhieu;
        public string MaPhieu { get => _MaPhieu; set { _MaPhieu = value; OnPropertyChanged(); } }

        public Nullable<System.DateTime> NgayLap { get; set; }

        private int _MaKH { get; set; }
        public int MaKH { get => _MaKH; set { _MaKH = value; OnPropertyChanged(); } }

        private int _MaNV { get; set; }
        public int MaNV { get => _MaNV; set { _MaNV = value; OnPropertyChanged(); } }

        private int _ThanhToan { get; set; }
        public int ThanhToan { get => _ThanhToan; set { _ThanhToan = value; OnPropertyChanged(); }}

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ChiTietPhieuBan> ChiTietPhieuBans { get; set; }

        private KhachHang _KhachHang;
        public virtual KhachHang KhachHang { get => _KhachHang; set { _KhachHang = value; OnPropertyChanged(); } }

        public virtual NguoiDung NguoiDung { get; set; }
    }
}
