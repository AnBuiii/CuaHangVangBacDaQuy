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
    internal class CheckLoginTest
    {
        private LoginViewModel viewModel;
        [SetUp]
        public void SetUp()
        {
            viewModel = new LoginViewModel
            {
                IsLogin = true
            };
        }
        [Test]
        public void CheckLoginTest_EmptyUsername()
        {
            viewModel.Password = "password";
            bool check = viewModel.CheckLogin();
            Assert.AreEqual(false, check);
        }
        [Test]
        public void CheckLoginTest_EmptyPassword()
        {
            viewModel.UserName = "admin";
            bool check = viewModel.CheckLogin();
            Assert.AreEqual(false, check);
        }
        [Test]
        public void CheckLoginTest_Fail_123_1()
        {
            viewModel.UserName = "123";
            viewModel.Password = "123";
            bool check = viewModel.CheckLogin();
            Assert.AreEqual(false, check);
        }
        [Test]
        public void CheckLoginTest_Fail_123_2()
        {
            viewModel.UserName = "123";
            viewModel.Password = "admin";
            bool check = viewModel.CheckLogin();
            Assert.AreEqual(false, check);
        }
        [Test]
        public void CheckLoginTest_Fail_123_3()
        {
            viewModel.UserName = "123";
            viewModel.Password = "password";
            bool check = viewModel.CheckLogin();
            Assert.AreEqual(false, check);
        }
        [Test]
        public void CheckLoginTest_Fail_admin_1()
        {
            viewModel.UserName = "admin";
            viewModel.Password = "123";
            bool check = viewModel.CheckLogin();
            Assert.AreEqual(false, check);
        }
        [Test]
        public void CheckLoginTest_Fail_admin_2()
        {
            viewModel.UserName = "admin";
            viewModel.Password = "password";
            bool check = viewModel.CheckLogin();
            Assert.AreEqual(false, check);
        }
        [Test]
        public void CheckLoginTest_Fail_importStaff_1()
        {
            viewModel.UserName = "importStaff1";
            viewModel.Password = "123";
            bool check = viewModel.CheckLogin();
            Assert.AreEqual(false, check);
        }
        [Test]
        public void CheckLoginTest_Fail_importStaff_2()
        {
            viewModel.UserName = "importStaff1";
            viewModel.Password = "admin";
            bool check = viewModel.CheckLogin();
            Assert.AreEqual(false, check);
        }
        [Test]
        public void CheckLoginTest_Fail_saleStaff_1()
        {
            viewModel.UserName = "saleStaff1";
            viewModel.Password = "123";
            bool check = viewModel.CheckLogin();
            Assert.AreEqual(false, check);
        }
        [Test]
        public void CheckLoginTest_Fail_saleStaff_2()
        {
            viewModel.UserName = "saleStaff1";
            viewModel.Password = "admin";
            bool check = viewModel.CheckLogin();
            Assert.AreEqual(false, check);
        }
        [Test]
        public void CheckLoginTest_Success_admin()
        {
            viewModel.UserName = "admin";
            viewModel.Password = "admin";
            bool check = viewModel.CheckLogin();
            Assert.AreEqual(true, check);
        }
        [Test]
        public void CheckLoginTest_Success_importStaff()
        {
            viewModel.UserName = "importStaff1";
            viewModel.Password = "password";
            bool check = viewModel.CheckLogin();
            Assert.AreEqual(true, check);
        }
        [Test]
        public void CheckLoginTest_Success_saleStaff()
        {
            viewModel.UserName = "saleStaff1";
            viewModel.Password = "password";
            bool check = viewModel.CheckLogin();
            Assert.AreEqual(true, check);
        }

    }
}
