using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.VisualStudio.TestTools.UnitTesting.Extensions;
using System;
using System.Collections.Generic;
using System.Text;

namespace AssertExTests
{
    [TestClass]
    public class ShouldThrowTests
    {
        [TestMethod]
        public void WithMessageShouldThrowIfMessageIsNotPartOfTheExceptionMessage()
        {
            var st = new ShouldThrow(new ArgumentException("oh no, something bombed!!"));
            Assert.ThrowsException<AssertFailedException>(() => st.WithMessage("Everything is bright."));
        }

        [TestMethod]
        public void WithMessageShouldNotThrowIfMessageIsPartOfTheExceptionMessage()
        {
            var st = new ShouldThrow(new ArgumentException("oh no, something bombed!!"));
            st.WithMessage("something bombed!");
        }

        [TestMethod]
        public void WithExactMessageShouldThrowIfMessageIsNotEqualToTheExceptionMessage()
        {
            var st = new ShouldThrow(new ArgumentException("oh no, something bombed!!"));
            Assert.ThrowsException<AssertFailedException>(() => st.WithExactMessage("something bombed!"));
        }

        [TestMethod]
        public void WithExactMessageShouldNotThrowIfMessageIsPartOfTheExceptionMessage()
        {
            var st = new ShouldThrow(new ArgumentException("oh no, something bombed!!"));
            st.WithExactMessage("oh no, something bombed!!");
        }
    }
}
