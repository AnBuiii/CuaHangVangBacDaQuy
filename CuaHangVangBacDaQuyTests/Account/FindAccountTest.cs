using CuaHangVangBacDaQuy.viewmodels;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CuaHangVangBacDaQuyTests.Account
{
    internal class FindAccountTest
    {
        private AccountViewModel viewModel;
        private readonly List<string> typeSearchs = new List<string> { "Tên người dùng", "Quyền hạn", "Tên đăng nhập"};
        private readonly List<string> textSearchs = new List<string> { "", "Admin", "ADMIN", "admin", "~`@#$%^&" };

        [SetUp]
        public void SetUp()
        {
            viewModel = new AccountViewModel();
        }

        [TestCase(0, 0, 3)]
        [TestCase(0, 1, 1)]
        [TestCase(0, 2, 1)]
        [TestCase(0, 3, 1)]
        [TestCase(0, 4, 0)]

        [TestCase(1, 0, 3)]
        [TestCase(1, 1, 1)]
        [TestCase(1, 2, 1)]
        [TestCase(1, 3, 1)]
        [TestCase(1, 4, 0)]

        [TestCase(2, 0, 3)]
        [TestCase(2, 1, 1)]
        [TestCase(2, 2, 1)]
        [TestCase(2, 3, 1)]
        [TestCase(2, 4, 0)]

        public void FindCustomer(int typeSearchIdx, int textSearchIdx, int expect)
        {

            viewModel.SelectedSearchType = typeSearchs[typeSearchIdx];
            viewModel.ContentSearch = textSearchs[textSearchIdx];
            viewModel.Search();
            Assert.AreEqual(expect, viewModel.NguoiDungList.Count);
        }
    } 
}
