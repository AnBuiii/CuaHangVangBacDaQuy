using CuaHangVangBacDaQuy.models;
using CuaHangVangBacDaQuy.viewmodels.DialogContentViewModel;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CuaHangVangBacDaQuyTests.Customer
{
    [TestFixture]
    internal class EditCustomerTest
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



        public void EditCustomer(int nameIdx, int genderIdx, int addressIdx, int phoneIdx, bool expect)
        {
            KhachHang hm = DataProvider.Ins.DB.KhachHangs.FirstOrDefault();
            KhachHang preEdit = new KhachHang() { TenKH = hm.TenKH, DiaChi = hm.DiaChi, SoDT = hm.SoDT, GioiTinh = hm.GioiTinh };

            viewModel.EditedCustomer = hm;
            viewModel.CustomerName = customerNames[nameIdx];
            viewModel.Address = customerAddresses[addressIdx];
            viewModel.Gender = customerGenders[genderIdx];
            viewModel.PhoneNumber = customerPhones[phoneIdx];
            viewModel.ActionEditCustomer();

            KhachHang a = DataProvider.Ins.DB.KhachHangs.FirstOrDefault();
            Assert.AreEqual(expect, a.TenKH == customerNames[nameIdx] && a.DiaChi == customerAddresses[addressIdx] && a.SoDT == customerPhones[phoneIdx]);


            viewModel.EditedCustomer = DataProvider.Ins.DB.KhachHangs.FirstOrDefault();
            viewModel.CustomerName = preEdit.TenKH;
            viewModel.Address = preEdit.DiaChi;
            viewModel.Gender = preEdit.GioiTinh;
            viewModel.PhoneNumber = preEdit.SoDT;
            viewModel.ActionEditCustomer();

        }
    }
}
