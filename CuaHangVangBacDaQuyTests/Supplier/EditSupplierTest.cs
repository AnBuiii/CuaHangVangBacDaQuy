using CuaHangVangBacDaQuy.models;
using CuaHangVangBacDaQuy.viewmodels;
using CuaHangVangBacDaQuy.viewmodels.DialogContentViewModel;
using NUnit.Framework;
using NUnit.Framework.Constraints;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CuaHangVangBacDaQuyTests.Supplier
{
    [TestFixture]
    internal class EditSupplierTest
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
        //null check
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


        public void EditSupplier(int nameIdx, int addressIdx, int phoneIdx, bool expect)
        {
            NhaCungCap hm = DataProvider.Ins.DB.NhaCungCaps.FirstOrDefault();
            NhaCungCap preEdit = new NhaCungCap() { TenNCC = hm.TenNCC, DiaChi = hm.DiaChi, SoDT = hm.SoDT };

            viewModel.EditedSupplier = hm;
            viewModel.SupplierName = supplierNames[nameIdx];
            viewModel.SupplierAddress = supplierAddresses[addressIdx];
            viewModel.SupplierPhoneNumber = supplierPhones[phoneIdx];
            viewModel.ActionEditSupplier();

            NhaCungCap a = DataProvider.Ins.DB.NhaCungCaps.FirstOrDefault();
            Assert.AreEqual(expect, a.TenNCC == supplierNames[nameIdx] && a.DiaChi == supplierAddresses[addressIdx] && a.SoDT == supplierPhones[phoneIdx]);


            viewModel.EditedSupplier = DataProvider.Ins.DB.NhaCungCaps.FirstOrDefault();
            viewModel.SupplierName = preEdit.TenNCC;
            viewModel.SupplierAddress = preEdit.DiaChi;
            viewModel.SupplierPhoneNumber = preEdit.SoDT;
            viewModel.ActionEditSupplier();

        }
    }
}
