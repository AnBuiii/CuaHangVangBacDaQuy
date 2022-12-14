using CuaHangVangBacDaQuy.viewmodels.Converter;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CuaHangVangBacDaQuyTests
{
    internal class EncodePasswordTest
    {
        [Test]
        public void EncodePasswordTest1()
        {
            string encode = Encode.EncodePassword("");
            string expect = "d41d8cd98f00b204e9800998ecf8427e";
            Assert.AreEqual(expect, encode);
        }
        [Test]
        public void EncodePasswordTest2()
        {
            string encode = Encode.EncodePassword("123123123");
            string expect = "9cbde6632a5dd27eb3280f5b78801a64";
            Assert.AreEqual(expect, encode);
        }
        [Test]
        public void EncodePasswordTest3()
        {
            string encode = Encode.EncodePassword("admin");
            string expect = "db69fc039dcbd2962cb4d28f5891aae1";
            Assert.AreEqual(expect, encode);
        }
    }
}
