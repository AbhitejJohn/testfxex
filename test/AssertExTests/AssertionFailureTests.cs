using Microsoft.VisualStudio.TestTools.UnitTesting;
using MSTest.TestFramework.AssertExtensions;
using System;

namespace AssertExTests
{
    [TestClass]
    public class AssertionFailureTests
    {
        [TestMethod]
        public void HandleShouldThrowWithMessage()
        {
            ExceptionUtilities.ThrowsExceptionWithMessage<AssertFailedException>(() =>
                AssertionFailure.Handle("foobar"), "foobar");
        }

        [TestMethod]
        public void HandleWithExceptionShouldThrowWithMessage()
        {
            ExceptionUtilities.ThrowsExceptionWithMessage<AssertFailedException>(() =>
                AssertionFailure.Handle("foobar", new NullReferenceException()), "foobar");
        }

        [TestMethod]
        public void HandleWithExceptionShouldContainInnerException()
        {
            ExceptionUtilities.ThrowsExceptionWithInnerException<AssertFailedException>(() =>
                AssertionFailure.Handle("foobar", new NullReferenceException()), typeof(NullReferenceException));
        }
    }
}
