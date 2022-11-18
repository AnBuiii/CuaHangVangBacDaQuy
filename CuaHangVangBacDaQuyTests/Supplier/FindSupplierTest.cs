using CuaHangVangBacDaQuy.viewmodels;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace CuaHangVangBacDaQuyTests.Supplier
{
    [TestFixture]
    internal class FindSupplierTest
    {
        private SupplierViewModel viewModel;
        private readonly List<string> typeSearchs = new List<string> { "Tên nhà cung cấp", "Địa chỉ", "Số điện thoại" };
        private readonly List<string> textSearchs = new List<string> { null, "123", "146", "~`@#", "Công ty Đá Quý 123", "Cơ sở Mỹ Nghệ 2", "0972845928", "Linh Trung, Thủ Đức, Hồ Chí Minh", "Thủ Dầu 1, Bình Dương" };
        [SetUp]
        public void SetUp()
        {
            viewModel = new SupplierViewModel();
        }



        [TestCase(0, 0, 5)]
        [TestCase(0, 1, 1)]
        [TestCase(0, 2, 0)]
        [TestCase(0, 3, 0)]
        [TestCase(0, 4, 1)]
        [TestCase(0, 5, 1)]
        [TestCase(0, 6, 0)]
        [TestCase(0, 7, 0)]
        [TestCase(0, 8, 0)]

        [TestCase(1, 0, 5)]
        [TestCase(1, 1, 0)]
        [TestCase(1, 2, 0)]
        [TestCase(1, 3, 0)]
        [TestCase(1, 4, 0)]
        [TestCase(1, 5, 0)]
        [TestCase(1, 6, 0)]
        [TestCase(1, 7, 1)]
        [TestCase(1, 8, 1)]

        [TestCase(2, 0, 5)]
        [TestCase(2, 1, 1)]
        [TestCase(2, 2, 0)]
        [TestCase(2, 3, 0)]
        [TestCase(2, 4, 0)]
        [TestCase(2, 5, 0)]
        [TestCase(2, 6, 1)]
        [TestCase(2, 7, 0)]
        [TestCase(2, 8, 0)]



        public void FindSupplier(int typeSearchIdx, int textSearchIdx, int expect)
        {
            
            viewModel.SelectedSearchType = typeSearchs[typeSearchIdx];
            viewModel.ContentSearch = textSearchs[textSearchIdx];
            viewModel.Search();
            Assert.AreEqual(expect, viewModel.SuppliersList.Count);
        }
    }
}
