using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MSTest.TestFramework.TestRunExtensions;

namespace TestRunExTests
{
    [TestClass]
    public class FlakyTestClassAttributeTests
    {
        private FlakyTestClassAttribute flakyTestClass;

        [TestInitialize]
        public void TestInit()
        {
            this.flakyTestClass = new FlakyTestClassAttribute(5);
        }

        [TestMethod]
        public void GetTestMethodAttributeShouldReturnTestMethodPassedIfItsAlreadyAFlakyType()
        {
            var flakyTM = new FlakyTestMethodAttribute(5);

            var tm = this.flakyTestClass.GetTestMethodAttribute(flakyTM);

            Assert.AreEqual(flakyTM, tm);
        }

        [TestMethod]
        public void GetTestMethodAttributeShouldReturnAFlakyTestMethodAttribute()
        {
            var simpleTM = new TestMethodAttribute();

            var tm = this.flakyTestClass.GetTestMethodAttribute(simpleTM);

            Assert.AreEqual(typeof(FlakyTestMethodAttribute), tm.GetType());
        }
    }
}
