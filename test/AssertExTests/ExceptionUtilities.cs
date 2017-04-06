using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace AssertExTests
{
    /// <summary>
    /// Utility class to help validate exceptions thrown in unit tests.
    /// </summary>
    /// <remarks>
    /// This only exists because we cannot use Assert.That.Throws<>.WithMessage() and equivalents to test itself.
    /// </remarks>
    public static class ExceptionUtilities
    {
        public static void ThrowsExceptionWithMessage<T>(Action action, string message) where T:Exception
        {
            var exception = Assert.ThrowsException<T>(action);
            StringAssert.Contains(exception.Message, message);
        }
        public static void ThrowsExceptionWithInnerException<T>(Action action, Type innerExceptionType) where T : Exception
        {
            var exception = Assert.ThrowsException<T>(action);
            Assert.IsNotNull(exception.InnerException, "Inner exception is null.");
            Assert.AreEqual(innerExceptionType, exception.InnerException.GetType());
        }
    }
}
