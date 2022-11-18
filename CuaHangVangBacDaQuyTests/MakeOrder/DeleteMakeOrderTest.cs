using CuaHangVangBacDaQuy.models;
using CuaHangVangBacDaQuy.viewmodels;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CuaHangVangBacDaQuyTests.MakeOrder
{

    internal class DeleteMakeOrderTest
    {
        ImportReceiptViewModel viewModel;

        [SetUp()]
        public void SetUp()
        {
            viewModel = new ImportReceiptViewModel();
        }

        [TestCase(true)]

        public void DeleteOrder(bool expect)
        {
            viewModel.SelectedImportReceipt = DataProvider.Ins.DB.PhieuMuas.Where(x => x.MaPhieu == "f03cc8fa-5781-491a-8bd7-47a08f9f3e35").FirstOrDefault();
            viewModel.DeleteImportReceipt();

            PhieuMua phieuMua = DataProvider.Ins.DB.PhieuMuas.Where(x => x.MaPhieu == "f03cc8fa-5781-491a-8bd7-47a08f9f3e35").FirstOrDefault();
            Assert.AreEqual(expect, phieuMua == null);
        }
    }
}
