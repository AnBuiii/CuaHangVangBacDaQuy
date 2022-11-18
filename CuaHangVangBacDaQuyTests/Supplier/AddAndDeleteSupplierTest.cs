using CuaHangVangBacDaQuy.models;
using CuaHangVangBacDaQuy.viewmodels;
using CuaHangVangBacDaQuy.viewmodels.DialogContentViewModel;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace CuaHangVangBacDaQuyTests.Supplier
{
    [TestFixture]
    internal class AddAndDeleteSupplierTest
    {
        private AddOrEditSupplierViewModel viewModel;
        private readonly List<string> supplierNames = new List<string> { null, "  ", "(@#$%", "An Bùi", "An", "Phong" };
        private readonly List<string> supplierAddresses = new List<string> { null, " ", "(@#$%", "Hồ Chí Minh" };
        private readonly List<string> supplierPhones = new List<string> { null, " ", "123123", "01", "0923234345" };


        [SetUp]
        public void SetUp()
        {
            viewModel = new AddOrEditSupplierViewModel();
        }
        // null check
        [TestCase(0, 3, 4, false)]
        [TestCase(5, 0, 4, false)]
        [TestCase(5, 3, 0, false)]
        // empty check
        [TestCase(1, 3, 4, false)]
        [TestCase(5, 1, 4, false)]
        [TestCase(5, 3, 1, false)]

        [TestCase(2, 2, 2, false)]
        [TestCase(2, 2, 3, false)]
        [TestCase(2, 2, 4, true)]

        [TestCase(2, 3, 2, false)]
        [TestCase(2, 3, 3, false)]
        [TestCase(2, 3, 4, true)]

        [TestCase(3, 2, 2, false)]
        [TestCase(3, 2, 3, false)]
        [TestCase(3, 2, 4, true)]

        [TestCase(3, 3, 2, false)]
        [TestCase(3, 3, 3, false)]
        [TestCase(3, 3, 4, true)]

        [TestCase(4, 2, 2, false)]
        [TestCase(4, 2, 3, false)]
        [TestCase(4, 2, 4, true)]

        [TestCase(4, 3, 2, false)]
        [TestCase(4, 3, 3, false)]
        [TestCase(4, 3, 4, true)]


        public void AddAndDeleteSupplier(int nameIdx, int addressIdx, int phoneIdx, bool expect)
        {
            viewModel.SupplierName = supplierNames[nameIdx];
            viewModel.SupplierAddress = supplierAddresses[addressIdx];
            viewModel.SupplierPhoneNumber = supplierPhones[phoneIdx];
            viewModel.ActionAddSupplier();

            int code = viewModel.supplierCode;

            NhaCungCap a = DataProvider.Ins.DB.NhaCungCaps.Where(x => x.MaNCC == code).FirstOrDefault();

            if (a != null)
            {
                DataProvider.Ins.DB.NhaCungCaps.Attach(a);
                DataProvider.Ins.DB.NhaCungCaps.Remove(a);
                DataProvider.Ins.DB.SaveChanges();
            }
            Assert.AreEqual(expect, a != null);
            Assert.AreEqual(true, DataProvider.Ins.DB.NhaCungCaps.Where(x => x.MaNCC == code).FirstOrDefault() == null); 
        }

    }
}
