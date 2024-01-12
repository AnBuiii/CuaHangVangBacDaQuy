using CuaHangVangBacDaQuy.models;
using CuaHangVangBacDaQuy.viewmodels.DialogContentViewModel;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CuaHangVangBacDaQuyTests
{
    [TestFixture]
    internal class CheckValidAccount
    {
        private AddOrEditAccountViewModel viewModel;
        [SetUp]
        public void SetUp()
        {
            viewModel = new AddOrEditAccountViewModel();
        } 

        [Test]
        public void CheckValidAccount_EmptyAccountName()
        {
            viewModel.AccountUsername = "user";
            viewModel.PasswordAccount = "abcdef";
            viewModel.SelectedPermission = DataProvider.Ins.DB.QuyenHans.Where(x => x.TenQH == "Admin").FirstOrDefault();
            bool check = viewModel.CheckValidAccount();
            Assert.AreEqual(false, check);
        }
        [Test]
        public void CheckValidAccount_EmptyAccountUserName()
        {
            viewModel.AccountName = "Người dùng";
            viewModel.PasswordAccount = "abcdef";
            viewModel.SelectedPermission = DataProvider.Ins.DB.QuyenHans.Where(x => x.TenQH == "Admin").FirstOrDefault();
            bool check = viewModel.CheckValidAccount();
            Assert.AreEqual(false, check);
        }
        [Test]
        public void CheckValidAccount_EmptyAccountPassword()
        {
            viewModel.AccountName = "Người dùng";
            viewModel.AccountUsername = "user";
            viewModel.SelectedPermission = DataProvider.Ins.DB.QuyenHans.Where(x => x.TenQH == "Admin").FirstOrDefault();
            bool check = viewModel.CheckValidAccount();
            Assert.AreEqual(false, check);
        }
        [Test]
        public void CheckValidAccount_EmptyAccountPermission()
        {
            viewModel.AccountName = "Người dùng";
            viewModel.AccountUsername = "user";
            viewModel.PasswordAccount = "abcdef";
            bool check = viewModel.CheckValidAccount();
            Assert.AreEqual(false, check);
        }
        [Test]
        public void CheckValidAccount_DuplicateUserName()
        {
            viewModel.AccountName = "Người dùng";
            viewModel.AccountUsername = "admin";
            viewModel.PasswordAccount = "abcdef";
            viewModel.SelectedPermission = DataProvider.Ins.DB.QuyenHans.Where(x => x.TenQH == "Admin").FirstOrDefault();
            bool check = viewModel.CheckValidAccount();
            Assert.AreEqual(false, check);
        }
        [Test]
        public void CheckValidAccount_WrongPassword()
        {
            viewModel.AccountName = "Người dùng";
            viewModel.AccountUsername = "user";
            viewModel.PasswordAccount = "abc";
            viewModel.SelectedPermission = DataProvider.Ins.DB.QuyenHans.Where(x => x.TenQH == "Admin").FirstOrDefault();
            bool check = viewModel.CheckValidAccount();
            Assert.AreEqual(false, check);
        }
        [Test]
        public void CheckValidAccount_Valid()
        {
            viewModel.AccountName = "Người dùng";
            viewModel.AccountUsername = "user";
            viewModel.PasswordAccount = "abcdef";
            viewModel.SelectedPermission = DataProvider.Ins.DB.QuyenHans.Where(x => x.TenQH == "Admin").FirstOrDefault();
            bool check = viewModel.CheckValidAccount();
            Assert.AreEqual(true, check);
        }
    }
}
