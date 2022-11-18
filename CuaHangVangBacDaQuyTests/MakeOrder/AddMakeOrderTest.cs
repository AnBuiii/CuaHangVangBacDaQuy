using CuaHangVangBacDaQuy.models;
using CuaHangVangBacDaQuy.viewmodels;
using CuaHangVangBacDaQuy.viewmodels.DialogContentViewModel;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace CuaHangVangBacDaQuyTests.MakeOrder
{
    [TestFixture]
    internal class AddMakeOrderTest
    {
        private AddOrEditImportReceiptViewModel viewModel;
        private ImportReceiptViewModel viewModel2;

        private readonly List<string> supplierNames = new List<string> { null, "Công ty Đá Quý 123", "Xưởng đúc vàng Linh Trung" };
        private readonly List<string> products = new List<string> { null, "Đá Topaz", "Kim cương", "Đá saphire" };
        private readonly List<int> productAmounts = new List<int> { -1, 0, 1, 10 };

        [SetUp]
        public void SetUp()
        {
            viewModel = new AddOrEditImportReceiptViewModel();
            viewModel2 = new ImportReceiptViewModel();
        }
        //null check
        //[TestCase(0, 3, 3, true)]
        //[TestCase(2, 0, 3, true)]
        //wrong amount check
        //[TestCase(2, 3, 0, true)]
        //[TestCase(2, 3, 1, true)]

        [TestCase(1, 1, 1, true)]
        [TestCase(1, 1, 2, true)]
        [TestCase(1, 2, 1, true)]
        [TestCase(1, 2, 2, true)]
        [TestCase(1, 3, 1, true)]
        [TestCase(1, 3, 2, true)]
        [TestCase(2, 1, 1, true)]
        [TestCase(2, 1, 2, true)]
        [TestCase(2, 2, 1, true)]
        [TestCase(2, 2, 2, true)]
        [TestCase(2, 3, 1, true)]
        [TestCase(2, 3, 2, true)]



        public void AddMakeOrder(int supplierIdx, int productIdx, int amountIdx, bool expect)
        {
            string code = Guid.NewGuid().ToString();
            string a = supplierNames[supplierIdx];
            string b = products[productIdx];
            viewModel.code = code;
            viewModel.SelectedSupplier = DataProvider.Ins.DB.NhaCungCaps.Where(x => x.TenNCC == a).FirstOrDefault();          
            viewModel.SelectedProductList = new ObservableCollection<ChiTietPhieuMua>() {
                new ChiTietPhieuMua() {
                    MaSP = DataProvider.Ins.DB.SanPhams.Where(x => x.TenSP ==b).FirstOrDefault().MaSP,
                    SoLuong = productAmounts[amountIdx]}
            };
            viewModel.AddNewImportReceipt();
            PhieuMua check = DataProvider.Ins.DB.PhieuMuas.Where(x => x.MaPhieu == code).FirstOrDefault();

            viewModel2.SelectedImportReceipt = DataProvider.Ins.DB.PhieuMuas.Where(x => x.MaPhieu == code).FirstOrDefault();
            viewModel2.DeleteImportReceipt();
            Assert.AreEqual(expect, check != null);

        }
    }
}
