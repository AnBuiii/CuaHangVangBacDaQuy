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
    using System;
    using System.Collections.Generic;
    
    public partial class PhieuBan
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public PhieuBan()
        {
            this.ChiTietPhieuBans = new HashSet<ChiTietPhieuBan>();
        }
    
        public string MaPhieu { get; set; }
        public Nullable<System.DateTime> NgayLap { get; set; }
        public int MaKH { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ChiTietPhieuBan> ChiTietPhieuBans { get; set; }
        public virtual KhachHang KhachHang { get; set; }
    }
}
