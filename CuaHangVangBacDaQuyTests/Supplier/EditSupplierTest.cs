using CuaHangVangBacDaQuy.models;
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
        [SetUp]
        public void SetUp()
        {
            viewModel = new AddOrEditSupplierViewModel();
        }

        //null check
        [TestCase(null, 0, 1, 1, false)]
        [TestCase("Kim cương", null, 1, 1, false)]
        [TestCase("Kim cương", 1, null, 1, false)]
        [TestCase("Kim cương", 1, 1, null, false)]
        //productname check
        [TestCase("Nhẫn 101", 1, 1, 1, true)]
        [TestCase("Nhẫn 102", 1, 1, 1, false)]
        [TestCase("Vòng 111", 1, 1, 1, true)]
        [TestCase("Kim cương", 1, 1, 1, true)]
        //product price check
        [TestCase("Kim cương", -1, 1, 1, false)]
        [TestCase("Kim cương", 0, 1, 1, false)]
        [TestCase("Kim cương", 1000000, 1, 1, true)]
        //product type check
        [TestCase("Kim cương", 1, 0, 1, false)]
        [TestCase("Kim cương", 1, 5, 1, true)]
        [TestCase("Kim cương", 1, 6, 1, false)]
        //product unit check
        [TestCase("Kim cương", 1, 1, 0, false)]
        [TestCase("Kim cương", 1, 1, 4, true)]
        [TestCase("Kim cương", 1, 1, 5, false)]


        public void EditProduct(string productName, decimal productPrice, int typeCode, int unitCode, bool expect)
        {
            SanPham hm = DataProvider.Ins.DB.SanPhams.FirstOrDefault();
            SanPham preEdit = new SanPham() { TenSP = hm.TenSP, DonGia = hm.DonGia, MaDV = hm.MaDV, MaLoaiSP = hm.MaLoaiSP };
           
            //viewModel.EditedProduct = DataProvider.Ins.DB.SanPhams.FirstOrDefault();
            //viewModel.ProductName = productName;
            //viewModel.ProductPrice = productPrice;
            //viewModel.SelectedTypeProduct = viewModel.TypeProductList.Where(x => x.MaLoaiSP == typeCode).FirstOrDefault();
            //viewModel.SelectedUnit = viewModel.UnitList.Where(x => x.MaDV == unitCode).FirstOrDefault();
            //viewModel.ActionEditProduct();

            //SanPham sanPham = DataProvider.Ins.DB.SanPhams.FirstOrDefault();
            //Assert.AreEqual(expect, sanPham.TenSP == productName && sanPham.DonGia == productPrice && sanPham.MaLoaiSP == typeCode && sanPham.MaDV == unitCode);

            //viewModel.EditedProduct = DataProvider.Ins.DB.SanPhams.FirstOrDefault();
            //viewModel.ProductName = preEdit.TenSP;
            //viewModel.ProductPrice = (decimal)preEdit.DonGia;
            //viewModel.SelectedTypeProduct = viewModel.TypeProductList.Where(x => x.MaLoaiSP == preEdit.MaLoaiSP).FirstOrDefault();
            //viewModel.SelectedUnit = viewModel.UnitList.Where(x => x.MaDV == preEdit.MaDV).FirstOrDefault();
            //viewModel.ActionEditProduct();

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
