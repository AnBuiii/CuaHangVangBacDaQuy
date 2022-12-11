using CuaHangVangBacDaQuy.models;
using CuaHangVangBacDaQuy.viewmodels.DialogContentViewModel;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CuaHangVangBacDaQuyTests
{
    [TestFixture]
    internal class CheckValidSaleOderTest
    {

        private AddOrEditSaleOrderViewModel viewModel;

        [SetUp]
        public void SetUp()
        {
            viewModel = new AddOrEditSaleOrderViewModel();
        }

        [Test]
        public void CheckValidSaleOrderTest_EmptyCustomer()
        {
            viewModel.SelectedProductList.Add(new ChiTietPhieuBan()
            {
                MaSP = DataProvider.Ins.DB.SanPhams.Where(x => x.TenSP == "Kim cương").FirstOrDefault().MaSP,
                SoLuong = 1
            });
            bool check = viewModel.CheckValidOrder();
            Assert.AreEqual(false, check);
        }

        [Test]
        public void CheckValidSaleOrderTest_EmptyProductList()
        {
            viewModel.SelectedCustomer = DataProvider.Ins.DB.KhachHangs.Where(x => x.TenKH == "Trần Trọng Hoàng").FirstOrDefault();
            
            bool check = viewModel.CheckValidOrder();
            Assert.AreEqual(false, check);
        }

        [Test]
        public void CheckValidSaleOrderTest_1ItemProductListWithWrongAmount1()
        {
            viewModel.SelectedCustomer = DataProvider.Ins.DB.KhachHangs.Where(x => x.TenKH == "Trần Trọng Hoàng").FirstOrDefault();
            viewModel.SelectedProductList.Add(new ChiTietPhieuBan()
            {
                MaSP = DataProvider.Ins.DB.SanPhams.Where(x => x.TenSP == "Kim cương").FirstOrDefault().MaSP,
                SoLuong = 0
            });
            bool check = viewModel.CheckValidOrder();
            Assert.AreEqual(false, check);
        }

        [Test]
        public void CheckValidSaleOrderTest_1ItemProductListWithWrongAmount2()
        {
            viewModel.SelectedCustomer = DataProvider.Ins.DB.KhachHangs.Where(x => x.TenKH == "Trần Trọng Hoàng").FirstOrDefault();
            viewModel.SelectedProductList.Add(new ChiTietPhieuBan()
            {
                MaSP = DataProvider.Ins.DB.SanPhams.Where(x => x.TenSP == "Kim cương").FirstOrDefault().MaSP,
                SoLuong = 196
            });
            bool check = viewModel.CheckValidOrder();
            Assert.AreEqual(false, check);
        }

        [Test]
        public void CheckValidSaleOrderTest_1ItemProductListWithRightAmount1()
        {
            viewModel.SelectedCustomer = DataProvider.Ins.DB.KhachHangs.Where(x => x.TenKH == "Trần Trọng Hoàng").FirstOrDefault();
            viewModel.SelectedProductList.Add(new ChiTietPhieuBan()
            {
                MaSP = DataProvider.Ins.DB.SanPhams.Where(x => x.TenSP == "Kim cương").FirstOrDefault().MaSP,
                SoLuong = 1
            });
            bool check = viewModel.CheckValidOrder();
            Assert.AreEqual(true, check);
        }
        [Test]
        public void CheckValidSaleOrderTest_1ItemProductListWithRightAmount2()
        {
            viewModel.SelectedCustomer = DataProvider.Ins.DB.KhachHangs.Where(x => x.TenKH == "Trần Trọng Hoàng").FirstOrDefault();
            viewModel.SelectedProductList.Add(new ChiTietPhieuBan()
            {
                MaSP = DataProvider.Ins.DB.SanPhams.Where(x => x.TenSP == "Kim cương").FirstOrDefault().MaSP,
                SoLuong = 195
            });
            bool check = viewModel.CheckValidOrder();
            Assert.AreEqual(true, check);
        }

        [Test]
        public void CheckValidSaleOrderTest_2ItemProductListWithWrongAmount1()
        {
            viewModel.SelectedCustomer = DataProvider.Ins.DB.KhachHangs.Where(x => x.TenKH == "Trần Trọng Hoàng").FirstOrDefault();
            viewModel.SelectedProductList.Add(new ChiTietPhieuBan()
            {
                MaSP = DataProvider.Ins.DB.SanPhams.Where(x => x.TenSP == "Kim cương").FirstOrDefault().MaSP,
                SoLuong = 0
            });
            viewModel.SelectedProductList.Add(new ChiTietPhieuBan()
            {
                MaSP = DataProvider.Ins.DB.SanPhams.Where(x => x.TenSP == "Đá Topaz").FirstOrDefault().MaSP,
                SoLuong = 1
            });
            bool check = viewModel.CheckValidOrder();
            Assert.AreEqual(false, check);
        }

        [Test]
        public void CheckValidSaleOrderTest_2ItemProductListWithWrongAmount2()
        {
            viewModel.SelectedCustomer = DataProvider.Ins.DB.KhachHangs.Where(x => x.TenKH == "Trần Trọng Hoàng").FirstOrDefault();
            viewModel.SelectedProductList.Add(new ChiTietPhieuBan()
            {
                MaSP = DataProvider.Ins.DB.SanPhams.Where(x => x.TenSP == "Kim cương").FirstOrDefault().MaSP,
                SoLuong = 196
            });
            viewModel.SelectedProductList.Add(new ChiTietPhieuBan()
            {
                MaSP = DataProvider.Ins.DB.SanPhams.Where(x => x.TenSP == "Đá Topaz").FirstOrDefault().MaSP,
                SoLuong = 1
            });
            bool check = viewModel.CheckValidOrder();
            Assert.AreEqual(false, check);
        }
        [Test]
        public void CheckValidSaleOrderTest_2ItemProductListWithWrongAmount3()
        {
            viewModel.SelectedCustomer = DataProvider.Ins.DB.KhachHangs.Where(x => x.TenKH == "Trần Trọng Hoàng").FirstOrDefault();
            viewModel.SelectedProductList.Add(new ChiTietPhieuBan()
            {
                MaSP = DataProvider.Ins.DB.SanPhams.Where(x => x.TenSP == "Kim cương").FirstOrDefault().MaSP,
                SoLuong = 1
            });
            viewModel.SelectedProductList.Add(new ChiTietPhieuBan()
            {
                MaSP = DataProvider.Ins.DB.SanPhams.Where(x => x.TenSP == "Đá Topaz").FirstOrDefault().MaSP,
                SoLuong = 0
            });
            bool check = viewModel.CheckValidOrder();
            Assert.AreEqual(false, check);
        }
        [Test]
        public void CheckValidSaleOrderTest_2ItemProductListWithWrongAmount4()
        {
            viewModel.SelectedCustomer = DataProvider.Ins.DB.KhachHangs.Where(x => x.TenKH == "Trần Trọng Hoàng").FirstOrDefault();
            viewModel.SelectedProductList.Add(new ChiTietPhieuBan()
            {
                MaSP = DataProvider.Ins.DB.SanPhams.Where(x => x.TenSP == "Kim cương").FirstOrDefault().MaSP,
                SoLuong = 1
            });
            viewModel.SelectedProductList.Add(new ChiTietPhieuBan()
            {
                MaSP = DataProvider.Ins.DB.SanPhams.Where(x => x.TenSP == "Đá Topaz").FirstOrDefault().MaSP,
                SoLuong = 203
            });
            bool check = viewModel.CheckValidOrder();
            Assert.AreEqual(false, check);
        }
        [Test]
        public void CheckValidSaleOrderTest_2ItemProductList_Valid1()
        {
            viewModel.SelectedCustomer = DataProvider.Ins.DB.KhachHangs.Where(x => x.TenKH == "Trần Trọng Hoàng").FirstOrDefault();
            viewModel.SelectedProductList.Add(new ChiTietPhieuBan()
            {
                MaSP = DataProvider.Ins.DB.SanPhams.Where(x => x.TenSP == "Kim cương").FirstOrDefault().MaSP,
                SoLuong = 1
            });
            viewModel.SelectedProductList.Add(new ChiTietPhieuBan()
            {
                MaSP = DataProvider.Ins.DB.SanPhams.Where(x => x.TenSP == "Đá Topaz").FirstOrDefault().MaSP,
                SoLuong = 1
            });
            bool check = viewModel.CheckValidOrder();
            Assert.AreEqual(true, check);
        }
        [Test]
        public void CheckValidSaleOrderTest_2ItemProductList_Valid2()
        {
            viewModel.SelectedCustomer = DataProvider.Ins.DB.KhachHangs.Where(x => x.TenKH == "Trần Trọng Hoàng").FirstOrDefault();
            viewModel.SelectedProductList.Add(new ChiTietPhieuBan()
            {
                MaSP = DataProvider.Ins.DB.SanPhams.Where(x => x.TenSP == "Kim cương").FirstOrDefault().MaSP,
                SoLuong = 1
            });
            viewModel.SelectedProductList.Add(new ChiTietPhieuBan()
            {
                MaSP = DataProvider.Ins.DB.SanPhams.Where(x => x.TenSP == "Đá Topaz").FirstOrDefault().MaSP,
                SoLuong = 202
            });
            bool check = viewModel.CheckValidOrder();
            Assert.AreEqual(true, check);
        }
        [Test]
        public void CheckValidSaleOrderTest_2ItemProductList_Valid3()
        {
            viewModel.SelectedCustomer = DataProvider.Ins.DB.KhachHangs.Where(x => x.TenKH == "Trần Trọng Hoàng").FirstOrDefault();
            viewModel.SelectedProductList.Add(new ChiTietPhieuBan()
            {
                MaSP = DataProvider.Ins.DB.SanPhams.Where(x => x.TenSP == "Kim cương").FirstOrDefault().MaSP,
                SoLuong = 195
            });
            viewModel.SelectedProductList.Add(new ChiTietPhieuBan()
            {
                MaSP = DataProvider.Ins.DB.SanPhams.Where(x => x.TenSP == "Đá Topaz").FirstOrDefault().MaSP,
                SoLuong = 1
            });
            bool check = viewModel.CheckValidOrder();
            Assert.AreEqual(true, check);
        }
        [Test]
        public void CheckValidSaleOrderTest_2ItemProductList_Valid4()
        {
            viewModel.SelectedCustomer = DataProvider.Ins.DB.KhachHangs.Where(x => x.TenKH == "Trần Trọng Hoàng").FirstOrDefault();
            viewModel.SelectedProductList.Add(new ChiTietPhieuBan()
            {
                MaSP = DataProvider.Ins.DB.SanPhams.Where(x => x.TenSP == "Kim cương").FirstOrDefault().MaSP,
                SoLuong = 195
            });
            viewModel.SelectedProductList.Add(new ChiTietPhieuBan()
            {
                MaSP = DataProvider.Ins.DB.SanPhams.Where(x => x.TenSP == "Đá Topaz").FirstOrDefault().MaSP,
                SoLuong = 202
            });
            bool check = viewModel.CheckValidOrder();
            Assert.AreEqual(true, check);
        }
    }
}
