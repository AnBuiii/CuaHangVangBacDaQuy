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
    internal class CheckValidSupplierTest
    {
        private AddOrEditSupplierViewModel viewModel;

        [SetUp]
        public void SetUp()
        {
            viewModel = new AddOrEditSupplierViewModel();
        }

        [Test]
        public void CheckValidSupplierTest_EmptySupplierName()
        {
            viewModel.SupplierAddress = "Tp.Hồ Chí Minh";
            viewModel.SupplierPhoneNumber = "0987654321";
            bool check = viewModel.CheckValidSupplier();
            Assert.AreEqual(false, check);
        }
        [Test]
        public void CheckValidSupplierTest_EmptySupplierAddress()
        {
            viewModel.SupplierName = "Cơ sở Mỹ Nghệ 5";
            viewModel.SupplierPhoneNumber = "0987654321";
            bool check = viewModel.CheckValidSupplier();
            Assert.AreEqual(false, check);
        }
        [Test]
        public void CheckValidSupplierTest_EmptySupplierPhone()
        {
            viewModel.SupplierName = "Cơ sở Mỹ Nghệ 5";
            viewModel.SupplierAddress = "Tp.Hồ Chí Minh";
            bool check = viewModel.CheckValidSupplier();
            Assert.AreEqual(false, check);
        }
        [Test]
        public void CheckValidSupplierTest_DuplicateSupplierName()
        {
            viewModel.SupplierName = "Công ty Đá Quý 123";
            viewModel.SupplierAddress = "Tp.Hồ Chí Minh";
            viewModel.SupplierPhoneNumber = "0987654321";
            bool check = viewModel.CheckValidSupplier();
            Assert.AreEqual(false, check);
        }
        [Test]
        public void CheckValidSupplierTest_DuplicateSupplierPhone()
        {
            viewModel.SupplierName = "Cơ sở Mỹ Nghệ 5";
            viewModel.SupplierAddress = "Tp.Hồ Chí Minh";   
            viewModel.SupplierPhoneNumber = "0921321122";
            bool check = viewModel.CheckValidSupplier();
            Assert.AreEqual(false, check);
        }
        [Test]
        public void CheckValidSupplierTest_WrongSupplierPhone1()
        {
            viewModel.SupplierName = "Cơ sở Mỹ Nghệ 5";
            viewModel.SupplierAddress = "Tp.Hồ Chí Minh";
            viewModel.SupplierPhoneNumber = "a";
            bool check = viewModel.CheckValidSupplier();
            Assert.AreEqual(false, check);
        }
        [Test]
        public void CheckValidSupplierTest_WrongSupplierPhone2()
        {
            viewModel.SupplierName = "Cơ sở Mỹ Nghệ 5";
            viewModel.SupplierAddress = "Tp.Hồ Chí Minh";
            viewModel.SupplierPhoneNumber = "123";
            bool check = viewModel.CheckValidSupplier();
            Assert.AreEqual(false, check);
        }
        [Test]
        public void CheckValidSupplierTest_WrongSupplierPhone3()
        {
            viewModel.SupplierName = "Cơ sở Mỹ Nghệ 5";
            viewModel.SupplierAddress = "Tp.Hồ Chí Minh";
            viewModel.SupplierPhoneNumber = "1234567890";
            bool check = viewModel.CheckValidSupplier();
            Assert.AreEqual(false, check);
        }
        [Test]
        public void CheckValidSupplierTest_Valid()
        {
            viewModel.SupplierName = "Cơ sở Mỹ Nghệ 5";
            viewModel.SupplierAddress = "Tp.Hồ Chí Minh";
            viewModel.SupplierPhoneNumber = "0987654321";
            bool check = viewModel.CheckValidSupplier();
            Assert.AreEqual(true, check);
        }


    }
}
