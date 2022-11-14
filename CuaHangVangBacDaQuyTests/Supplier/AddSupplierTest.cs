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
    internal class AddSupplierTest
    {
        private AddOrEditProductViewModel viewModel;
        private readonly List<string> supplierName = new List<string> { null, "  ", "(@#$%", "An Bùi", "An", "Phong" };
        private readonly List<string> supplierAddress = new List<string> { null, " ", "(@#$%", "Hồ Chí Minh" };
        private readonly List<string> supplierPhone = new List<string> { null, " ", "123123", "01", "0123234345" };


        [SetUp]
        public void SetUp()
        {
            viewModel = new AddOrEditProductViewModel();
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









        public void AddSupplier(int nameIdx, int addressIdx, int phoneIdx, bool expect)
        {

            //string code = Guid.NewGuid().ToString();
            //viewModel.



            //string productCode = Guid.NewGuid().ToString();
            //addOrEditProductViewModel.ProductCode = productCode;
            //addOrEditProductViewModel.ProductName = productName;
            //addOrEditProductViewModel.ProductPrice = productPrice;
            //addOrEditProductViewModel.SelectedTypeProduct = addOrEditProductViewModel.TypeProductList.Where(x => x.MaLoaiSP == typeCode).FirstOrDefault();
            //addOrEditProductViewModel.SelectedUnit = addOrEditProductViewModel.UnitList.Where(x => x.MaDV == unitCode).FirstOrDefault();
            //addOrEditProductViewModel.ActionAddProduct();
            //SanPham a = DataProvider.Ins.DB.SanPhams.Where(x => x.MaSP == productCode).FirstOrDefault();

            //if (a != null)
            //{
            //    DataProvider.Ins.DB.SanPhams.Attach(a);
            //    DataProvider.Ins.DB.SanPhams.Remove(a);
            //    DataProvider.Ins.DB.SaveChanges();
            //}
            //Assert.AreEqual(expect, a != null);

        }

    }
}
