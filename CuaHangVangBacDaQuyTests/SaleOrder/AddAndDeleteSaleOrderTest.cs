using CuaHangVangBacDaQuy.models;
using CuaHangVangBacDaQuy.viewmodels.DialogContentViewModel;
using CuaHangVangBacDaQuy.viewmodels;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CuaHangVangBacDaQuyTests.SaleOrder
{
    internal class AddAndDeleteSaleOrderTest
    {
        private AddOrEditSaleOrderViewModel viewModel;
        private SaleOrderViewModel viewModel2;

        private readonly List<string> supplierNames = new List<string> { null, "Trần Trọng Hoàng", "Nguyễn Văn A" };
        private readonly List<string> products = new List<string> { null, "Đá Topaz", "Kim cương", "Đá saphire" };
        private readonly List<int> productAmounts = new List<int> { -1, 0, 1, 10, 1000 };

        [SetUp]
        public void SetUp()
        {
            viewModel = new AddOrEditSaleOrderViewModel();
            viewModel2 = new SaleOrderViewModel();
        }
        //null check
        //[TestCase(0, 3, 3, true)]
        //[TestCase(2, 0, 3, true)]
        //wrong amount check
        //[TestCase(2, 3, 0, true)]
        //[TestCase(2, 3, 1, true)]

        [TestCase(1, 1, 2, true)]
        [TestCase(1, 1, 3, true)]
        [TestCase(1, 1, 4, false)]
        [TestCase(1, 2, 2, true)]
        [TestCase(1, 2, 3, true)]
        [TestCase(1, 2, 4, false)]
        [TestCase(1, 3, 2, true)]
        [TestCase(1, 3, 3, true)]
        [TestCase(1, 3, 4, false)]
        [TestCase(2, 1, 2, true)]
        [TestCase(2, 1, 3, true)]
        [TestCase(2, 1, 4, false)]
        [TestCase(2, 2, 2, true)]
        [TestCase(2, 2, 3, true)]
        [TestCase(2, 2, 4, false)]
        [TestCase(2, 3, 2, true)]
        [TestCase(2, 3, 3, true)]
        [TestCase(2, 3, 4, false)]



        public void AddAndDelteSaleOrder(int supplierIdx, int productIdx, int amountIdx, bool expect)
        {
            string code = Guid.NewGuid().ToString();
            string a = supplierNames[supplierIdx];
            string b = products[productIdx];
            viewModel.code = code;
            viewModel.SelectedCustomer = DataProvider.Ins.DB.KhachHangs.Where(x => x.TenKH == a).FirstOrDefault();
            viewModel.SelectedProductList = new ObservableCollection<ChiTietPhieuBan>() {
                new ChiTietPhieuBan() {
                    MaSP = DataProvider.Ins.DB.SanPhams.Where(x => x.TenSP ==b).FirstOrDefault().MaSP,
                    SoLuong = productAmounts[amountIdx]}
            };
            viewModel.AddNewSaleOrder();
            PhieuBan preDeletecheck = DataProvider.Ins.DB.PhieuBans.Where(x => x.MaPhieu == code).FirstOrDefault();
            if(preDeletecheck != null)
            {
                viewModel2.SelectedSaleOrder = DataProvider.Ins.DB.PhieuBans.Where(x => x.MaPhieu == code).FirstOrDefault();
                viewModel2.DeleteSaleOrder();
            }
            Assert.AreEqual(expect, preDeletecheck != null);
            Assert.AreEqual(true, DataProvider.Ins.DB.PhieuBans.Where(x => x.MaPhieu == code).FirstOrDefault() == null);

        }
    }
}
