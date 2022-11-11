using CuaHangVangBacDaQuy.models;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CuaHangVangBacDaQuyTests
{


    [TestFixture]
    internal class FindMakeOrderTest
    {
        public NhaCungCap khachHang1;
        public NhaCungCap khachHang2;

        public List<PhieuMua> phieuMuas;


        [SetUp]
        public void SetUp()
        {

            khachHang1 = new NhaCungCap() { MaNCC = 1, TenNCC = "Bùi An" };
            khachHang2 = new NhaCungCap() { MaNCC = 2, TenNCC = "Hoàng" };
            phieuMuas = new List<PhieuMua>()
            {
                new PhieuMua(){ MaPhieu = "1", NgayLap = DateTime.Parse("22/10/2022"), NhaCungCap = new NhaCungCap() { MaNCC = 1, TenNCC = "Bùi An" } },
                new PhieuMua(){ MaPhieu = "2", NgayLap = DateTime.Parse("22/10/2022"), NhaCungCap = new NhaCungCap() { MaNCC = 1, TenNCC = "Bùi An" }  },
                new PhieuMua(){ MaPhieu = "3", NgayLap = DateTime.Parse("21/10/2022"), NhaCungCap = new NhaCungCap() { MaNCC = 2, TenNCC = "Hoàng" }  },
                new PhieuMua(){ MaPhieu = "10", NgayLap = DateTime.Parse("21/10/2022"), NhaCungCap = new NhaCungCap() { MaNCC = 1, TenNCC = "Bùi An" }  },
            };
            
        }
        [TestCase("mã đơn hàng", null, 4)]
        [TestCase("mã đơn hàng", "1", 2)]
        [TestCase("mã đơn hàng", "2", 1)]
        [TestCase("mã đơn hàng", "Bùi An", 0)]
        [TestCase("mã đơn hàng", "21/10/2022", 0)]
        [TestCase("ngày lập đơn", null, 4)]
        [TestCase("ngày lập đơn", "1", 4)]
        [TestCase("ngày lập đơn", "2", 4)]
        [TestCase("ngày lập đơn", "Bùi An", 0)]
        [TestCase("ngày lập đơn", "21/10/2022", 2)]
        [TestCase("tên khách hàng", null, 4)]
        [TestCase("tên khách hàng", "1", 0)]
        [TestCase("tên khách hàng", "2", 0)]
        [TestCase("tên khách hàng", "Bùi An", 3)]
        [TestCase("tên khách hàng", "21/10/2022", 0)]







        public void FindOrder(string typeSearch, string textSearch, int expect)
        {
            List<PhieuMua> output = new List<PhieuMua>();
            if (phieuMuas == null)
            {
                Assert.AreEqual(0, expect);
                return;
            }

            if (textSearch == null) textSearch = "";

            switch (typeSearch)
            {
                case "mã đơn hàng":
                    output = new List<PhieuMua>(
                        phieuMuas.Where(x => x.MaPhieu.ToString().Contains(textSearch)));
                    break;
                case "ngày lập đơn":
                    output = new List<PhieuMua>(
                        phieuMuas.Where(x => x.NgayLap.ToString().Contains(textSearch)));
                    break;
                case "tên khách hàng":
                    output = new List<PhieuMua>(
                        phieuMuas.Where(x => x.NhaCungCap.TenNCC.ToString().Contains(textSearch)));
                    break;
            }
            Assert.AreEqual(expect, output.Count);

        }



    }
}
