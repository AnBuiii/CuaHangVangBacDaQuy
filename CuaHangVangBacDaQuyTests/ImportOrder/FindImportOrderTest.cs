using CuaHangVangBacDaQuy.models;
using CuaHangVangBacDaQuy.viewmodels;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CuaHangVangBacDaQuyTests.ImportOrder
{


    [TestFixture]
    internal class FindImportOrderTest
    {

        private ImportReceiptViewModel viewModel;
        private readonly List<string> typeSearchs = new List<string> { "Mã phiếu", "Tên nhà cung cấp"};
        private readonly List<string> textSearchs = new List<string> { null, "1", "d", "Cơ sở Mỹ Nghệ 1", "Công ty Đá Quý 123"};

        [SetUp]
        public void SetUp()
        {
            viewModel = new ImportReceiptViewModel();
        }

        [TestCase(0, 0, 11)]
        [TestCase(0, 1, 9)]
        [TestCase(0, 2, 9)]
        [TestCase(0, 3, 0)]
        [TestCase(0, 4, 0)]

        [TestCase(1, 0, 11)]
        [TestCase(1, 1, 6)]
        [TestCase(1, 2, 0)]
        [TestCase(1, 3, 2)]
        [TestCase(1, 4, 4)]

        public void FindImportOrder(int typeSearchIdx, int textSearchIdx, int expect)
        {

            viewModel.SelectedSearchType = typeSearchs[typeSearchIdx];
            viewModel.ContentSearch= textSearchs[textSearchIdx];
            viewModel.Search();
            Assert.AreEqual(expect, viewModel.ImportReceiptsList.Count);
        }
    }
}
