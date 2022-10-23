using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace CuaHangVangBacDaQuy.models
{
    class CurrentAccount
    {
        private static CurrentAccount _ins;
        public static CurrentAccount Ins
        {
            get
            {
                if (_ins == null)
                    _ins = new CurrentAccount();
                return _ins;
            }
            set
            {
                _ins = value;
            }
        }

        public int MaND { get; set; }
        public string TenND { get; set; }
        public int MaQH { get; set; }

        public void GetCurrentAccount(string username)
        {
            NguoiDung nguoiDung = new NguoiDung();
            nguoiDung = DataProvider.Ins.DB.NguoiDungs.Where(x => x.TenDangNhap == username).SingleOrDefault();

            MaND = nguoiDung.MaND; 
            TenND = nguoiDung.TenND;
            MaQH = nguoiDung.MaQH;
        }

        public void DisposeCurrentAccount()
        {
            MaND = -1;
            TenND = "";
            MaQH = -1;
        }
        public bool isAdmin()
        {
            return DataProvider.Ins.DB.QuyenHans.Where(x=> x.MaQH == MaQH  && x.MaQH == 1).Count() > 0;
        }
    }
}
