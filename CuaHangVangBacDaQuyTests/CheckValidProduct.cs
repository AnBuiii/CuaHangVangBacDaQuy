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
    
    internal class CheckValidProduct
    {
        private AddOrEditProductViewModel viewModel;
        [SetUp]
        public void SetUp()
        {
            viewModel = new AddOrEditProductViewModel();    
        }

        [Test]
        public void CheckValidProduct_EmptyProductName()
        {
            viewModel.ProductPrice = "1";
            viewModel.SelectedTypeProduct = DataProvider.Ins.DB.LoaiSanPhams.Where(x => x.TenLoaiSP == "Nhẫn").FirstOrDefault();
            viewModel.SelectedUnit = DataProvider.Ins.DB.DonVis.Where(x => x.TenDV == "cái").FirstOrDefault();
            bool check = viewModel.CheckValidProduct();
            Assert.AreEqual(false, check);
        }
        [Test]
        public void CheckValidProduct_EmptyProductPrice()
        {
            viewModel.ProductName = "Kim cương";
            viewModel.SelectedTypeProduct = DataProvider.Ins.DB.LoaiSanPhams.Where(x => x.TenLoaiSP == "Nhẫn").FirstOrDefault();
            viewModel.SelectedUnit = DataProvider.Ins.DB.DonVis.Where(x => x.TenDV == "cái").FirstOrDefault();
            bool check = viewModel.CheckValidProduct();
            Assert.AreEqual(false, check);
        }
        [Test]
        public void CheckValidProduct_EmptyProductType()
        {
            viewModel.ProductName = "Kim cương";
            viewModel.ProductPrice = "1";
            viewModel.SelectedUnit = DataProvider.Ins.DB.DonVis.Where(x => x.TenDV == "cái").FirstOrDefault();
            bool check = viewModel.CheckValidProduct();
            Assert.AreEqual(false, check);
        }
        [Test]
        public void CheckValidProduct_EmptyProductUnit()
        {
            viewModel.ProductName = "Kim cương";
            viewModel.ProductPrice = "1";
            viewModel.SelectedTypeProduct = DataProvider.Ins.DB.LoaiSanPhams.Where(x => x.TenLoaiSP == "Nhẫn").FirstOrDefault();
            bool check = viewModel.CheckValidProduct();
            Assert.AreEqual(false, check);
        }
        [Test]
        public void CheckValidProduct_DuplicateProductName()
        {
            viewModel.ProductName = "Kim cương";
            viewModel.ProductPrice = "1";
            viewModel.SelectedTypeProduct = DataProvider.Ins.DB.LoaiSanPhams.Where(x => x.TenLoaiSP == "Nhẫn").FirstOrDefault();
            viewModel.SelectedUnit = DataProvider.Ins.DB.DonVis.Where(x => x.TenDV == "cái").FirstOrDefault();
            bool check = viewModel.CheckValidProduct();
            Assert.AreEqual(false, check);
        }
        [Test]
        public void CheckValidProduct_WrongProductPrice1()
        {
            viewModel.ProductName = "Đá vương liệm";
            viewModel.ProductPrice = "a";
            viewModel.SelectedTypeProduct = DataProvider.Ins.DB.LoaiSanPhams.Where(x => x.TenLoaiSP == "Nhẫn").FirstOrDefault();
            viewModel.SelectedUnit = DataProvider.Ins.DB.DonVis.Where(x => x.TenDV == "cái").FirstOrDefault();
            bool check = viewModel.CheckValidProduct();
            Assert.AreEqual(false, check);
        }
        [Test]
        public void CheckValidProduct_WrongProductPrice2()
        {
            viewModel.ProductName = "Đá vương liệm";
            viewModel.ProductPrice = "-1";
            viewModel.SelectedTypeProduct = DataProvider.Ins.DB.LoaiSanPhams.Where(x => x.TenLoaiSP == "Nhẫn").FirstOrDefault();
            viewModel.SelectedUnit = DataProvider.Ins.DB.DonVis.Where(x => x.TenDV == "cái").FirstOrDefault();
            bool check = viewModel.CheckValidProduct();
            Assert.AreEqual(false, check);
        }
        [Test]
        public void CheckValidProduct_WrongProductPrice3()
        {
            viewModel.ProductName = "Đá vương liệm";
            viewModel.ProductPrice = "0";
            viewModel.SelectedTypeProduct = DataProvider.Ins.DB.LoaiSanPhams.Where(x => x.TenLoaiSP == "Nhẫn").FirstOrDefault();
            viewModel.SelectedUnit = DataProvider.Ins.DB.DonVis.Where(x => x.TenDV == "cái").FirstOrDefault();
            bool check = viewModel.CheckValidProduct();
            Assert.AreEqual(false, check);
        }
        [Test]
        public void CheckValidProduct_Valid()
        {
            viewModel.ProductName = "Đá vương liệm";
            viewModel.ProductPrice = "1";
            viewModel.SelectedTypeProduct = DataProvider.Ins.DB.LoaiSanPhams.Where(x => x.TenLoaiSP == "Nhẫn").FirstOrDefault();
            viewModel.SelectedUnit = DataProvider.Ins.DB.DonVis.Where(x => x.TenDV == "cái").FirstOrDefault();
            bool check = viewModel.CheckValidProduct();
            Assert.AreEqual(true, check);
        }

    }
}
