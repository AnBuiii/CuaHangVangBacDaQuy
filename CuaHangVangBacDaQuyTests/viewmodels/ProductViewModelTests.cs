using Microsoft.VisualStudio.TestTools.UnitTesting;
using CuaHangVangBacDaQuy.viewmodels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography.X509Certificates;
using CuaHangVangBacDaQuy.models;

namespace CuaHangVangBacDaQuy.viewmodels.Tests
{
    [TestClass()]
    public class ProductViewModelTests
    {
     
        
        [TestMethod()]
        public void ProductViewModelTest()
        {
            
            Assert.Fail();
        }

        public void LoadedTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void AddProductTest(SanPham b)
        {
            //khoi tao
            List<SanPham> a = new List<SanPham>();
            SanPham sanPham = new SanPham();
            // chay
             a.Add(sanPham);
            List<SanPham> expect = new List<SanPham>() { sanPham };
            Console.WriteLine(expect);
            Console.WriteLine(a);
            //kiem tra
            Assert.AreEqual(expect, a);

        }

        [            TestMethod()]
        public void EditProductTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void DeleteProductTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void SaveAddTest()
        {
            Assert.Fail();
        }
    }
}