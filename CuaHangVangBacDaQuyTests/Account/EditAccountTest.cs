using CuaHangVangBacDaQuy.models;
using CuaHangVangBacDaQuy.viewmodels.DialogContentViewModel;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CuaHangVangBacDaQuyTests.Account
{
    internal class EditAccountTest
    {
        private AddOrEditAccountViewModel viewModel;
        private readonly List<string> accountUsernames = new List<string> { null, "(@#$%", "admin", "hm" };
        private readonly List<string> accountNames = new List<string> { null, "#$%^&", "An Bùi" };
        private readonly List<string> accountPasswords = new List<string> { null, "(@#$%", "123456ab" };
        private readonly List<string> accountPermissions = new List<string> { null, "Admin", "Nhân viên" };

        [SetUp]
        public void SetUp()
        {
            viewModel = new AddOrEditAccountViewModel();
        }
        // null check
        [TestCase(0, 2, 2, 2, false)]
        [TestCase(3, 0, 2, 2, false)]
        [TestCase(3, 2, 0, 2, false)]
        [TestCase(3, 2, 2, 0, false)]

        [TestCase(1, 1, 1, 1, true)]
        [TestCase(1, 1, 1, 2, true)]
        [TestCase(1, 1, 2, 1, true)]
        [TestCase(1, 1, 2, 2, true)]
        [TestCase(1, 2, 1, 1, true)]
        [TestCase(1, 2, 1, 2, true)]
        [TestCase(1, 2, 2, 1, true)]
        [TestCase(1, 2, 2, 2, true)]

        [TestCase(2, 1, 1, 1, true)]
        [TestCase(2, 1, 1, 2, true)]
        [TestCase(2, 1, 2, 1, true)]
        [TestCase(2, 1, 2, 2, true)]
        [TestCase(2, 2, 1, 1, true)]
        [TestCase(2, 2, 1, 2, true)]
        [TestCase(2, 2, 2, 1, true)]
        [TestCase(2, 2, 2, 2, true)]

        [TestCase(3, 1, 1, 1, true)]
        [TestCase(3, 1, 1, 2, true)]
        [TestCase(3, 1, 2, 1, true)]
        [TestCase(3, 1, 2, 2, true)]
        [TestCase(3, 2, 1, 1, true)]
        [TestCase(3, 2, 1, 2, true)]
        [TestCase(3, 2, 2, 1, true)]
        [TestCase(3, 2, 2, 2, true)]


        public void EditAccount(int usernameIdx, int namesIdx, int passwordIdx, int permissionsIdx, bool expect)
        {
            NguoiDung hm = DataProvider.Ins.DB.NguoiDungs.FirstOrDefault();
            NguoiDung preEdit = new NguoiDung() { MatKhau = "admin", TenDangNhap = hm.TenDangNhap, MaQH = hm.MaQH, TenND = hm.TenND };

            viewModel.EditedAccount = DataProvider.Ins.DB.NguoiDungs.FirstOrDefault();
            viewModel.AccountUsername = accountUsernames[usernameIdx];
            viewModel.AccountName = accountNames[namesIdx];
            viewModel.PasswordAccount = accountPasswords[passwordIdx];
            viewModel.SelectedPermission = viewModel.PermissionsList.Where(x => x.TenQH == accountPermissions[permissionsIdx]).FirstOrDefault();
            viewModel.ActionEditAccount();

            NguoiDung a = DataProvider.Ins.DB.NguoiDungs.FirstOrDefault();
            //Assert.AreEqual(expect, a.TenND == accountNames[namesIdx] && a.TenDangNhap == accountUsernames[usernameIdx] && a.MaQH == viewModel.PermissionsList.FirstOrDefault(x => x.TenQH == accountPermissions[permissionsIdx]).MaQH && a.MatKhau == AddOrEditAccountViewModel.MD5Hash(AddOrEditAccountViewModel.Base64Encode(accountPasswords[passwordIdx])));

            viewModel.EditedAccount = DataProvider.Ins.DB.NguoiDungs.FirstOrDefault();
            viewModel.AccountUsername = preEdit.TenDangNhap;
            viewModel.AccountName = preEdit.TenND;
            viewModel.PasswordAccount = preEdit.MatKhau;
            viewModel.SelectedPermission = viewModel.PermissionsList.Where(x => x.MaQH == preEdit.MaQH).FirstOrDefault();
            viewModel.ActionEditAccount();
        }
    }
}
