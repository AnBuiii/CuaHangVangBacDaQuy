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
    
    public partial class PhieuMua:BaseViewModel
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public PhieuMua()
        {
            this.ChiTietPhieuMuas = new HashSet<ChiTietPhieuMua>();
        }

        private string _MaPhieu;
        public string MaPhieu { get => _MaPhieu; set { _MaPhieu = value; OnPropertyChanged(); } }
        public Nullable<System.DateTime> NgayLap { get; set; }

        private int _MaNCC { get; set; }
        public int MaNCC { get => _MaNCC; set { _MaNCC = value; OnPropertyChanged(); } }

        private int _MaNV { get; set; }
        public int MaNV { get => _MaNV; set { _MaNV = value; OnPropertyChanged(); } }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ChiTietPhieuMua> ChiTietPhieuMuas { get; set; }

        private NhaCungCap _NhaCungCap;

        public virtual NhaCungCap NhaCungCap { get => _NhaCungCap; set { _NhaCungCap = value; OnPropertyChanged(); } }
        public virtual NguoiDung NguoiDung { get; set; }
    }
}
