using CuaHangVangBacDaQuy.viewmodels;
using NUnit.Framework;
using System.Collections.Generic;

namespace CuaHangVangBacDaQuyTests.Product
{
    [TestFixture]
    internal class FindProductTest
    {
        private ProductViewModel productViewModel;
        private readonly List<string> typeSearchs = new List<string> { "Mã sản phẩm", "Tên sản phẩm", "Loại sản phẩm", "Đơn vị" };
        private readonly List<string> textSearchs = new List<string> { null, "0", "1", "2", "Đá Topaz", "Nhẫn SJC 20", "Vàng Tây", "Nhẫn", "Vòng tay", "Đá quý", "cái", "lượng", "chỉ" };
        [SetUp]
        public void SetUp()
        {
            productViewModel = new ProductViewModel();
        }

        [TestCase(3, 0, 0)]
        [TestCase(0, 1, 13)]

        [TestCase(0, 2, 10)]
        [TestCase(0, 3, 12)]
        [TestCase(0, 4, 0)]
        [TestCase(0, 5, 0)]

        [TestCase(1, 4, 1)]
        [TestCase(1, 5, 1)]
        [TestCase(1, 6, 0)]
        [TestCase(1, 7, 3)]

        [TestCase(2, 6, 0)]
        [TestCase(2, 7, 6)]
        [TestCase(2, 8, 1)]
        [TestCase(2, 9, 5)]

        [TestCase(3, 7, 0)]
        [TestCase(3, 8, 0)]
        [TestCase(3, 9, 0)]
        [TestCase(3, 10, 6)]




        public void FindProduct(int typeSearch, int textSearch, int expect)
        {
            string a = typeSearchs[typeSearch];
            string b = textSearchs[textSearch];

            productViewModel.SelectedSearchType = a;
            productViewModel.ContentSearch = b;
            productViewModel.Search();
            Assert.AreEqual(expect, productViewModel.ProductsList.Count);
        }
    }
}
