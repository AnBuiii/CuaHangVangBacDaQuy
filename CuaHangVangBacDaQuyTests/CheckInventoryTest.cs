using NUnit.Framework;
using NUnit.Framework.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CuaHangVangBacDaQuyTests
{
    [TestFixture]
    internal class CheckInventoryTest
    {

        [SetUp]
        public void SetUp()
        {

        }
        [TestCase(-1, 0, -1)]
        [TestCase(0, -1, -1)]
        [TestCase(0, 0, 0)]
        [TestCase(0, 1, 1)]
        [TestCase(0, 99, 99)]
        [TestCase(0, 100, 100)]
        [TestCase(1, 0, -1)]
        [TestCase(1, 1, 0)]
        [TestCase(1, 99, 98)]
        [TestCase(1, 100, 99)]
        [TestCase(99, 0, -1)]
        [TestCase(99, 1, -1)]
        [TestCase(99, 99, 0)]
        [TestCase(99, 100, 1)]
        [TestCase(100, 0, -1)]
        [TestCase(100, 1, -1)]
        [TestCase(100, 99, -1)]
        [TestCase(100, 100, 0)]


        public void CheckInventory(int sellSum, int buySum, int expect)
        {
            int output = buySum - sellSum;
            if (buySum < 0 || sellSum < 0)
            {
                output = -1;
            }
            else
            {
                if (output < 0) output = -1;
            }


            Assert.AreEqual(expect, output);
        }

    }
}
