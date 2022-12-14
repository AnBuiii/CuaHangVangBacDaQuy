using CuaHangVangBacDaQuy.models;
using CuaHangVangBacDaQuy.viewmodels;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CuaHangVangBacDaQuyTests
{
    [TestFixture]
    internal class CheckValidPasswordChange
    {
        private MainViewModel viewModel;

        [SetUp]
        public void SetUp()
        {
            viewModel = new MainViewModel();
            NguoiDung.Logged = DataProvider.Ins.DB.NguoiDungs.Where(x => x.TenDangNhap == "admin").FirstOrDefault();
        }

        [Test]
        public void CheckValidPasswordChange_EmptyOldPassword()
        {
            viewModel.NewPassword = "aDmin";
            viewModel.ConfirmPassword = "aDmin";
            bool check = viewModel.CheckValidPasswordChange();
            Assert.AreEqual(false, check);
        }
        [Test]
        public void CheckValidPasswordChange_EmptyNewPassword()
        {
            viewModel.OldPassword = "admin";
            viewModel.ConfirmPassword = "aDmin";
            bool check = viewModel.CheckValidPasswordChange();
            Assert.AreEqual(false, check);
        }
        [Test]
        public void CheckValidPasswordChange_EmptyConfirmPassword()
        {
            viewModel.OldPassword = "admin";
            viewModel.NewPassword = "aDmin";
            bool check = viewModel.CheckValidPasswordChange();
            Assert.AreEqual(false, check); ;
        }
        [Test]
        public void CheckValidPasswordChange_OldPasswordNotCorrect()
        {
            viewModel.OldPassword = "123456";
            viewModel.NewPassword = "aDmin";
            viewModel.ConfirmPassword = "aDmin";
            bool check = viewModel.CheckValidPasswordChange();
            Assert.AreEqual(false, check);
        }
        [Test]
        public void CheckValidPasswordChange_WrongNewPassword()
        {
            viewModel.OldPassword = "admin";
            viewModel.NewPassword = "123";
            viewModel.ConfirmPassword = "aDmin";
            bool check = viewModel.CheckValidPasswordChange();
            Assert.AreEqual(false, check);
        }
        [Test]
        public void CheckValidPasswordChange_ConfirmPasswordNotMatch()
        {
            viewModel.OldPassword = "admin";
            viewModel.NewPassword = "aDmin";
            viewModel.ConfirmPassword = "admin";
            bool check = viewModel.CheckValidPasswordChange();
            Assert.AreEqual(false, check);
        }
        [Test]
        public void CheckValidPasswordChange_Valid()
        {
            viewModel.OldPassword = "admin";
            viewModel.NewPassword = "aDmin";
            viewModel.ConfirmPassword = "aDmin";
            bool check = viewModel.CheckValidPasswordChange();
            Assert.AreEqual(true, check);
        }
    }
}
