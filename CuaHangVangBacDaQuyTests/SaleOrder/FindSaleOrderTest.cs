using CuaHangVangBacDaQuy.viewmodels;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CuaHangVangBacDaQuyTests.SaleOrder
{
    internal class FindSaleOrderTest
    {
        private SaleOrderViewModel viewModel;
        private readonly List<string> typeSearchs = new List<string> { "Mã phiếu", "Khách hàng" };
        private readonly List<string> textSearchs = new List<string> { null, "1", "d", "Trần Trọng Hoàng", "Nguyễn Văn A" };

        [SetUp]
        public void SetUp()
        {
            viewModel = new SaleOrderViewModel();
        }

        [TestCase(0, 0, 9)]
        [TestCase(0, 1, 6)]
        [TestCase(0, 2, 8)]
        [TestCase(0, 3, 0)]
        [TestCase(0, 4, 0)]

        [TestCase(1, 0, 9)]
        [TestCase(1, 1, 0)]
        [TestCase(1, 2, 0)]
        [TestCase(1, 3, 2)]
        [TestCase(1, 4, 2)]

        public void FindSaleOrder(int typeSearchIdx, int textSearchIdx, int expect)
        {

            viewModel.SelectedSearchType = typeSearchs[typeSearchIdx];
            viewModel.ContentSearch = textSearchs[textSearchIdx];
            viewModel.Search();
            Assert.AreEqual(expect, viewModel.SaleOrdersList.Count);
        }
    }
}
