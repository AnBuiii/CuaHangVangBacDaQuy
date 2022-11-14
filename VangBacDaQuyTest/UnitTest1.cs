using Microsoft.VisualStudio.TestTools.UnitTesting;
using NUnit.Framework;
using System;
using Assert = NUnit.Framework.Assert;

namespace VangBacDaQuyTest
{
    [TestFixture]
    public class UnitTest1
    {

        [TestCase(1)]
        [TestCase(2)]

        public void Hmm(int a)
        {
            Assert.AreEqual(1, a);
        }
    }
}
