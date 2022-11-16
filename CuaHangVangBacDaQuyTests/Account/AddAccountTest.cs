using CuaHangVangBacDaQuy.models;
using CuaHangVangBacDaQuy.viewmodels.DialogContentViewModel;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace CuaHangVangBacDaQuyTests.Account
{
    [TestFixture]
    internal class AddAccountTest
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

        [TestCase(2, 1, 1, 1, false)]
        [TestCase(2, 1, 1, 2, false)]
        [TestCase(2, 1, 2, 1, false)]
        [TestCase(2, 1, 2, 2, false)]
        [TestCase(2, 2, 1, 1, false)]
        [TestCase(2, 2, 1, 2, false)]
        [TestCase(2, 2, 2, 1, false)]
        [TestCase(2, 2, 2, 2, false)]

        [TestCase(3, 1, 1, 1, true)]
        [TestCase(3, 1, 1, 2, true)]
        [TestCase(3, 1, 2, 1, true)]
        [TestCase(3, 1, 2, 2, true)]
        [TestCase(3, 2, 1, 1, true)]
        [TestCase(3, 2, 1, 2, true)]
        [TestCase(3, 2, 2, 1, true)]
        [TestCase(3, 2, 2, 2, true)]




        public void AddAccount(int usernameIdx, int namesIdx, int passwordIdx, int permissionsIdx, bool expect)
        {
            viewModel.AccountUsername = accountUsernames[usernameIdx];
            viewModel.AccountName = accountNames[namesIdx];
            viewModel.PasswordAccount = accountPasswords[passwordIdx];
            viewModel.SelectedPermission = viewModel.PermissionsList.Where(x => x.TenQH == accountPermissions[permissionsIdx]).FirstOrDefault();
            viewModel.ActionAddAccount();

            int code = viewModel.accountCode;

            NguoiDung a = DataProvider.Ins.DB.NguoiDungs.Where(x => x.MaND == code).FirstOrDefault();

            if (a != null)
            {
                DataProvider.Ins.DB.NguoiDungs.Attach(a);
                DataProvider.Ins.DB.NguoiDungs.Remove(a);
                DataProvider.Ins.DB.SaveChanges();
            }
            Assert.AreEqual(expect, a != null);
        }
    }
}
