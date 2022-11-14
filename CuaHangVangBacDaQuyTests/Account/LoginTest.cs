using CuaHangVangBacDaQuy.models;
using CuaHangVangBacDaQuy.viewmodels;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CuaHangVangBacDaQuyTests.Account
{
    [TestFixture]
    public class LoginTest
    {
        private List<NguoiDung> listNguoiDung;
        [SetUp]
        public void SetUp()
        {
            listNguoiDung = new List<NguoiDung>() { 
                new NguoiDung() { MaND = 1, TenND = "ADMIN", TenDangNhap = "admin", MatKhau = "db69fc039dcbd2962cb4d28f5891aae1", MaQH = 1 }, 
                new NguoiDung() { MaND = 2, TenND = "An Bùi", TenDangNhap = "builehoaian", MatKhau = "9c600b0cee325636ffbaac14db0841c4", MaQH = 2 },
                new NguoiDung() { MaND = 3, TenND = "ADMIN", MatKhau = "db69fc039dcbd2962cb4d28f5891aae1", MaQH = 1 },
            };

        }
        #region testcase
        [TestCase("admin","admin", true)]
        [TestCase("builehoaian", "anbui", true)]
        [TestCase(null,null, false)]
        #endregion

        public void Login(string username, string password, bool expect)
        {
            //thay hàm login
            int accountFound = 0;
            if(!(username == null || password == null))
            {
                string a = LoginViewModel.Base64Encode(password);
                string passEncode = LoginViewModel.MD5Hash(a);

                accountFound = listNguoiDung.Where(x => x.TenDangNhap == username && x.MatKhau == passEncode).Count();
            }
           
            Assert.AreEqual(accountFound> 0, expect, "thiếu thông tin đăng nhập");
 
        }
    }
}
