using CuaHangVangBacDaQuy.models;
using CuaHangVangBacDaQuy.viewmodels.DialogContentViewModel;
using NUnit.Framework;
using System;
using System.Linq;

namespace CuaHangVangBacDaQuyTests
{
    [TestFixture]
    public class CheckValidOrderTest
    {
        private AddOrEditImportReceiptViewModel viewModel;

        [SetUp]
        public void SetUp()
        {
            viewModel = new AddOrEditImportReceiptViewModel();
        }

        public void CheckValidOrder()
        {
            viewModel.SelectedSupplier = DataProvider.Ins.DB.NhaCungCaps.Where(x => x.TenNCC == "Công ty Đá Quý 123").FirstOrDefault();
            viewModel.SelectedProductList.Add(new ChiTietPhieuMua()
            {
                MaSP = DataProvider.Ins.DB.SanPhams.Where(x => x.TenSP == "Kim cương").FirstOrDefault().MaSP,
                SanPham = DataProvider.Ins.DB.SanPhams.Where(x => x.TenSP == "Kim cương").FirstOrDefault(),
                SoLuong = 1
            });
            bool check = viewModel.CheckValidOrder();
            Assert.AreEqual(true, check);
        }
    }
}
