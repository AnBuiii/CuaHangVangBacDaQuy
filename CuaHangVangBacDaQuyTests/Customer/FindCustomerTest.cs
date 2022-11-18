

using CuaHangVangBacDaQuy.viewmodels;
using NUnit.Framework;
using System.Collections.Generic;

namespace CuaHangVangBacDaQuyTests.Customer
{

    [TestFixture]
    internal class FindCustomerTest
    {
        private CustomerViewModel viewModel;
        private readonly List<string> typeSearchs = new List<string> { "Tên khách hàng", "Số điện thoại", "Giới tính", "Địa chỉ" };
        private readonly List<string> textSearchs = new List<string> { null, "0929812532", "0931242564", "Nam", "Nữ", "An", "Hoàng" };

        [SetUp]
        public void SetUp()
        {
            viewModel = new CustomerViewModel();
        }

        [TestCase(0, 0, 6)]
        [TestCase(0, 1, 0)]
        [TestCase(0, 2, 0)]
        [TestCase(0, 3, 0)]
        [TestCase(0, 4, 0)]
        [TestCase(0, 5, 1)]
        [TestCase(0, 6, 1)]

        [TestCase(1, 0, 6)]
        [TestCase(1, 1, 1)]
        [TestCase(1, 2, 1)]
        [TestCase(1, 3, 0)]
        [TestCase(1, 4, 0)]
        [TestCase(1, 5, 0)]
        [TestCase(1, 6, 0)]

        [TestCase(2, 0, 6)]
        [TestCase(2, 1, 0)]
        [TestCase(2, 2, 0)]
        [TestCase(2, 3, 4)]
        [TestCase(2, 4, 0)]
        [TestCase(2, 5, 0)]
        [TestCase(2, 6, 0)]

        [TestCase(3, 0, 6)]
        [TestCase(3, 1, 0)]
        [TestCase(3, 2, 0)]
        [TestCase(3, 3, 0)]
        [TestCase(3, 4, 0)]
        [TestCase(3, 5, 0)]
        [TestCase(3, 6, 0)]

        public void FindCustomer(int typeSearchIdx, int textSearchIdx, int expect)
        {

            viewModel.SelectedSearchType = typeSearchs[typeSearchIdx];
            viewModel.ContentSearch = textSearchs[textSearchIdx];
            viewModel.Search();
            Assert.AreEqual(expect, viewModel.CustomerList.Count);
        }
    }
}
