using CuaHangVangBacDaQuy.viewmodels;
using NUnit.Framework;

namespace CuaHangVangBacDaQuyTests
{
    [TestFixture]
    internal class FindProdcutTest
    {
        private ProductViewModel productViewModel;
        [SetUp]
        public void SetUp()
        {
            productViewModel = new ProductViewModel();
        }


        [TestCase("mã sản phẩm", "",12)]

        
        public void FindProductTest(string typeSearch, string textSearch, int expect)
        {
    
            //Assert.AreEqual(expect, productViewModel.Search(typeSearch, textSearch));
        }
    }
}
