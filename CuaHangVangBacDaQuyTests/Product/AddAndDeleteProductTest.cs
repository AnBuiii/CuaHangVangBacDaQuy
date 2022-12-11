using CuaHangVangBacDaQuy.models;
using CuaHangVangBacDaQuy.viewmodels;
using CuaHangVangBacDaQuy.viewmodels.DialogContentViewModel;
using NUnit.Framework;
using System;
using System.Collections.ObjectModel;
using System.Linq;

namespace CuaHangVangBacDaQuyTests.Product
{
    [TestFixture]
    internal class AddAndDeleteProductTest
    {
        private ProductViewModel productViewModel;
        private AddOrEditProductViewModel addOrEditProductViewModel;
        [SetUp]
        public void SetUp()
        {
            productViewModel = new ProductViewModel();
            addOrEditProductViewModel = new AddOrEditProductViewModel();
        }
        //null check
        [TestCase(null, 0, 1, 1, false)]
        [TestCase("Vòng 111", null, 1, 1, false)]
        [TestCase("Vòng 111", 1, null, 1, false)]
        [TestCase("Vòng 111", 1, 1, null, false)]
        //productname check
        [TestCase("Nhẫn 101", 1, 1, 1, true)]
        [TestCase("Nhẫn 102", 1, 1, 1, false)]
        [TestCase("Nhẫn bạc", 1, 1, 1, true)]
        [TestCase("Vòng 111", 1, 1, 1, true)]
        //product price check
        [TestCase("Vòng 111", -1, 1, 1, false)]
        [TestCase("Vòng 111", 0, 1, 1, false)]
        [TestCase("Vòng 111", 1000000, 1, 1, true)]
        //product type check
        [TestCase("Vòng 111", 1, 0, 1, false)]
        [TestCase("Vòng 111", 1, 5, 1, true)]
        [TestCase("Vòng 111", 1, 6, 1, false)]
        //product unit check
        [TestCase("Vòng 111", 1, 1, 0, false)]
        [TestCase("Vòng 111", 1, 1, 4, true)]
        [TestCase("Vòng 111", 1, 1, 5, false)]






        public void AddProduct(string productName, decimal productPrice, int typeCode, int unitCode, bool expect)
        {
            string productCode = Guid.NewGuid().ToString();
            addOrEditProductViewModel.ProductCode = productCode;
            addOrEditProductViewModel.ProductName = productName;
            //addOrEditProductViewModel.ProductPrice = productPrice;
            addOrEditProductViewModel.SelectedTypeProduct = addOrEditProductViewModel.TypeProductList.Where(x => x.MaLoaiSP == typeCode).FirstOrDefault();
            addOrEditProductViewModel.SelectedUnit = addOrEditProductViewModel.UnitList.Where(x => x.MaDV == unitCode).FirstOrDefault();
            addOrEditProductViewModel.ActionAddProduct();
            SanPham preDeleteCheck = DataProvider.Ins.DB.SanPhams.Where(x => x.MaSP == productCode).FirstOrDefault();

            if (preDeleteCheck != null)
            {
                DataProvider.Ins.DB.SanPhams.Attach(preDeleteCheck);
                DataProvider.Ins.DB.SanPhams.Remove(preDeleteCheck);
                DataProvider.Ins.DB.SaveChanges();
            }
            Assert.AreEqual(expect, preDeleteCheck != null);
            Assert.AreEqual(true, DataProvider.Ins.DB.SanPhams.Where(x => x.MaSP == productCode).FirstOrDefault() == null);

        }

    }
}
