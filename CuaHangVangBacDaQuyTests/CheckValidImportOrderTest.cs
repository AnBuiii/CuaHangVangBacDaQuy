using CuaHangVangBacDaQuy.models;
using CuaHangVangBacDaQuy.viewmodels.DialogContentViewModel;
using NUnit.Framework;
using System;
using System.Linq;

namespace CuaHangVangBacDaQuyTests
{
    [TestFixture]
    public class CheckValidImportOrderTest
    {
        private AddOrEditImportReceiptViewModel viewModel;

        [SetUp]
        public void SetUp()
        {
            viewModel = new AddOrEditImportReceiptViewModel();
        }

        [Test]
        public void CheckValidImportOrderTest_EmptySupplier()
        {
            viewModel.SelectedSupplier = null;
            viewModel.SelectedProductList.Add(new ChiTietPhieuMua()
            {
                MaSP = DataProvider.Ins.DB.SanPhams.Where(x => x.TenSP == "Kim cương").FirstOrDefault().MaSP,
                SoLuong = 1
            });
            bool check = viewModel.CheckValidOrder();
            Assert.AreEqual(false, check);
        }

        [Test]
        public void CheckValidImportOrderTestr_EmptyProductList()
        {
            viewModel.SelectedSupplier = DataProvider.Ins.DB.NhaCungCaps.Where(x => x.TenNCC == "Công ty Đá Quý 123").FirstOrDefault();
            bool check = viewModel.CheckValidOrder();
            Assert.AreEqual(false, check);
        }

        [Test]
        public void CheckValidImportOrderTest_1ItemProductListWithWrongAmount()
        {
            viewModel.SelectedSupplier = null;
            viewModel.SelectedProductList.Add(new ChiTietPhieuMua()
            {
                MaSP = DataProvider.Ins.DB.SanPhams.Where(x => x.TenSP == "Kim cương").FirstOrDefault().MaSP,
                SoLuong = 0
            });
            bool check = viewModel.CheckValidOrder();
            Assert.AreEqual(false, check);
        }

        [Test]
        public void CheckValidImportOrderTest_1ItemProductList_Valid()
        {
            viewModel.SelectedSupplier = viewModel.SelectedSupplier = DataProvider.Ins.DB.NhaCungCaps.Where(x => x.TenNCC == "Công ty Đá Quý 123").FirstOrDefault();
            viewModel.SelectedProductList.Add(new ChiTietPhieuMua()
            {
                MaSP = DataProvider.Ins.DB.SanPhams.Where(x => x.TenSP == "Kim cương").FirstOrDefault().MaSP,
                SoLuong = 1
            });
            bool check = viewModel.CheckValidOrder();
            Assert.AreEqual(true, check);
        }

        [Test]
        public void CheckValidImportOrderTest_2ItemProductListWithWrongAmount_1()
        {
            viewModel.SelectedSupplier = viewModel.SelectedSupplier = DataProvider.Ins.DB.NhaCungCaps.Where(x => x.TenNCC == "Công ty Đá Quý 123").FirstOrDefault();
            viewModel.SelectedProductList.Add(new ChiTietPhieuMua()
            {
                MaSP = DataProvider.Ins.DB.SanPhams.Where(x => x.TenSP == "Kim cương").FirstOrDefault().MaSP,
                SoLuong = 0
            });
            viewModel.SelectedProductList.Add(new ChiTietPhieuMua()
            {
                MaSP = DataProvider.Ins.DB.SanPhams.Where(x => x.TenSP == "Đá Topaz").FirstOrDefault().MaSP,
                SoLuong = 1
            });
            bool check = viewModel.CheckValidOrder();
            Assert.AreEqual(false, check);
        }

        [Test]
        public void CheckValidImportOrderTest_2ItemProductListWithWrongAmount_2()
        {
            viewModel.SelectedSupplier = viewModel.SelectedSupplier = DataProvider.Ins.DB.NhaCungCaps.Where(x => x.TenNCC == "Công ty Đá Quý 123").FirstOrDefault();
            viewModel.SelectedProductList.Add(new ChiTietPhieuMua()
            {
                MaSP = DataProvider.Ins.DB.SanPhams.Where(x => x.TenSP == "Kim cương").FirstOrDefault().MaSP,
                SoLuong = 1
            });
            viewModel.SelectedProductList.Add(new ChiTietPhieuMua()
            {
                MaSP = DataProvider.Ins.DB.SanPhams.Where(x => x.TenSP == "Đá Topaz").FirstOrDefault().MaSP,
                SoLuong = 0
            });
            bool check = viewModel.CheckValidOrder();
            Assert.AreEqual(false, check);
        }

        [Test]
        public void CheckValidImportOrderTest_2ItemProductList_Valid()
        {
            viewModel.SelectedSupplier = viewModel.SelectedSupplier = DataProvider.Ins.DB.NhaCungCaps.Where(x => x.TenNCC == "Công ty Đá Quý 123").FirstOrDefault();
            viewModel.SelectedProductList.Add(new ChiTietPhieuMua()
            {
                MaSP = DataProvider.Ins.DB.SanPhams.Where(x => x.TenSP == "Kim cương").FirstOrDefault().MaSP,
                SoLuong = 1
            });
            viewModel.SelectedProductList.Add(new ChiTietPhieuMua()
            {
                MaSP = DataProvider.Ins.DB.SanPhams.Where(x => x.TenSP == "Đá Topaz").FirstOrDefault().MaSP,
                SoLuong = 1
            });
            bool check = viewModel.CheckValidOrder();
            Assert.AreEqual(true, check);
        }
    }
}
