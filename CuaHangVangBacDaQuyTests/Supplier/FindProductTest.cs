using CuaHangVangBacDaQuy.viewmodels;
using NUnit.Framework;

namespace CuaHangVangBacDaQuyTests.Supplier
{
    [TestFixture]
    internal class FindProductTest
    {
        private ProductViewModel productViewModel;
        [SetUp]
        public void SetUp()
        {
            productViewModel = new ProductViewModel();
        }
        


        [TestCase("Mã sản phẩm", "",12)]
        [TestCase("Mã sản phẩm", "0", 3)]
        [TestCase("Mã sản phẩm", "1", 4)]
        [TestCase("Mã sản phẩm", "2", 3)]
        [TestCase("Mã sản phẩm", "Nhẫn 101", 0)]
        [TestCase("Mã sản phẩm", "Vòng 101", 0)]

        [TestCase("Tên sản phẩm", "", 12)]
        [TestCase("Tên sản phẩm", "Nhẫn 101", 1)]
        [TestCase("Tên sản phẩm", "Vòng 101", 1)]
        [TestCase("Tên sản phẩm", "Vàng tây", 1)]
        [TestCase("Tên sản phẩm", "Nhẫn", 4)]
        /*
        [TestCase("Loại sản phẩm", "Vàng tấy", 4)]
        [TestCase("Loại sản phẩm", "Nhẫn", 4)]
        [TestCase("Loại sản phẩm", "Vòng tay", 4)]
        [TestCase("Loại sản phẩm", "Đá quý", 4)]
        [TestCase("Loại sản phẩm", "Nhẫn", 4)]

        [TestCase("Đơn vị", "", 4)]
        [TestCase("Đơn vị", "Đá quý", 4)]
        [TestCase("Đơn vị", "cái", 4)]
        [TestCase("Đơn vị", "lượng", 4)]
        [TestCase("Đơn vị", "chỉ", 4)]
        */



        public void FindProduct(string typeSearch, string textSearch, int expect)
        {
            
            productViewModel.SelectedSearchType = typeSearch;
            productViewModel.ContentSearch = textSearch;
            productViewModel.Search();
            Assert.AreEqual(expect, productViewModel.ProductsList.Count);
        }
    }
}
