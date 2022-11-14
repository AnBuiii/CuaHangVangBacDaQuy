
using CuaHangVangBacDaQuy.models;
using CuaHangVangBacDaQuy.viewmodels.DialogContentViewModel;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace CuaHangVangBacDaQuyTests.Customer
{

    [TestFixture]
    internal class AddCustomerTest
    {
        private AddOrEditCustomerViewModel viewModel;
        private readonly List<string> customerNames = new List<string> { null, "  ", "(@#$%", "An", "Ai đó" };
        private readonly List<string> customerGenders = new List<string> { null, "Nam", "Nữ" };
        private readonly List<string> customerAddresses = new List<string> { null, " ", "(@#$%", "Hồ Chí Minh" };
        private readonly List<string> customerPhones = new List<string> { null, " ", "123123", "01", "0923234345" };


        [SetUp]
        public void SetUp()
        {
            viewModel = new AddOrEditCustomerViewModel();
        }
        // null check
        [TestCase(0, 3, 4, false)]
        [TestCase(5, 0, 4, false)]
        [TestCase(5, 3, 0, false)]
        // empty check
        [TestCase(1, 3, 4, false)]
        [TestCase(5, 1, 4, false)]
        [TestCase(5, 3, 1, false)]

        [TestCase(2, 2, 2, false)]
        [TestCase(2, 2, 3, false)]
        [TestCase(2, 2, 4, true)]

        [TestCase(2, 3, 2, false)]
        [TestCase(2, 3, 3, false)]
        [TestCase(2, 3, 4, true)]

        [TestCase(3, 2, 2, false)]
        [TestCase(3, 2, 3, false)]
        [TestCase(3, 2, 4, true)]

        [TestCase(3, 3, 2, false)]
        [TestCase(3, 3, 3, false)]
        [TestCase(3, 3, 4, true)]

        [TestCase(4, 2, 2, false)]
        [TestCase(4, 2, 3, false)]
        [TestCase(4, 2, 4, true)]

        [TestCase(4, 3, 2, false)]
        [TestCase(4, 3, 3, false)]
        [TestCase(4, 3, 4, true)]


        public void AddCustomer(int nameIdx, int genderIdx, int addressIdx, int phoneIdx, bool expect)
        {
            viewModel.CustomerName = customerNames[nameIdx];
            viewModel.Address = customerAddresses[addressIdx];
            viewModel.PhoneNumber = customerPhones[phoneIdx];
            viewModel.ActionAddCustomer();

            //int code = viewModel.customerCode;

            //NhaCungCap a = DataProvider.Ins.DB.NhaCungCaps.Where(x => x.MaNCC == code).FirstOrDefault();

            //if (a != null)
            //{
            //    DataProvider.Ins.DB.NhaCungCaps.Attach(a);
            //    DataProvider.Ins.DB.NhaCungCaps.Remove(a);
            //    DataProvider.Ins.DB.SaveChanges();
            //}
            //Assert.AreEqual(expect, a != null);
        }

    }
}
