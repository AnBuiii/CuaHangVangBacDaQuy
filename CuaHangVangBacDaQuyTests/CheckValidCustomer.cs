using CuaHangVangBacDaQuy.viewmodels.DialogContentViewModel;
using CuaHangVangBacDaQuy.views.userControlDialog;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;

namespace CuaHangVangBacDaQuyTests
{
    [TestFixture]
    internal class CheckValidCustomer
    {
        private AddOrEditCustomerViewModel viewModel;
        [SetUp]
        public void SetUp()
        {
            viewModel = new AddOrEditCustomerViewModel();
        }

        [Test]
        public void CheckValidCustomerTest_EmptyCustomerName()
        {
            viewModel.Gender = "Nam";
            viewModel.Address = "Tp.Hồ Chí Minh";
            viewModel.PhoneNumber = "0987654321";
            bool check = viewModel.CheckValidCustomer();
            Assert.AreEqual(false, check);
        }
        [Test]
        public void CheckValidCustomerTest_EmptyCustomerGender()
        {
            viewModel.CustomerName = "Khách hàng";
            viewModel.Address = "Tp.Hồ Chí Minh";
            viewModel.PhoneNumber = "0987654321";
            bool check = viewModel.CheckValidCustomer();
            Assert.AreEqual(false, check);
        }
        [Test]
        public void CheckValidCustomerTest_EmptyCustomerAddress()
        {
            viewModel.CustomerName = "Khách hàng";
            viewModel.Gender = "Nam";
            viewModel.PhoneNumber = "0987654321";
            bool check = viewModel.CheckValidCustomer();
            Assert.AreEqual(false, check);
        }
        [Test]
        public void CheckValidCustomerTest_EmptyCustomerPhone()
        {
            viewModel.CustomerName = "Khách hàng";
            viewModel.Gender = "Nam";
            viewModel.Address = "Tp.Hồ Chí Minh";
            bool check = viewModel.CheckValidCustomer();
            Assert.AreEqual(false, check);
        }
        [Test]
        public void CheckValidCustomerTest_DuplicateCustomerName()
        {
            viewModel.CustomerName = "Trần Trọng Hoàng";
            viewModel.Gender = "Nam";
            viewModel.Address = "Tp.Hồ Chí Minh";
            viewModel.PhoneNumber = "0987654321";
            bool check = viewModel.CheckValidCustomer();
            Assert.AreEqual(false, check);
        }
        [Test]
        public void CheckValidCustomerTest_DuplicateCustomerPhone()
        {
            viewModel.CustomerName = "Khách hàng";
            viewModel.Gender = "Nam";
            viewModel.Address = "Tp.Hồ Chí Minh";   
            viewModel.PhoneNumber = "0923234345";
            bool check = viewModel.CheckValidCustomer();
            Assert.AreEqual(false, check);
        }
        [Test]
        public void CheckValidCustomerTest_WrongCustomerPhone1()
        {
            viewModel.CustomerName = "Khách hàng";
            viewModel.Gender = "Nam";
            viewModel.Address = "Tp.Hồ Chí Minh";
            viewModel.PhoneNumber = "a";
            bool check = viewModel.CheckValidCustomer();
            Assert.AreEqual(false, check);
        }
        [Test]
        public void CheckValidCustomerTest_WrongCustomerPhone2()
        {
            viewModel.CustomerName = "Khách hàng";
            viewModel.Gender = "Nam";
            viewModel.Address = "Tp.Hồ Chí Minh";
            viewModel.PhoneNumber = "123";
            bool check = viewModel.CheckValidCustomer();
            Assert.AreEqual(false, check);
        }
        [Test]
        public void CheckValidCustomerTest_WrongCustomerPhone3()
        {
            viewModel.CustomerName = "Khách hàng";
            viewModel.Gender = "Nam";
            viewModel.Address = "Tp.Hồ Chí Minh";
            viewModel.PhoneNumber = "1234567890";
            bool check = viewModel.CheckValidCustomer();
            Assert.AreEqual(false, check);
        }
        [Test]
        public void CheckValidCustomerTest_Valid()
        {
            viewModel.CustomerName = "Khách hàng";
            viewModel.Gender = "Nam";
            viewModel.Address = "Tp.Hồ Chí Minh";
            viewModel.PhoneNumber = "0987654321";
            bool check = viewModel.CheckValidCustomer();
            Assert.AreEqual(true, check);
        }
    }
}
