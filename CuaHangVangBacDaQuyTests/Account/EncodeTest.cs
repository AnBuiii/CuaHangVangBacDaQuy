
using CuaHangVangBacDaQuy.viewmodels;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CuaHangVangBacDaQuyTests.Account
{
    [TestFixture]
    public class EncodeTest
    {
        
        [SetUp]
        public void SetUp()
        {
            

        }
        #region testcase
        [TestCase("admin", "db69fc039dcbd2962cb4d28f5891aae1")]
        [TestCase(null, "d41d8cd98f00b204e9800998ecf8427e")]
        [TestCase("123123123", "9cbde6632a5dd27eb3280f5b78801a64")]
        #endregion

        public void Encode(string input, string expect)
        {
            //if(input == null) { input = ""; }
            //string encode1 = LoginViewModel.Base64Encode(input);
            //string output = LoginViewModel.MD5Hash(encode1);
            //Assert.AreEqual(expect, output);

        }
    }
}
