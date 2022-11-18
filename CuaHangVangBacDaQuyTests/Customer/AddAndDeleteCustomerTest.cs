
using CuaHangVangBacDaQuy.models;
using CuaHangVangBacDaQuy.viewmodels.DialogContentViewModel;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace CuaHangVangBacDaQuyTests.Customer
{

    [TestFixture]
    internal class AddAndDeleteCustomerTest
    {
        private AddOrEditCustomerViewModel viewModel;
        private readonly List<string> customerNames = new List<string> { null, "  ", "(@#$%", "An", "Ai đó" };
        private readonly List<string> customerGenders = new List<string> { null, "Nam", "Nữ" };
        private readonly List<string> customerAddresses = new List<string> { null, " ", "(@#$%", "Hồ Chí Minh" };
        private readonly List<string> customerPhones = new List<string> { null, " ", "123123", "01", "0923234346" };


        [SetUp]
        public void SetUp()
        {
            viewModel = new AddOrEditCustomerViewModel();
        }
        // null check
        [TestCase(0, 1, 3, 4, false)]
        [TestCase(4, 0, 3, 4, false)]
        [TestCase(4, 1, 0, 4, false)]
        [TestCase(4, 1, 3, 0, false)]
        //empty check
        [TestCase(1, 1, 3, 4, false)]
        [TestCase(4, 1, 1, 4, false)]
        [TestCase(4, 1, 3, 1, false)]

        [TestCase(2, 1, 2, 2, false)]
        [TestCase(2, 1, 2, 3, false)]
        [TestCase(2, 1, 2, 4, true)]
        [TestCase(2, 1, 3, 2, false)]
        [TestCase(2, 1, 3, 3, false)]
        [TestCase(2, 1, 3, 4, true)]

        [TestCase(2, 2, 2, 2, false)]
        [TestCase(2, 2, 2, 3, false)]
        [TestCase(2, 2, 2, 4, true)]
        [TestCase(2, 2, 3, 2, false)]
        [TestCase(2, 2, 3, 3, false)]
        [TestCase(2, 2, 3, 4, true)]

        [TestCase(3, 1, 2, 2, false)]
        [TestCase(3, 1, 2, 3, false)]
        [TestCase(3, 1, 2, 4, true)]
        [TestCase(3, 1, 3, 2, false)]
        [TestCase(3, 1, 3, 3, false)]
        [TestCase(3, 1, 3, 4, true)]

        [TestCase(3, 2, 2, 2, false)]
        [TestCase(3, 2, 2, 3, false)]
        [TestCase(3, 2, 2, 4, true)]
        [TestCase(3, 2, 3, 2, false)]
        [TestCase(3, 2, 3, 3, false)]
        [TestCase(3, 2, 3, 4, true)]

        [TestCase(4, 1, 2, 2, false)]
        [TestCase(4, 1, 2, 3, false)]
        [TestCase(4, 1, 2, 4, true)]
        [TestCase(4, 1, 3, 2, false)]
        [TestCase(4, 1, 3, 3, false)]
        [TestCase(4, 1, 3, 4, true)]

        [TestCase(4, 2, 2, 2, false)]
        [TestCase(4, 2, 2, 3, false)]
        [TestCase(4, 2, 2, 4, true)]
        [TestCase(4, 2, 3, 2, false)]
        [TestCase(4, 2, 3, 3, false)]
        [TestCase(4, 2, 3, 4, true)]



        public void AddAndDeleteCustomer(int nameIdx, int genderIdx, int addressIdx, int phoneIdx, bool expect)
        {
            viewModel.CustomerName = customerNames[nameIdx];
            viewModel.Gender = customerGenders[genderIdx];
            viewModel.Address = customerAddresses[addressIdx];
            viewModel.PhoneNumber = customerPhones[phoneIdx];
            viewModel.ActionAddCustomer();

            int code = viewModel.customerCode;

            KhachHang  a = DataProvider.Ins.DB.KhachHangs.Where(x => x.MaKH == code).FirstOrDefault();

            if (a != null)
            {
                DataProvider.Ins.DB.KhachHangs.Attach(a);
                DataProvider.Ins.DB.KhachHangs.Remove(a);
                DataProvider.Ins.DB.SaveChanges();
            }
            Assert.AreEqual(expect, a != null);
            Assert.AreEqual(true, DataProvider.Ins.DB.KhachHangs.Where(x => x.MaKH == code).FirstOrDefault() == null);
        }

    }
}
